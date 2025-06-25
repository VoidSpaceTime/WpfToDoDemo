using MaterialDesignThemes.Wpf;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfDemo.Common
{
    /// <summary>
    /// 对话主机服务接口，继承自IDialogService
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            // 如果参数为null，则初始化为一个新的DialogParameters对象
            if (parameters == null)
                parameters = new DialogParameters();

            // 从依赖注入容器中解析出弹窗内容实例
            var content = containerExtension.Resolve<object>(name);

            // 验证弹窗内容实例是否为FrameworkElement类型
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            // 如果内容的DataContext为null且未自动绑定ViewModel，则自动绑定ViewModel
            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            // 验证DataContext是否实现了IDialogHostAware接口
            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            // 设置DialogHost名称
            viewModel.DialogHostName = dialogHostName;

            // 定义弹窗打开时的事件处理器
            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                // 调用ViewModel的OnDialogOpend方法
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                // 更新弹窗内容
                eventArgs.Session.UpdateContent(content);
            };

            // 显示弹窗并返回结果
            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }
    }
}
