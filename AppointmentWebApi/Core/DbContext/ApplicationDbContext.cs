using AppointmentWebApi.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentWebApi.Core.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
