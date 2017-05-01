using MonkeyHubApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyHubApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriaPage : ContentPage
    {
        private CategoriaViewModel ViewModel => BindingContext as CategoriaViewModel;

        public CategoriaPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ViewModel.ShowContentCommand.Execute(e.SelectedItem);
            }
        }

        protected override void OnAppearing()
        {
            (this.BindingContext as CategoriaViewModel)?.LoadAsync();
            base.OnAppearing();
        }
    }
}
