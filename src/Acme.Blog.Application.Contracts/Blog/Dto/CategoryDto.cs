using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.Blog.Blog.Dto
{
    public class CategoryDto : EntityDto
    {
        public string Name { get; set; }
    }
}
