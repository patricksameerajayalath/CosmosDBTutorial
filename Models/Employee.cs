using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CosmosDBTutorial.Models
{
    public class Employee
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "dob")]
        public DateTime DOB { get; set; }

        [JsonProperty(PropertyName = "ismanager")]
        public bool Manager { get; set; }
    }
}
