using MyToDo.Shared.Dtos;
using System.Collections.ObjectModel;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        private ObservableCollection<MemoDto> memoDtos;
        private DelegateCommand addCommand;
        private bool isRightDrawerOpen;
        private readonly IMemoService memoService;
        public MemoViewModel(IMemoService memoService)
        {
            MemoDtos = new ObservableCollection<MemoDto> { };
            AddCommand = new DelegateCommand(AddCommandExecute);
            this.memoService = memoService;
            CreateMemoList();
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

        private async void CreateMemoList()
        {
            MemoDtos.Clear();
            var memoResult = await memoService.GetAllAsync(new MyToDo.Shared.Parameters.QueryParameter() { PageIndex = 0, PageSize = 100 });
            if (memoResult.Status)
            {
                foreach (var item in memoResult.Data.Items)
                {
                    MemoDtos.Add(item);

                }
            }
            /*         for (int i = 0; i < 20; i++)
                     {
                         MemoDtos.Add(new MemoDto
                         {
                             Title = $"备忘录事项{i + 1}",
                             Content = $"备忘录事项{i + 1}的内容",
                         });
                     }*/
        }
    }
}

