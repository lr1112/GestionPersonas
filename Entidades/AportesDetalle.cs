using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionPersonas.Entidades
{
    public class AportesDetalle
    {
        [Key]
        public int Id { get; set; }
        public int AporteId { get; set; }
        public int TipoAporteId { get; set; } 
        public double Monto { get; set; }
        
        public AportesDetalle()
        {
            Id = 0;
            AporteId = 0;
            TipoAporteId = 0;
            Monto = 0;
            


        }

        public AportesDetalle(int id, int aporteId, int tipoAportesId, double monto)
        {
            Id = id;
            AporteId = aporteId;
            TipoAporteId = tipoAportesId;
            Monto = monto;
        }
    }
}