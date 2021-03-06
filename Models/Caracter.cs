using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace heroes_api.Models
{
    public class Caracter
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "urlImage")]
        public string UrlImage { get; set; }
    }
}