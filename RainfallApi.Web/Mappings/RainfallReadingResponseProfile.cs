using AutoMapper;
using RainfallApi.Core.Entities;
using RainfallApi.Web.Responses;
namespace RainfallApi.Application.Mappings
{
    public class RainfallReadingResponseProfile : Profile
    {
        public RainfallReadingResponseProfile()
        {
            CreateMap<RainfallReadingResponseModel, RainfallReadingResponse>().ReverseMap();
        }
    }
}
