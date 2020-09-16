using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Purchase> PurchasesCurrency { get; set; }
        public virtual ICollection<Favorite> FavoritesCurrency { get; set; }
    }
}
