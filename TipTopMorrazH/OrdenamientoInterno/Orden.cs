using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTopMorrazH.OrdenamientoInterno
{
    public class Orden
    {
        public int Codigo { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantiProd { get; set; }
        public string TipoCombo { get; set; }
        public string NombreCombo { get; set; }
        public double Total { get; set; }
        public int Llave { get; set; }
    }
}
