﻿namespace U4___Rework
{
    class Museum : PlaceOfInterest
    {
        public string Type { get; set; }
        //Darbo dienos
        public bool[] WorksOn { get; set; }
        public double TicketPrice { get; set; }
        public bool HasGuide { get; set; }
        public const string Id = "mu";

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
            return string.Format("{0,-10} | {1,-20} | {2,-4} | {3} | {4,-4} | {5,-10}", Name, Adress, Year, string.Join(" | ", WorksOn), TicketPrice, HasGuide);
        }
        public override string ToCsv()
        {
            return string.Format("{0},{1},{2},{3},{4},{5}", Name, Adress, Year, string.Join(",", WorksOn), TicketPrice, HasGuide);
        }
    }
}
