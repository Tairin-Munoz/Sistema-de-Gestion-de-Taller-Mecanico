using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;

namespace TallerMecanico.Infrastructure.Mappings;

public class PropietarioProfile : Profile
{
    public PropietarioProfile()
    {
        CreateMap<Propietario, PropietarioDto>();
        CreateMap<PropietarioDto, Propietario>();
    }
}