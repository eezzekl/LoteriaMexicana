


// See https://aka.ms/new-console-template for more information
using Loteria.AccesoDatos;
using Loteria.BussinlesLogic;

Console.WriteLine("Bienvenido a la generación de tablas de lotería.");
Console.Write("Por favor, ingrese la cantidad de tablas que desea generar: ");

int cantidadTablas = int.Parse(Console.ReadLine());

var tablas = CreaCartas.Tablas(cantidadTablas);

Console.WriteLine("Se han creado {0} tablas", tablas.Count);
foreach (var item in tablas)
{
    Console.WriteLine("item {0}", item.NombreTabla);
}
Console.WriteLine("Tablas generadas exitosamente. Presione cualquier tecla para salir.");
Console.ReadKey();