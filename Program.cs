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
            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(),"L4Data_*.csv");

            City[] city = new City[MaxCities];

            foreach (string path in filePaths)
            {
                ReadCityData(path);
            }
        }
        public static City ReadCityData(string fName)
        {
            City city = new City();
            using (StreamReader reader = new StreamReader(fName))
            {
                city.CityName = reader.ReadLine();
                city.ResponsiblePerson = reader.ReadLine();

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

                    switch (data[0])
                    {
                        case "Mo":
                            string author = data[count++];
                            string intendedFor = data[count++];
                            Monument monument = new Monument(name, adress, year, author, intendedFor);
                            if (!city.Monuments.Contains(monument))
                            {
                                city.Monuments.AddPlace(monument);
                            }
                            break;
                        case "Mu":
                            string type = data[count++];
                            bool[] worksOn = new bool[6];
                            for (int i = 0; i < 7; i++)
                                worksOn[i] = bool.Parse(data[count++]);
                            double ticketPrice = double.Parse(data[count++]);
                            bool hasGuide = bool.Parse(data[count++]);
                            Museum museum = new Museum(name, adress, year, type, worksOn, ticketPrice, hasGuide);
                            if (!city.Museums.Contains(museum))
                            {
                                city.Museums.AddPlace(museum);
                            }
                            break;
                    }
                }
            }
            return city;
        }
    }
}
