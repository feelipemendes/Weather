using Weather.Server.Dtos;
using Weather.Server.Entities;

namespace Weather.Server.Repositories.Interfaces
{
	public interface ILocationBookmarkRepository
	{
		Task<bool> SaveLocationBookmarkAsync(LocationBookmarkDto dto);
		Task<IEnumerable<LocationBookmark>> GetAllBookmarkedLocationsAsync();
	}
}
