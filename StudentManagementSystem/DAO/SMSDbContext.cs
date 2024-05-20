using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.DAO
{
    public class SMSDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public SMSDbContext(DbContextOptions<SMSDbContext> options) : base(options)
        {

        }
    }
}
