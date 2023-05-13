namespace hacktm.Models
{
    public class RouteModel
    {
        public List<StreetModel> Streets { get; set; }  
        public double RouteLengthInKm { get; set; }

        public double WeightInTons { get; set; }
    }
}
