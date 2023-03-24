using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.BO
{
    public class Tabla
    {
        public Tabla()
        {
        }

        public Tabla(int idTabla, string nombreTabla, DateTime creadaEn)
        {
            IdTabla = idTabla;
            NombreTabla = nombreTabla;
            CreadaEn = creadaEn;
        }

        public int IdTabla { get; set; }
        public string NombreTabla { get; set; }
        public DateTime CreadaEn { get; set; }
    }

}
