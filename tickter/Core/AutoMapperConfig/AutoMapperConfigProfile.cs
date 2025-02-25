using AutoMapper;
using tickter.Core.DTO;
using tickter.Core.Entities;

namespace tickter.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile: Profile 
    {
        public AutoMapperConfigProfile()
        {
            // Tickets 
            CreateMap<CreateTicketDto, Ticket>();
            CreateMap<Ticket, GetTicketDto>();
            CreateMap<UpdateTicketDto, Ticket>();
        }
    }
}
