using System;

namespace Matriz
{
    class Program
    {
        static unsafe void Main()
        {
            MatrizDispersa<int> matriz = new MatrizDispersa<int>(0);
            matriz.insert(1, 1, "1");
            matriz.insert(1, 2, "1");
            matriz.insert(1, 3, "1");
            matriz.insert(1, 4, "1");
            matriz.insert(2, 2, "1");
            //(pos_x, pos_y, nombre)
            //matriz.insert(3, 7, "1");
            matriz.mostrar();
            matriz.Graficar();
        }
    }
}