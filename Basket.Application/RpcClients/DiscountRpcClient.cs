
using Basket.Application.Configurations;
using Discount.Grpc.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Application.RpcClients
{
    public class DiscountRpcClient
    {
        public static async Task<DiscountModel> GetDiscountAsync(string productId, IOptions<DefaultConfig> config)
        {
            var client = GetClient(config);

            var request = new GetDiscountRequest
            {
                ProductId = productId,
            };

            return await client.GetDiscountAsync(request);
        }

        public static async Task<DiscountModel> CreateDiscountAsync(CreateDiscountModel payload, IOptions<DefaultConfig> config)
        {
            var client = GetClient(config);

            var request = new CreateDiscountRequest
            {
                Payload = payload,
            };

            return await client.CreateDiscountAsync(request);
        }

        public static async Task<DiscountModel> UpdateDiscountAsync(DiscountModel payload, IOptions<DefaultConfig> config)
        {
            var client = GetClient(config);

            var request = new UpdateDiscountRequest
            {
                Payload = payload,
            };

            return await client.UpdateDiscountAsync(request);
        }

        public static async Task<DeleteDiscountResponse> DeleteDiscountAsync(string productId, IOptions<DefaultConfig> config)
        {
            var client = GetClient(config);

            var request = new DeleteDiscountRequest
            {
                ProductId = productId,
            };

            return await client.DeleteDiscountAsync(request);
        }


        private static DiscountProtoServiceClient GetClient(IOptions<DefaultConfig> config)
        {
            var channel = GrpcChannel.ForAddress($"https://{config.Value.DiscountRpcHost}");
            var client = new DiscountProtoServiceClient(channel);
            return client;
        }
    }
}
