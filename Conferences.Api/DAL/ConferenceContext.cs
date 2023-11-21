using Conferences.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conferences.Api.DAL;

public class ConferenceContext : DbContext
{
    public DbSet<Conference> Conferences { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public ConferenceContext(DbContextOptions<ConferenceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}