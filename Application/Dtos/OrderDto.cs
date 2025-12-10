namespace Application.Dtos
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
    }
}
