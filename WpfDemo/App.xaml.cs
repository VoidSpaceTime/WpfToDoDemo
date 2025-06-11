using System.Windows;
using WpfDemo.Sercive;
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
            containerRegistry.RegisterForNavigation<AboutView>();
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<ToDoView, ToDoViewModel>();
            containerRegistry.RegisterForNavigation<MemoView, MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();

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
            containerRegistry.GetContainer().RegisterInstance(@"http://localhost:5136/", serviceKey: "webUrl");
            containerRegistry.GetContainer()
             .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));

            //containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IMemoService, MemoService>();
            //containerRegistry.Register<IDialogHostService, DialogHostService>();
        }

    }

}
