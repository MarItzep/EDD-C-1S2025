using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

class SubNode
{
    public int Value { get; set; }
    public SubNode Next { get; set; } = null;
    
    public SubNode(int val)
    {
        Value = val;
    }
}

class Node
{
    public int Index { get; set; }
    public Node Next { get; set; } = null;
    public Node Prev { get; set; } = null;
    public SubNode List { get; set; } = null;
    
    public void Append(int value)
    {
        // Verificar si el valor ya existe para evitar duplicados
        SubNode current = List;
        while (current != null)
        {
            if (current.Value == value)
                return; // Valor ya existe
            current = current.Next;
        }
        
        SubNode newNode = new SubNode(value);
        if (List == null)
        {
            List = newNode;
        }
        else
        {
            SubNode aux = List;
            while (aux.Next != null)
            {
                aux = aux.Next;
            }
            aux.Next = newNode;
        }
    }
    
    public void Print()
    {
        SubNode aux = List;
        while (aux != null)
        {
            Console.Write($"{aux.Value} ");
            aux = aux.Next;
        }
        Console.WriteLine();
    }
}

class ListOfList
{
    public Node Head { get; set; } = null;
    public Node Tail { get; set; } = null;
    
    // Buscar un nodo por su índice
    public Node FindNode(int index)
    {
        Node current = Head;
        while (current != null)
        {
            if (current.Index == index)
                return current;
            current = current.Next;
        }
        return null;
    }
    
    // Insertar conexión para grafo no dirigido
    public void InsertUndirected(int from, int to)
    {
        // Insertar en ambas direcciones para crear una conexión no dirigida
        Insert(from, to);
        Insert(to, from);
    }
    
    public void Insert(int index, int value)
    {
        // Buscar si ya existe un nodo con este índice
        Node existingNode = FindNode(index);
        
        if (existingNode != null)
        {
            // Si el nodo ya existe, solo agregamos el valor a su lista
            existingNode.Append(value);
        }
        else
        {
            // Si no existe, creamos un nuevo nodo
            Node newNode = new Node();
            newNode.Index = index;
            
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
                newNode.Append(value);
            }
            else
            {
                if (index < Head.Index)
                {
                    Head.Prev = newNode;
                    newNode.Next = Head;
                    Head = newNode;
                    newNode.Append(value);
                }
                else
                {
                    Node aux = Head;
                    while (aux.Next != null && index > aux.Next.Index)
                    {
                        aux = aux.Next;
                    }
                    
                    if (index == aux.Index)
                    {
                        aux.Append(value);
                    }
                    else
                    {
                        newNode.Next = aux.Next;
                        newNode.Prev = aux;
                        
                        if (aux.Next != null)
                        {
                            aux.Next.Prev = newNode;
                        }
                        else
                        {
                            Tail = newNode;
                        }
                        
                        aux.Next = newNode;
                        newNode.Append(value);
                    }
                }
            }
        }
    }
    
    public void PrintList()
    {
        Node aux = Head;
        while (aux != null)
        {
            Console.WriteLine($"Index: {aux.Index}");
            aux.Print();
            aux = aux.Next;
        }
    }
    
    public void Graph()
    {
        using (StreamWriter file = new StreamWriter("graph.dot"))
        {
            file.WriteLine("graph G {");
            
            // Para mantener registro de las aristas que ya se han añadido
            HashSet<string> addedEdges = new HashSet<string>();
            
            Node currentNode = Head;
            while (currentNode != null)
            {
                file.WriteLine($"  {currentNode.Index} [label=\"{currentNode.Index}\"];");
                
                SubNode currentSubnode = currentNode.List;
                while (currentSubnode != null)
                {
                    // Crear una clave única para cada arista independiente del orden
                    string edgeKey = currentNode.Index < currentSubnode.Value ? 
                        $"{currentNode.Index}_{currentSubnode.Value}" : 
                        $"{currentSubnode.Value}_{currentNode.Index}";
                    
                    // Solo agregar la arista si no ha sido agregada antes
                    if (!addedEdges.Contains(edgeKey))
                    {
                        // Usar "--" en lugar de "->" para indicar arista no dirigida
                        file.WriteLine($"  {currentNode.Index} -- {currentSubnode.Value};");
                        addedEdges.Add(edgeKey);
                    }
                    
                    currentSubnode = currentSubnode.Next;
                }
                
                currentNode = currentNode.Next;
            }
            
            file.WriteLine("}");
        }
        
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = "-Tpng ./graph.dot -o ./graph.png",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            
            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error al crear la imagen.");
                }
                else
                {
                    Console.WriteLine("Imagen creada con éxito.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar la imagen: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        ListOfList adjacencyList = new ListOfList();
        

        adjacencyList.InsertUndirected(2, 2); // V2 -- R2
        adjacencyList.InsertUndirected(1, 1); // V1 -- R1
        adjacencyList.InsertUndirected(3, 1); // V3 -- R1
        adjacencyList.InsertUndirected(3, 3); // V3 -- R3
        adjacencyList.InsertUndirected(3, 4); // V3 -- R4

        // Imprimir la lista de adyacencia
        Console.WriteLine("Lista de adyacencia (Grafo no dirigido):");
        adjacencyList.PrintList();
        
        // Generar el archivo DOT y la imagen del grafo
        adjacencyList.Graph();
        Console.WriteLine("Se ha generado la imagen del grafo no dirigido.");
        
    }
}