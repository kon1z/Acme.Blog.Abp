using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Application.Blog.Dto;
using Acme.Blog.Application.Blog.IAppServices;
using Acme.Blog.Domain.Blog.Entities;
using Acme.Blog.Domain.Blog.IRepositories;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Application.Blog.AppServices
{
    public class LableAppService(ILabelRepository labelRepository) : BlogAppServiceBase, ILableService
    {
        private readonly ILabelRepository _labelRepository = labelRepository;

        public async Task<LableDto> AddLable(LableDto dto)
        {
            var lable = new Label(dto.Name);
            var ans = await _labelRepository.InsertAsync(lable);
            return ObjectMapper.Map<Label, LableDto>(ans);
        }

        public async Task<LableDto> UpdateLable(LableDto dto)
        {
            var lable = await _labelRepository.FirstOrDefaultAsync(r => r.Id == dto.Id && !r.IsDeleted);
            if (lable == null)
            {
                throw new EntityNotFoundException(nameof(lable));
            }

            var ans = await _labelRepository.UpdateAsync(lable);
            return ObjectMapper.Map<Label, LableDto>(ans);
        }

        public async Task DeleteLable(Guid id)
        {
            var lable = await _labelRepository.FirstOrDefaultAsync(r => r.Id == id);
            if (lable == null)
            {
                throw new EntityNotFoundException(nameof(lable));
            }

            await _labelRepository.DeleteAsync(lable, true);
        }

        public async Task<List<LableDto>> GetLables(LableDto dto)
        {
            var lables = await _labelRepository.GetListAsync();
            return ObjectMapper.Map<List<Label>, List<LableDto>>(lables);
        }
    }
}
