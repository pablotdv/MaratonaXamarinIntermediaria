using System;
using System.Collections.Generic;
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

        public Command SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);
        }

        private bool CanExecuteSearchCommand(object arg)
        {
            return !string.IsNullOrWhiteSpace(SearchTerm);
        }

        async private void ExecuteSearchCommand(object obj)
        {
            await Task.Delay(2000);
            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                $"Você pesquisa por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"Obrigado.", "Ok");
            }
            else await App.Current.MainPage.DisplayAlert("MonkeyHubApp", $"De nada.", "Ok");
        }
    }
}
