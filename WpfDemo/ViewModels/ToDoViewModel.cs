using MyToDo.Shared.Dtos;
using System.Collections.ObjectModel;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class ToDoViewModel : BindableBase
    {
        private ObservableCollection<ToDoDto> toDoDtos;
        private DelegateCommand addCommand;
        private bool isRightDrawerOpen;
        private readonly IToDoService toDoService;
        public ToDoViewModel(IToDoService toDoService)
        {
            ToDoDtos = new ObservableCollection<ToDoDto> { };
            AddCommand = new DelegateCommand(AddCommandExecute);
            this.toDoService = toDoService;
            CreateToDoList();
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

        private async Task CreateToDoList()
        {
            ToDoDtos.Clear();
            var todoResult = await toDoService.GetAllAsync(new MyToDo.Shared.Parameters.QueryParameter() { PageIndex = 0, PageSize = 100});
            if (todoResult.Status)
            {
                foreach (var item in todoResult.Data.Items)
                {
                    ToDoDtos.Add(item);

                }
            }
        }
    }
}
