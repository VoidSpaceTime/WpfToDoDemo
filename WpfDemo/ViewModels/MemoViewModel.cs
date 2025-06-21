using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class MemoViewModel : NavigationViewModel
    {
        private ObservableCollection<MemoDto> memoDtos;
        private DelegateCommand<string> executeCommand;
        private bool isRightDrawerOpen;
        private readonly IMemoService memoService;

        public MemoViewModel(IMemoService memoService, IContainerProvider containerProvider, IEventAggregator eventAggregator) : base(containerProvider, eventAggregator)
        {
            MemoDtos = new ObservableCollection<MemoDto> { };
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<MemoDto>(Selected);
            DeletedCommand = new DelegateCommand<MemoDto>(Deleted);
            this.memoService = memoService;
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
        public ObservableCollection<MemoDto> MemoDtos
        {
            get { return memoDtos; }
            set { memoDtos = value; RaisePropertyChanged(); }
        }
        public DelegateCommand<string> ExecuteCommand
        {
            get { return executeCommand; }
            private set
            {
                executeCommand = value; RaisePropertyChanged();
            }
        }
        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
  

        public DelegateCommand<MemoDto> SelectedCommand { get; private set; }
        public DelegateCommand<MemoDto> DeletedCommand { get; private set; }
        private MemoDto currentDto;

        public MemoDto CurrentDto
        {
            get { return currentDto; }
            set { currentDto = value; RaisePropertyChanged(); }
        }
        private void AddCommandExecute()
        {
            CurrentDto = new MemoDto();
            IsRightDrawerOpen = true;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private async void GetDataAsync()
        {
            UpdateLoading(true); // 显示加载中
            MemoDtos.Clear();
            var todoResult = await memoService.GetAllAsync(new QueryParameter() { PageIndex = 0, PageSize = 100, Search = Search });
            if (todoResult.Status)
            {
                foreach (var item in todoResult.Data.Items)
                {
                    MemoDtos.Add(item);
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
        /// 编辑选中备忘录事项 
        /// </summary>
        /// <param name="dto"></param>
        private void Selected(MemoDto dto)
        {
            //打开右侧抽屉
            IsRightDrawerOpen = true;
            UpdateLoading(true); // 显示加载中
            //获取选中备忘录事项数据
            memoService.GetFirstOrDefaultAsync(dto.Id).ContinueWith(task =>
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
                    memoService.AddAsync(CurrentDto).ContinueWith(task =>
                    {
                        if (task.Result.Status)
                        {
                            System.Windows.Application.Current.Dispatcher.Invoke(() =>
                            {
                                MemoDtos.Add(task.Result.Data);
                            });
                        }
                    });
                }
                else
                {
                    // 编辑
                    memoService.    UpdateAsync(CurrentDto).ContinueWith(task =>
                    {
                        if (task.Result.Status)
                        {
                            var memos = MemoDtos.FirstOrDefault(x => x.Id == CurrentDto.Id);
                            if (memos != null && task.Result.Data != null)
                            {
                                // 用数据库返回的最新数据更新本地集合
                                memos.Title = task.Result.Data.Title;
                                memos.Content = task.Result.Data.Content;
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
        private void Deleted(MemoDto dto)
        {
            if (dto == null || dto.Id == 0)
            {
                return;
            }
            UpdateLoading(true); // 显示加载中
            memoService.DeleteAsync(dto.Id).ContinueWith(task =>
            {
                if (task.Result.Status)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        MemoDtos.Remove(dto);
                    });
                }
            });
            UpdateLoading(false); // 隐藏加载中
        }
    }
}

