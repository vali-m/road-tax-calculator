using hacktm.Entities;

namespace hacktm.Models
{
    public class UpdateStreetModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public bool Accessible { get; set; }

        public Street ToEntity()
        {
            return new Street
            {
                Id = this.Id,
                Name = this.Name,
                MaxWeight = this.MaxWeight,
                Accessible = this.Accessible
            };
        }
    }
}
