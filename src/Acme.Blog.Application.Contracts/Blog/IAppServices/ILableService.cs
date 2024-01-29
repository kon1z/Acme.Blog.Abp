﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Blog.Dto;

namespace Acme.Blog.Blog.IAppServices
{
    public interface ILableService
    {
        Task<LableDto> AddLable(LableDto dto);
        Task<LableDto> UpdateLable(LableDto dto);
        Task DeleteLable(Guid id);
        Task<List<LableDto>> GetLables(LableDto dto);
    }
}
