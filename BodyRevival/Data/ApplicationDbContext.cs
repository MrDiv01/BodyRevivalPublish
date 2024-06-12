using BodyRevival.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BodyRevival.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<AppUser> Users {  get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<Packet> Packet { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Customers> Customers { get; set; }



    }
}
