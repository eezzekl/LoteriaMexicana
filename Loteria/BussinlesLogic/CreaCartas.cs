using Loteria.AccesoDatos;
using Loteria.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.BussinlesLogic
{
    public class CreaCartas
    {
        public static List<Tabla> Tablas(int numTablas)
        {
            List<Tabla> tablas = new List<Tabla>();

            // Obtener todas las cartas de la base de datos
            List<Carta> cartas = CartaDAO.ObtenerTodasLasCartas();

            for (int i = 0; i < numTablas; i++)
            {
                Tabla tabla = GenerarTablaLoteria(cartas);
                while (TablaDAO.VerificarTablaExistente(tabla))
                {
                    tabla = GenerarTablaLoteria(cartas);
                }
                TablaDAO.InsertarTabla(tabla);
                tablas.Add(tabla);
            }
            return tablas;
        }

        static Tabla GenerarTablaLoteria(List<Carta> cartas)
        {
            // Crear una nueva tabla vacía
            Tabla tabla = new Tabla();

            // Seleccionar 16 cartas al azar sin repetición
            Random random = new Random();
            HashSet<int> indicesCartasSeleccionadas = new HashSet<int>();
            while (indicesCartasSeleccionadas.Count < 16)
            {
                int indiceCarta = random.Next(cartas.Count);
                indicesCartasSeleccionadas.Add(indiceCarta);
            }

            // Agregar las cartas seleccionadas a la tabla
            foreach (int indiceCarta in indicesCartasSeleccionadas)
            {
                var cartaTabla = new CartaTabla()
                {
                    IdCarta = indiceCarta,
                    IdTabla = tabla.IdTabla,
                    Posicion = indiceCarta
                };
                CartaTablaDAO.InsertarCartaTabla(cartaTabla);
            }

            return tabla;
        }
    }
}


