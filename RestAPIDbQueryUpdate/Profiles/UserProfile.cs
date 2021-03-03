using AutoMapper;
using RestAPIDbQueryUpdate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIDbQueryUpdate.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, object>()
                .ReverseMap();
        }
    }
}
