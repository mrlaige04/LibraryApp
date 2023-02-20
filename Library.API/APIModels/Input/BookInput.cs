using System.ComponentModel.DataAnnotations;

namespace Library.API.APIModels.Input
{
    public class BookInput
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; } 
        
        [RegularExpression("data:image\\/(png|jpeg|gif);base64,(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?",
            ErrorMessage ="Invalid base64 image")]
        public string cover { get; set; }
        public string content { get; set; }
        public string genre { get; set; }
    }
}
