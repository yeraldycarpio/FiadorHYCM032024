using System;
using System.Collections.Generic;

namespace FiadorHYCM032024.Models
{
    public partial class ReferenciasFamiliare
    {
        public int Id { get; set; }
        public int IdFiador { get; set; }
        public string Nombre { get; set; } = null!;
        public string Relacion { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Direccion { get; set; } = null!;

        public virtual Fiador IdFiadorNavigation { get; set; } = null!;
    }
}
