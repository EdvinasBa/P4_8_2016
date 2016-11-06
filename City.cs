namespace U4___Rework
{
    class City
    {
        public string CityName { get; set; }
        public string ResponsiblePerson { get; set; }
        public PlaceOfInterestContainer Monuments { get; set; }
        public PlaceOfInterestContainer Museums { get; set; }

        public City() { }

        public City(string cityName)
        {
            CityName = cityName;
            Monuments = new PlaceOfInterestContainer(Program.MaxPlaces);
            Museums = new PlaceOfInterestContainer(Program.MaxPlaces);
        }

        public City(string cityName, string responsiblePerson)
        {
            CityName = cityName;
            ResponsiblePerson = responsiblePerson;
            Monuments = new PlaceOfInterestContainer(Program.MaxPlaces);
            Museums = new PlaceOfInterestContainer(Program.MaxPlaces);
        }


    }
}
