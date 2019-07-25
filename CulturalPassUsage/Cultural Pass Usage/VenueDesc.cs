using System;
using System.Collections.Generic;
using System.Text;

namespace Cultural_Pass_Usage
{
    public class VenueDesc
    {
        public bool success = false;
        public int i = 1;

        public int Parsed(string input)
        {
            if (Int32.TryParse(input, out int number))
            {
                return number;
            }
            return -1;
        }

        public VenueDesc(string input, int limit, Venue[] venueArray)
        {  

            do
            {
                var parsed = Parsed(input);
                if (parsed > 0 && parsed <= limit)
                {
                    // keep getting out of bounds exception, not sure why...
                    venueArray[parsed].WriteDesc();
                    success = true;
                }
                else
                {
                    Console.Write($"Attempt #{i}: That isn't a valid selection. Please try again or type \"x\" to exit:  ");
                    success = false;
                    input = Console.ReadLine();
                    i++;
                    if (input.ToLowerInvariant() == "x")
                    {
                        Environment.Exit(0);
                    }
                    continue;
                }

            }
            while (success == false);

        }
    }
}
