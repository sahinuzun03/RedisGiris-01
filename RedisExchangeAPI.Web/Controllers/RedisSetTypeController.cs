using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class RedisSetTypeController : Controller
    {
        private readonly RedisServices _redisService;
        private readonly IDatabase _db;

        private string listKey = "hashname";
        public RedisSetTypeController(RedisServices redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            List<string> nameList = new List<string>();
            if (!_db.KeyExists(listKey)) //Sliding Expriration kapatmak için kullandık.
            {
                _db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }

            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //if (!_db.KeyExists(listKey)) //Sliding Expriration kapatmak için kullandık.
            //{
            //    _db.KeyExpire(listKey, DateTime.Now.AddMinutes(5)); //eğer bunu koymazsam herr ADd metodu çalıştığında bunu gidip süresini arttıracak
            //}

            _db.KeyExpire(listKey, DateTime.Now.AddMinutes(5)); //Her defasında +5 minute olacak
            //listkey'e 5 dk bir ömür verdim. Her defasında çalışt
            _db.SetAdd(listKey, name);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteItem(string name)
        {

            //Sıra Değişir!!
            _db.SetRemoveAsync(listKey,name);
            return RedirectToAction("Index");

        }



    }
}
