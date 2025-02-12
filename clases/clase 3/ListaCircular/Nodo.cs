using System;

namespace ListaCircular
{
    public unsafe struct Node
    {
        public int Id;
        public fixed char NombreLocal[50];
        public fixed char NombrePropietario[50];
        public fixed char Direccion[100];
        public Node* Next;

        public Node(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Id = id;
            Next = null;
            // fixed  es una palabra clave que crea un puntero a un tipo de datos específico, en este caso, un char
            fixed (char* ptr = NombreLocal)
                nombreLocal.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = NombrePropietario)
                nombrePropietario.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Direccion)
                direccion.AsSpan().CopyTo(new Span<char>(ptr, 100));
        }
        // Sobrecarga del método ToString para mostrar los datos del nodo
        public override string ToString()
        {
            fixed (char* ptrNombre = NombreLocal, ptrPropietario = NombrePropietario, ptrDireccion = Direccion)
            {
                return $"ID: {Id}, NombreLocal: {new string(ptrNombre)}, Propietario: {new string(ptrPropietario)}, Dirección: {new string(ptrDireccion)}";
            }
        }
    }
}
