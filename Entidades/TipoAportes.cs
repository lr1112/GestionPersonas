using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionPersonas.Entidades
{
    public class TipoAportes
    {
        [Key]
        public int TipoAporteId { get; set; }
        public string Descripcion { get; set; }
        public double MontoDeseado { get; set; }
        public double MontoLogrado { get; set; } 

        public TipoAportes()
        {
            TipoAporteId = 0;
            Descripcion = string.Empty;
            MontoDeseado = 0;
            MontoLogrado = 0;

        }

        public TipoAportes(int tipoAporteId, string descripcion, double montoDeseado, double montoLogrado)
        {
            TipoAporteId = tipoAporteId;
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            MontoDeseado = montoDeseado;
            MontoLogrado = montoLogrado;
        }
    }
}