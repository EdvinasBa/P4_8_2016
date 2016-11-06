using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4___Rework
{
    class City
    {
        public string CityName { get; set; }
        public PlaceOfInterestContainer Monuments { get; set; }
        public PlaceOfInterestContainer Museums { get; set; };

        public City(string cityName)
        {
            CityName = cityName;
            PlaceOfInterestContainer Monuments = new PlaceOfInterestContainer(MaxPlaces);
            PlaceOfInterestContainer Museums = new PlaceOfInterestContainer(MaxPlaces);
        }
    }
}
