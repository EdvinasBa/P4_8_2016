namespace U4___Rework
{
    class PlaceOfInterest
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Year { get; set; }
        //public string Id { get; set; }

        public PlaceOfInterest() { }

        public PlaceOfInterest(string name, string adress, int year)
        {
            Name = name;
            Adress = adress;
            Year = year;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Adress.GetHashCode() ^ Year.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PlaceOfInterest);
        }

        public bool Equals(PlaceOfInterest place)
        {
            if (ReferenceEquals(place, null))
                return false;
            if (this.GetType() != place.GetType())
                return false;
            return (Name == place.Name) && (Adress == place.Adress) && (Year == place.Year);
        }

        public static bool operator ==(PlaceOfInterest lhs, PlaceOfInterest rhs)
        {
            //Check if one of the sides is not null
            if (ReferenceEquals(lhs, null))
            {
                if (ReferenceEquals(rhs, null))
                    return false;
                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(PlaceOfInterest lhs, PlaceOfInterest rhs)
        {
            return !(lhs == rhs);
        }
    }
}
