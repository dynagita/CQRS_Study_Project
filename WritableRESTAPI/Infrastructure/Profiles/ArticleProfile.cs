using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WritableRESTAPI.Entity;
using WritableRESTAPI.ViewModel;

namespace WritableRESTAPI.Infrastructure.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleViewModel, Article>()
                .ReverseMap();
        }
    }
}
