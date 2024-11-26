using Weather.Server.Models.Base;

namespace Weather.Server.Entities
{
	public class LocationBookmark : BaseEntity
	{
		public string LocationName { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
	}
}
