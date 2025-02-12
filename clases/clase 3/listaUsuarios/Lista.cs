using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class ListaSimple<T> where T : unmanaged
    {
        private Nodo<T>* cabeza;

        public ListaSimple()
        {
            cabeza = null;
        }

        public void Insertar(T data)
        {
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = data;
            nuevo->Sig = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T>* temp = cabeza;
                while (temp->Sig != null)
                {
                    temp = temp->Sig;
                }
                temp->Sig = nuevo;
            }
        }

        public void Eliminar(T data)
        {
            if (cabeza == null) return;

            if (cabeza->Data.Equals(data))
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }

            Nodo<T>* actual = cabeza;
            while (actual->Sig != null && !actual->Sig->Data.Equals(data))
            {
                actual = actual->Sig;
            }

            if (actual->Sig != null)
            {
                Nodo<T>* temp = actual->Sig;
                actual->Sig = actual->Sig->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
        public void ModificarUsuario(int id, string nuevoNombre, string nuevoApellido, string nuevoCorreo)
        {
            // se valida que sea tipo usuario
            if (typeof(T) != typeof(User.Usuario))
            {
                throw new InvalidOperationException("El método ModificarUsuario solo puede ser utilizado con listas de usuarios.");
            }
            // Recorre la lista enlazada.
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                // Obtiene el puntero al dato y lo interpreta como un Usuario.
                User.Usuario* pUsuario = (User.Usuario*)(&temp->Data);
                if (pUsuario->id == id)
                {
                    // Como la memoria es no administrada, los buffers ya tienen una dirección fija.
                    // Simplemente se asignan a punteros locales.
                    char* n = pUsuario->name;
                    char* l = pUsuario->lastName;
                    char* c = pUsuario->correo;
                    int i;
                    // Actualiza el campo name 
                    for (i = 0; i < nuevoNombre.Length && i < 50 - 1; i++)
                    {
                        n[i] = nuevoNombre[i];
                    }
                    n[i] = '\0';

                    // Actualiza el campo lastName
                    for (i = 0; i < nuevoApellido.Length && i < 50 - 1; i++)
                    {
                        l[i] = nuevoApellido[i];
                    }
                    l[i] = '\0';

                    //actualiza el campo
                    for (i = 0; i < nuevoCorreo.Length && i < 100 - 1; i++)
                    {
                        c[i] = nuevoCorreo[i];
                    }
                    c[i] = '\0';
                    Console.WriteLine("Usuario modificado exitosamente.");
                    return; // finalizar tras modificar
                }
                temp = temp->Sig;
            }
            Console.WriteLine("Usuario no encontrado.");
        }




        public void Imprimir()
        {
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                Console.WriteLine(temp->Data);
                temp = temp->Sig;
            }
        }

        ~ListaSimple()
        {
            while (cabeza != null)
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
    }
}
