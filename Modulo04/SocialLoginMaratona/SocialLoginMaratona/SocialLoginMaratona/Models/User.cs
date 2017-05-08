using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialLoginMaratona.Models
{
    /// <summary>
    /// Usuário
    /// </summary>
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        
    }
}
