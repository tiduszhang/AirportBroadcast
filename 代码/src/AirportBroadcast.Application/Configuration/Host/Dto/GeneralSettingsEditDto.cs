using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace AirportBroadcast.Configuration.Host.Dto
{
    public class GeneralSettingsEditDto
    {
        public string Timezone { get; set; }

        /// <summary>
        /// This value is only used for comparing user's timezone to default timezone
        /// </summary>
        public string TimezoneForComparison { get; set; }

        public string PlayWay { get; set; }
                  
        public int AutoPlayGetLuaTimeSpan { get; set; }

        public int PlayTimes { get; set; }

        public List<ChooseLanguages> CanPlayLanguages { get; set; }

        public int UnActiveLimitTime { get; set; }


    } 

    public class ChooseLanguages
    {
        public int Id { get; set; }

        public bool IsChecked { get; set; }

        public string LanguageName { get; set; }
    }
}