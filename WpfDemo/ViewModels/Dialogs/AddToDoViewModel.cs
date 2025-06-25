using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;

namespace WpfDemo.ViewModels.Dialogs
{
    internal class AddToDoViewModel : IDialogHostAware
    {
        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }



        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
        }
        private void Save()
        {
            var param = new DialogParameters();
            DialogHost.Close(DialogHostName, param);
        }
        private void Cancel()
        {
            DialogHost.Close(DialogHostName);
        }
    }
}
