namespace WebApp.DataStructure
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNane => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Address { get; set; }
    }
}