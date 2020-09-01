namespace ixiaBackend_application.Models.ModelsView
{
    public class ProductView
    {
        public int Id { get; set; }
        public CategoryView Category { get; set; }
        public CompanyView Company { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
