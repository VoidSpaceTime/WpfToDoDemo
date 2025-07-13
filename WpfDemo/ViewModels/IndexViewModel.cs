using MyToDo.Shared.Dtos;
using Prism.Ioc;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.Common.Modles;
using WpfDemo.Extensions;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {
        private readonly IToDoService toDoService;
        private readonly IMemoService memoService;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        public IDialogHostService DialogService { get; }
        public IndexViewModel(IDialogHostService dialogService, IContainerProvider containerProvider, IEventAggregator eventAggregator)
            : base(containerProvider, eventAggregator)
        {
            this.toDoService = containerProvider.Resolve<IToDoService>();
            this.memoService = containerProvider.Resolve<IMemoService>();
            this.regionManager = containerProvider.Resolve<IRegionManager>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            TaskBars = new ObservableCollection<TaskBar>();
            Title = $"你好!{AppSession.UserName},现在是{DateTime.Now:yyyy年MM月dd日 dddd}";
            CreateTaskBars();
            DialogService = dialogService;
            EditToDoCommand = new DelegateCommand<ToDoDto>(AddToDo);
            EditMemoCommand = new DelegateCommand<MemoDto>(AddMemo);
            ToDoCompltedCommand = new DelegateCommand<ToDoDto>(Complted);
            NavigateCommand = new DelegateCommand<TaskBar>(Navigate);
            this.eventAggregator = eventAggregator;
        }


        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public DelegateCommand<ToDoDto> EditToDoCommand { get; private set; }
        public DelegateCommand<MemoDto> EditMemoCommand { get; private set; }
        public DelegateCommand<ToDoDto> ToDoCompltedCommand { get; private set; }
        public DelegateCommand<TaskBar> NavigateCommand { get; private set; }
        private SummaryDto summary;
        public SummaryDto Summary
        {
            get { return summary; }
            set { summary = value; RaisePropertyChanged(); }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }


        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办":
                    {
                        AddToDo(null);
                    }
                    break;
                case "新增备忘录": AddMemo(null); break;
            }
        }
        async void AddToDo(ToDoDto toDoDto)
        {
            var dialogParameters = new DialogParameters();
            if (toDoDto != null)
            {
                dialogParameters.Add("Value", toDoDto);
            }

            var dialogResult = await DialogService.ShowDialogAsync("AddToDoView", dialogParameters);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var todo = dialogResult.Parameters.GetValue<ToDoDto>("Value");
                if (todo.Id > 0)
                {
                    await toDoService.UpdateAsync(todo).ContinueWith(t =>
                      {
                          if (t.Result.Status)
                          {
                              var td = Summary.ToDoList.FirstOrDefault(Summary.ToDoList.FirstOrDefault(x => x.Id == todo.Id));
                              if (td != null)
                              {
                                  td.Title = todo.Title;
                                  td.Content = todo.Content;
                              }
                          }
                      });
                }
                else
                {
                    // 新增待办
                    var result = await toDoService.AddAsync(todo);
                    if (result.Status)
                    {
                        Summary.ToDoList.Add(result.Data);
                    }
                }
            }
            Refresh();
        }
        async void AddMemo(MemoDto memoDto)
        {
            var dialogParameters = new DialogParameters();
            if (memoDto != null)
            {
                dialogParameters.Add("Value", memoDto);
            }
            var dialogResult = await DialogService.ShowDialogAsync("AddMemoView", dialogParameters);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var memo = dialogResult.Parameters.GetValue<MemoDto>("Value");

                if (memo.Id > 0)
                {
                    await memoService.UpdateAsync(memo).ContinueWith(t =>
                    {
                        if (t.Result.Status)
                        {
                            var mm = Summary.ToDoList.FirstOrDefault(Summary.ToDoList.FirstOrDefault(x => x.Id == memo.Id));
                            if (mm != null)
                            {
                                mm.Title = memo.Title;
                                mm.Content = memo.Content;
                            }
                        }
                    });
                }
                else
                {
                    // 新增待办
                    var result = await memoService.AddAsync(memo);
                    if (result.Status)
                    {
                        Summary.MemoList.Add(result.Data);
                    }
                }
            }
            Refresh();
        }
        private async void Complted(ToDoDto toDoDto)
        {
            if (toDoDto == null) return;
            toDoDto.Status = 1; // 设置为已完成
            var result = await toDoService.UpdateAsync(toDoDto);
            if (result.Status)
            {
                var find = Summary.ToDoList.FirstOrDefault(x => x.Id.Equals(toDoDto.Id));
                if (find != null)
                {
                    Summary.ToDoList.Remove(find);
                }
            }
            Refresh();
            eventAggregator.SendMessage($"已完成!");
        }
        void CreateTaskBars()
        {
            TaskBars.Add(new TaskBar()
            {
                Title = "汇总",
                Icon = "ExpandAll",
                Color = "#FFCA0FF",
                Target = "ToDoView"
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "已完成",
                Icon = "ClockCheckOutline",
                Color = "#FF1ECA3A",
                Target = "ToDoView"
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "完成比例",
                Icon = "ChartLineVariant",
                Color = "#FF02C6DC",
                Target = ""
            });
            TaskBars.Add(new TaskBar()
            {
                Title = "备忘录",
                Icon = "PlayerlistStart",
                Color = "#FFFFA000",
                Target = "MemoView"
            });
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {

            Refresh();
            base.OnNavigatedTo(navigationContext);

        }
        async void Refresh()
        {
            UpdateLoading(true);
            var summaryResult = await toDoService.GetSummaryAsync();
            if (summaryResult.Status)
            {
                Summary = summaryResult.Data;
            }
            else
            {
                Summary = new SummaryDto();
            }
            TaskBars[0].Content = Summary.Sum.ToString();
            TaskBars[1].Content = Summary.CompletedCount.ToString();
            TaskBars[2].Content = Summary.CompletedRadio;
            TaskBars[3].Content = Summary.MemoCount.ToString();
            UpdateLoading(false);
        }
        private void Navigate(TaskBar bar)
        {
            if (string.IsNullOrWhiteSpace(bar.Target)) return;
            var param = new NavigationParameters();
            if (bar.Title == "已完成")
            {
                param.Add("Value", 2); // 传递已完成状态
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.Target, param);
        }

    }
}
