using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common.Modles;

namespace WpfDemo.ViewModels
{
    public class ToDoViewModel : BindableBase
    {
        private ObservableCollection<MemoDto> toDoDtos;
        private DelegateCommand addCommand;
        private bool isRightDrawerOpen;

        public ToDoViewModel()
        {
            ToDoDtos = new ObservableCollection<MemoDto> { };
            CreateToDoList();
            AddCommand = new DelegateCommand(AddCommandExecute);
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
        public ObservableCollection<MemoDto> ToDoDtos
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

        private void CreateToDoList()
        {
            for (int i = 0; i < 20; i++)
            {
                ToDoDtos.Add(new MemoDto
                {
                    Title = $"待办事项{i + 1}",
                    Content = $"待办事项{i + 1}的内容",
                });
            }
        }
    }
}
