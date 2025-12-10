namespace Application.Common
{
    public static class CachingKeys
    {
        public const string OrdersListKey = "orders";

        public static string GetOrderKey(Guid orderId) => $"orders:{orderId}";
    }
}
