using BKS.Task.DL.DTO;
using Microsoft.EntityFrameworkCore;

namespace BKS.Task.DL;

public class BKSContext: DbContext
{
    public DbSet<UserMessageDto> UserMessage { get; set; }

    public BKSContext(DbContextOptions<BKSContext> context): base(context)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserMessageDto>().ToTable("UserMessage").Property(f => f.Id).ValueGeneratedOnAdd();
    }
}