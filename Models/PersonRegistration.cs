using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoranaRegistration.Models
{
    public class PersonRegistration
    {
        [JsonProperty(PropertyName ="id")]
        public string id { get; set; }

        [DisplayName("CPR-nr")]
        [JsonProperty(PropertyName = "cpr")]
        public string cpr { get; set; }

        [DisplayName("Fulde navn")]
        [JsonProperty(PropertyName = "fullname")]
        public string fullname { get; set; }

        [DisplayName("Adresse")]
        [JsonProperty(PropertyName = "adress")]
        public string adress { get; set; }

        [DisplayName("PostNr")]
        [JsonProperty(PropertyName = "zipcode")]
        public int zipcode{ get; set; }

        [DisplayName("Test resultat")]
        [JsonProperty(PropertyName = "testresult")]
        public bool testresult { get; set; }


    }
}
