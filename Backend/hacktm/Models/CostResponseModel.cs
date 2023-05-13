namespace hacktm.Models
{
    public class CostResponseModel
    {
        public double Cost { get; set; }
        public double CostPerKm { get; set; }

        public List<StreetModel> HighestCostStreets { get; set; }
    }
}
