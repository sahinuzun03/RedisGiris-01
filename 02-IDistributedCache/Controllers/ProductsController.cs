using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace _02_IDistributedCache.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductsController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
            //Data Kaydetmek için key, value bytedizisi ve en sonra cache ile ilgili veri süresi vs ayarı yapabiliriz.
            //Set string olarak ayarladığım zaman string veri kaydı yapmamı sağlar.

            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            _distributedCache.SetString("name","Şahin",cacheEntryOptions);
            return View();
        }

        public IActionResult Show()
        {
            //Getirilecek verinin sahip olduğu key bilgisini verdik.
            string name = _distributedCache.GetString("name");
            return View();
        }

        public IActionResult Remove()
        {
            //Silinecek olan veriyi silmek için key bilgisini verdik.
            _distributedCache.Remove("name");
            return View();
        }

    }
}
