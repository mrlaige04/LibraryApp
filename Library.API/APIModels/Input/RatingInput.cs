using System.ComponentModel.DataAnnotations;

namespace Library.API.APIModels.Input
{
    public class RatingInput 
    {
        [Range(1,5)]
        public decimal rating { get; set; }
    }
}
