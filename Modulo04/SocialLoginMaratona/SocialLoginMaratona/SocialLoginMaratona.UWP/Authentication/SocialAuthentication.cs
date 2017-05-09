using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SocialLoginMaratona.UWP;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace SocialLoginMaratona.UWP
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(provider);

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