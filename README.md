# Cultural Pass Usage

A C# console app designed to allow users to view information about the 2019 [Cultural Pass](https://fundforthearts.org/culturalpass/) venues, including statistics about the number of visitors from the [Jeffersonville Township Public Library](https://jefflibrary.org). Users will have the option to add or remove Cultural Pass numbers, edit information about Venues, and create individual Venue CSV files needed to upload into [Beanstack](https://beanstack.com) in order to give Readers their badge credit for visiting the venue.  

## File Input

The app will use/require 3 files:

* CSV listing all Cultural Pass numbers in Beanstack (from Beanstack's `Program Reports: Readers (All Registered Readers)` report for the 2019 Cultural Pass program)
* JSON file listing all Venues, including Badge ID number
* CSV of all Cultural Pass Numbers logged at each Venue
