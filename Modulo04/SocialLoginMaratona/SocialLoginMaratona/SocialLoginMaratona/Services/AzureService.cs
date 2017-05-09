using Microsoft.WindowsAzure.MobileServices;
using SocialLoginMaratona.Helpers;
using SocialLoginMaratona.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]
namespace SocialLoginMaratona.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "https://maratona-xamarin-brasil.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;

        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if (!string.IsNullOrWhiteSpace(Helpers.Settings.AuthToken) && !string.IsNullOrWhiteSpace(Helpers.Settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(Helpers.Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Helpers.Settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops!", "Não conseguimos efetuar o seu login, tente novamente!", "OK");
                });

                return false;
            }

            Settings.AuthToken = user.MobileServiceAuthenticationToken;
            Settings.UserId = user.UserId;

            return true;
        }

    }
}
