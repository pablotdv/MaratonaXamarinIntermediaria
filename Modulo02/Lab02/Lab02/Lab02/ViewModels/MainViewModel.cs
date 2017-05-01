using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _propriedadeTexto = "Texto inicial";

        public string PropriedadeTexto
        {
            get { return _propriedadeTexto; }
            set
            {
                _propriedadeTexto = value;
                OnPropertyChanged();
            }
        }

    }
}
