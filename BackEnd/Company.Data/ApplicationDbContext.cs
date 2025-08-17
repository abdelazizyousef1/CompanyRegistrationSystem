using Company.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Company.Data
{
        public class ApplicationDbContext : IdentityDbContext<CompanyModel, IdentityRole<Guid>, Guid>
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }
            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                 builder.Entity<OtpVerification>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Otps)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

        }
        }
    

}
