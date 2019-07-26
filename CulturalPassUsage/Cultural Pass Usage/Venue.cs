using System;
using System.Collections.Generic;
using Newtonsoft.Json;



namespace Cultural_Pass_Usage
{

    public class RootObject
    {
        public Venue[] Venue { get; set; }
    }

    public class Venue : IEquatable<Venue>
    {
        //---------------------------------------------
        // Deserializing the JSON data into Properties
        //---------------------------------------------
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

        [JsonProperty(PropertyName = "recommended_age_youngest")]
        public int Youngest { get; set; }

        [JsonProperty(PropertyName = "recommended_age_oldest")]
        public int Oldest { get; set; }


        //-----------------------------------------------------------------------------------------
        // Equals and GetHashCode methods are required for implementing IEquatable:
        // https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1?view=netframework-4.8 
        //-----------------------------------------------------------------------------------------
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
        }


        // output the Venue properties in a profile format
        public void WriteDesc()
        {
            string line = "------------------------------------------------------------------------------------------";

            Console.WriteLine("");
            Console.WriteLine(line);
            Console.WriteLine($"#{ID}. {Name.ToUpperInvariant()} \n");
            Console.WriteLine($"Category: {Category} \n");
            Console.WriteLine($"\"{Description}\" \n");
            Console.WriteLine($"Availability:  {AvailabilityDesc} \n");
            Console.WriteLine($"Recommended for ages {Youngest} to {Oldest} \n");
            Console.WriteLine(line);
            Console.WriteLine();
        }

        

    }
}
