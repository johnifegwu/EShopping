﻿
namespace Basket.Core.Entities
{
    public class DefaultConfig
    {
        public string Environment { get; set; } = default!;
        public string RedisUrl { get; set; } = default!;
        public string DiscountRpcHost { get; set; } = default!;
    }
}
