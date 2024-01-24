using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Blog.Blog.Dto;
using Acme.Blog.Blog.Entities;
using Acme.Blog.Blog.IAppServices;
using Acme.Blog.Blog.IRepositories;
using Volo.Abp.Domain.Repositories;

namespace Acme.Blog.Blog.AppServices
{
    public class LableService(ILabelRepository labelRepository) : BlogAppServiceBase, ILableService
    {
        private readonly ILabelRepository _labelRepository = labelRepository;

        public async Task AddAndUpdateLable(LableDto dto)
        {
            var lable = ObjectMapper.Map<LableDto, Label>(dto);
            if (dto.Id == default)
            {
                
                await _labelRepository.InsertAsync(lable, true);
                return;
            }

            lable = await _labelRepository.FirstOrDefaultAsync(r => r.Id == dto.Id);
            if(lable == null)
            {
                return;
            }

            lable.Name = dto.Name;

            await _labelRepository.UpdateAsync(lable, true);


        }

        public async Task<bool> DeleteLable(Guid id)
        {
            var lable = await _labelRepository.FirstOrDefaultAsync(r => r.Id == id);
            if (lable == null)
            {
                return false;
            }

            await _labelRepository.DeleteAsync(lable, true);
            return true;
        }

        public async Task<List<LableDto>> GetLables(LableDto dto)
        {
            var lables = await _labelRepository.GetListAsync();
            return ObjectMapper.Map<List<Label>, List<LableDto>>(lables);
        }
    }
}
