using Microsoft.EntityFrameworkCore;
using Weather.Server.Entities;

namespace Weather.Server.Data
{
	public class WeatherDbContext : DbContext
	{
		public DbSet<LocationBookmark> LocationBookmarks { get; set; }

		public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configurations for LocationBookmark
			modelBuilder.Entity<LocationBookmark>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.name)
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.country)
					.HasMaxLength(100);

				entity.Property(e => e.state)
					.HasMaxLength(100);

				entity.Property(e => e.CreatedAt)
					.HasDefaultValueSql("GETDATE()");
			});
		}
	}
}
