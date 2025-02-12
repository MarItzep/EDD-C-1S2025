using Gtk;
using System;

namespace InterfazPrueba
{
    class Program
    {
        static void Main()
        {
            Application.Init();
            Window win = new Window("Interfaz Prueba");
            win.SetDefaultSize(800, 600);
            win.DeleteEvent += delegate { Application.Quit(); };
            // añadiendo un boton
            Button button = new Button("Presiona aca");
            button.Clicked += delegate { Console.WriteLine("Funcionando"); };
            win.Add(button);
            // agregar esta parte para que se muestre la ventana
            // si no les dara error  al ejecutar
            
            win.ShowAll();

            Application.Run();
        }
    }
}