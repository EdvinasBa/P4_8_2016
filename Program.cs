using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace U4___Rework
{
    class Program
    {
        public const int MaxPlaces = 100;
        public const int MaxCities = 3;

        static void Main(string[] args)
        {
            City[] cities = new City[MaxCities];

            cities[0] = new City("Kaunas");
            cities[1] = new City("Vilnius");
            cities[2] = new City("Panevezys");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L4Data_*.csv");
            foreach (string path in filePaths)
                ReadCityData(cities, path);

            DataTableToFile(cities, "table.txt");

            PrintGuideCountsToConsole(cities);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Oldest place:");
            Console.ForegroundColor = ConsoleColor.Gray;

            PrintPlaceToConsole((FindOldestPlace(cities)));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Sorted by year and by name:");
            Console.ForegroundColor = ConsoleColor.Gray;

            PlaceOfInterestContainer sortedPlaces = SortCityPlaces(cities);

            PrintAllPlacesToConsole(sortedPlaces);
            PlaceNamesToFile(sortedPlaces, "VisosVietos.csv");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Places after 1990:");
            Console.ForegroundColor = ConsoleColor.Gray;

            PlaceOfInterestContainer afterYear = PlacesAfterCertainYear(cities,1990);
            PrintAllPlacesToConsole(afterYear);
            PlacesToFile(afterYear, "Po1990.csv");

            Console.ReadLine();

        }
        public static void ReadCityData(City[] cities, string fName)
        {
            using (StreamReader reader = new StreamReader(fName))
            {
                int cityIndex = GetCityIndex(cities, reader.ReadLine());
                cities[cityIndex].ResponsiblePerson = reader.ReadLine();
                string line = null;

                while (null != (line = reader.ReadLine()))
                {
                    string[] data = line.Split(',');
                    int count = 1;
                    string name = data[count++];
                    string adress = data[count++];
                    int year = int.Parse(data[count++]);

                    switch (data[0].ToLower())
                    {
                        case Monument.Id:
                            string author = data[count++];
                            string intendedFor = data[count++];
                            Monument monument = new Monument(name, adress, year, author, intendedFor);
                            if (!cities[cityIndex].Monuments.Contains(monument))
                                cities[cityIndex].Monuments.AddPlace(monument);
                            break;
                        case Museum.Id:
                            string type = data[count++];
                            bool[] worksOn = new bool[7];
                            for (int i = 0; i < 7; i++)
                                worksOn[i] = bool.Parse(data[count++]);
                            data[count] = data[count].Replace('.', ',');
                            double ticketPrice = double.Parse(data[count++]);
                            bool hasGuide = bool.Parse(data[count++]);
                            Museum museum = new Museum(name, adress, year, type, worksOn, ticketPrice, hasGuide);
                            if (!cities[cityIndex].Museums.Contains(museum))
                                cities[cityIndex].Museums.AddPlace(museum);
                            break;
                    }
                }
            }
        }

        public static void PlaceNamesToFile(PlaceOfInterestContainer places, string fName)
        {
            using (StreamWriter writer = new StreamWriter(fName))
            {
                for (int i = 0; i < places.Count; i++)
                    writer.WriteLine(places.GetPlace(i).Name);
            }
        }

        public static void PlacesToFile(PlaceOfInterestContainer places, string fName)
        {
            using (StreamWriter writer = new StreamWriter(fName))
            {
                for (int i = 0; i < places.Count; i++)
                    writer.WriteLine(places.GetPlace(i).ToCsv());
            }
        }

        public static void DataTableToFile(City[] cities, string fName)
        {
            using (StreamWriter writer = new StreamWriter(@fName))
            {
                foreach(City city in cities)
                {
                    writer.WriteLine(city.CityName);
                    if (city.Museums.Count > 0)
                    {
                        writer.WriteLine("Museums: ");
                        for (int i = 0; i < city.Museums.Count; i++)
                            writer.WriteLine(city.Museums.GetPlace(i).ToString());
                        writer.WriteLine(new string('-', city.Museums.GetPlace(0).ToString().Length));
                    }
                    if (city.Monuments.Count > 0)
                    {
                        writer.WriteLine("Monuments: ");
                        for (int i = 0; i < city.Monuments.Count; i++)
                            writer.WriteLine(city.Monuments.GetPlace(i).ToString());
                        writer.WriteLine(new string('-', city.Monuments.GetPlace(0).ToString().Length));
                    }
                    writer.WriteLine();
                }
            } 
        }

        //Printing to console begin

        public static void PrintMuseumsToConsole(City[] cities)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Museums:");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < cities.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(cities[i].CityName);
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < cities[i].Museums.Count; j++)
                {
                    Console.WriteLine(cities[i].Museums.GetPlace(j).ToString().Replace(',', '|'));
                }
            }
            Console.WriteLine();
        }

        public static void PrintMonumentsToConsole(City[] cities)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Monuments:");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < cities.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(cities[i].CityName);
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int j = 0; j < cities[i].Monuments.Count; j++)
                {
                    Console.WriteLine(cities[i].Monuments.GetPlace(j).ToString().Replace(',', '|'));
                }
            }
            Console.WriteLine();
        }

        public static void PrintPlaceToConsole(PlaceOfInterest place)
        {
            if (place is Museum)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Museum:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(place.ToString());
            }
            if (place is Monument)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Monument:");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(place.ToString());
            }
        }

        public static void PrintAllPlacesToConsole(PlaceOfInterestContainer place)
        {
            for (int i = 0; i < place.Count; i++)
                Console.WriteLine(place.GetPlace(i).ToString());
        }

        public static void PrintGuideCountsToConsole(City[] cities)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Guide count:");
            Console.ForegroundColor = ConsoleColor.Gray;
            int total = 0;
            foreach (City city in cities)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("{0,-10} : ", city.CityName);
                Console.ForegroundColor = ConsoleColor.Gray;
                int count = CountGuides(city);
                Console.WriteLine(count);
                total += count;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            string totalText = "Total";
            Console.Write("{0,-10} : ", totalText);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(total);
            Console.WriteLine();
        }

        //Printing to console end

        public static void JoinPlaces(PlaceOfInterestContainer places1, PlaceOfInterestContainer places2, PlaceOfInterestContainer newPlace)
        {
            for (int i = 0; i < places1.Count; i++)
                newPlace.AddPlace(places1.GetPlace(i));
            for (int i = 0; i < places2.Count; i++)
                newPlace.AddPlace(places2.GetPlace(i));
        }

        public static PlaceOfInterestContainer JoinCitiesPlaces(City[] cities)
        {
            PlaceOfInterestContainer allPlaces = new PlaceOfInterestContainer(MaxPlaces);

            foreach (City city in cities)
                JoinPlaces(city.Monuments, city.Museums, allPlaces);

            return allPlaces;
        }

        public static int CountGuides(City city)
        {
            int guides = 0;
            for (int i = 0; i < city.Museums.Count; i++)
            {
                Museum museum = city.Museums.GetPlace(i) as Museum;
                if (museum.HasGuide)
                {
                    guides++;
                }
            }
            return guides;
        }

        public static int GetCityIndex(City[] cities, string cityName)
        {
            int count = 0;
            foreach (City city in cities)
            {
                count++;
                if (city.CityName == cityName)
                    return count - 1;
            }
            return -1;
        }

        public static PlaceOfInterest FindOldestPlace(City[] cities)
        {
            PlaceOfInterestContainer allPlaces = new PlaceOfInterestContainer(MaxPlaces);

            foreach (City city in cities)
                JoinPlaces(city.Monuments, city.Museums, allPlaces);

            int minIndex = 0;
            int minValue = 3000;

            for (int i = 0; i < allPlaces.Count; i++)
                if (allPlaces.GetPlace(i).Year < minValue)
                {
                    minValue = allPlaces.GetPlace(i).Year;
                    minIndex = i;
                }
            return allPlaces.GetPlace(minIndex);
        }

        public static PlaceOfInterestContainer SortCityPlaces(City[] cities)
        {
            PlaceOfInterestContainer allPlaces = JoinCitiesPlaces(cities);

            int minPos = 0;
            for (int i = 0; i < allPlaces.Count; i++)
            {
                minPos = i;

                for (int j = i + 1; j < allPlaces.Count; j++)
                {
                    if (allPlaces.GetPlace(j).Year < allPlaces.GetPlace(minPos).Year)
                        minPos = j;
                }

                if (minPos != i)
                {
                    PlaceOfInterest temp = allPlaces.GetPlace(i);
                    allPlaces.SetPlace(allPlaces.GetPlace(minPos), i);
                    allPlaces.SetPlace(temp, minPos);
                }
                else
                {
                        for (int j = i + 1; j < allPlaces.Count; j++)
                        {
                            //CompareTo returns -1 if first string is "smaller"
                            if (allPlaces.GetPlace(i).Name.CompareTo(allPlaces.GetPlace(minPos).Name) < 0)
                                minPos = j;
                        }
                        if (minPos != i)
                        {
                            PlaceOfInterest temp = allPlaces.GetPlace(i);
                            allPlaces.SetPlace(allPlaces.GetPlace(minPos), i);
                            allPlaces.SetPlace(temp, minPos);
                        }
                }
            }
            return allPlaces;
        }
        
        public static PlaceOfInterestContainer PlacesAfterCertainYear(City[] cities, int minYear)
        {
            PlaceOfInterestContainer all = new PlaceOfInterestContainer(MaxPlaces);

            all = JoinCitiesPlaces(cities);

            PlaceOfInterestContainer filtered = new PlaceOfInterestContainer(all.Count);

            for (int i = 0; i < all.Count; i++)
                if (all.GetPlace(i).Year > minYear)
                    filtered.AddPlace(all.GetPlace(i));

            return filtered;
        }
        
    }
}