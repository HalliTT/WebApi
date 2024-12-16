namespace WebApi.Models
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<UserSport> UserSports { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
