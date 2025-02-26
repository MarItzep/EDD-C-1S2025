namespace Estructuras
{
    public unsafe struct Nodo
    {
        public int Id;
        public fixed char name[50]; 
        public fixed char last_name[50]; 
        public fixed char correo[50]; 
        public Nodo* Siguiente;
 

    }
}