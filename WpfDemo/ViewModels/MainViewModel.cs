using System.Collections.ObjectModel;
using WpfDemo.Commen.Modles;
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
        }
        /// <summary>
        /// 导航到指定的菜单栏
        /// </summary>
        /// <param name="bar"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Navigate(MenuBar bar)
        {
            if(bar == null && string.IsNullOrWhiteSpace(bar.NameSpace))
            {
                return;
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.Title);
            //regionManager.RequestNavigate(PrismManager.MainViewRegionName, bar.Title);//不知道对不对
        }

        // 用于导航的命令
        public DelegateCommand<MenuBar> NavigateCommond { get; private set; }
        private ObservableCollection<MenuBar> menuBars;
        private readonly IRegionManager regionManager;

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
                NameSpace = "SettingView"
            });
        }
    }
}
