using AutoMapper;
using WritableRESTAPI.Entity;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Infrastructure.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>()
                .ReverseMap();
        }
    }
}
