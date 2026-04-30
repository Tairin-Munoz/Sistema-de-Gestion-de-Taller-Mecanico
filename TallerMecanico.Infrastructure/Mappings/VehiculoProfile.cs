using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
namespace TallerMecanico.Infrastructure.Mappings;

public class VehiculoProfile : Profile
{
    public VehiculoProfile()
    {
        CreateMap<Vehiculo, VehiculoDto>();
        CreateMap<VehiculoDto, Vehiculo>();

        CreateMap<Servicio, ServicioDto>();
        CreateMap<ServicioDto, Servicio>();

        CreateMap<OrdenTrabajo, OrdenTrabajoDto>();
        CreateMap<OrdenTrabajoDto, OrdenTrabajo>()
            .ForMember(dest => dest.Fecha,
                opt => opt.MapFrom(src => DateTime.Parse(src.Fecha)));

    }
}