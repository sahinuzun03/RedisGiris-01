using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisServices _redisService;
        private readonly IDatabase _db;
        public string hashKey { get; set; } = "sozluk";

        public HashTypeController(RedisServices redisServices)
        {
            _redisService = redisServices;
            _db = _redisService.GetDb(5);
        }

        //KEY - VALUE şeklinde çalışırlar
        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            if (_db.KeyExists(hashKey))
            {
                _db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    list.Add(x.Name, x.Value);
                });
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, string val)
        {
            _db.HashSet(hashKey, name, val);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            _db.HashDelete(hashKey, name);
            return RedirectToAction("Index");
        }
    }
}
