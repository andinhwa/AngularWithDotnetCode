namespace WebApp.Core.Models
{
    public class Customer: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNane => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Address { get; set; }
    }
}