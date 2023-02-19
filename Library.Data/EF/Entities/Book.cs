using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.EF.Entities
{
    [Table("Books")]
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string cover { get; set; }
        public string content { get; set; }
        public string genre { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
