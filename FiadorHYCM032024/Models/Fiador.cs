using System;
using System.Collections.Generic;

namespace FiadorHYCM032024.Models
{
    public partial class Fiador
    {
        public Fiador()
        {
            ReferenciasFamiliare = new List<ReferenciasFamiliare>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Correo { get; set; } = null!;
        public string Ocupacion { get; set; } = null!;
        public decimal IngresoMensual { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual IList<ReferenciasFamiliare> ReferenciasFamiliare { get; set; }
    }
}
