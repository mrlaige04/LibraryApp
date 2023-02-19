using AutoMapper;
using Library.API.APIModels.Input;
using Library.API.APIModels.Output;
using Library.Data.EF.Entities;

namespace Library.API
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BookInput, Book>();
            CreateMap<ReviewInput, Review>();
            CreateMap<RatingInput, Rating>();

            
            
            CreateMap<Book, BookRatingRevNumber>()
                .ForMember(x => x.title, opt => opt.MapFrom(x => x.title))
                .ForMember(x => x.author, opt => opt.MapFrom(x => x.author))
                .ForMember(x => x.rating, opt => opt.MapFrom(x => x.Ratings.Count() > 0 ? x.Ratings.ToList().Average(r => r.score) : 0))
                .ForMember(x => x.reviewsNumber, opt => opt.MapFrom(x => x.Reviews.Count));


            CreateMap<Review, ReviewOutput>();

            CreateMap<Book, FullBookDetail>()
                .ForMember(x => x.id, opt => opt.MapFrom(x => x.id))
                .ForMember(x => x.title, opt => opt.MapFrom(x => x.title))
                .ForMember(x => x.author, opt => opt.MapFrom(x => x.author))
                .ForMember(x => x.cover, opt => opt.MapFrom(x => x.cover))
                .ForMember(x => x.content, opt => opt.MapFrom(x => x.content))
                .ForMember(x => x.genre, opt => opt.MapFrom(x => x.genre))
                .ForMember(x => x.reviews, opt => opt.MapFrom(x => x.Reviews));
        }
    }
}
