namespace Template_restaurant_app.Application.Dtos.Metrics
{
    public class ReturnTotalAmountSoldOfProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalAmountSold { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
