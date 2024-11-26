using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Weather.Server.Data
{
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherDbContext>
	{
		public WeatherDbContext CreateDbContext(string[] args)
		{
			{
				var basePath = AppDomain.CurrentDomain.BaseDirectory;

				IConfigurationRoot configuration = new ConfigurationBuilder()
					.SetBasePath(basePath)
					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
					.AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true)
					.Build();

				var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
				optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

				return new WeatherDbContext(optionsBuilder.Options, configuration);
			}
		}
	}
}
