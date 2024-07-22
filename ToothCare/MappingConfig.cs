using AutoMapper;
using ToothCare.Models.DTO;

namespace ToothCare
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DoctorsDTO, DoctorsCreateDTO>().ReverseMap();
            CreateMap<DoctorsDTO, DoctorsUpdateDTO>().ReverseMap();

            CreateMap<ClientsDTO, ClientsCreateDTO>().ReverseMap();
            CreateMap<ClientsDTO, ClientsUpdateDTO>().ReverseMap();

            CreateMap<AppointmentsDTO, AppointmentsCreateDTO>().ReverseMap();
            CreateMap<AppointmentsDTO, AppointmentsCreateDTO>().ReverseMap();

            CreateMap<ProceduresDTO, ProceduresCreateDTO>().ReverseMap();
            CreateMap<ProceduresDTO, ProceduresCreateDTO>().ReverseMap();
        }
        
     
    }
}
