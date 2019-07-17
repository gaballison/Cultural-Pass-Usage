using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    public class RootObject3
    {
        public StaticHours[] StaticHours { get; set; }
    }

    public class StaticHours : IHours
    {

        public string Date { get; set; }


        [JsonProperty(PropertyName = "start_time")]
        public string OpenHours { get; set; }

        [JsonProperty(PropertyName = "end_time")]
        public string CloseHours { get; set; }
    }


}
