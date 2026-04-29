using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TallerMecanico.Core.Entities
{
    public class OrdenTrabajo : BaseEntity
    {
        public int VehiculoId { get; set; }
        public int ServicioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Servicio Servicio { get; set; }
    }
}