using System;

namespace ixiaBackend_application.Models.ModelsView
{
    public class FavoriteView
    {
        public UserView User { get; set; }

        public PriceView Price { get; set; }

        public DateTime FavoriteTime { get; set; }
    }
}
