namespace net6Api.Models
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public int AuthorId { get; set; }
        public author author { get; set; }
    }
}
