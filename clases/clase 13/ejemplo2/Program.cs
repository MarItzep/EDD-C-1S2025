using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

[Serializable]
public class HuffmanNode : IComparable<HuffmanNode>
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public HuffmanNode Left { get; set; }
    public HuffmanNode Right { get; set; }

    public int CompareTo(HuffmanNode other)
    {
        return Frequency.CompareTo(other.Frequency);
    }
}


class HuffmanCompression
{

    public static (string compressed, HuffmanNode root) CompressWithTree(string input)
    {
        //Recorre el texto para saber cuantas veces aparece un caracter
        Dictionary<char, int> frequencies = new Dictionary<char, int>();
        foreach (char c in input)
        {
            
            if (frequencies.ContainsKey(c))
                frequencies[c]++;
            else
                frequencies[c] = 1;
        }

        //Cola de prioridad para manejar los nodos segun su frecuencia
        PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();
        //Crea un nodo por caracter y lo añade a la cola de prioridad
        foreach (var kvp in frequencies)
        {
            priorityQueue.Enqueue(new HuffmanNode { Character = kvp.Key, Frequency = kvp.Value });
        }

        //Construir el arbol de huffman, debe haber 2
        //elementos como minimo para ejecutarse
        while (priorityQueue.Count > 1)
        {
            HuffmanNode left = priorityQueue.Dequeue();
            Console.WriteLine(left.Character);            
            HuffmanNode right = priorityQueue.Dequeue();
            Console.WriteLine(right.Character);
            Console.WriteLine("----");
            //Combina dos nodos con menor frecuencia en un nodo padre
            HuffmanNode parent = new HuffmanNode
            {
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right
            };
           
            priorityQueue.Enqueue(parent);
        }
        //Crea nodo raiz
        HuffmanNode root = priorityQueue.Dequeue();
        Dictionary<char, string> codes = GenerateHuffmanCodes(root);

        StringBuilder compressed = new StringBuilder();
        //Reemplaza cada caracter del texto original por su codigo huffman
        foreach (char c in input)
        {
            compressed.Append(codes[c]);
        }

        return (compressed.ToString(), root);
    }


    public static string Decompress(string compressed, HuffmanNode root)
    {
        StringBuilder decompressed = new StringBuilder();
        HuffmanNode current = root;
        //Recorre los bits comprimidos por el arbol 
        //Al llegar al nodo hoja se añade el caracter al resultado y se reinicia desde la raiz
        foreach (char bit in compressed)
        {
            if (bit == '0')
                current = current.Left;
            else if (bit == '1')
                current = current.Right;

            if (current.Character != '\0')
            {
                decompressed.Append(current.Character);
                current = root;
            }
        }

        return decompressed.ToString();
    }

    private static Dictionary<char, string> GenerateHuffmanCodes(HuffmanNode root)
    {
        var codes = new Dictionary<char, string>(); //Crea diccionario para almacenar el codigo a cada caracter
        GenerateHuffmanCodes(root, "", codes);
        return codes;
    }

    private static void GenerateHuffmanCodes(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        //Realiza un recorrido del arbol
        
        if (node == null)
            return;


        if (node.Character != '\0')
            codes[node.Character] = code;

        //A los nodos izquierdos les asigna un 0
        GenerateHuffmanCodes(node.Left, code + "0", codes);
        //A los nodos derecha les asigna un 1
        GenerateHuffmanCodes(node.Right, code + "1", codes);
    }
    //Los caracteres en las hojas obtienen el codigo acumulado del camino desde la raiz
    


}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;

    public void Enqueue(T item)
    {
        //Añade al final
        list.Add(item);
        int i = list.Count - 1;

        //Sube el elemento 
        while (i > 0)
        {
            //Compara el elemento con su padre
            //Si es menor intercambia las posiciones
            int parent = (i - 1) / 2;
            //si nodo riaz >= nodo no intercambia
            if (list[i].CompareTo(list[parent]) >= 0)
                break;
            //Intercambio
            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (list.Count == 0)
            throw new InvalidOperationException("Queue is empty");
        //Obtiene el nodo minoma frecuencia
        T front = list[0];
        list[0] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        int current = 0;

        //Reordena
        while (true)
        {
            int left = 2 * current + 1;
            int right = 2 * current + 2;
            int smallest = current;
            //Encuentra el menor entre el actual y sus hijos
            if (left < list.Count && list[left].CompareTo(list[smallest]) < 0)
                smallest = left;
            if (right < list.Count && list[right].CompareTo(list[smallest]) < 0)
                smallest = right;
            if (smallest == current)
                break;

            //Intercambia con el hijo menor
            Swap(current, smallest);
            current = smallest;
        }
        return front;
    }

    private void Swap(int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string jsonFile = "vehiculos.json";
        string compressedFile = "vehiculo.edd";
        string treeFile = "huffman_tree.json";
        string decompressedFile = "vehiculos_descomprimido.json";

        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1. Comprimir archivo");
        Console.WriteLine("2. Descomprimir archivo");
        Console.Write("Opción: ");
        string opcion = Console.ReadLine();

        if (opcion == "1")
        {
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine($"El archivo {jsonFile} no existe.");
                return;
            }

            string input = File.ReadAllText(jsonFile);
            var (compressed, root) = HuffmanCompression.CompressWithTree(input);

            File.WriteAllText(compressedFile, compressed);
            File.WriteAllText(treeFile, JsonSerializer.Serialize(root));

            Console.WriteLine("\nArchivo comprimido y árbol guardado exitosamente.");
        }
        else if (opcion == "2")
        {
            if (!File.Exists(compressedFile) || !File.Exists(treeFile))
            {
                Console.WriteLine("Archivo comprimido o árbol Huffman no encontrados.");
                return;
            }

            string compressed = File.ReadAllText(compressedFile);
            HuffmanNode root = JsonSerializer.Deserialize<HuffmanNode>(File.ReadAllText(treeFile));

            string decompressed = HuffmanCompression.Decompress(compressed, root);
            File.WriteAllText(decompressedFile, decompressed);
            // Guarda el archivo descomprimido
            Console.WriteLine("\nArchivo descomprimido exitosamente.");

            /// imprimir el contenido del archivo descomprimido
            /// 
            string descomprimido = File.ReadAllText(decompressedFile);
            Console.WriteLine("\nContenido del archivo descomprimido:");


        }
        else
        {
            Console.WriteLine("Opción inválida.");
        }
    }
}

/// menu 
/// 