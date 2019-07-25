using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    class OldProgram
    {
        public static bool success = false;
        public static readonly string line = "------------------------------------------------------------------------------------------";


        static void OldMain(string[] args)
        {
            // PrintTitle();
            Title title = new Title();
            title.PrintTitle();

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            var fileName = Path.Combine(directory.FullName, "CulturalPassVenues2019.json");

            var venues = DeserializeVenues(fileName);
            int venueCount = venues.Count;
            int counter = 1;


            PrintBoxTop();
            Console.WriteLine("|  There are *" + venueCount + "* venues participating this year:                                            |");
            PrintBoxBottom();
            Console.WriteLine();

            // create array of venues with each venue at the position of counter-1
            Venue[] venueByID = new Venue[venueCount];

            foreach (var venue in venues)
            {
                Console.WriteLine(counter + ". " + venue.Name);
                int cID = counter - 1;
                venueByID[cID] = venue;
                counter++;
            }

            Console.WriteLine();
            Console.Write("Enter the list number of the venue you would like to see:  ");
            string input = Console.ReadLine();


            //while (selection.success)
            //{
            //    Console.Write("Enter the number of another venue you'd like to see, or type \"x\" to quit: ");
            //    string input2 = Console.ReadLine();

            //    if (input2.ToLowerInvariant() == "x")
            //    {
            //        Environment.Exit(0);
            //    }
            //    else
            //    {
            //        VenueDesc selection2 = new VenueDesc(input2, venueCount, venueByID);
            //        continue;
            //    }
            //}



            Console.WriteLine();

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

        //public static int Parsed(string input)
        //{
        //    if (Int32.TryParse(input, out int number))
        //    {
        //        return number;
        //    }
        //    return -1;
        //}

        //public static void GetDesc (string input, int limit, Venue[] venueArray)
        //{
        //    int i = 1;
        //    do
        //    {
        //        var parsed = Parsed(input);
        //        if (parsed > 0 && parsed <= limit)
        //        {
        //            // keep getting out of bounds exception, not sure why...
        //            venueArray[parsed].WriteDesc();
        //            success = true;
        //        }
        //        else
        //        {
        //            Console.Write($"Attempt #{i}: That isn't a valid selection. Please try again or type \"x\" to exit:  ");
        //            success = false;
        //            input = Console.ReadLine();
        //            i++;
        //            if (input.ToLowerInvariant() == "x")
        //            {
        //                Environment.Exit(0);
        //            }
        //            continue;
        //        }

        //    }
        //    while (success == false);
        //}

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
