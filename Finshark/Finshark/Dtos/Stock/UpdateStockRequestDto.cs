namespace Finshark.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        public string Symbol { get; set; } = string.Empty; //string.Empty để không bị lỗi null
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
