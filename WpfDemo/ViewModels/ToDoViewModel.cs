using MyToDo.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private ObservableCollection<ToDoDto> toDoDtos;
        private DelegateCommand addCommand;
        private bool isRightDrawerOpen;
        private readonly IToDoService toDoService;
        public ToDoViewModel(IToDoService toDoService, IContainerProvider containerProvider, IEventAggregator eventAggregator) : base(containerProvider, eventAggregator)
        {
            ToDoDtos = new ObservableCollection<ToDoDto> { };
            AddCommand = new DelegateCommand(AddCommandExecute);
            this.toDoService = toDoService;

        }

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set
            {
                isRightDrawerOpen = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<ToDoDto> ToDoDtos
        {
            get { return toDoDtos; }
            set { toDoDtos = value; RaisePropertyChanged(); }
        }
        public DelegateCommand AddCommand
        {
            get { return addCommand; }
            private set
            {
                addCommand = value;
                RaisePropertyChanged();
            }
        }

        private void AddCommandExecute()
        {
            IsRightDrawerOpen = true;

        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private async void GetDataAsync()
        {
            UpdateLoading(true); // 显示加载中
            ToDoDtos.Clear();
            var todoResult = await toDoService.GetAllAsync(new MyToDo.Shared.Parameters.QueryParameter() { PageIndex = 0, PageSize = 100 });
            if (todoResult.Status)
            {
                foreach (var item in todoResult.Data.Items)
                {
                    ToDoDtos.Add(item);
                }
            }
            UpdateLoading(false); // 隐藏加载中
        }
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            GetDataAsync();

        }
    }
}
