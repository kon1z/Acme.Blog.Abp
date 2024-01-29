using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Application.Blog.Dto;
using Acme.Blog.Application.Blog.IAppServices;
using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Application.Blog.AppServices
{
    public class CategoryAppService(ICategoryRepository categoryRepository) : ApplicationService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto dto)
        {
            var category = new Category(dto.Name);
            var ans = await _categoryRepository.InsertAsync(category, true);
            return ObjectMapper.Map<Category, CategoryDto>(ans);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(Guid id, string name)
        {
            var category = await _categoryRepository.FindAsync(id);
            if (category == null)
            {
                throw new EntityNotFoundException(nameof(category));
            }
            category.Name = name;
            var ans = await _categoryRepository.UpdateAsync(category, true);
            return ObjectMapper.Map<Category, CategoryDto>(ans);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(r => r.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }
            await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var ans = await _categoryRepository.GetListAsync();
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(ans);
        }
    }
}
