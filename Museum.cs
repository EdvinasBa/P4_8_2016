namespace U4___Rework
{
    class Museum : PlaceOfInterest
    {
        public string Type { get; set; }
        //Darbo dienos
        public bool[] WorksOn { get; set; }
        public double TicketPrice { get; set; }
        public bool HasGuide { get; set; }

        public Museum() { }

        public Museum(string name, string adress, int year, string type, bool[] worksOn, double ticketPrice, bool hasGuide)
            : base(name, adress, year)
        {
            Type = type;
            WorksOn = worksOn;
            TicketPrice = ticketPrice;
            HasGuide = hasGuide;
        }

        public override string ToString()
        {
<<<<<<< HEAD
            return string.Format("{0,-10}, {1,-10}, {2,-4}, {3}, {4,-4}, {5,-10}", Name, Adress, Year, string.Join(",", WorksOn), TicketPrice, HasGuide);
=======
            return string.Format("{0,-10}, {1,-10}, {2,-10}, {3}, {4,-10}, {5,-10}", Name, Adress, Year, string.Join(",", WorksOn), TicketPrice, HasGuide);
>>>>>>> d58d9636e49e852fc38cc1eda0f6d793156ee4cf
        }
    }
}
