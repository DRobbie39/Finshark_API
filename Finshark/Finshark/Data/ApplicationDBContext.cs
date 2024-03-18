using Finshark.Models; //Dùng các lớp models
using Microsoft.EntityFrameworkCore; //Dùng entity framework

namespace Finshark.Data
{
    public class ApplicationDBContext : DbContext
    {
        //Tạo database sau khi tìm xong các bảng ở dưới
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base (dbContextOptions)
        {

        }

        //Phương thức lấy dữ liệu từ bảng Stock trong CSDL
        public DbSet<Stock> Stocks {  get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
