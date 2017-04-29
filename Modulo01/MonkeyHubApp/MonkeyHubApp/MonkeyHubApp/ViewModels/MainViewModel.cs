using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MonkeyHubApp.Models;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string BaseUrl = "https://monkey-hub-api.azurewebsites.net/api/";

        public async Task<List<Tag>> GetTagsAsync()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync($"{BaseUrl}Tags").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return JsonConvert.DeserializeObject<List<Tag>>(
                        await new StreamReader(responseStream)
                            .ReadToEndAsync().ConfigureAwait(false));
                }
            }

            return null;
        }

        private string _searchTerm;

        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            Resultados = new ObservableCollection<Tag>();
        }

        private bool CanExecuteSearchCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(SearchTerm);
        }

        async private void ExecuteSearchCommand(object obj)
        {
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                $"Você pesquisa por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Obrigado.", "Ok");
                var tags = await GetTagsAsync();
                Resultados.Clear();
                if (tags != null)
                {
                    foreach (var tag in tags)
                    {
                        Resultados.Add(tag);
                    }
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"De nada.", "Ok");
                Resultados.Clear();
            }
        }
    }
}
