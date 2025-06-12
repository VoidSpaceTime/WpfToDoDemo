using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common.Events;

namespace WpfDemo.Extensions
{
    public static class DialogExtensions
    {
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
    }
}
