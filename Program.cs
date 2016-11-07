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
            cities[2] = new City("Biržai");


            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L4Data_*.csv");
            //Console.WriteLine(string.Join("\n", filePaths));
            foreach (string path in filePaths)
            {
                ReadCityData(cities, path);
            }

            PrintMuseumsToConsole(cities);
            PrintMonumentsToConsole(cities);
            PrintAllGuidesToConsole(cities);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Oldest place:");
            Console.ForegroundColor = ConsoleColor.Gray;
            PrintPlace((FindOldestPlace(cities)));

            Console.ReadLine();

        }
        public static void ReadCityData(City[] cities, string fName)
        {
            //City city = new City();
            using (StreamReader reader = new StreamReader(fName))
            {
                int cityIndex = GetCityIndex(cities, reader.ReadLine());
                cities[cityIndex].ResponsiblePerson = reader.ReadLine();

                string line = null;
                //Mo = Monument
                //Mu = Museum

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
                            {
                                cities[cityIndex].Monuments.AddPlace(monument);
                            }
                            break;
                        case Museum.Id:
                            string type = data[count++];
                            bool[] worksOn = new bool[7];
                            for (int i = 0; i < 7; i++)
                                worksOn[i] = bool.Parse(data[count++]);
                            double ticketPrice = double.Parse(data[count++]);
                            bool hasGuide = bool.Parse(data[count++]);
                            Museum museum = new Museum(name, adress, year, type, worksOn, ticketPrice, hasGuide);
                            if (!cities[cityIndex].Museums.Contains(museum))
                            {
                                cities[cityIndex].Museums.AddPlace(museum);
                            }
                            break;
                    }
                }
            }
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

        public static void PrintPlace(PlaceOfInterest place)
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

        public static void PrintAllGuidesToConsole(City[] cities)
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

        public static PlaceOfInterest FindOldestPlace(City[] cities)
        {

            PlaceOfInterestContainer allPlaces = new PlaceOfInterestContainer(MaxPlaces);

            foreach (City city in cities)
            {
                for (int i = 0; i < city.Museums.Count; i++)
                    allPlaces.AddPlace(city.Museums.GetPlace(i));
                for (int i = 0; i < city.Monuments.Count; i++)
                    allPlaces.AddPlace(city.Monuments.GetPlace(i));
            }

            int minIndex = 0;
            int minValue = 3000;

            for (int i = 0; i < allPlaces.Count; i++)
            {
                if (allPlaces.GetPlace(i).Year < minValue)
                {
                    minValue = allPlaces.GetPlace(i).Year;
                    minIndex = i;
                }
            }
            return allPlaces.GetPlace(minIndex);
        }
        /*
         * Rikiavimas
void Išrinkimas {
    int nuo, t;
    for(int i = 0; i < N - 1; i++) {
        nuo = i;
        for(int j = i+1; j < N; j++)
            if (a[j] < a[nuo]) nuo = j;
        t = a[nuo];
        a[nuo] = a[i];
        a[i] = t;
    }
}
        */
    }
}
