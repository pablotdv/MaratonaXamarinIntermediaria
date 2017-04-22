using MaratonaXamarin.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MaratonaXamarin.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnCarregar_Click(object sender, RoutedEventArgs e)
        {
            var api = new UserApi();
            var users = await api.ListAsync(new Developer()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Angelo",
                Email = "angelo@angelo.com",
                State = "SP",
                City = "Carapicuiba"
            });

            lvwItens.ItemsSource = users.OrderBy(a => a.Name).Select(a => a.Name);
        }
    }
}
