using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    class Program
    {
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

            // deserialize data from JSON file into array of Venue objects
            var venues = DeserializeVenues(fileName);

            // figure out how many venues are in the list & create a counter to make a numbered list for menus
            int venueCount = venues.Count;

            // printing out prettyfied info on how many Venues are in the list
            PrintBoxTop();
            Console.WriteLine("|  There are *" + venueCount + "* venues participating this year:                                            |");
            PrintBoxBottom();
            Console.WriteLine();

            // generate a numbered list of Venue objects, ordered by Venue ID
            // and save the user's selection in a variable
            string input = MainMenu(venues);
            int parsedInput = Parsed(input);

            // using Find method to search for input match in the List of Venues
            Venue result = FindByID(venues, parsedInput);

            if (result != null)
            {
                // use the WriteDesc() method to generate a profile of the user's selected Venue
                result.WriteDesc();

                // get user input to determine next action
                Console.WriteLine("Would you like to: \n\t1. Remove this venue \n\t2. Exit");
                string proceed = Console.ReadLine();

                //------------------------------------------
                // BEGIN INTERACTING WITH DATA
                // currently only have option to remove
                // but options to add and modify are coming
                //------------------------------------------
                if (Parsed(proceed) == 1)
                {
                    // confirm they really do want to delete the item, then delete and save or quit
                    Delete(venues, result, parsedInput);
                }
                else
                {
                    Environment.Exit(0);
                }

                Save(venues, fileName);
                
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

        public static string MainMenu(List<Venue> venues)
        {
            // create a numerical list of venues, not necessarily always matching with ID
            foreach (var venue in venues)
            {
                Console.WriteLine($"{venue.ID}. {venue.Name}");
            }

            // get and return user's selection
            Console.WriteLine();
            Console.Write("Enter the list number of the venue you would like to see:  ");
            string input = Console.ReadLine();

            return input;
        }

        public static void Save(List<Venue> venues, string fileName)
        {
            Console.Write("Save your changes?   Y/N: \t");
            string saveChoice = Console.ReadLine();

            // if they choose to save, serialize and then quit
            if (saveChoice.ToUpperInvariant() == "Y")
            {
                SerializeVenueToFile(venues, fileName);
                Console.WriteLine("\nYour changes have been successfully recorded. Goodbye!");
                Environment.Exit(0);
            }
            // otherwise just quit
            else
            {
                Console.WriteLine("\nNo changes have been made. \nThanks for using the program. Goodbye!");
                Environment.Exit(0);
            }
        }
        public static void Delete(List<Venue> venues, Venue result, int parsedInput)
        {
            // before deletion, confirm they actually WANT to delete
            Console.Write($"You want to delete {result.Name}; are you sure? Y/N \t");
            string deleteConf = Console.ReadLine();

            
            if (deleteConf.ToUpperInvariant() == "Y")
            {
                // if they confirm deletion, remove object with the maching ID and Name properties
                venues.Remove(new Venue() { ID = result.ID, Name = result.Name });

                // double check the object was actually deleted by searching for it by ID again
                Venue newResult = FindByID(venues, parsedInput);

                if (newResult != null)
                {
                    Console.WriteLine("We still found that object, that's not good.");
                }
                else
                {
                    Console.WriteLine("{0} was successfully deleted!\n", result.Name);
                }

            }
        }
        public static Venue FindByID(List<Venue> venues, int parsedInput)
        {
            // find and return the first object whose ID matches the user's selection
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
            // check and make sure user's input is actually a positive integer
            if (Int32.TryParse(input, out int number))
            {
                return number;
            }
            return -1;
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
