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
                        case "mo":
                            string author = data[count++];
                            string intendedFor = data[count++];
                            Monument monument = new Monument(name, adress, year, author, intendedFor);
                            if (!cities[cityIndex].Monuments.Contains(monument))
                            {
                                cities[cityIndex].Monuments.AddPlace(monument);
                            }
                            break;
                        case "mu":
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
            foreach(City city in cities)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("{0,-10} : ",city.CityName);
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
        }
    }
}
