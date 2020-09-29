namespace ixiaBackend_application.Models.ModelsView
{
    public class ProductView
    {
        public int Id { get; set; }
        public TypeView Type { get; set; }
        public CompanyView Company { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
        public int TotalFavorite { get; set; }
    }
}
