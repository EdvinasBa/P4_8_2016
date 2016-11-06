﻿using System;
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
        public const int MaxCities = 2;

        static void Main(string[] args)
        {

            City[] cities = new City[MaxCities];

            cities[0] = new City("Kaunas");
            cities[1] = new City("Vilnius");
          //  cities[2] = new City("Biržai");


            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "L4Data_*.csv");
            //Console.WriteLine(string.Join("\n", filePaths));
            foreach (string path in filePaths)
            {
                ReadCityData(cities, path);
            }

            for (int i = 0; i < cities.Length; i++)
                for (int j = 0; j < cities[i].Museums.Count; j++)
                {
                    Console.WriteLine(cities[i].Museums.GetPlace(j).ToString());
                }
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
                    return count-1;
            }
            return -1;
        }
    }
}
