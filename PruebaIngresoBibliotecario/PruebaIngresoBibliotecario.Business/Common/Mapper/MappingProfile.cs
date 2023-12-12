namespace PruebaIngresoBibliotecario.Business.Common.Mapper
{
    using AutoMapper;
    using PruebaIngresoBibliotecario.Core.Dtos;
    using PruebaIngresoBibliotecario.Core.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoanCreateDto, Loan>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdentificacionUsuario))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.TipoUsuario));

            CreateMap<Loan, LoanCreateResponseDto>()
               .ForMember(dest => dest.FechaMaximaDevolucion, opt => opt.MapFrom(src => src.DevolutionDate.ToString("dd/MM/yyyy")));

            CreateMap<Loan, LoanDto>()
               .ForMember(dest => dest.FechaMaximaDevolucion, opt => opt.MapFrom(src => src.DevolutionDate.Date))
               .ForMember(dest => dest.IdentificacionUsuario, opt => opt.MapFrom(src => src.UserId))
               .ForMember(dest => dest.TipoUsuario, opt => opt.MapFrom(src => src.UserType));
        }
    }
}
