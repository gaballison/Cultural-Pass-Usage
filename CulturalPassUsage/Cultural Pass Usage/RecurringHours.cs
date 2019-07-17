using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    public class RootObject2
    {
        public RecurringHours[] RecurringHours { get; set; }
    }

    public class RecurringHours : IHours
    {

        public Days Day { get; set; }

        [JsonProperty(PropertyName = "open")]
        public bool Open { get; set; }

        [JsonProperty(PropertyName = "start_time")]
        public string OpenHours { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public string CloseHours { get; set; }
    }

    public enum Days
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}
