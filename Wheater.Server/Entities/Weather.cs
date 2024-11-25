using Weather.Server.Models.Base;

namespace Wheater.Server.Models
{
	public class Weather : BaseEntity
	{
        public string City { get; set; }
        public int MyProperty { get; set; }
    }
}
