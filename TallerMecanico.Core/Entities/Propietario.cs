using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TallerMecanico.Core.Entities
{
    public class Propietario : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string CI { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}