namespace PathFinder.DatabaseService.Model
{
    public class StreetView
    {
        public StreetView() { }

        public StreetView(int number, string path, double lat, double lng) 
        {
            Number = number;
            Path = path;
            Lat = lat;
            Lng = lng;
        }

        public int Id { get; set; }

        public int Number { get; set; }

        public string Path { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }
}
