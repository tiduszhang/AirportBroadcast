using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using AirportBroadcast.AudioSets.dtos;
using AirportBroadcast.Authorization;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Domain.playSets;
using System.Collections.Generic;
using System.Linq;

namespace AirportBroadcast.Baseinfo
{
    public interface IAudioPlaySetAppService : ICrudAppService<AudioPlaySetDto>
    {
        List<ChooseDeviceDto> GetDeviceList(EntityDto input);
        void UpdateDeviceList(UpdateDeviceListInputDto input);

        List<ChooseTopPwrPortDto> GetTopPwrPortList(UpdateDeviceListInputDto input);
        void UpdateTopPwrPortList(UpdateDeviceListInputDto input);

        List<AudioPlaySetTempleDto> GetTempleList(EntityDto input);

        void AddTempleList(AudioPlaySetTempleInputDto input);

        void DeleteTempleList(EntityDto input);

        List<AudioTemplteBaseDto> GetAllTemplteList();
    }

    [AbpAuthorize]
    public class AudioPlaySetAppService : CrudAppService<AudioPlaySet, AudioPlaySetDto>, IAudioPlaySetAppService
    {
        private readonly IRepository<AudioDevice> deviceRepository;
        private readonly IRepository<TopPwrPort> topPwrRepository;
        private readonly IRepository<AudioPlaySetTemple> templteRepository;
        private readonly IRepository<AudioTemplte> tRepository;

        public AudioPlaySetAppService(IRepository<AudioPlaySet> repository,
            IRepository<AudioDevice> deviceRepository,
            IRepository<TopPwrPort> topPwrRepository,
            IRepository<AudioPlaySetTemple> templteRepository,
            IRepository<AudioTemplte> tRepository
            ) : base(repository)
        {
            this.deviceRepository = deviceRepository;
            this.topPwrRepository = topPwrRepository;
            this.templteRepository = templteRepository;
            this.tRepository = tRepository;


        }
        #region 声卡选择和设置
        public List<ChooseDeviceDto> GetDeviceList(EntityDto input)
        {
            var result = new List<ChooseDeviceDto>();

            var dids = new List<int>();
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity != null && entity.AudioDevices != null)
            {
                entity.AudioDevices.ForEach(x => { dids.Add(x.Id); });

            }

            deviceRepository.GetAll().ToList().ForEach(item =>
            {
                var dto = new ChooseDeviceDto
                {
                    Id = item.Id,
                    IsChoose = dids.Contains(item.Id),
                    Name = item.Name,
                    Remark = item.Remark,
                    Code = item.Code
                };
                result.Add(dto);

            });

            return result;
        }

        public void UpdateDeviceList(UpdateDeviceListInputDto input)
        {
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity == null) throw new UserFriendlyException("未找到此条记录！");

            if (entity.AudioDevices != null)
            {
                entity.AudioDevices.Clear();
            }
            else
            {
                entity.AudioDevices = new List<AudioDevice>();
            }

            var entitys = deviceRepository.GetAll().Where(x => input.Dids.Contains(x.Id)).ToList();

            entity.AudioDevices.AddRange(entitys);
        }
        #endregion

        #region 电源端口管理

        public List<ChooseTopPwrPortDto> GetTopPwrPortList(UpdateDeviceListInputDto input)
        {
            var result = new List<ChooseTopPwrPortDto>();

            var dids = new List<int>();
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity != null)
            {
                if (input.DType==1 && entity.CnTopPwrPorts != null)
                {
                    entity.CnTopPwrPorts.ForEach(x => { dids.Add(x.Id); });
                }
                if (input.DType == 2 && entity.EnTopPwrPorts != null)
                {
                    entity.EnTopPwrPorts.ForEach(x => { dids.Add(x.Id); });
                }
            }

            topPwrRepository.GetAll().ToList().ForEach(item =>
            {
                var dto = new ChooseTopPwrPortDto
                {
                    Id = item.Id,
                    IsChoose = dids.Contains(item.Id),
                    Name = item.Name,
                    Remark = item.Remark,
                    Code = item.Code
                };
                result.Add(dto);

            });

            return result;
        }

        public void UpdateTopPwrPortList(UpdateDeviceListInputDto input)
        {
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity == null) throw new UserFriendlyException("未找到此条记录！");
            if (input.DType == 1)
            {
                if (entity.CnTopPwrPorts != null)
                {
                    entity.CnTopPwrPorts.Clear();
                }
                else
                {
                    entity.CnTopPwrPorts = new List<TopPwrPort>();
                }

                var entitys = topPwrRepository.GetAll().Where(x => input.Dids.Contains(x.Id)).ToList();

                entity.CnTopPwrPorts.AddRange(entitys);
            }else if (input.DType == 2)
            {
                if (entity.EnTopPwrPorts != null)
                {
                    entity.EnTopPwrPorts.Clear();
                }
                else
                {
                    entity.EnTopPwrPorts = new List<TopPwrPort>();
                }

                var entitys = topPwrRepository.GetAll().Where(x => input.Dids.Contains(x.Id)).ToList();

                entity.EnTopPwrPorts.AddRange(entitys);

            }
           

        }
        #endregion

        #region 模版管理

        public List<AudioPlaySetTempleDto> GetTempleList(EntityDto input)
        {
            var dids = new List<int>();
            var entity = Repository.FirstOrDefault(input.Id);
            return entity.Templtes.MapTo<List<AudioPlaySetTempleDto>>();
        }


        public void AddTempleList(AudioPlaySetTempleInputDto input)
        {
            var entity = Repository.FirstOrDefault(input.Id);
            if (entity == null) throw new UserFriendlyException("未找到此条记录！");

            if (entity.Templtes == null)
            {
                entity.Templtes = new List<AudioPlaySetTemple>();
            }
             
            entity.Templtes.Add(new AudioPlaySetTemple() {
                TempleId = input.TempId,
                Sort = input.Sort
            }); 
        }

        public void DeleteTempleList(EntityDto input)
        {
            templteRepository.Delete(input.Id);                
        }

        public List<AudioTemplteBaseDto> GetAllTemplteList()
        {
           return  tRepository.GetAll().ToList().MapTo<List<AudioTemplteBaseDto>>();

        }

        #endregion
    }
}
