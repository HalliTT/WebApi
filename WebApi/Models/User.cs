using System.Net;

namespace WebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegNumber { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<UserSport> UsersSports { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<ParentChild> ParentChildren { get; set; }
    }
}
