using AutoMapper;
using AirportBroadcast.Authorization.Users;
using AirportBroadcast.Authorization.Users.Dto;
using AirportBroadcast.Domain.baseinfo;
using AirportBroadcast.Baseinfo.dtos;
using AirportBroadcast.Domain.playSets;
using AirportBroadcast.AudioSets.dtos;
using AirportBroadcast.Domain.activeMq;
using System;
using AirportBroadcast.ActiveMQ.Dto;

namespace AirportBroadcast
{
    internal static class CustomDtoMapper
    {
        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();

        public static void CreateMappings(IMapperConfigurationExpression mapper)
        {
            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal(mapper);

                _mappedBefore = true;
            }
        }

        private static void CreateMappingsInternal(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());

            mapper.CreateMap<AudioDigit, AudioDigitDto>()
                   .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioDigitDto, AudioDigit>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());
            //
            mapper.CreateMap<AudioAirLine, AudioAirLineDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioAirLineDto, AudioAirLine>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());
            //
            mapper.CreateMap<AudioCheckIn, AudioCheckInDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioCheckInDto, AudioCheckIn>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioCity, AudioCityDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioCityDto, AudioCity>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());
            //

            mapper.CreateMap<AudioConst, AudioConstDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioConstDto, AudioConst>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());
            //

            mapper.CreateMap<AudioGate, AudioGateDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioGateDto, AudioGate>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioHour, AudioHourDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioHourDto, AudioHour>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioMinite, AudioMiniteDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioMiniteDto, AudioMinite>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioReason, AudioReasonDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioReasonDto, AudioReason>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioTurnPlate, AudioTurnPlateDto>()
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioTurnPlateDto, AudioTurnPlate>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());

            //
            mapper.CreateMap<AudioTemplte, AudioTemplteDto>()
               .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioLanguage.Name));

            mapper.CreateMap<AudioTemplteDto, AudioTemplte>()
                 .ForMember(entity => entity.AudioLanguage, options => options.Ignore());
            //  .ForMember(entity => entity.Details, options => options.Ignore());

            //
            mapper.CreateMap<AudioTemplteDetail, AudioTemplteDetailDto>();

            mapper.CreateMap<AudioTemplteDetailDto, AudioTemplteDetail>()
                 .ForMember(entity => entity.AudioTemplte, options => options.Ignore());



            //硬件设置 ，声卡、电源控制器端口

            mapper.CreateMap<AudioDevice, AudioDeviceDto>();

            mapper.CreateMap<AudioDeviceDto, AudioDevice>()
                  .ForMember(entity => entity.AudioPlaySets, options => options.Ignore());

            mapper.CreateMap<TopPwrPort, TopPwrPortDto>();

            mapper.CreateMap<TopPwrPortDto, TopPwrPort>()
                  .ForMember(entity => entity.CnAudioPlaySets, options => options.Ignore())
                  .ForMember(entity => entity.EnAudioPlaySets, options => options.Ignore());

            //播放设置
            mapper.CreateMap<AudioPlaySet, AudioPlaySetDto>();

            mapper.CreateMap<AudioPlaySetDto, AudioPlaySet>()
                  .ForMember(entity => entity.Templtes, options => options.Ignore())
                  .ForMember(entity => entity.AudioDevices, options => options.Ignore())
                   .ForMember(entity => entity.CnTopPwrPorts, options => options.Ignore())
                  .ForMember(entity => entity.EnTopPwrPorts, options => options.Ignore());

            mapper.CreateMap<AirShowDataDto, AirshowData>()
                .ForMember(entity => entity.Dlytime, options => options.MapFrom(src =>string.IsNullOrEmpty( src.Dlytime) ? DateTime.Parse("2000-01-01"):DateTime.ParseExact(src.Dlytime, "HHmm", System.Globalization.CultureInfo.CurrentCulture))
)
                .ForMember(entity => entity.FlightDateTime, options => options.MapFrom(src => src.FlightDateTime.HasValue ? src.FlightDateTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.ExecutionDateTime, options => options.MapFrom(src => src.ExecutionDateTime.HasValue ? src.ExecutionDateTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.DepPlanTime, options => options.MapFrom(src => src.DepPlanTime.HasValue ? src.DepPlanTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.DepForecastTime, options => options.MapFrom(src => src.DepForecastTime.HasValue ? src.DepForecastTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.DepartTime, options => options.MapFrom(src => src.DepartTime.HasValue ? src.DepartTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.ArrPlanTime, options => options.MapFrom(src => src.ArrPlanTime.HasValue ? src.ArrPlanTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.ArrForecastTime, options => options.MapFrom(src => src.ArrForecastTime.HasValue ? src.ArrForecastTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.ArriveTime, options => options.MapFrom(src => src.ArriveTime.HasValue ? src.ArriveTime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.Gateopentime, options => options.MapFrom(src => src.Gateopentime.HasValue ? src.Gateopentime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.Gateclosetime, options => options.MapFrom(src => src.Gateclosetime.HasValue ? src.Gateclosetime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.CheckinTimeStart, options => options.MapFrom(src => src.CheckinTimeStart.HasValue ? src.CheckinTimeStart.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.CheckinTimeEnd, options => options.MapFrom(src => src.CheckinTimeEnd.HasValue ? src.CheckinTimeEnd.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.Xlsjtime, options => options.MapFrom(src => src.Xlsjtime.HasValue ? src.Xlsjtime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.Xlxjtime, options => options.MapFrom(src => src.Xlxjtime.HasValue ? src.Xlxjtime.Value : DateTime.Parse("2000-01-01")))
                .ForMember(entity => entity.ReciveTime, options => options.MapFrom(src => DateTime.Now))
;
            mapper.CreateMap<AirshowData, AirShowDataDto>()
                 .ForMember(dto => dto.DlytimeStr, options => options.MapFrom(src => src.Dlytime.Ticks== DateTime.Parse("2000-01-01").Ticks?"" : src.Dlytime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.FlightDateTimeStr, options => options.MapFrom(src => src.FlightDateTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.FlightDateTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.ExecutionDateTimeStr, options => options.MapFrom(src => src.ExecutionDateTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.ExecutionDateTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.DepPlanTimeStr, options => options.MapFrom(src => src.DepPlanTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.DepPlanTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.DepForecastTimeStr, options => options.MapFrom(src => src.DepForecastTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.DepForecastTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.DepartTimeStr, options => options.MapFrom(src => src.DepartTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.DepartTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.ArrPlanTimeStr, options => options.MapFrom(src => src.ArrPlanTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.ArrPlanTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.ArrForecastTimeStr, options => options.MapFrom(src => src.ArrForecastTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.ArrForecastTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.ArriveTimeStr, options => options.MapFrom(src => src.ArriveTime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.ArriveTime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.GateopentimeStr, options => options.MapFrom(src => src.Gateopentime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.Gateopentime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.GateclosetimeStr, options => options.MapFrom(src => src.Gateclosetime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.Gateclosetime.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.CheckinTimeStartStr, options => options.MapFrom(src => src.CheckinTimeStart.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.CheckinTimeStart.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.CheckinTimeEndStr, options => options.MapFrom(src => src.CheckinTimeEnd.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.CheckinTimeEnd.ToString("yyyy-MM-dd HH:mm")))
                 .ForMember(dto => dto.XlsjtimeStr, options => options.MapFrom(src => src.Xlsjtime.Ticks == DateTime.Parse("2000-01-01").Ticks ? "" : src.Xlsjtime.ToString("yyyy-MM-dd HH:mm")))
                
                 .ForMember(dto => dto.GateOld, options => options.Ignore())
                  ;

            mapper.CreateMap<AudioPlaySetTemple, AudioPlaySetTempleDto>()
                  .ForMember(dto => dto.Tid, options => options.MapFrom(src => src.Id))
                  .ForMember(dto => dto.Type, options => options.MapFrom(src => src.AudioTemplte.Type))
                  .ForMember(dto => dto.Sort, options => options.MapFrom(src => src.Sort))
                  .ForMember(dto => dto.Remark, options => options.MapFrom(src => src.AudioTemplte.Remark))
                  .ForMember(dto => dto.LanguageId, options => options.MapFrom(src => src.AudioTemplte.LanguageId))
                  .ForMember(dto => dto.Id, options => options.MapFrom(src => src.AudioTemplte.Id))
                  .ForMember(dto => dto.Content, options => options.MapFrom(src => src.AudioTemplte.Content))
                  .ForMember(dto => dto.AudioLanguageName, options => options.MapFrom(src => src.AudioTemplte.AudioLanguage.Name));

        }
    }
}