using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Weather.Server.Entities;

namespace Weather.Server.Data
{
	public class WeatherDbContext : DbContext
	{
		public DbSet<LocationBookmark> LocationBookmarks { get; set; }
		public IConfiguration Configuration { get; }

		public WeatherDbContext(DbContextOptions<WeatherDbContext> options, IConfiguration configuration) : base(options)
		{
			Configuration = configuration;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configurations for LocationBookmark
			modelBuilder.Entity<LocationBookmark>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.LocationName)
					.HasColumnName("LocationName")
					.IsRequired()
					.HasMaxLength(100);

				entity.Property(e => e.Latitude)
					.HasColumnName("Latitude")
					.IsRequired();

				entity.Property(e => e.Longitude)
					.HasColumnName("Longitude")
					.IsRequired();

				entity.Property(e => e.Country)
					.HasColumnName("Country")
					.HasMaxLength(100);

				entity.Property(e => e.State)
					.HasColumnName("State")
					.HasMaxLength(100);

				entity.Property(e => e.CreatedAt)
					.HasColumnName("CreatedAt");
			});
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = Configuration["ConnectionStrings:DefaultConnection"]; 
				optionsBuilder.UseSqlServer(connectionString);
			}
		}
	}
}
