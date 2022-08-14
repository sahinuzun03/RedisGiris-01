using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace _01_RedısDers_01.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        //Get memory'den bilgileri almak Set memory'e kaydetmek için kullanılır. 
        //Key,Value çiftinden tutulur , Tarih bilgisi cache'e gönderildi
        //object key -> zaman
        public IActionResult Index()
        {
            //I. YOL
            if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))//key değeri memory'de yoksa 
            {
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            //II. YOL
            if (!_memoryCache.TryGetValue("zaman", out string zamancache))
            {

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

                //options.AbsoluteExpiration = DateTime.Now.AddSeconds(30); // 30 saniyelik süre verdim
                options.SlidingExpiration = TimeSpan.FromSeconds(10); // 10 saniyede bir ben bu veriye erişirsem verinin erişim süresi artacaktır.Her bir 10 saniyede erişmeye çalıştığım zaman sıkıntı yaratmayacaktır.


                _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options); //constructor metot ile cache ömür vermiş oldum.
            }


            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("zaman", out string zamancache); //Eğer alırsa viewbag'e atacak ve ben gidip ekranda bunun bilgisini yazdırabileceğim.

            ViewBag.zaman = zamancache;

            ////Aşağıdaki metot alabiliyorsa alır alamaz ise zaman keyine sahip bir metot sonucu döner.
            //_memoryCache.GetOrCreate<string>("zaman", entry => 
            //{
            //    return DateTime.Now.ToString();
            //});

            /*ViewBag.zaman = _memoryCache.Get<string>("zaman");*/ //object key zaman 
            return View();

            //_memoryCache.Remove("zaman"); //cahce silme
        }

        //ABSOLUTE EXPİRATİON :

        //Absolute EXPİRATİON ' DA 
        /*
         * SEN SÜRE VERDİĞİN ZAMAN CACHE BELİRLİ BİR SÜRE CAHCE'DE DURUYOR SONRASINDA SİLİNİYOR
         
         */

        //SLİDİNG EXPİRATİON
        /*
         * CAHCE BELİRLİ BİR SÜRE TUTULUYOR FAKAT SİZ ONU ÇAĞIRIĞ KULLANIRSANIZ TEKRARDAN SÜRESİ ARTIYOR.
         */
    }
}
