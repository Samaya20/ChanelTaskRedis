namespace ChanelTaskRedis.Models
{
    public class Chanel
    {
        public string Name { get; set; }
        public List<string> Subscribers { get; set; }
        public List<string> Messages { get; set; }
    }
}
