using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using SocialLoginMaratona.Droid;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace SocialLoginMaratona.Droid
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                SocialLoginMaratona.Helpers.Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                SocialLoginMaratona.Helpers.Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}