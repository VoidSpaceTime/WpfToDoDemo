using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common.Modles;

namespace WpfDemo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        public IndexViewModel(IDialogService dialogService)
        {
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ToDoDtos = new ObservableCollection<MemoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            DialogService = dialogService;
        }
        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<MemoDto> ToDoDtos { get; set; }
        public ObservableCollection<MemoDto> MemoDtos { get; set; }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        public IDialogService DialogService { get; }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办":
                    {
                        AddToDo();
                    }
                    break;
                case "新增备忘录": AddMemo(); break;
            }

        }
        void AddToDo()
        {
            DialogService.ShowDialogAsync("AddToDoView");
        }
        void AddMemo()
        {
            DialogService.ShowDialogAsync("MemoView");
        }
        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar()
            {
                Title = "汇总",
                Icon = "ExpandAll",
                Content = "9",
                Color = "#FFCA0FF",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "已完成",
                Icon = "ClockCheckOutline",
                Content = "9",
                Color = "#FF1ECA3A",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "完成比例",
                Icon = "ChartLineVariant",
                Content = "100%",
                Color = "#FF02C6DC",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "备忘录",
                Icon = "PlayerlistStart",
                Content = "9",
                Color = "#FFFFA000",
                Target = ""
            });
        }
    }
}
