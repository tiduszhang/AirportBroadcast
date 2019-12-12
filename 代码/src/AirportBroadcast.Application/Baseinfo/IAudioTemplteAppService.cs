using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;
using System.Collections.Generic;
using System.Linq;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioTemplteAppService : ICrudAppService<AudioTemplteDto, int, GetAllAudioInputDto>
    {
        void UpdateDetail(AudioTemplteDetailInput input);

        void DeteleDetail(AudioTemplteDetailDelInput input);

        List<AudioConstDto> GetAllAudioConst();

    }

    [AbpAuthorize(AppPermissions.Pages_BaseInfo_AudioTemplte)]
    public class AudioTemplteAppService : CrudAppService<AudioTemplte, AudioTemplteDto, int, GetAllAudioInputDto>, IAudioTemplteAppService
    {
        private readonly IRepository<AudioTemplteDetail> detailRepository;
        private readonly IRepository<AudioConst> constRepository;

        public AudioTemplteAppService(IRepository<AudioTemplte> repository,
            IRepository<AudioConst> constRepository,
            IRepository<AudioTemplteDetail> detailRepository) : base(repository)
        {
            this.detailRepository = detailRepository;
            this.constRepository = constRepository;
        }

        protected override IQueryable<AudioTemplte> CreateFilteredQuery(GetAllAudioInputDto input)
        {
            var query = base.CreateFilteredQuery(input);
            if (input.LanguageId.HasValue)
                query = query.Where(x => x.LanguageId == input.LanguageId.Value);

            if (!string.IsNullOrEmpty(input.Code))
                query = query.Where(x => x.Type.Contains(input.Code));

            if (!string.IsNullOrEmpty(input.FileName))
                query = query.Where(x => x.Content.Contains(input.FileName));

            return query;


        }

        public override AudioTemplteDto Update(AudioTemplteDto input)
        {
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity == null) return null;
            entity.LanguageId = input.LanguageId;
            entity.Remark = input.Remark;
            entity.Type = input.Type;
            Repository.Update(entity);
            return entity.MapTo<AudioTemplteDto>();


        }
        public void UpdateDetail(AudioTemplteDetailInput input)
        {
            var bean = Repository.FirstOrDefault(input.Id);
            if (bean == null)
                throw new UserFriendlyException("未找到主模板！");

            var details = detailRepository.GetAll().Where(x => x.TemplteId == input.Id);

            foreach (var item in details)
            {
                item.Sort = item.Sort * 10;
            }
            input.Detail.Sort = input.Detail.Sort * 10 - 1;

            var curItem = input.Detail.MapTo<AudioTemplteDetail>();
            curItem.TemplteId = bean.Id;

            detailRepository.InsertOrUpdate(curItem);
            CurrentUnitOfWork.SaveChanges();
            details = detailRepository.GetAllIncluding(x=>x.AudioConst).Where(x => x.TemplteId == input.Id);
            var startIndx = 1;
            var context = string.Empty;

            details.OrderBy(x => x.Sort).ToList().ForEach(x =>
            {
                x.Sort = startIndx;
                //detailRepository.Update(x);
                startIndx++;
                context += x.IsParamter ? "[" +x.ParamterType.ToString()+ "]" : x.AudioConst.Content;
              
            });

            bean.Content = context;
           // Repository.Update(bean);
        }

        public void DeteleDetail(AudioTemplteDetailDelInput input)
        {
            var bean = Repository.FirstOrDefault(input.Id);
            if (bean == null)
                throw new UserFriendlyException("未找到主模板！");

            detailRepository.Delete(x => x.Id == input.Did);
            CurrentUnitOfWork.SaveChanges();
            var startIndx = 1;
            var context = string.Empty;
            var details = detailRepository.GetAllIncluding(x => x.AudioConst).Where(x => x.TemplteId == input.Id);
            details.OrderBy(x => x.Sort).ToList().ForEach(x =>
            {
                x.Sort = startIndx;
                startIndx++;
                context += x.IsParamter ? " [" + x.ParamterType.ToString() + "] " : x.AudioConst.Content;
            });

            bean.Content = context;
        }

        public List<AudioConstDto> GetAllAudioConst()
        {
            return constRepository.GetAll().ToList().MapTo<List<AudioConstDto>>();
        }

    }
}
