using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialLoginMaratona.iOS;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace SocialLoginMaratona.iOS
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provider);

                SocialLoginMaratona.Helpers.Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                SocialLoginMaratona.Helpers.Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private UIKit.UIViewController GetController()
        {
            var windows = UIKit.UIApplication.SharedApplication.KeyWindow;

            var root = windows.RootViewController;

            if (root == null) return null;

            var current = root;

            while(current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}