using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DBforum : IdentityDbContext<ApplicationUser>
{
    public DbSet<Discussion> discussions { get; set; }
    public DbSet<Forum> forums { get; set; }
    public DbSet<Topic> topics { get; set; }
    public DbSet<Post> posts { get; set; }
    public DbSet<Like> likes { get; set; }

    public DBforum(DbContextOptions<DBforum> options)
        : base(options)
    {
    }
}
