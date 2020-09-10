namespace ixiaBackend_application.Models.ModelsView
{
    public class CompanyView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        // public List<ProductView> Products { get; set; }
    }
}
