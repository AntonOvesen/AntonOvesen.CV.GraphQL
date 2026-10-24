using AntonOvesen.CV.GraphQL.Core;
using Microsoft.EntityFrameworkCore;

namespace AntonOvesen.CV.GraphQL.Infrastructure;

public class CVContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Skill> Skills { get; set; }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().OwnsOne(x => x.Phone);
        modelBuilder.Entity<User>().HasMany(x => x.Skills).WithOne(x => x.User);
        modelBuilder.Entity<User>().HasMany(x => x.Projects).WithOne(x => x.User);
        modelBuilder.Entity<User>().HasMany(x => x.Educations).WithOne(x => x.User);
        modelBuilder.Entity<User>().HasMany(x => x.Companies).WithOne(x => x.User);

        modelBuilder.Entity<Skill>().HasMany(x => x.Projects).WithMany(x => x.Skills);
        modelBuilder.Entity<Skill>().HasMany(x => x.Educations).WithMany(x => x.Skills);
        modelBuilder.Entity<Skill>().HasMany(x => x.Companies).WithMany(x => x.Skills);

        modelBuilder.Entity<Education>().HasOne(x => x.Institution).WithMany(x => x.Educations);
    }
}