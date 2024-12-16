using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(MembershipContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var role = new Role[]
            {
               new() { Name = "Aktivt Medlem" },
               new() { Name = "Passivt Medlem" },
               new() { Name = "Bestyrelse Medlem" },
               new() { Name = "Forældre Medlem" }
            };
            foreach (Role r in role)
            {
                context.Roles.Add(r);
            }
            context.SaveChanges();

            var sport = new Sport[]
            {
                new() { Name = "Fodbold", Price = 800 },
                new() { Name = "Håndbold", Price = 700},
                new() { Name = "Gymnastik", Price = 600 },
                new() { Name = "Bamsekarate", Price = 500 }
            };
            foreach (Sport s in sport)
            {
                context.Sports.Add(s);
            }
            context.SaveChanges();

            var address = new Address[]
            {
                new() { Road = "Hovedgaden 1", City = "København", Zipcode = "1000" },
                new() { Road = "Hovedgaden 2", City = "Aalborg", Zipcode = "2000" },
                new() { Road = "Hovedgaden 3", City = "Rønne", Zipcode = "3000" },
                new() { Road = "Hovedgaden 4", City = "Horsens", Zipcode = "4000" }
            };
            foreach (Address a in address)
            {
                context.Addresses.Add(a);
            }
            context.SaveChanges();

            var parent = new User[]
            {
                new() { Name = "John", RegNumber = "191091", AddressId = address.Single(s => s.City == "København").Id },
                new() { Name = "Jane", RegNumber = "191092", AddressId = address.Single(s => s.City == "Aalborg").Id },
                new() { Name = "Jill", RegNumber = "191093", AddressId = address.Single(s => s.City == "Rønne").Id },
                new() { Name = "Jack", RegNumber = "191094", AddressId = address.Single(s => s.City == "Horsens").Id }
            };
            foreach (User p in parent)
            {
                context.Users.Add(p);
            }
            context.SaveChanges();

            var children = new User[]
            {
                new() { Name = "Jen", RegNumber = "191021", AddressId = address.Single(s => s.City == "København").Id },
                new() { Name = "Jiy", RegNumber = "191019", AddressId = address.Single(s => s.City == "København").Id },
                new() { Name = "Jan", RegNumber = "191017", AddressId = address.Single(s => s.City == "Rønne").Id },
                new() { Name = "Yul", RegNumber = "191007", AddressId = address.Single(s => s.City == "København").Id }
            };
            foreach (User c in children)
            {
                context.Users.Add(c);
            }
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");

            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.ParentChildren ON");
            var parentChildren = new ParentChild[]
            {
                new() { ParentId = parent.Single(s => s.Name == "John").Id, ChildId = children.Single(s => s.Name == "Jen").Id },
                new() { ParentId = parent.Single(s => s.Name == "John").Id, ChildId = children.Single(s => s.Name == "Jiy").Id },
                new() { ParentId = parent.Single(s => s.Name == "John").Id, ChildId = children.Single(s => s.Name == "Yul").Id },
                new() { ParentId = parent.Single(s => s.Name == "Jill").Id, ChildId = children.Single(s => s.Name == "Jan").Id }
            };
            foreach (ParentChild pc in parentChildren)
            {
                context.ParentChildren.Add(pc);
            }
            context.SaveChanges();

            var memberShip = new Membership[]
            {
                new() { UserId = parent.Single(s => s.Name == "John").Id, RoleId = role.Single(s => s.Name == "Forældre Medlem").Id },
                new() { UserId = parent.Single(s => s.Name == "Jane").Id, RoleId = role.Single(s => s.Name == "Bestyrelse Medlem").Id, SportId = sport.Single(s => s.Name == "Gymnastik").Id },
                new() { UserId = parent.Single(s => s.Name == "Jill").Id, RoleId = role.Single(s => s.Name == "Forældre Medlem").Id },
                new() { UserId = parent.Single(s => s.Name == "Jack").Id, RoleId = role.Single(s => s.Name == "Aktivt Medlem").Id },
                new() { UserId = children.Single(s => s.Name == "Jen").Id, RoleId = role.Single(s => s.Name == "Aktivt Medlem").Id },
                new() { UserId = children.Single(s => s.Name == "Jiy").Id, RoleId = role.Single(s => s.Name == "Aktivt Medlem").Id },
                new() { UserId = children.Single(s => s.Name == "Jan").Id, RoleId = role.Single(s => s.Name == "Passivt Medlem").Id, InactiveDate = new DateTime() },
                new() { UserId = children.Single(s => s.Name == "Yul").Id, RoleId = role.Single(s => s.Name == "Passivt Medlem").Id, InactiveDate = new DateTime() }
            };
            foreach (Membership m in memberShip)
            {
                context.Memberships.Add(m);
            }
            context.SaveChanges();

            //Fodbold, Håndbold, Gymnastik, Bamsekarate
            var userSport = new UserSport[]
            {
                new() { UserId = parent.Single(s => s.Name == "John").Id, SportId = sport.Single(s => s.Name == "Fodbold").Id },
                new() { UserId = parent.Single(s => s.Name == "Jane").Id, SportId = sport.Single(s => s.Name == "Gymnastik").Id },
                new() { UserId = parent.Single(s => s.Name == "Jill").Id, SportId = sport.Single(s => s.Name == "Håndbold").Id },
                new() { UserId = parent.Single(s => s.Name == "Jack").Id, SportId = sport.Single(s => s.Name == "Bamsekarate").Id },
                new() { UserId = children.Single(s => s.Name == "Jen").Id, SportId = sport.Single(s => s.Name == "Fodbold").Id },
                new() { UserId = children.Single(s => s.Name == "Jiy").Id, SportId = sport.Single(s => s.Name == "Håndbold").Id },
            };
            foreach (UserSport us in userSport)
            {
                context.UserSports.Add(us);
            }
            context.SaveChanges();
        }
    }
}
