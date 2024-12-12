using ChanelTaskRedis.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using System.Threading.Channels;

namespace ChanelTaskRedis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConnectionMultiplexer _redis;

        public HomeController()
        {
            _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { { "redis-18215.c232.us-east-1-2.ec2.redns.redis-cloud.com", 18215 } },
                User = "default",
                Password = "g5Y6eLmitq4diGJGt6p6jbElfuupPd1D"
            });
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateChannel(string channelName)
        {
            var db = _redis.GetDatabase();
            var channelKey = $"channel:{channelName}";

            if (!db.HashExists(channelKey, "exists"))
            {
                db.HashSet(channelKey, new[] { new HashEntry("exists", 1) }); 

                var defaultSubscribers = new[] { "user1", "user2", "user3" };
                db.SortedSetAdd(channelKey, defaultSubscribers, 0);
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetChannels()
        {

            var channels = new List<Chanel>();

            return View(channels);
        }
    }
}
