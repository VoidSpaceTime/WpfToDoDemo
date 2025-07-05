using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using WpfDemo.Common;
using WpfDemo.Extensions; // Ensure Prism.Events namespace is included  

namespace WpfDemo.Views
{
    /// <summary>  
    /// MainView.xaml 的交互逻辑  
    /// </summary>  
    public partial class MainView : Window
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IDialogHostService dialogHostService;
        public MainView(IEventAggregator eventAggregator, IDialogHostService dialogHostService)
        {
            InitializeComponent();
            // 注册全局消息通知 提示消息
            eventAggregator.ResigiterMessage(arg =>
            {
                Snackbar.MessageQueue!.Enqueue(arg);
            });

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
            this.eventAggregator = eventAggregator;
            this.dialogHostService = dialogHostService;

            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += async (s, e) =>
            {
                var dialogResult = await dialogHostService.Question("温馨提示", "确认退出系统?");
                if (dialogResult.Result != Prism.Dialogs.ButtonResult.OK) return;
                this.Close();
            };
            colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            colorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };

            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };
            this.dialogHostService = dialogHostService;
        }
    }

}