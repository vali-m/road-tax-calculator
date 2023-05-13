using hacktm.Entities;

namespace hacktm.Models
{
    public class RegisterStreetModel
    {
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public bool Accessible { get; set; }

        public Street ToEntity()
        {
            return new Street
            {
                Name = this.Name,
                MaxWeight = this.MaxWeight,
                Accessible = this.Accessible
            };
        }
    }
}
