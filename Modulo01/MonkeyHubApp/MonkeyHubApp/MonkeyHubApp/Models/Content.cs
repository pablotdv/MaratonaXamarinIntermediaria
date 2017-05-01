using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyHubApp.Models
{
    public class Content
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Tag Tag { get; set; }
        public string Banner { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }   
}
