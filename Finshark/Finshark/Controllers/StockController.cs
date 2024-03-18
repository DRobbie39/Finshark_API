using Finshark.Data;
using Finshark.Dtos.Stock;
using Finshark.Mappers;
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
            //Select(s => s.ToStockDto()) sẽ trả về các mảng không thay đổi hoặc các list không thay đổi
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());

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

            return Ok(stock.ToStockDto());
        }

        //FromBody để đưa dữ liệu JSON vào thân HTTP, chứ không đưa bằng URL
        //CreateStockRequest stockDto là phương thức DTO cho người dùng nhập dữ liệu cần thiết, tránh nhập nhiều dữ liệu không cần thiết
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto) //FromBody để đưa dữ liệu JSON vào thân HTTP, chứ không đưa bằng URL
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            //Chạy vào phương thức GetById để lấy Id sau đó tạo mới Id và chạy ToStockDto
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }
    }
}
