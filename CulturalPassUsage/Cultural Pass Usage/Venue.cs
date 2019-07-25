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

    public class Venue : IEquatable<Venue>
    {
        
        // Deserializing the JSON data 

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

        //[JsonProperty(PropertyName = "availability_recurring")]
        //public bool RecurringBool { get; set; }

        //[JsonProperty(PropertyName = "availability_recurring_schedule")]
        //public List<RecurringHours> Recurring {get; set; }

        //[JsonProperty(PropertyName = "availability_static_schedule")]
        //public List<StaticHours> Static { get; set; }

        [JsonProperty(PropertyName = "recommended_age_youngest")]
        public int Youngest { get; set; }

        [JsonProperty(PropertyName = "recommended_age_oldest")]
        public int Oldest { get; set; }


        // Need to implement custom Equals method in order to use Contains method
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Venue objAsVenue = obj as Venue;
            if (objAsVenue == null) return false;
            else return Equals(objAsVenue);
        }
        public override int GetHashCode()
        {
            return ID;
        }
        public bool Equals(Venue other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
            //throw new NotImplementedException();
        }

        public void WriteDesc()
        {
            string line = "------------------------------------------------------------------------------------------";

            Console.WriteLine("");
            Console.WriteLine(line);
            Console.WriteLine($"#{ID}. {Name.ToUpperInvariant()}");
            Console.WriteLine();
            Console.WriteLine($"\"{Description}\"");
            Console.WriteLine();
            Console.WriteLine($"Availability:  {AvailabilityDesc}");
            Console.WriteLine();
            Console.WriteLine($"Recommended for ages {Youngest} to {Oldest}");
            Console.WriteLine(line);
            Console.WriteLine();
        }

        public void AddVenue()
        {
            // input, parse, save?
        }

        //TODO: Add better input cleaning/verification for modification
        public void ModifyVenueProp(Venue obj, string propName)
        {
            string printPropName = null;

            switch (propName)
            {
                case "AvailabilityDesc":
                    printPropName = "Availability Description";
                    break;
                case "Youngest":
                    printPropName = "Youngest Age";
                    break;
                case "Oldest":
                    printPropName = "Oldest Age";
                    break;
                default:
                    printPropName = propName;
                    break;
            }

            Console.Write($"Enter a new {printPropName} (or hit enter to continue):  ");
            string newProp = Console.ReadLine();
            var oldProp = obj.GetType().GetProperty(propName).GetValue(obj);
            // Console.WriteLine($"\nOld {printPropName}: {oldProp}");

            if (newProp != null && obj.GetType().GetProperty(propName).GetValue(obj) != null)
            {
                propName = newProp;
                var newPropVal = obj.GetType().GetProperty(propName).GetValue(obj);
                Console.WriteLine($"\nOld {printPropName}: {oldProp}\n\tNew value: {newPropVal}\n");
        
            }
            else
            {
                Console.WriteLine($"\nOld {printPropName}: {oldProp}\tNothing changed.");
            }
        }

        

    }
}
