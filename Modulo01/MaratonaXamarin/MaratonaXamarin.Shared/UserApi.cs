using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaratonaXamarin.Shared
{
    public class UserApi
    {
        private readonly MobileServiceClient _client;
        public UserApi()
        {
            _client = new MobileServiceClient("http://hellomonkeys.azurewebsites.net/");
        }

        public async Task<List<Users>> ListAsync(Developer developer)
        {
            var users = await _client.InvokeApiAsync<Developer, List<Users>>("monkeys", developer);

            return users;
        }
    }
}
