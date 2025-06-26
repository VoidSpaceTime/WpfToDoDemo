using MaterialDesignThemes.Wpf;
using MyToDo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;

namespace WpfDemo.ViewModels.Dialogs
{
    internal class AddToDoViewModel : BindableBase, IDialogHostAware
    {
        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }



        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        private ToDoDto model;

        public ToDoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }


        public void OnDialogOpend(IDialogParameters parameters)
        {
            if(parameters.ContainsKey("Value"))
            {
                Model = parameters.GetValue<ToDoDto>("Value");
            }
            else
            {
                Model = new ToDoDto();
            }
        }
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Title) || string.IsNullOrWhiteSpace(Model.Content))
            {
                return;
            }

            var result = new DialogResult(ButtonResult.OK);
            result.Parameters.Add("Value", Model);
            DialogHost.Close(DialogHostName, result);
        }
        private void Cancel()
        {
            DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }
    }
}
