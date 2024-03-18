using Finshark.Data;
using Microsoft.AspNetCore.Mvc;

namespace Finshark.Controllers
{
    [Route("finshark/stock")]
    [ApiController]
    public class StockController : ControllerBase 
    {
        private readonly ApplicationDBContext _context; //Tăng khả năng bảo mật để tránh các tác nhân ngoài thay đổi CSDL
        //Constructor khởi tạo CSDL
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        //Phương thức trả về tất cả đối tượng theo dạng danh sách
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList();

            return Ok(stocks);
        }

        //Phương thức chỉ lấy một đối tượng lấy theo id
        [HttpGet("{id}")] //Trích xuất chuỗi id
        public IActionResult GetById([FromRoute] int id) //Xong điền vào tham số rồi thực thi đoạn code ở dưới
        {
            var stock = _context.Stocks.Find(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }
    }
}
