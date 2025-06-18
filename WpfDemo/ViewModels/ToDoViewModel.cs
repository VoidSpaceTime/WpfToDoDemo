using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class ToDoViewModel : NavigationViewModel
    {
        private ObservableCollection<ToDoDto> toDoDtos;
        private DelegateCommand<string> executeCommand;
        private bool isRightDrawerOpen;
        public ToDoViewModel(IToDoService toDoService, IContainerProvider containerProvider, IEventAggregator eventAggregator) : base(containerProvider, eventAggregator)
        {
            ToDoDtos = new ObservableCollection<ToDoDto> { };
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Selected);
            DeletedCommand = new DelegateCommand<ToDoDto>(Deleted);
            this.toDoService = toDoService;
        }


        private readonly IToDoService toDoService;

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
        public DelegateCommand<string> ExecuteCommand
        {
            get { return executeCommand; }
            private set
            {
                executeCommand = value;
                RaisePropertyChanged();
            }
        }
        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }
        public DelegateCommand<ToDoDto> DeletedCommand { get; private set; }
        private ToDoDto currentDto;

        public ToDoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }
        private void AddCommandExecute()
        {
            CurrentDto = new ToDoDto();
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
            var todoResult = await toDoService.GetAllFilterAsync(new ToDoParameter() { PageIndex = 0, PageSize = 100, Search = Search, Status = SelectIndex - 1 });
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
        /// <summary>
        /// 编辑选中待办事项 
        /// </summary>
        /// <param name="dto"></param>
        private void Selected(ToDoDto dto)
        {
            //打开右侧抽屉
            IsRightDrawerOpen = true;
            UpdateLoading(true); // 显示加载中
            //获取选中待办事项数据
            toDoService.GetFirstOrDefaultAsync(dto.Id).ContinueWith(task =>
            {
                if (task.Result.Status)
                {
                    CurrentDto = task.Result.Data;
                }
            });
            UpdateLoading(false); // 隐藏加载中
        }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增":
                    AddCommandExecute();
                    break;
                case "查询":
                    GetDataAsync();
                    break;
                case "保存":
                    Save();
                    break;
                default:
                    break;
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentDto.Title) || string.IsNullOrWhiteSpace(CurrentDto.Content))
            {
                return;
            }
            IsRightDrawerOpen = false;
            UpdateLoading(true); // 显示加载中   
            try
            {
                if (CurrentDto.Id == 0)
                {
                    //新增
                    toDoService.AddAsync(CurrentDto).ContinueWith(task =>
                     {
                         if (task.Result.Status)
                         {
                             System.Windows.Application.Current.Dispatcher.Invoke(() =>
                             {
                                 ToDoDtos.Add(task.Result.Data);
                             });
                         }
                     });
                }
                else
                {
                    // 编辑
                    toDoService.UpdateAsync(CurrentDto).ContinueWith(task =>
                    {
                        if (task.Result.Status)
                        {
                            var todos = ToDoDtos.FirstOrDefault(x => x.Id == CurrentDto.Id);
                            if (todos != null && task.Result.Data != null)
                            {
                                // 用数据库返回的最新数据更新本地集合
                                todos.Title = task.Result.Data.Title;
                                todos.Content = task.Result.Data.Content;
                                todos.Status = task.Result.Data.Status;
                            }
                        }
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }

        }
        private void Deleted(ToDoDto dto)
        {
            if (dto == null || dto.Id == 0)
            {
                return;
            }
            UpdateLoading(true); // 显示加载中
            toDoService.DeleteAsync(dto.Id).ContinueWith(task =>
            {
                if (task.Result.Status)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        ToDoDtos.Remove(dto);
                    });
                }
            });
            UpdateLoading(false); // 隐藏加载中
        }
    }
}