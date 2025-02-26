using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Estructuras
{
    public unsafe class ListaSimple
    {
        private Nodo* cabeza;

        //* Constructor
        public ListaSimple()
        {
            cabeza = null;
        }

        //* Método para insertar un nuevo nodo en la lista
        public void Insertar(int id, string name, string last_name, string correo)
        {
            // marshal AllocHGlobal para reservar memoria no administrada
            Nodo* nuevoNodo = (Nodo*)Marshal.AllocHGlobal(sizeof(Nodo));
            nuevoNodo->Id = id;
            nuevoNodo->Siguiente = null;

            // Limpiar y copiar valores en los buffers fijos
            CopiarCadena(nuevoNodo->name, name);
            CopiarCadena(nuevoNodo->last_name, last_name);
            CopiarCadena(nuevoNodo->correo, correo);

            // Insertar el nodo al final de la lista
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                Nodo* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevoNodo;
            }
        }

        //* Método para mostrar la lista
        public void Mostrar()
        {
            Nodo* actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual->Id} | Nombre: {ObtenerCadena(actual->name)} {ObtenerCadena(actual->last_name)} | Correo: {ObtenerCadena(actual->correo)}");
                actual = actual->Siguiente;
            }
        }
        public string GenerarGraphviz()
        {
            if (cabeza == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=LR;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Lista Simple\";\n";

            Nodo* actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                string name = ObtenerCadena(actual->name);
                string last_name = ObtenerCadena(actual->last_name);
                string correo = ObtenerCadena(actual->correo);
                graphviz += $"        n{index} [label = \"{{<data> ID: {actual->Id} \\n Nombre: {name} {last_name} \\n Correo: {correo} }}\"];\n";
                actual = actual->Siguiente;
                index++;
            }

            actual = cabeza;
            for (int i = 0; actual != null && actual->Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";

            string dotFilePath = "lista.dot";
            string pngFilePath = "lista.png";

            // Guardar el código DOT en un archivo
            File.WriteAllText(dotFilePath, graphviz);

            // Generar la imagen con Graphviz (dot -Tpng lista.dot -o lista.png)
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng {dotFilePath} -o {pngFilePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            // Abrir la imagen generada automáticamente
            Process.Start(new ProcessStartInfo(pngFilePath) { UseShellExecute = true });

            return graphviz;
        }

        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            Nodo* actual = cabeza;
            while (actual != null)
            {
                Nodo* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
        }

 
        private void CopiarCadena(char* destino, string fuente)
        {
            for (int i = 0; i < 50; i++)
            {
                // Asignar '\0' si el índice supera la longitud de la cadena fuente
                destino[i] = i < fuente.Length ? fuente[i] : '\0';
            }
        }

        private string ObtenerCadena(char* fuente)
        {
            char[] buffer = new char[50];
            for (int i = 0; i < 50 && fuente[i] != '\0'; i++)
            {
                buffer[i] = fuente[i];
            }
            return new string(buffer).TrimEnd('\0');
        }

    
    }
    }

