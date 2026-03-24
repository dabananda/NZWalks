using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "8cbb8e54-23a7-4b71-a253-4ebf6f663548";
            var writerRoleId = "75b7e480-a52d-4c54-ba99-ffb1e3e30717";

            var roles = new List<IdentityRole>
            {
                new() { Id = readerRoleId, ConcurrencyStamp = readerRoleId, Name = "Reader", NormalizedName = "Reader".ToUpper() },
                new() { Id = writerRoleId, ConcurrencyStamp = writerRoleId, Name = "Writer", NormalizedName = "Writer".ToUpper() }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
