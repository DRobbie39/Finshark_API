using Finshark.Data;
using Finshark.Dtos.Stock;
using Finshark.Interfaces;
using Finshark.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Controllers
{
    [Route("finshark/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase 
    {
        private readonly ApplicationDBContext _context; //Tăng khả năng bảo mật để tránh các tác nhân ngoài thay đổi CSDL
        private readonly IStockRepository _stockRepo;
        //Constructor khởi tạo CSDL
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
            _context = context;
        }

        //Phương thức trả về tất cả đối tượng theo dạng danh sách
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Select(s => s.ToStockDto()) sẽ trả về các mảng không thay đổi hoặc các list không thay đổi
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        //Phương thức chỉ lấy một đối tượng lấy theo id
        [HttpGet("{id}")] //Trích xuất chuỗi id
        public async Task<IActionResult> GetById([FromRoute] int id) //Xong điền vào tham số rồi thực thi đoạn code ở dưới
        {
            var stock = await _context.Stocks.FindAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        //FromBody để đưa dữ liệu JSON vào thân HTTP, chứ không đưa bằng URL
        //CreateStockRequest stockDto là phương thức DTO cho người dùng nhập dữ liệu cần thiết, tránh nhập nhiều dữ liệu không cần thiết
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) //FromBody để đưa dữ liệu JSON vào thân HTTP, chứ không đưa bằng URL
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            //Chạy vào phương thức GetById để lấy Id sau đó tạo mới Id và chạy ToStockDto
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(stockModel == null)
            {
                return NotFound();
            }

            //Update thẳng vào đối tượng CSDL mà kh cần trừu tượng hóa nó
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(stockModel == null)
            {
                return NotFound();
            }

            //Không thêm await do Remove() không cho
            _context.Stocks.Remove(stockModel);

            await _context.SaveChangesAsync();

            //NoContent() để cho biết việc xóa đã thành công
            return NoContent();
        }
    }
}