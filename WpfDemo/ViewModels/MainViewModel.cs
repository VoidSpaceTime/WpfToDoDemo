using System.Collections.ObjectModel;
using WpfDemo.Common.Modles;
using WpfDemo.Extensions;

namespace WpfDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommond = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
            GoBackCommand = new DelegateCommand(() =>
            {
                if (regionNavigationJournal != null && regionNavigationJournal.CanGoBack)
                {
                    regionNavigationJournal.GoBack();
                }
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (regionNavigationJournal != null && regionNavigationJournal.CanGoForward)
                {
                    regionNavigationJournal.GoForward();
                }
            });
        }
        /// <summary>
        /// 导航到指定的菜单栏
        /// </summary>
        /// <param name="bar"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Navigate(MenuBar bar)
        {
            if (bar == null || string.IsNullOrWhiteSpace(bar.NameSpace))
            {
                return;
            }
            // 通过命名空间获取对应的视图类型
            //   xmlns:b="http://schemas.microsoft.com/xaml/behaviors" 注册行为
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back =>
            {
                // 记录导航历史
                regionNavigationJournal = back.Context.NavigationService.Journal;
            });
            //regionManager.RequestNavigate(PrismManager.MainViewRegionName, bar.Title);//不知道对不对

        }

        // 用于导航的命令
        public DelegateCommand<MenuBar> NavigateCommond { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal regionNavigationJournal;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar
            {
                Icon = "Home",
                Title = "首页",
                NameSpace = "IndexView"
            });
            MenuBars.Add(new MenuBar
            {
                Icon = "NotebookOutline",
                Title = "待办事项",
                NameSpace = "ToDoView"
            });
            MenuBars.Add(new MenuBar
            {
                Icon = "NotebookPlus",
                Title = "备忘录",
                NameSpace = "MemoView"
            });
            MenuBars.Add(new MenuBar
            {
                Icon = "Cog",
                Title = "设置",
                NameSpace = "SettingsView"
            });
        }
    }
}
