using System.Windows;
using Prism.Events;
using WpfDemo.Extensions; // Ensure Prism.Events namespace is included  

namespace WpfDemo.Views
{
    /// <summary>  
    /// MainView.xaml 的交互逻辑  
    /// </summary>  
    public partial class MainView : Window
    {
        public MainView(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            // 使用扩展方法注册等待消息窗口
            eventAggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;

                if (DialogHost.IsOpen)
                    DialogHost.DialogContent = new ProgressView();
            });

            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false; // 选择菜单后关闭抽屉  
            };
        }
    }

}