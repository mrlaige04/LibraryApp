namespace Library.API.APIModels.Output
{
    public class BookRatingRevNumber
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public decimal rating { get; set; }
        public decimal reviewsNumber { get; set; }
        public string cover { get; set; }
    }
}
