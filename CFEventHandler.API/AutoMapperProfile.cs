using AutoMapper;
using CFEventHandler.Models;
using CFEventHandler.Models.DTO;
using EventHandler = CFEventHandler.Models.EventHandler;

namespace CFEventHandler.API
{
    /// <summary>
    /// AutoMapper profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<APIKeyInstance, APIKeyInstanceDTO>().ReverseMap();

            CreateMap<EventClient, EventClientDTO>().ReverseMap();

            CreateMap<EventFilter, EventFilterDTO>().ReverseMap();

            CreateMap<EventHandler, EventHandlerDTO>().ReverseMap();

            CreateMap<EventInstance, EventInstanceDTO>().ReverseMap();

            CreateMap<EventParameter, EventParameterDTO>().ReverseMap();

            CreateMap<EventType, EventTypeDTO>().ReverseMap();

            //CreateMap<PagingInfo, PagingInfoDTO>().ReverseMap();
        }
    }
}
