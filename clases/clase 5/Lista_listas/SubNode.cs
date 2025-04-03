namespace nodes{


unsafe class SubNode
{
    // nodo de la lista de caracteres
    public char Data;
    public SubNode* Next;

    public SubNode(char data)
    {
        Data = data;
        Next = null;
    }
}
}