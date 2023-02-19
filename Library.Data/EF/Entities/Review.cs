namespace Library.Data.EF.Entities
{
    public class Review
    {
        public int id { get; set; }
        public int bookId { get; set; }
        public string message { get; set; }
        public string reviewer { get; set; }

        public Book Book { get; set; }
    }
}
