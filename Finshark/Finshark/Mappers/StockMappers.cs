using Finshark.Dtos.Stock;
using Finshark.Models;

namespace Finshark.Mappers
{
    public static class StockMappers
    {
        /*Phương thức này như là kiểu trung gian, nó đưa dữ liệu từ các object (CSDL thật từ SQL Server) vào các object tự tạo (DTO) nhằm hiển thị ra các
        các thuộc tính mà mình mong muốn*/
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }

        /*Phương thức này cũng giống kiểu trung gian, nhưng khi tạo mới một object thì nó phải được lưu vô CSDL thật, chứ không được lưu vô object tự
        tạo (DTO) */
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap  =stockDto.MarketCap
            };
        }
    }
}