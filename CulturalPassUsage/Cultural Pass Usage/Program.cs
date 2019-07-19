using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    class Program
    {
        public static bool success = false;
        public static readonly string line = "------------------------------------------------------------------------------------------";


        static void Main(string[] args)
        {
            PrintTitle();

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
            int i = 1;

            do
            {
                var parsed = Parsed(input);
                if (parsed > 0 && parsed <= venueCount)
                {
                    // make method to write out all of the venue stuff
                    venueByID[parsed].WriteDesc();
                    //Console.WriteLine($"Attempt #{i}: Huzzah, we're good! ");
                    success = true;
                }
                else
                {
                    Console.Write($"Attempt #{i}: That isn't a valid selection. Please try again or type \"x\" to exit:  ");
                    success = false;
                    input = Console.ReadLine();
                    i++;
                    if (input == "x")
                    {
                        Environment.Exit(0);
                    }
                    continue;
                }

            }
            while (success == false);


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

        public static int Parsed(string input)
        {
            if (Int32.TryParse(input, out int number))
            {
                return number;
            }
            return -1;
        }

        public static int GetInt(string input, int limit)
        {
            bool success = Int32.TryParse(input, out int number);

            if (success && number <= limit && number > 0)
            {
                return number;
            }
            else if (success && ( (number > limit) || (number < 0) ) )
            {
                Console.WriteLine("That number is not a valid selection. Try again: ");
            }
            else
            {
                Console.WriteLine("That isn't even a number. Try again: ");
            }

            return 0;
        }




        public static void PrintTitle()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" ___________________________________________________________________________________________");
            Console.WriteLine("|                                                                                           |");
            Console.WriteLine("|   ::::::::  :::    ::: :::    ::::::::::: :::    ::: :::::::::      :::     :::           |");
            Console.WriteLine("|  :+:    :+: :+:    :+: :+:        :+:     :+:    :+: :+:    :+:   :+: :+:   :+:           |");
            Console.WriteLine("|  +:+        +:+    +:+ +:+        +:+     +:+    +:+ +:+    +:+  +:+   +:+  +:+           |");
            Console.WriteLine("|  +#+        +#+    +:+ +#+        +#+     +#+    +:+ +#++:++#:  +#++:++#++: +#+           |");
            Console.WriteLine("|  +#+        +#+    +#+ +#+        +#+     +#+    +#+ +#+    +#+ +#+     +#+ +#+           |");
            Console.WriteLine("|  #+#    #+# #+#    #+# #+#        #+#     #+#    #+# #+#    #+# #+#     #+# #+#           |");
            Console.WriteLine("|   ########   ########  ########## ###      ########  ###    ### ###     ### ##########    |");
            Console.WriteLine("|                                                                                           |");
            Console.WriteLine("|  :::::::::     :::      ::::::::   ::::::::        ::::::::   :::::::    :::   ::::::::   |");
            Console.WriteLine("|  :+:    :+:  :+: :+:   :+:    :+: :+:    :+:      :+:    :+: :+:   :+: :+:+:  :+:    :+:  |");
            Console.WriteLine("|  +:+    +:+ +:+   +:+  +:+        +:+                   +:+  +:+  :+:+   +:+  +:+    +:+  |");
            Console.WriteLine("|  +#++:++#+ +#++:++#++: +#++:++#++ +#++:++#++          +#+    +#+ + +:+   +#+   +#++:++#+  |");
            Console.WriteLine("|  +#+       +#+     +#+        +#+        +#+        +#+      +#+#  +#+   +#+         +#+  |");
            Console.WriteLine("|  #+#       #+#     #+# #+#    #+# #+#    #+#       #+#       #+#   #+#   #+#  #+#    #+#  |");
            Console.WriteLine("|  ###       ###     ###  ########   ########       ##########  #######  ####### ########   |");
            Console.WriteLine("|___________________________________________________________________________________________|");
            Console.WriteLine();
            Console.WriteLine();
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
