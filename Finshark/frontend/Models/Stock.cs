using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace frontend.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty; //string.Empty để không bị lỗi null
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")] //Định dạng tiền chỉ có tối đa 18 số và có tối đa 2 dấu phẩy
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")] //Định dạng tiền chỉ có tối đa 18 số và có tối đa 2 dấu phẩy
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
    }
}
