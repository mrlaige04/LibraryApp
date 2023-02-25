namespace Library.API.APIModels.Output
{
    public class FullBookDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string cover { get; set; }
        public string content { get; set; }
        public string genre { get; set; }
        public decimal rating { get; set; }
        public List<ReviewOutput> reviews { get; set; } = new List<ReviewOutput>();
    }
}
