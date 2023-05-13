namespace hacktm.Entities
{
    public class Street
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public bool Accessible { get; set; }
    }
}
