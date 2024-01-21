using projekt.Models;

namespace ZTPAPP.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
