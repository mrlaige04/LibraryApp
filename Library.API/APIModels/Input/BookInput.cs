using System.ComponentModel.DataAnnotations;

namespace Library.API.APIModels.Input
{
    public class BookInput
    { 
        public int id { get; set; }
        [MinLength(1, ErrorMessage = "Title must have at least 1 character")]
        public string title { get; set; }
        [MinLength(1, ErrorMessage = "Author must have at least 1 character")]
        public string author { get; set; } 
        
        [RegularExpression("^data:image\\/(jpeg|jpg|png);base64,(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?",
            ErrorMessage ="Invalid base64 image")]
        public string cover { get; set; }
        [MinLength(1, ErrorMessage = "Content must have at least 1 character")]
        public string content { get; set; }
        [MinLength(1, ErrorMessage = "Genre must have at least 1 character")]
        public string genre { get; set; }
    }
}
