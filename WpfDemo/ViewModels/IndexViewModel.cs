using MyToDo.Shared.Dtos;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.Common.Modles;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        public IDialogHostService DialogService { get; }
        public IndexViewModel(IDialogHostService dialogService, IContainerProvider containerProvider) : base(containerProvider)
        {
            toDoService = containerProvider.Resolve<IToDoService>();
            memoService = containerProvider.Resolve<IMemoService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            ToDoDtos = new ObservableCollection<ToDoDto>();
            MemoDtos = new ObservableCollection<MemoDto>();
            TaskBars = new ObservableCollection<TaskBar>();
            CreateTaskBars();
            DialogService = dialogService;
        }


        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ToDoDto> ToDoDtos { get; set; }
        public ObservableCollection<MemoDto> MemoDtos { get; set; }
        public DelegateCommand<string> ExecuteCommand { get; set; }

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
        async Task AddToDo()
        {
            var dialogResult = await DialogService.ShowDialogAsync("AddToDoView", null);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");
                if (todo.Id > 0)
                {

                }
                else
                {
                    // 新增待办
                    var result = await toDoService.AddAsync(todo);
                    if (result.Status)
                    {
                        ToDoDtos.Add(result.Data);
                    }
                }
            }
        }
        async void AddMemo()
        {
            var dialogResult = await DialogService.ShowDialogAsync("AddMemoView", null);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");
                if (memo.Id > 0)
                {

                }
                else
                {
                    // 新增待办
                    var result = await memoService.AddAsync(memo);
                    if (result.Status)
                    {
                        MemoDtos.Add(result.Data);
                    }
                }
            }
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
