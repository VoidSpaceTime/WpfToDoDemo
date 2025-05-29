using System.Configuration;
using System.Data;
using System.Windows;
using WpfDemo.ViewModels;
using WpfDemo.Views;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 注册依赖注入容器中的类型和服务。
        /// 在此方法中可以将应用程序所需的服务、视图、视图模型等类型注册到容器中，
        /// 以便在应用程序生命周期内进行依赖注入和解析。
        /// </summary>
        /// <param name="containerRegistry">用于注册类型的容器注册表。</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册导航
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }

}
