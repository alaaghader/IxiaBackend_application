using Microsoft.AspNetCore.Http;

namespace ixiaBackend_application.ModelsInput
{
    public class ProfileInput
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        //public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
    }
}
