using MonkeyHubApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyHubApp.ViewModels
{
    public class ContentWebViewModel : BaseViewModel
    {
        public Content ContentWeb { get; }

        public ContentWebViewModel(Content content)
        {
            ContentWeb = content;
        }
    }
}
