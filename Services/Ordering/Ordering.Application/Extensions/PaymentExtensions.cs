
using eShopping.Models;
using Ordering.Core.Entities;
using Stripe;

namespace Ordering.Application.Extensions
{
    internal static class PaymentExtensions
    {
        public static async Task<bool> ProcessPaymentAsync(this Order order, DefaultConfig config)
        {
            StripeConfiguration.ApiKey = config.StripeApiKey;

            var token = await CreateTokenAsync(order);

            var options = new ChargeCreateOptions
            {
                Amount = (long)(order.TotalPrice * 100), // Stripe uses the smallest currency unit
                Currency = order.Currency.ToLower(),
                Description = $"Order for {order.FirstName} {order.LastName} \n Order guid: {order.OrderGuid}",
                Source = token
            };

            var service = new ChargeService();

            try
            {
                var charge = await service.CreateAsync(options);
                order.IsPaid = charge.Status == "succeeded";
                order.PaymentReference = charge.Id;
                order.PaymentProviderUsed = "Stripe";
                order.PaymentMethod = 1;
                return order.IsPaid.Value;
            }
            catch (StripeException ex)
            {
                Console.WriteLine($"Stripe error: {ex.Message}");
                return false;
            }
        }

        private static async Task<string> CreateTokenAsync(Order order)
        {
            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = order.CardNumber,
                    ExpMonth = long.Parse(order.Expiration.Split('/')[0]).ToString(),
                    ExpYear = long.Parse(order.Expiration.Split('/')[1]).ToString(),
                    Cvc = order.CVV
                }
            };

            var service = new TokenService();
            var token = await service.CreateAsync(options);
            return token.Id;
        }
    }
}
