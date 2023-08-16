using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveVault.Models;

namespace SaveVault.Repositories;

public class SaveVaultDbContext : DbContext
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
		EntityTypeBuilder<UniversalSave> saveModel = modelBuilder.Entity<UniversalSave>();

		saveModel.HasOne(u => u.User);
		saveModel.HasOne(u => u.Game);
		saveModel.HasMany(u => u.AccessedAdditionalContent);
	}
}