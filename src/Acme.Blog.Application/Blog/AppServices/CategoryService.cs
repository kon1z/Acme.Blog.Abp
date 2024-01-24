using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Blog.Dto;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IAppServices;
using Acme.Blog.Blog.IRepositories;
using Volo.Abp.Application.Services;

namespace Acme.Blog.Blog.AppServices
{
    public class CategoryService(ICategoryRepository categoryRepository) : ApplicationService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task CreateCategoryAsync(CategoryDto dto)
        {
            var category = ObjectMapper.Map<CategoryDto, Category>(dto);
            await _categoryRepository.InsertAsync(category);
        }
    }
}
