using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    class Program
    {
        //public static bool success = false;
        public static readonly string line = "------------------------------------------------------------------------------------------";


        static void Main(string[] args)
        {
            // print the prettyfied title
            Title title = new Title();
            title.PrintTitle();

            // create variable for current directory + input file path
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            var fileName = Path.Combine(directory.FullName, "CulturalPassVenues2019.json");

            // Deserialize data from JSON file into array of objects
            var venues = DeserializeVenues(fileName);

            int venueCount = venues.Count;
            int counter = 1;

            // printing out general info
            PrintBoxTop();
            Console.WriteLine("|  There are *" + venueCount + "* venues participating this year:                                            |");
            PrintBoxBottom();
            Console.WriteLine();

            // create a numerical list of venues -- might be able to do with just using ID?
            foreach (var venue in venues)
            {
                Console.WriteLine(counter + ". " + venue.Name);
                counter++;
            }

            Console.WriteLine();
            Console.Write("Enter the list number of the venue you would like to see:  ");
            string input = Console.ReadLine();
            int parsedInput = Parsed(input);

            // using Find method to search for input match in the List of Venues
            Venue result = FindByID(venues, parsedInput);


            var outputFileName = Path.Combine(directory.FullName, "UpdatedCulturalPassVenues2019.json");
            if (result != null)
            {
                DisplayResult(result);

                // get user input to determine next action
                Console.WriteLine("Would you like to: \n\t1. Modify this venue \n\t2. Remove this venue \n\t3. Add another venue\n\t4. <ENTER> (or any other key) Exit\n");
                string proceed = Console.ReadLine();
                if (Parsed(proceed) == 1)
                {
                    Console.WriteLine($"You want to MODIFY {result.Name}");
                    // methods to modify venue
                    // create array of properties, loop through them and call method on each
                    // it would be easier to just delete things
                }
                else if (Parsed(proceed) == 2)
                {
                    // create method to delete venue
                    Console.Write($"You want to delete {result.Name}; are you sure? Y/N \t");
                    string deleteConf = Console.ReadLine();
                    if(deleteConf.ToUpperInvariant() == "Y")
                    {
                        venues.Remove(new Venue() { ID = result.ID, Name = result.Name }) ;

                        Venue newResult = FindByID(venues, parsedInput);

                        if(newResult != null)
                        {
                            Console.WriteLine("We still found that object, that's not good.");
                        }
                        else
                        {
                            Console.WriteLine("SUCCESS! Object deleted. Venues now contains: ");
                            foreach(Venue venue in venues)
                            {
                                Console.WriteLine("ID: {0} \tName: {1}", venue.ID, venue.Name);
                            }
                        }
                        
                    }
                }
                else if (Parsed(proceed) == 3)
                {
                    // create method to add venue
                    Console.WriteLine("You want to ADD a new venue, how ambitious!");
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine($"\nID# {parsedInput} not found");
            }

            Console.ReadLine();
        }
        // END OF MAIN PROGRAM

        public static List<Venue> DeserializeVenues(string fileName)
        {
            var venues = new List<Venue>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                venues = serializer.Deserialize<List<Venue>>(jsonReader);
            }
            return venues;
        }

        public static void SerializeVenueToFile(List<Venue> venues, string fileName)
        {
            var serializer = new JsonSerializer();
            using (var writer = new StreamWriter(fileName))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jsonWriter, venues);
            }
        }

        public static Venue FindByID(List<Venue> venues, int parsedInput)
        {
            Venue result = venues.Find(
                delegate (Venue ex)
                {
                    return ex.ID == parsedInput;
                }
                );

            return result;
        }

        public static int Parsed(string input)
        {
            if (Int32.TryParse(input, out int number))
            {
                return number;
            }
            return -1;
        }

        private static void DisplayResult(Venue result)
        {
            Console.WriteLine();
            result.WriteDesc();
        }

        //TODO: Add better input cleaning/verification for modification
        public static void ModifyVenueProp(Venue inputObject, string propName)
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
            var oldProp = t.GetProperty(propName).GetValue(obj);
            //ConsoleKeyInfo cki;
            ConsoleKeyInfo temp = Console.ReadKey(true);
            Console.TreatControlCAsInput = false;
            if (temp.Key == ConsoleKey.Enter)
            {
                Console.WriteLine($"\nOld {printPropName}: {oldProp}\tNothing changed.");
            }
            else
            {
                t.GetProperty(propName).SetValue(obj, newProp);
                var newPropVal = t.GetProperty(propName).GetValue(obj);
                Console.WriteLine($"\nOld {printPropName}: {oldProp}\n\tNew value: {newPropVal}\n");
            }

        }

        public static void PrintBoxTop()
        {
            Console.WriteLine(" ___________________________________________________________________________________________");
            Console.WriteLine("|                                                                                           |");
        }

        public static void PrintBoxBottom()
        {
            Console.WriteLine("|___________________________________________________________________________________________|");
        }

        
    }
}
