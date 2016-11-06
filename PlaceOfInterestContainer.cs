using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U4___Rework
{
    class PlaceOfInterestContainer
    {
        public PlaceOfInterest[] Places { get; set; }
        public int Count { get; protected set; }

        public PlaceOfInterestContainer (int max)
        {
            Places = new PlaceOfInterest[max];
        }

        void AddPlace(PlaceOfInterest place)
        {
            Places[Count++] = place;
        }

        PlaceOfInterest GetPlace(int index)
        {
            return Places[index];
        }

        public bool Contains(PlaceOfInterest place)
        {
            return Places.Contains(place);
        }
    }
}
