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

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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

        public ObservableCollection<string> Resultados { get; }

        public Command SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
            Resultados = new ObservableCollection<string>(new[] { "abc", "cde", "1", "2", "3", "4", "5", "6", "7", "8" });
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
                Resultados.Clear();
                for (int i = 1; i <= 30; i++)
                {
                    Resultados.Add($"Sim {i}");
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
