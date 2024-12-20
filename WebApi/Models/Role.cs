﻿namespace WebApi.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
