using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisServices
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _redis;//redis ile haberleşeceğim prop
        public IDatabase db { get; set; }


        public RedisServices(IConfiguration configurtion)
        {
            _redisHost = configurtion["Redis:Host"];
            _redisPort = configurtion["Redi:Port"];
        }

        public void Connect()
        {
            //Redis server ile haberleşecek olan metot
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = ConnectionMultiplexer.Connect(configString);//Redis server ile haberleştim.
                    

        }

        public IDatabase GetDb(int db) //Redis tarafındanbulunan 0-15 arasındaki dblerden 1 tanesini seçmek için kullanacağım yer.
        {
            return _redis.GetDatabase(db);//Numarasını benim verdiğim DB'yi getirecek olan yer.
        }
    }
}
