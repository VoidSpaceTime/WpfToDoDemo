using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Shared.Dtos
{
    public class SummaryDto : BaseDto
    {
        private int sum;

        public int Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChanged(); }
        }
        private int completedCount;

        public int CompletedCount
        {
            get { return completedCount; }
            set { completedCount = value; OnPropertyChanged(); }
        }
        private int memoCount;

        public int MemoCount
        {
            get { return memoCount; }
            set { memoCount = value; OnPropertyChanged(); }
        }
        private int todoCount;

        public int ToDoCount
        {
            get { return todoCount; }
            set { todoCount = value; OnPropertyChanged(); }
        }
        private string completedRadio;

        public string CompletedRadio
        {
            get { return completedRadio; }
            set { completedRadio = value; }
        }
        private ObservableCollection<ToDoDto> toDoList;

        public ObservableCollection<ToDoDto> ToDoList
        {
            get { return toDoList; }
            set { toDoList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<MemoDto> memoList;
        public ObservableCollection<MemoDto> MemoList
        {
            get { return memoList; }
            set { memoList = value; OnPropertyChanged(); }
        }





    }
}
