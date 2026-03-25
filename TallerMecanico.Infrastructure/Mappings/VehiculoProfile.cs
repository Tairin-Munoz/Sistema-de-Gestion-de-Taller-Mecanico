using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TallerMecanico.Infrastructure.Mappings;

public class VehiculoProfile : Profile
{
    public VehiculoProfile()
    {
        CreateMap<Vehiculo, VehiculoDto>().ReverseMap();
    }
}
