namespace ixiaBackend_application.Models.ModelsView
{
    public class PriceView
    {
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public double PriceNumber { get; set; }
        public ProductView Product { get; set; }
        public CurrencyView Currency { get; set; }
        public CountryView Country { get; set; }
    }
}
