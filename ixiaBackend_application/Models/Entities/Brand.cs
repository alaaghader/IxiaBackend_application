namespace ixiaBackend_application.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Company Company { get; set; }
    }
}
