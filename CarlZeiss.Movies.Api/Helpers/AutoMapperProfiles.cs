using AutoMapper;
using CarlZeiss.Movies.Api.Dtos;
using CarlZeiss.Movies.Api.Models;

namespace CarlZeiss.Movies.Api.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>().ReverseMap();

            CreateMap<UserBookingDto, Booking>();
            CreateMap<Booking, UserBookingReturnDto>();

            CreateMap<SeatMaster, SeatDto>()
                .ForMember(dest => dest.SeatId, opt => opt
               .MapFrom(src => src.Id))
               .ReverseMap();
            CreateMap<SeatDto, BookedSeat>()
                .ReverseMap();

            CreateMap<Show, ShowDetailsReturnDto>().ReverseMap();
            CreateMap<ShowDetailsDto, Show>().ReverseMap();
            CreateMap<ShowDetailsDto, Movie>().ReverseMap();

        }
    }
}
