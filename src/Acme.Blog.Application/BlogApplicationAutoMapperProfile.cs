using Acme.Blog.Application.Blog.Dto;
using Acme.Blog.Domain.Blog.Entities;
using AutoMapper;

namespace Acme.Blog;

public class BlogApplicationAutoMapperProfile : Profile
{
	public BlogApplicationAutoMapperProfile()
	{
		CreateMap<Article, ArticleDto>();
		CreateMap<Article, ArticleDetailDto>()
			.ForMember(dest => dest.Content,
				opt =>
					opt.MapFrom(src => src.Content != null ? src.Content.Content : string.Empty));
	}
}