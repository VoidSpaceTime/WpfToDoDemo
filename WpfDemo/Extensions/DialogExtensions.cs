﻿using Prism.Dialogs;
using WpfDemo.Common;
using WpfDemo.Common.Events;

namespace WpfDemo.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="dialogHostName"></param>
        /// <returns></returns>
        public static async Task<IDialogResult> Question(this IDialogHostService dialogService, string title, string content, string dialogHostName = "Root")
        {
            var parameters = new DialogParameters
            {
                { "Title", title },
                { "Content", content },
            };
            return await dialogService.ShowDialogAsync("MsgView", parameters, dialogHostName);
        }
        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="updateModel"></param>
        public static void UpdateLoading(this IEventAggregator eventAggregator, UpdateModel updateModel)
        {
            eventAggregator.GetEvent<UpdateLoadingEvent>().Publish(updateModel);
        }
        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="updateModel"></param>
        public static void Register(this IEventAggregator eventAggregator, Action<UpdateModel> updateModel)
        {
            eventAggregator.GetEvent<UpdateLoadingEvent>().Subscribe(updateModel);
        }
        /// <summary>
        /// 注册提示消息事件
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="updateModel"></param>
        public static void ResigiterMessage(this IEventAggregator eventAggregator, Action<MessgaeModel> updateModel, string filterName = "Main")
        {
            eventAggregator.GetEvent<MessageEvent>().Subscribe(updateModel, ThreadOption.PublisherThread, true, (f) =>
            {
                return f.Filter.Equals(filterName);
            });
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator eventAggregator, string message, string filterName = "Main")
        {
            eventAggregator.GetEvent<MessageEvent>().Publish(new MessgaeModel()
            {
                Message = message,
                Filter = filterName
            });
        }
    }
}
