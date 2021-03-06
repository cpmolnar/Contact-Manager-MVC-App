﻿using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ContactManager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public ApplicationUser(SqlDataReader reader)
        {
            UserName = reader.GetString(0);
        }

        public string HomeTown { get; set; }
        public System.DateTime? BirthDate { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ContactManager.Models.AspNetContact> Contacts { get; set; }

        public System.Data.Entity.DbSet<ContactManager.Models.AspNetGroup> AspNetGroups { get; set; }

        public System.Data.Entity.DbSet<ContactManager.Models.AspNetGroupUser> AspNetGroupUsers { get; set; }

        public System.Data.Entity.DbSet<ContactManager.Models.AspNetGroupContact> AspNetGroupContacts { get; set; }
    }
}