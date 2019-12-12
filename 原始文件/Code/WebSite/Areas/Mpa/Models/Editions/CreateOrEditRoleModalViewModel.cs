using Abp.AutoMapper;
using AirportBroadcast.Editions.Dto;
using AirportBroadcast.Web.Areas.Mpa.Models.Common;

namespace AirportBroadcast.Web.Areas.Mpa.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionForEditOutput))]
    public class CreateOrEditEditionModalViewModel : GetEditionForEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode
        {
            get { return Edition.Id.HasValue; }
        }

        public CreateOrEditEditionModalViewModel(GetEditionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}