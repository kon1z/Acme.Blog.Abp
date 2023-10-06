using Acme.Blog.AppServices;
using Acme.Blog.Dtos;
using Acme.Blog.Entities;
using AutoMapper;

namespace Acme;

public class BlogApplicationAutoMapperProfile : Profile
{
    public BlogApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"{src.Content.Substring(0, 299)}..."));
        CreateMap<Article, ArticleDetailDto>();
    }
}