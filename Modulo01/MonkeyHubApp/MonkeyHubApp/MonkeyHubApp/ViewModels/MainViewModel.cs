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
using MonkeyHubApp.Services;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string BaseUrl = "https://monkey-hub-api.azurewebsites.net/api/";
        
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
        public Command AboutCommand { get; }
        public Command<Tag> ShowCategoriaCommand { get; }

        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            AboutCommand = new Command(ExecuteAboutCommand);
            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);
            Resultados = new ObservableCollection<Tag>();
            _monkeyHubApiService = monkeyHubApiService;
        }

        private async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(tag);
        }

        async void ExecuteAboutCommand(object obj)
        {
            await PushAsync<AboutViewModel>();
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
                var tags = await _monkeyHubApiService.GetTagsAsync();
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
