namespace Order.API.Requests
{
    public record CreateOrderRequest(string CustomerName,string Product,decimal Price);

}
