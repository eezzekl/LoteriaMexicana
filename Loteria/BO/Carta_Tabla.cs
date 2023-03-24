using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.BO
{
    public class CartaTabla
    {
        public CartaTabla()
        {
        }

        public CartaTabla(Carta carta, Tabla tabla, int posicion)
        {
            Carta = carta;
            Tabla = tabla;
            Posicion = posicion;
        }

        public int IdCarta { get; set; }
        public int IdTabla { get; set; }
        public int Posicion { get; set; }

        public Carta Carta { get; set; }
        public Tabla Tabla { get; set; }
    }

}
