namespace WebApi.Models
{
    public class UserSport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SportId { get; set; }

        public virtual User User { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
