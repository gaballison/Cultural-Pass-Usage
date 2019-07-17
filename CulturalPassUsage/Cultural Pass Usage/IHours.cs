using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    interface IHours
    {
        string OpenHours { get; set; }

        string CloseHours { get; set; }
    }
}
