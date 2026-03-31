using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Entities
{
    public class Vehiculo : BaseEntity
    {
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Placa { get; set; } = null!;

        public int PropietarioId { get; set; }

        public virtual Propietario? Propietario { get; set; }
    }
}
