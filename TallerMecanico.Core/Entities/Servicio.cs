using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerMecanico.Core.Entities
{
    public class Servicio : BaseEntity
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
    }
}
