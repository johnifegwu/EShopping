
using Basket.Application.Configurations;
using Discount.Grpc.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.Application.RpcClients
{
    public class DiscountRpcClient
    {
        public static async Task<DiscountModel> GetDiscountAsync(string productId, DiscountProtoServiceClient client)
        {
            var request = new GetDiscountRequest
            {
                ProductId = productId,
            };

            return await client.GetDiscountAsync(request);
        }

        public static async Task<DiscountModel> CreateDiscountAsync(CreateDiscountModel payload, DiscountProtoServiceClient client)
        {
            var request = new CreateDiscountRequest
            {
                Payload = payload,
            };

            return await client.CreateDiscountAsync(request);
        }

        public static async Task<DiscountModel> UpdateDiscountAsync(DiscountModel payload, DiscountProtoServiceClient client)
        {
            var request = new UpdateDiscountRequest
            {
                Payload = payload,
            };

            return await client.UpdateDiscountAsync(request);
        }

        public static async Task<DeleteDiscountResponse> DeleteDiscountAsync(string productId, DiscountProtoServiceClient client)
        {
            var request = new DeleteDiscountRequest
            {
                ProductId = productId,
            };

            return await client.DeleteDiscountAsync(request);
        }
    }
}
