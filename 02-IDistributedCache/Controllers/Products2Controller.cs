using _02_IDistributedCache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace _02_IDistributedCache.Controllers
{
    public class Products2Controller : Controller
    {
        private IDistributedCache _distributedCache;
        public Products2Controller(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            Product product = new Product() { Id = 1, Name = "Kalem", Price = 100 };

            string jsonProduct = JsonConvert.SerializeObject(product);

            //Byte olarak nasıl veriyi gönderiyoruz!!
            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distributedCache.Set("product:1", byteProduct);

            //Veriyi JSON'a çevirip gönderdik
           //await _distributedCache.SetStringAsync("product:1", jsonProduct, cacheEntryOptions);

            //Products 1 dediğim zaman redis tarafında bir dosya açıyor ve açtığı dosyanın içerisinde gelen dataları yukarıda verdiğim product:1 , product:2 isimlerine göre kaydetmesini sağlıyor.
            return View();
        }

        //Byte Olarak veri gönderdiğimiz zaman nasıl yapılacağını verdik
        public async Task<IActionResult> Index2()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            Product product = new Product() { Id = 1, Name = "Kalem", Price = 100 };

            string jsonProduct = JsonConvert.SerializeObject(product);

            //Byte olarak nasıl veriyi gönderiyoruz!!
            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);
            _distributedCache.Set("product:1", byteProduct);

            return View();
        }

        public IActionResult Show()
        {
            string jsonProduct = _distributedCache.GetString("product:1");

            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);

            ViewBag.product = p;
            return View();
        }


        //Byte olarak veriyi nasıl okuyacağımızı yapıyoruz.
        public IActionResult Show2()
        {
            //Byte olarak veriyi Redis tarafından okuduk.
            Byte[] byteProduct = _distributedCache.Get("product:1");
            
            //Gelen veriyi Json formatında byte diziye çevirip göndermiştim JSON tipi tekrardan okudum
            string jsonProduct = Encoding.UTF8.GetString(byteProduct);
            //Okuduğum json deserialize edip Product nesnesine dönüştürüdüm
            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);

            ViewBag.product = p;
            return View();
        }
    }
}
