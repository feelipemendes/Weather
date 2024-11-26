namespace Weather.Server.Models.Base
{
	public class BaseEntity
	{
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
