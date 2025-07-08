using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly ILoginService loginService;
        public LoginViewModel(ILoginService loginService)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
        }
        public string Title => "ToDo";
        public DialogCloseListener RequestClose { get; }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            LoginOut();
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
        async Task Login()
        {
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password))
            {
                // Show error message for empty fields
                return;
            }
            await loginService.LoginAsync(new MyToDo.Shared.Dtos.UserDto()
            {
                Account = Account,
                Password = Password
            }).ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    // Handle successful login
                    RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                }
                else
                {
                    // Handle login failure
                    RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
                }
            });
        }
        void LoginOut()
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.No));

        }
    }

}
