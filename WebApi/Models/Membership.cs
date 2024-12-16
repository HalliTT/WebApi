namespace WebApi.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? SportId { get; set; }
        public DateTime? InactiveDate { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
