namespace Library.API.APIModels.Output
{
    public class Recommend
    {
        public List<BookRatingRevNumber> books { get; set; } = new List<BookRatingRevNumber>();
    }
}
