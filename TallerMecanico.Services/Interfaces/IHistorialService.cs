using System.Collections.Generic;
using System.Threading.Tasks;
using TallerMecanico.Core.DTOs;
using TallerMecanico.Core.QueryFilters;

namespace TallerMecanico.Services.Interfaces;

public interface IHistorialService
{
    Task<IEnumerable<HistorialDto>> GetHistorialAsync(HistorialQueryFilter filter);
}
