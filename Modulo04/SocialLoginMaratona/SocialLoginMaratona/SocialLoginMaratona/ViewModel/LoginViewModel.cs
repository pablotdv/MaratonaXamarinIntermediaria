using SocialLoginMaratona.Helpers;
using SocialLoginMaratona.Services;
using SocialLoginMaratona.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SocialLoginMaratona.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        AzureService azureService;
        INavigation navigation;

        ICommand loginCommand;

        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));

        public LoginViewModel(INavigation nav)
        {
            azureService = DependencyService.Get<AzureService>();
            navigation = nav;

            Title = "Social Login Demo";
        }
        private async Task ExecuteLoginCommandAsync()
        {
            if (IsBusy || !(await LoginAsync()))
                return;
            else
            {
                var mainPage = new MainPage();
                await navigation.PushAsync(mainPage);
                RemovePageFromStack();
            }
        }

        private void RemovePageFromStack()
        {
            var existingPage = navigation.NavigationStack.ToList();
            foreach(var page in existingPage)
            {
                if (page.GetType() == typeof(LoginPage))
                    navigation.RemovePage(page);
            }
        }

        public Task<bool> LoginAsync()
        {
            if(Settings.IsLoggedIn)
            {
                return Task.FromResult(true);
            }

            return azureService.LoginAsync();
        }
    }
}
