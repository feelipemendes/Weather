using Weather.Server.Models.Base;

namespace Weather.Server.Entities
{
	public class LocationBookmark : BaseEntity
	{
		public string name { get; set; }
		public double lat { get; set; }
		public double lon { get; set; }
		public string country { get; set; }
		public string state { get; set; }
	}
}
