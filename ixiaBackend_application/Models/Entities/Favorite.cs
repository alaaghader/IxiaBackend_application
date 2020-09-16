using System;

namespace ixiaBackend_application.Models.Entities
{
    public class Favorite
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime FavoriteTime { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        public virtual Country Country{ get; set; }
        public virtual Currency Currency { get; set; }
    }
}