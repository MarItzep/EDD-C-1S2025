using System;
using List;
using User;
class Program
{
    static unsafe void Main()
    {
        ListaSimple<Usuario> lista = new ListaSimple<Usuario>();

        // Asegúrate de pasar un string para el correo, no un int.
        lista.Insertar(new Usuario(1, "Juan", "Pérez", "juan@example.com"));
        lista.Insertar(new Usuario(2, "María", "López", "maria@example.com"));
        lista.Insertar(new Usuario(3, "Carlos", "Díaz", "carlos@example.com"));

        Console.WriteLine("Lista antes de eliminar:");
        lista.Imprimir();
        // 2 
        // busqueda de usuario por id   

        // return usuario

        lista.Eliminar(new Usuario(2, "María", "López", "maria@example.com"));
        Console.WriteLine("\nLista después de eliminar:");
        lista.Imprimir();

        // MODIFICAR USUARIO
        lista.ModificarUsuario(1, "Marcos", "Pérez", " mar@example.com   ");
        Console.WriteLine("\nLista después de modificar:");
        lista.Imprimir();

    }
}
