using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Linq.Expressions;

namespace Cultural_Pass_Usage
{

    public class RootObject
    {
        public Venue[] Venue { get; set; }
    }

    public class Venue : IEquatable<Venue>, IComparable<Venue>, IQueryable<Venue>
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

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();


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

        public int SortByNameAscending(string name1, string name2)
        {
            return name1.CompareTo(name2);
        }

        public int CompareTo(Venue compareVenue)
        {
            if (compareVenue == null)
                return 1;

            else
                return this.ID.CompareTo(compareVenue.ID);
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

        public void AddVenue(Venue inputObject)
        {
            Venue obj = new Venue();
            Type t = obj.GetType();
            Type y = inputObject.GetType();
            PropertyInfo[] props = t.GetProperties();

            //Console.WriteLine("Properties (N = {0}):", props.Length);
            foreach (var prop in props)
            {
                var oldValue = prop.GetValue(obj);
                prop.SetValue(obj, y.GetProperty(prop.Name).GetValue(inputObject));
                var newValue = prop.GetValue(obj);
                // Console.WriteLine($"{prop.Name} was {oldValue} and now is {newValue}");
            }
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

        public IEnumerator<Venue> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
