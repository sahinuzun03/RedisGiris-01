using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisServices _redisServices;
        private readonly IDatabase _db;
        public StringTypeController(RedisServices redisServices)
        {
            _redisServices = redisServices;
            _db = _redisServices.GetDb(0);
        }
        public IActionResult Index()
        {

            _db.StringSet("name", "Şahin Uzun"); //name değerine Şahin atamasını gerçekleştirdim.
            _db.StringSet("ziyaretçi", 100);

            return View();
        }

        public IActionResult Show()
        {
            var value = _db.StringGet("name");

            var value2 = _db.StringGetRange("name",0,3); //0'dan başlayıp bana 3'ü indexde dahil karakterleri getir dedim.

            var value4 = _db.StringLength("name"); //name keywordüne sahip değerin uzunluğunu getir dedim.

            _db.StringIncrement("ziyaretçi", 1); //Her burası tetiklendiğinde 1 er 1 'er ziyaretçi keyword sahip kişiyi arttır diyoruz.

            var count = _db.StringDecrementAsync("ziyaretçi", 1).Result; // Burada 1'er 1'er asenkron azalt ve veriyi bize getir dedik.


            Byte[] resimbyte = default(byte[]);

            _db.StringSet("resim",resimbyte); // Buraya resim'i byte olarak kaydedebilirim.

            if (value.HasValue)
            {
                ViewBag.value = value.ToString();
            }

            return View();
        }

    }
}
