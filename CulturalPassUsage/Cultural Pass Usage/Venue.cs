using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{

    public class RootObject
    {
        public Venue[] Venue { get; set; }
    }

    public class Venue
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "availability_desc")]
        public string AvailabilityDesc { get; set; }

        [JsonProperty(PropertyName = "availability_recurring")]
        public bool RecurringBool { get; set; }

        [JsonProperty(PropertyName = "availability_recurring_schedule")]
        public List<RecurringHours> Recurring {get; set; }

        [JsonProperty(PropertyName = "availability_static_schedule")]
        public List<StaticHours> Static { get; set; }

        [JsonProperty(PropertyName = "recommended_age_youngest")]
        public int Youngest { get; set; }

        [JsonProperty(PropertyName = "recommended_age_oldest")]
        public int Oldest { get; set; }
    }
}
