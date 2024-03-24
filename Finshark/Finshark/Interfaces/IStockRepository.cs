using Finshark.Dtos.Stock;
using Finshark.Models;

namespace Finshark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        //Thêm dấu chấm hỏi để có khi trả về kiểu NULL vì FirstOrDefault có thể trả về NULL
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
