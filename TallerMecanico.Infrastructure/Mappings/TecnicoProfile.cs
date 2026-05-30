using AutoMapper;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Mappings;

public class TecnicoProfile : Profile
{
    public TecnicoProfile()
    {
        CreateMap<Tecnico, TecnicoDto>();
        CreateMap<TecnicoDto, Tecnico>();
    }
}
