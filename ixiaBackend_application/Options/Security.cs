using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ixiaBackend_application.Options
{
    public class Security
    {

        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public string Key { get; set; }
        public int ExpireInDays { get; set; }

        public SecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}
