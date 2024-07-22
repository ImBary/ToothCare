using AutoMapper;
using ToothCareAPI.Model;
using ToothCareAPI.Model.DTO;

namespace ToothCareAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //CLIENTS
            CreateMap<Clients, ClientsDTO>().ReverseMap();
            CreateMap<Clients, ClientsCreateDTO>().ReverseMap();
            CreateMap<Clients, ClientsUpdateDTO>().ReverseMap();

            //DOCTORS
            CreateMap<Doctors, DoctorsDTO>().ReverseMap();
            CreateMap<Doctors, DoctorsCreateDTO>().ReverseMap();
            CreateMap<Doctors, DoctorsUpdateDTO>().ReverseMap();

            //Appointments
            CreateMap<Appointments, AppointmentsDTO>().ReverseMap();
            CreateMap<Appointments, AppointmentsCreateDTO>().ReverseMap();
            CreateMap<Appointments, AppointmentsUpdateDTO>().ReverseMap();

            //PROCEDURES
            CreateMap<Procedures, ProceduresDTO>().ReverseMap();
            CreateMap<Procedures, ProceduresCreateDTO>().ReverseMap();
            CreateMap<Procedures, ProceduresUpdateDTO>().ReverseMap();
            //USER
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
