namespace WebApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Road { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
