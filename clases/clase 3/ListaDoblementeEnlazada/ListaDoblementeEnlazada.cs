using System;
using System.Runtime.InteropServices;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDoblementeEnlazada
    {
        // apuntador head y tail
        private Node* head;
        // apuntador tail que es el último nodo de la lista
        private Node* tail;

        public ListaDoblementeEnlazada()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, string nombreLocal, string nombrePropietario, string direccion)
        {
            Node* nuevoNodo = (Node*)NativeMemory.Alloc((nuint)sizeof(Node));
            *nuevoNodo = new Node(id, nombreLocal, nombrePropietario, direccion);
            // Si la lista está vacía, el nuevo nodo será el primero
            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            // Si la lista está vacía, no hay nada que eliminar
            Node* actual = head;
            while (actual != null)
            {
                // Si el nodo a eliminar es la cabeza de la lista, movemos la cabeza al siguiente nodo
                if (actual->Id == id)
                {
                    if (actual->Prev != null)
                        actual->Prev->Next = actual->Next;
                    else
                        head = actual->Next;

                    if (actual->Next != null)
                        actual->Next->Prev = actual->Prev;
                    else
                        tail = actual->Prev;
                    // Liberamos la memoria del nodo eliminado
                    NativeMemory.Free(actual);
                    return;
                }
                // Si el nodo a eliminar no es la cabeza de la lista, avanzamos al siguiente nodo
                actual = actual->Next;
            }
        }

// Método para mostrar los nodos de la lista inicializados del head al tail
        public void Mostrar()
        {
            // Si la lista está vacía, no hay nada que mostrar
            Node* actual = head;
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }
// Método para mostrar los nodos de la lista inicializados del tail al head
        public void MostrarReversa()
        {
            // Si la lista está vacía, no hay nada que mostrar
            Node* actual = tail;
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Prev;
            }
        }
        // Destructor de la clase
        ~ListaDoblementeEnlazada()
        {
            // Liberamos la memoria de todos los nodos de la lista
            Node* actual = head;
            while (actual != null)
            {
                Node* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}
