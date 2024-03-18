namespace Finshark.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
        //Thuộc tính điều hướng, giúp cho việc cho tới các thuộc tính trong model Stock
        public Stock? Stock { get; set; }
    }
}
