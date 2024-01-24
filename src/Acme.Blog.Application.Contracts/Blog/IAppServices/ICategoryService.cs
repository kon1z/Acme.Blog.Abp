﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Blog.Dto;
using Volo.Abp.Application.Services;

namespace Acme.Blog.Blog.IAppServices
{
    public interface ICategoryService : IApplicationService
    {
        Task CreateCategoryAsync(CategoryDto dto);
        Task<bool> UpdateCategoryAsync(Guid id, string name);
        Task<bool> DeleteCategoryAsync(Guid id);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
    }
}