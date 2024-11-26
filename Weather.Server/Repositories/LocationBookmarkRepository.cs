using Microsoft.EntityFrameworkCore;
using Weather.Server.Data;
using Weather.Server.Dtos;
using Weather.Server.Entities;
using Weather.Server.Repositories.Interfaces;

namespace Weather.Server.Repositories
{
	public class LocationBookmarkRepository : ILocationBookmarkRepository
	{
		private readonly WeatherDbContext _context;

		public LocationBookmarkRepository(WeatherDbContext context)
		{
			_context = context;
		}

		public async Task<bool> SaveLocationBookmarkAsync(LocationBookmarkDto dto)
		{
			var bookmark = new LocationBookmark
			{
				LocationName = dto.LocationName,
				Latitude = dto.Latitude,
				Longitude = dto.Longitude,
				Country = dto.Country,
				State = dto.State
			};

			await _context.Set<LocationBookmark>().AddAsync(bookmark);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<IEnumerable<LocationBookmark>> GetAllBookmarkedLocationsAsync()
		{
			return await _context.Set<LocationBookmark>()
				.Select(b => new LocationBookmark
				{
					Id = b.Id,
					Country = b.Country,
					State = b.State,
					LocationName = b.LocationName,
					Latitude = b.Latitude,
					Longitude = b.Longitude
				})
				.ToListAsync();
		}
	}
}
