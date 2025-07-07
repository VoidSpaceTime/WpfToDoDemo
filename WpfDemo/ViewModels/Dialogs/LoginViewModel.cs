using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;

namespace WpfDemo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public LoginViewModel()
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
        }
        public string Title => "ToDo";
        public DialogCloseListener RequestClose { get; }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
        public DelegateCommand<string> ExecuteCommand { get; set; }
        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; RaisePropertyChanged(); }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; RaisePropertyChanged(); }
        }

        private void Execute(string parameter)
        {
            switch (parameter)
            {
                case "Login":
                    Login();
                    break;
                case "LoginOut":
                    LoginOut();
                    break;
                case "Cancel":
                    break;
                default:
                    throw new ArgumentException("Invalid command parameter", nameof(parameter));
            }
        }
        void Login()
        {
            // Simulate a login operation
        }
        void LoginOut()
        {
            // Simulate a login operation
        }
    }

}
