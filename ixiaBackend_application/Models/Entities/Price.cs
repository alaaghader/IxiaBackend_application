namespace ixiaBackend_application.Models.Entities
{
    public class Price
    {
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public double PriceNumber { get; set; }

        public virtual Product Product { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Country Country { get; set; }
    }
}
