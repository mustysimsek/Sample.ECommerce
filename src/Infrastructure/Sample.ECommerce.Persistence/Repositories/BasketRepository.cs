using Sample.ECommerce.Domain.Entities;
using Sample.ECommerce.Domain.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Sample.ECommerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _database;
        private readonly string _redisBasketFolder;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public BasketRepository(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _database = redisCon.GetDatabase();
            _redisBasketFolder = "Basket:";
        }
        public async Task<Basket> GetBasketAsync(Guid basketId)
        {
            var basket = await _database.StringGetAsync(_redisBasketFolder + basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
        }
        public async Task<Basket> UpdateBasketAsync(Basket basket)
        {
            var data = await _database.StringSetAsync(_redisBasketFolder + basket.Id, JsonSerializer.Serialize(basket), ExpireTime);
            return data != true ? null : await GetBasketAsync(basket.Id);
        }
        public async Task DeleteBasketAsync(Guid basketId)
        {
            await _database.KeyDeleteAsync(_redisBasketFolder + basketId);
        }
    }
}