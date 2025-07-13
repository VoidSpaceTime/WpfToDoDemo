using MyToDo.Shared.Dtos;
using WpfDemo.Common;
using WpfDemo.Extensions;
using WpfDemo.Sercive;

namespace WpfDemo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly ILoginService loginService;
        private bool isManualClose = false;
        private readonly IEventAggregator eventAggregator;

        public LoginViewModel(ILoginService loginService, IEventAggregator eventAggregator)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.loginService = loginService;
            SelectIndex = 0;
            RegisterUser = new RegisterUserDto();
            this.eventAggregator = eventAggregator;
        }
        public string Title => "ToDo";
        public DialogCloseListener RequestClose { get; }
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            if (!isManualClose)
            {
                LoginOut();
            }
            isManualClose = false; // 重置标志位
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
        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }
        private RegisterUserDto registerUser;

        public RegisterUserDto RegisterUser
        {
            get { return registerUser; }
            set { registerUser = value; RaisePropertyChanged(); }
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
                case "ToRegister":
                    SelectIndex = 1;
                    break;
                case "ToLogin":
                    SelectIndex = 0;
                    break;
                case "Register":
                    Register();
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
            var res = await loginService.LoginAsync(new UserDto()
            {
                Account = Account,
                Password = Password
            });
            if (res != null && res.Status)
            {
                AppSession.UserName = res.Data.UserName;
                isManualClose = true;
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                return;
            }
            else
            {
                isManualClose = true;
                //RequestClose.Invoke(new DialogResult(ButtonResult.Cancel));
                //eventAggregator.SendMessage("用户名或密码错误!");
                eventAggregator.SendMessage(res.Message, "Login");
                return;
            }
        }
        void LoginOut()
        {
            isManualClose = true;
            RequestClose.Invoke(new DialogResult(ButtonResult.No));
        }
        async Task Register()
        {
            if (registerUser.Password.Equals(registerUser.NewPassword) && !string.IsNullOrWhiteSpace(registerUser.UserName) && !string.IsNullOrWhiteSpace(registerUser.Password) && !string.IsNullOrWhiteSpace(registerUser.Account))
            {
                await loginService.RegisterAsync(new UserDto()
                {
                    Account = registerUser.Account,
                    Password = registerUser.Password,
                    UserName = registerUser.UserName

                }).ContinueWith(e =>
                {
                    if (e.IsCompletedSuccessfully)
                    {
                        // Handle successful registration
                        SelectIndex = 0; // Switch back to login view
                        eventAggregator.SendMessage($"注册成功", "Login");
                        return;
                    }
                    else
                    {
                        // Handle registration failure
                        // Show error message or handle accordingly
                        eventAggregator.SendMessage($"{e.Result.Message},注册失败，请稍后再试！", "Login");
                        return;
                    }
                });
            }
            else
            {
                eventAggregator.SendMessage("注册失败，请检查输入信息是否完整！", "Login");
            }
            return;
        }
    }

}
