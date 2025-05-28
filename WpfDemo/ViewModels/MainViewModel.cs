using System.Collections.ObjectModel;
using WpfDemo.Commen.Modles;

namespace WpfDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }
        private ObservableCollection<MenuBar> menuBars;

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
