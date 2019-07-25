﻿using System;
using System.Collections.Generic;
using System.IO;
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
            Venue result = venues.Find(
                delegate (Venue ex)
                {
                    return ex.ID == parsedInput;
                }
                );

            var outputFileName = Path.Combine(directory.FullName, "UpdatedCulturalPassVenues2019.json");
            if (result != null)
            {
                DisplayResult(result);

                // get user input to determine next action
                Console.WriteLine("Would you like to: \n\t1. Modify this information \n\t2. Exit\n");
                string proceed = Console.ReadLine();
                if (Parsed(proceed) == 2)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("This part is coming next...");
                }
            }
            else
            {
                Console.WriteLine($"\nID# {parsedInput} not found");
            }

            Console.ReadLine();
        }

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
