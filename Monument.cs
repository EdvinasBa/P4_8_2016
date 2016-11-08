namespace U4___Rework
{
    class Monument : PlaceOfInterest
    {
        public string Author { get; set; }
        public string IntendedFor { get; set; }
        public const string Id = "mo";

        public Monument() { }

        public Monument(string name, string adress, int year, string author, string intendedFor) 
            : base(name, adress, year)
        {
            Author = author;
            IntendedFor = intendedFor;
        }
        public override string ToString()
        {
            return string.Format("{0,-10} | {1,-20} | {2,-4} | {3,-10} | {4,-10}", Name, Adress, Year, Author, IntendedFor);
        }
        public override string ToCsv()
        {
            return string.Format("{0},{1},{2},{3},{4}", Name, Adress, Year, Author, IntendedFor);
        }

    }
}
