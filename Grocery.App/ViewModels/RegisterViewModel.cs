
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string email = "user@mail.com";

        [ObservableProperty]
        private string username = "Username";

        [ObservableProperty]
        private string password = "user1234";

        [ObservableProperty]
        private string registerMessage;

        public RegisterViewModel(IAuthService authService, GlobalViewModel global)
        { //_authService = App.Services.GetServices<IAuthService>().FirstOrDefault();
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        private void Register()
        {
            // register user with email and password inside a database
            Client? newClient = _authService.Register(Username, Email, Password);

            // login immidately after registering
            Client? authenticatedClient = _authService.Login(Email, Password);
            if (authenticatedClient != null)
            {
                registerMessage = $"Welkom {authenticatedClient.Name}!";
                _global.Client = authenticatedClient;
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                registerMessage = "Ongeldige inloggegevens.";
            }
        }
    }
}
