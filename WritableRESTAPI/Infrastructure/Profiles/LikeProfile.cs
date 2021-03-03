using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Infrastructure.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<LikeViewModel, Like>()
                .ReverseMap();
        }
    }
}
