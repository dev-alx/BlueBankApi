using AutoMapper;
using Contact.Core.Dto;
using Contact.Core.Enitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Client, ClientResponseDto>().ReverseMap();            
            CreateMap<AccountMovement, AccountMovementDto>()
                .ForMember(c => c.RecordDate, o => o.MapFrom(m => m.RecorDate))
                .ReverseMap();
        }
    }
}
