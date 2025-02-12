using System;
using System.Runtime.InteropServices;
// el nodo sea tipo persona

namespace List
{
    public unsafe struct Nodo<T> where T : unmanaged
    {
        public T Data;
        public Nodo<T>* Sig;
    }

}
