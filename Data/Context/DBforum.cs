using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DBforum : IdentityDbContext<ApplicationUser>
{
    public DbSet<Discussion> discussions { get; set; } = null!;
    public DbSet<Forum> forums { get; set; } = null!;
    public DbSet<Topic> topics { get; set; } = null!;
    public DbSet<Post> posts { get; set; } = null!;
    public DbSet<Report> reports { get; set; } = null!;
    public DbSet<Like> likes { get; set; } = null!;
    public DbSet<Save> saves { get; set; } = null!;

    public DBforum(DbContextOptions<DBforum> options)
        : base(options)
    {
    }
}
