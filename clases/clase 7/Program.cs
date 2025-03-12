using  System ;


///creacion de un nodo del arbol

public class nodo
{
    public int valor;  // valor almacenar en el nodo
    public nodo Izquierda; // referencia al hijo izquierdo
    public nodo Derecha;  // referencia al hijo derecho
    public nodo(int valor)
    {
        this.valor = valor;
        this.Izquierda = null;
        this.Derecha = null;
    }
}

///creacion de la clase arbol binario de busqueda
///
public class ArbolBinarioBusqueda
{
    private nodo raiz;
    //constructor del arbol
    public ArbolBinarioBusqueda()
    {
        raiz = null;
    }

    public void Insertar(int valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private nodo InsertarRecursivo(nodo actual, int valor)
    {
        if (actual == null)
        {
            return new nodo(valor);
        }
        if (valor < actual.valor) // si el valor es menor que el valor del nodo actual
        {
            actual.Izquierda = InsertarRecursivo(actual.Izquierda, valor);
        }
        else if (valor > actual.valor) // si el valor es mayor que el valor del nodo actual
        {
            actual.Derecha = InsertarRecursivo(actual.Derecha, valor);
        }
        // si el valor es igual al valor del nodo actual, no se inserta
        return actual;
    }
    // metodo para buscar un valor en el arbol
    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }
    // metoodo privado recursivo para buscar un valor
    private bool BuscarRecursivo(nodo actual, int valor)
    {
        if (actual == null) // si el nodo actual es nulo
        {
            return false;
        }
        if (valor == actual.valor ) // si el valor es igual al valor del nodo actual
        {
            return true;
        }
        return valor < actual.valor
            ? BuscarRecursivo(actual.Izquierda, valor)
            : BuscarRecursivo(actual.Derecha, valor);
    }

    public void RecorridoEnOrden()
    {
        RecorridoEnOrdenRecursivo(raiz);
        Console.WriteLine();//salto de linea
    }

    private void RecorridoEnOrdenRecursivo(nodo actual)
    {
        if (actual != null) // si el nodo actual no es nulo
        {   // visita del sub arbol izquierdo
            RecorridoEnOrdenRecursivo(actual.Izquierda);
            // visita del nodo actual e visualizacion de su valor
            Console.Write(actual.valor + " ");
            // visita del sub arbol derecho
            RecorridoEnOrdenRecursivo(actual.Derecha);
        }
    }
    //
}

// clase principal del programa para el arbol binario de busqueda

class program 
{
    static void Main(string[] args)
    {
        ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
        arbol.Insertar(50);
        arbol.Insertar(30);
        arbol.Insertar(20);
        arbol.Insertar(40);
        arbol.Insertar(70);
        arbol.Insertar(60);
        arbol.Insertar(80);

        Console.WriteLine("Recorrido en orden del arbol binario de busqueda");
        arbol.RecorridoEnOrden();

        Console.WriteLine("Busqueda de un valor en el arbol binario de busqueda");
        Console.WriteLine("valor 0",arbol.Buscar(0));
        Console.WriteLine(arbol.Buscar(20));
        Console.WriteLine(arbol.Buscar(40));

        //Console.WriteLine(arbol.Buscar(100));
    }
}