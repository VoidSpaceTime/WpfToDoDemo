using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common.Modles;

namespace WpfDemo.ViewModels
{
    public class MemoViewModel : BindableBase
    {
        public MemoViewModel()
        {
            MemoDtos = new ObservableCollection<MemoDto> { };
            CreateMemoList();
            AddCommand = new DelegateCommand(AddCommandExecute);
        }
        private ObservableCollection<MemoDto> memoDtos;
        private DelegateCommand addCommand;
        private bool isRightDrawerOpen;


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

        private void CreateMemoList()
        {
            for (int i = 0; i < 20; i++)
            {
                MemoDtos.Add(new MemoDto
                {
                    Title = $"备忘录事项{i + 1}",
                    Content = $"备忘录事项{i + 1}的内容",
                });
            }
        }
    }
}

