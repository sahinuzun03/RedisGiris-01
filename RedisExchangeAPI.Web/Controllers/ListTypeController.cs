using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisServices _redisServices;
        private readonly IDatabase _db;

        private string listKey = "names";
        public ListTypeController(RedisServices redisServices)
        {
            _redisServices = redisServices;
            _db = redisServices.GetDb(0);
        }
        public IActionResult Index()
        {
            List<string> namesList = new List<string>();

            if (_db.KeyExists(listKey))
            {
                //Bu key değeri var mı diye kontrol edeceğiz!! Varsa okuma işlemimizi gerçekleştireceğiz
                // 0'dan sonsuza kadar okurum diyor / istersen bana start ve stop değerlerinide verebilirsin diyor.
                _db.ListRange(listKey).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });

            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {

            _db.ListRightPush(listKey, name); //Sona ekleme sağa eklemek için eft
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string name)
        {

            _db.ListRemoveAsync(listKey, name).Wait(); //anahtar'da buluna  kişinin silinmesi işlemini gerçekleştiriyorum.
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFırstItem(string name)
        {

            _db.ListLeftPop(listKey);//Baştan silme işlemini gerçekleştirir.
            _db.ListRightPop(listKey);//Sondan silme işlemini gerçekleştirir.
            return RedirectToAction("Index");
        }
    }
}
