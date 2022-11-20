using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisServices _redisService;
        private readonly IDatabase _db;
        private string listKey = "sortedsetnames";
        public SortedSetTypeController(RedisServices redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(4);
        }

        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();

            if (_db.KeyExists(listKey))
            {
                _db.SortedSetScan(listKey).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });

                _db.SortedSetRangeByRank(listKey, 0, 5, order: Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            _db.SortedSetAdd(listKey, name, score);//Sıralı elemanı ekledi verisi ile beraber
            _db.KeyExpire(listKey, DateTime.Now.AddMinutes(1)); //Süresini uzattı
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            _db.SortedSetRemove(listKey, name); //Silme işlemini name'e göre yaptık

            return RedirectToAction("Index");
        }
    }
}
