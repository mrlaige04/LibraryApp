namespace Library.Data.EF.Entities
{
    public class Rating
    {
        public int id { get; set; }
        public int bookId { get; set; }
        public decimal score { get; set; }

        public Book Book { get; set; }
    }
}
