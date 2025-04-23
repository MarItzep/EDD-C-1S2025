using Estructura.merkle;


class Program
{
    static void Main(string[] args)
    {
        MerkleTree tree = new MerkleTree();
        // agregar facturas al arbol
        tree.Insert(1, 101, 150.50, "07-04-25", "Efectivo");
        tree.Insert(2, 102, 200.75, "07-04-25", "Tarjeta");
        tree.Insert(3, 103, 300.00, "07-04-25", "Transferencia");
        tree.Insert(4, 104, 250.25, "07-04-25", "Efectivo");
        tree.Insert(5, 105, 400.00, "07-04-25", "Tarjeta");
        tree.Insert(6, 106, 500.50, "07-04-25", "Transferencia");
        tree.Insert(7, 107, 600.75, "07-04-25", "Efectivo");
        tree.Insert(8, 108, 700.00, "07-04-25", "Tarjeta");

        // generar el grafo 
        Console.WriteLine(tree.GenerateDot());

    }
}