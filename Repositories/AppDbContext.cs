using Microsoft.EntityFrameworkCore;
using SaveVault.Models;

namespace SaveVault.Repositories;

public class AppDbContext : DbContext


{



	public DbSet<UniversalSave> Saves { get; set; }
	public DbSet<Game> Games { get; set; }
	public DbSet<AdditionalContent> AdditionalContents { get; set; }
	public DbSet<User> Users { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase("InMemoryDb");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var saveModel = modelBuilder.Entity<UniversalSave>();

		saveModel.HasOne(u => u.User);
		saveModel.HasOne(u => u.Game);
		saveModel.HasMany(u => u.AccessedAdditionalContent);
	}
}