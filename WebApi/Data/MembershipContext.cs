using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class MembershipContext : DbContext
    {
        public MembershipContext(DbContextOptions<MembershipContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ParentChild> ParentChildren { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<UserSport> UserSports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User and Address Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AddressId);

            // Membership Relationships
            modelBuilder.Entity<Membership>()
                .HasOne(m => m.User)
                .WithMany(u => u.Memberships)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Membership>()
                .HasOne(m => m.Role)
                .WithMany(r => r.Memberships)
                .HasForeignKey(m => m.RoleId);

            modelBuilder.Entity<Membership>()
                .HasOne(m => m.Sport)
                .WithMany(s => s.Memberships)
                .HasForeignKey(m => m.SportId)
                .IsRequired(false); // Nullable

            // ParentChild Relationships
            modelBuilder.Entity<ParentChild>()
                .HasOne(pc => pc.Parent)
                .WithMany(p => p.ParentChildren)
                .HasForeignKey(pc => pc.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParentChild>()
                .HasOne(pc => pc.Child)
                .WithMany()
                .HasForeignKey(pc => pc.ChildId)
                .OnDelete(DeleteBehavior.Restrict);

            // UsersSports Relationships
            modelBuilder.Entity<UserSport>()
                .HasOne(us => us.User)
                .WithMany(u => u.UsersSports)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSport>()
                .HasOne(us => us.Sport)
                .WithMany(s => s.UserSports)
                .HasForeignKey(us => us.SportId);

        }
    }
}
