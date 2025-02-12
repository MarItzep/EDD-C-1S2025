using System;
using System.Runtime.InteropServices;

namespace User
{
    // se define la estructura usuario pero tipo unsafe
    public unsafe struct Usuario
    {
        public int id;
        // Se cambia el tipo de dato de string a char* para que sea compatible con el uso de punteros
        public fixed char name[50];
        public fixed char lastName[50];
        public fixed char correo[100];
        // Se cambia el tipo de dato de Usuario a Usuario* para que sea compatible con el uso de punteros
        public Usuario* sig;

        // Constructor que inicializa un nuevo Usuario con sus datos.
        public Usuario(int id, string name, string lastName, string correo)
        {
            this.id = id; // Se asigna el id
            sig = null; // Se inicializa el siguiente nodo como null
            // Se inicializan los arreglos de caracteres con los valores de los strings
            // se usa fixed
            //  se usa fixed para que los punteros no se muevan en memoria
            fixed (char* n = this.name, l = this.lastName, c = this.correo)
            {
                setFixedString(name, n, 50);
                setFixedString(lastName, l, 50);
                setFixedString(correo, c, 100);
            }
        }
        //setFixedString: Copia una cadena de entrada en un arreglo fijo de caracteres, asegurándose de no sobrepasar el tamaño máximo permitido y colocando un carácter nulo al final.
        private static void setFixedString(string str, char* fixedStr, int maxLength)
        {
            int i;
            for (i = 0; i < str.Length && i < maxLength - 1; i++)
            {
                fixedStr[i] = str[i];
            }
            fixedStr[i] = '\0';
        }
       // getFixedString: Obtiene una cadena de un arreglo fijo de caracteres, deteniéndose al encontrar un carácter nulo.
        private static string getFixedString(char* fixedStr)
        {
            string str = "";
            for (int i = 0; fixedStr[i] != '\0'; i++)
            {
                str += fixedStr[i];
            }
            return str;
        }


// ToString: Devuelve una cadena con los datos del usuario.
        public override string ToString()
        {
            fixed (char* n = name, l = lastName, c = correo)
            {
                return $"ID: {id}, Nombre: {getFixedString(n)}, Apellido: {getFixedString(l)}, Correo: {getFixedString(c)}";
            }
        }
    }
}
