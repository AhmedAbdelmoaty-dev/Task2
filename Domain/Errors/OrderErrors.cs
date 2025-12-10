namespace Domain.Errors
{
    public sealed record  OrderErrors 
    {

        public static readonly Error NotFound =
            new("The order was not found.", "ORDER_NOT_FOUND");
    }
}
