using System;
using System.Runtime.InteropServices;
using nodes;
// nodo de caracteres

unsafe class Node
{
    // nodo de la lista de nodos
    public int Index;
    public Node* Next;
    public Node* Prev;
    public SubNode* List;

    public Node(int index)
    {
        Index = index;
        Next = null;
        Prev = null;
        List = null;
    }
    // agrega un nodo a la lista de caracteres
    public void Append(char data)
    {
        // crea un nuevo nodo de caracteres
        SubNode* newNode = (SubNode*)Marshal.AllocHGlobal(sizeof(SubNode));
        *newNode = new SubNode(data);

        if (List == null)
        {
            List = newNode;
        }
        else
        {
            SubNode* temp = List;
            while (temp->Next != null)
            {
                temp = temp->Next;
            }
            temp->Next = newNode;
        }
    }
    // imprime la lista de caracteres
    public void Print()
    {
        SubNode* temp = List;
        while (temp != null)
        {
            Console.WriteLine(temp->Data);
            temp = temp->Next;
        }
    }
}
// lista de nodos
unsafe class ListOfLists
{
    private Node* head;// nodo de la cabeza
    private Node* tail;// nodo de la cola

    public ListOfLists()// constructor
    {
        head = null;
        tail = null;
    }
// libera la memoria de los nodos
    ~ListOfLists()
    {
        Node* temp = head;// recorre la lista de nodos
        while (temp != null)// mientras el nodo actual no sea nulo
        {
            Node* aux = temp->Next; // guarda el siguiente nodo
            Marshal.FreeHGlobal((IntPtr)temp); // libera la memoria del nodo actual
            temp = aux; // el nodo actual es el siguiente nodo
        }
    }
// inserta un nodo en la lista de nodos
/// CANCIONES 
/// AUTOR-COMPOSITOR   
///    0
///    
// LISTA DE CANCIONES
// ALBUM
// CANCION12    - AUTOR12

//  indice     valor
//    0         H
//    1         E
//    2         L
//    2         L
//    3         O
//    0         W
///   2     =   L , L 

    public void Insert(int index, char value)
    {
        // crea un nuevo nodo
        Node* newNode = (Node*)Marshal.AllocHGlobal(sizeof(Node));
        *newNode = new Node(index); // inicializa el nodo
            // si la lista esta vacia
        if (head == null)
        {
            head = newNode;// el nuevo nodo es la cabeza
            tail = newNode; // el nuevo nodo es la cola
            newNode->Append(value); // agrega el valor a la lista de caracteres
        }
        else
        {
            // si el indice es menor al de la cabeza
            if (index < head->Index)
            {
                // el nuevo nodo es la cabeza
                head->Prev = newNode;
                newNode->Next = head;// el siguiente nodo es la cabeza
                head = newNode; // el nuevo nodo es la cabeza
                newNode->Append(value); // agrega el valor a la lista de caracteres
            }
            else
            {
                // si el indice es mayor al de la cola
                Node* aux = head;
                // recorre la lista de nodos
                while (aux->Next != null)
                {
                    // si el indice es menor al del siguiente nodo
                    if (index < aux->Next->Index)
                    { 
                        // si el indice es igual al del siguiente nodo
                        if (index == aux->Index)
                        {
                            aux->Append(value);// agrega el valor a la lista de caracteres
                            Marshal.FreeHGlobal((IntPtr)newNode);// libera la memoria del nuevo nodo
                        }
                        else
                        {
                            // el siguiente nodo del nuevo nodo es el siguiente nodo del nodo actual
                            newNode->Next = aux->Next;
                            newNode->Prev = aux;// el nodo anterior del nuevo nodo es el nodo actual
                            aux->Next->Prev = newNode; // el nodo anterior del siguiente nodo del nodo actual es el nuevo nodo
                            aux->Next = newNode;// el siguiente nodo del nodo actual es el nuevo nodo
                            newNode->Append(value);// agrega el valor a la lista de caracteres
                        }
                        return;
                    }
                    aux = aux->Next;// el nodo actual es el siguiente nodo
                }
                // si el indice es igual al de la cola
                if (index == aux->Index)
                {// agrega el valor a la lista de caracteres
                    aux->Append(value);
                    Marshal.FreeHGlobal((IntPtr)newNode);// libera la memoria del nuevo nodo
                    //el metodo marshal es utilizado para liberar la memoria de un puntero
                }
                else
                {
                    // el siguiente nodo de la cola es el nuevo nodo
                    tail->Next = newNode;
                    // el nodo anterior del nuevo nodo es la cola
                    newNode->Prev = tail;
                    // la cola es el nuevo nodo
                    tail = newNode;
                    // agrega el valor a la lista de caracteres
                    newNode->Append(value);
                }
            }
        }
    }
// imprime la lista de nodos
    public void Print()
    {
        // recorre la lista de nodos
        Node* temp = head;
        // mientras el nodo actual no sea nulo
        while (temp != null)
        { // imprime el indice
            Console.WriteLine($"Indice: {temp->Index}");
            temp->Print(); // imprime la lista de caracteres
            // imprime la lista de caracteres
            temp = temp->Next;
        }
    }
}
// clase principal
class Program
{
    static unsafe void Main()
    {
        // crea una lista de listas
        ListOfLists l = new ListOfLists();
        l.Insert(0, 'H');
        l.Insert(1, 'E');
        l.Insert(2, 'L');
        l.Insert(2, 'L');
        l.Insert(3, 'O');
        l.Insert(0, 'W');
        l.Insert(1, 'O');
        l.Insert(2, 'R');
        l.Insert(3, 'L');
        l.Insert(4, 'D');
        l.Insert(5, '!');
        l.Insert(2, 'A');
        l.Insert(2, 'E');
        l.Insert(2, 'I');
        l.Insert(2, 'O');
        l.Insert(2, 'U');

        l.Print();
    }
}
