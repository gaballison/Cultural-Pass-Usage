using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Cultural_Pass_Usage
{
    class Program
    {
        public readonly string _line = "------------------------------------------------------------------------------------------";

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
            string[] venueByID = new string[venueCount];

            foreach (var venue in venues)
            {
                Console.WriteLine(counter + ". " + venue.Name);
                int cID = counter - 1;
                venueByID[cID] = venue.Name;
                counter++;
            }

            Console.WriteLine();
            Console.Write("Enter the list number of the venue you would like to see:  ");
            string input = Console.ReadLine();

            int result = 0;

            if (Int32.TryParse(input, out int numValue))
            {
                result = numValue;
            }
            else
            {
                Console.WriteLine($"You entered '{input}', which isn't a number. Try again: ");
                string input2 = Console.ReadLine();
            }

            Console.WriteLine();

            for (var i = 0; i < venueCount; i++)
            {
                if ((result - 1) == i)
                {
                    Console.WriteLine("");
                    Console.WriteLine(venueByID[i]);
                    Console.WriteLine("Description:  [how do you refer to a property from an object in a list??]");
                }
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
