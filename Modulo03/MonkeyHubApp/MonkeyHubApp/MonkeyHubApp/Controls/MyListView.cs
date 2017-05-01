using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MonkeyHubApp.Controls
{
    public class MyListView : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
                    BindableProperty.Create("ItemTappedCommand",
                        typeof(ICommand),
                        typeof(MyListView));

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }

        public MyListView(ListViewCachingStrategy strategy)
            : base(strategy)
        {
            Initialize();
        }

        public MyListView()
            : this(ListViewCachingStrategy.RecycleElement)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ItemSelected += (sender, e) =>
            {
                if (ItemTappedCommand == null)
                    return;

                if (ItemTappedCommand.CanExecute(e.SelectedItem))
                    ItemTappedCommand.Execute(e.SelectedItem);
            };
        }
    }
}
