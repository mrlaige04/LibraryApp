using System.ComponentModel.DataAnnotations;

namespace Library.API.APIModels.Input
{
    public class BookInput
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; } 
        
        [RegularExpression("^data:image\\/[a-zA-Z]+;base64,([^\\s]+)$",
            ErrorMessage ="Invalid base64 image")]
        public string cover { get; set; }
        public string content { get; set; }
        public string genre { get; set; }
    }
}
