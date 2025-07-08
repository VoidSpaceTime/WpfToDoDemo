using System.Windows;
using WpfDemo.Common;
using WpfDemo.Sercive;
using WpfDemo.ViewModels;
using WpfDemo.ViewModels.Dialogs;
using WpfDemo.Views;
using WpfDemo.Views.Dialogs;

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
        protected override void OnInitialized()
        {
            // 在应用程序初始化后执行的代码
            // 获取依赖注入容器中的 IDialogService 实例
            var dialog = Container.Resolve<IDialogService>();
            dialog.ShowDialog("LoginView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    // 如果登录失败，则关闭应用程序
                    Application.Current.Shutdown();
                    return;
                }
                else
                {
                    // 例如，显示主窗口或执行其他初始化逻辑
                    var service = App.Current.MainWindow.DataContext as IConfigurationService;
                    if (service != null)
                    {
                        service.Configure();
                    }
                    base.OnInitialized();
                }
            }); 
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
            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();

            /*
                1.	containerRegistry.GetContainer()
                获取当前依赖注入容器（通常是 DryIoc 容器实例）。
                2.	.Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"))
                注册 HttpRestClient 类型到容器中，并指定当需要构造 HttpRestClient 时，
                构造函数中的 string 类型参数会自动注入名为 "webUrl" 的服务（即下面注册的 URL 字符串）。
                这样，HttpRestClient 的构造函数如果有 string webUrl 参数，就会自动注入 "http://localhost:3389/"。
                3.	.RegisterInstance(@"http://localhost:3389/", serviceKey: "webUrl")
                向容器中注册一个名为 "webUrl" 的字符串实例，其值为 "http://localhost:3389/"。
                这为上面注册的 HttpRestClient 提供了依赖的实际值。
             */
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:17381/", serviceKey: "webUrl");
            containerRegistry.GetContainer()
             .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));

            //containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();

            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.Register<ILoginService, LoginService>();

            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<AddMemoView, AddMemoViewModel>();

            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();

            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();

        }

    }

}
