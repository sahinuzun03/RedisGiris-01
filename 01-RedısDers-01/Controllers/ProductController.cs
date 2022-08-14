using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace _01_RedısDers_01.Controllers
{
    //Burada Controller sınıfı açıldı sonrasında IMemoryCache interface'inin nesnesi üretildi ve constructorda içerisi dolduruldu.
    //.NET Core 6 ' da builder.Services.AddMemoryCache ile servis eklendi.

    //Get metodu ile Memory'e atılan veri çağırıldı Set Metodu ile memory'e veri atanması gerçekleştirildi.
    //Örnek olarak biz burada veri Datetime.Now diyerek bugünün zamanını  set ettim ve Show metodunda Show ekranın memory'e atmış olduğum zaman bilgisini memory'den çektim ve ekranda gösterdim.

    //MemoryCacheExpiration açmış olduğum cache'e süre vermek için nesnesini ürettim ve bu nesnenin propertylerinden AbsoluteExpiration ile belirli süre sonra silinmesini veya çağrıldıkça o sürenin arttırılmasını sağlayan SlidingExpiration metodu ile cache işleminin arttırılmasını sağladım.
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
