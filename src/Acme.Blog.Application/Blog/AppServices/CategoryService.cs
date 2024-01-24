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
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.AppServices
{
    public class CategoryService(ICategoryRepository categoryRepository) : ApplicationService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public async Task CreateCategoryAsync(CategoryDto dto)
        {
            var category = ObjectMapper.Map<CategoryDto, Category>(dto);
            await _categoryRepository.InsertAsync(category,true);
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, string name)
        {
            var category = await _categoryRepository.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            category.Name = name;
            await _categoryRepository.UpdateAsync(category,true);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.FirstOrDefaultAsync(r=>r.Id == id);
            if (category == null)
            {
                return false;
            }
            await _categoryRepository.DeleteAsync(category,true);
            return true;
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var ans = await _categoryRepository.GetListAsync();
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(ans);
        }
    }
}
