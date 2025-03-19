using System;

class Repuestos
{
    public int ID;
    public string Repuesto;
    public string Detalles;
    public double Costo;

    public Repuestos(int id, string repuesto, string detalle, double costo)
    {
        ID = id;
        Repuesto = repuesto;
        Detalles = detalle;
        Costo = costo;

    }
}

class AvlNode
{

    public Repuestos item;
    public AvlNode? left;
    public AvlNode? right;
    public int height;

    public AvlNode(Repuestos Item)
    {
        item = Item;
        left = null;
        right = null;
        height = 0;
    }


}


class AvlTree
{
    private AvlNode root;
    string connections = "";
    string nodes = "";

    public void insert(Repuestos item)
    {
        root = insertRecursive(item, root);
    }

    public int GetHeight(AvlNode node)
    {
        return node == null ? -1 : node.height;
    }

    public int getMaxHeight(int leftNode, int rightNode)
    {
        return leftNode > rightNode ? leftNode : rightNode;
    }

   

    public AvlNode insertRecursive(Repuestos item, AvlNode node)
    {
        if(node == null)
        {
            node = new AvlNode(item);
        }
        else if(item.ID < node.item.ID)
        {
            node.left = insertRecursive(item, node.left);

            if(GetHeight(node.left) - GetHeight(node.right) == 2)
            {
                //derecho
                if(item.ID < node.left.item.ID)
                {
                    node = rotateright(node);

                } else 
                {
                    node = doubleRight(node);
                }

            }

        } 
        else if(item.ID > node.item.ID)
        {
            node.right = insertRecursive(item, node.right);
            if(GetHeight(node.right) - GetHeight(node.left) == 2) 
            {
                //izquierdo
                if(item.ID > node.right.item.ID)
                {
                    node = rotateleft(node);
                }
                else 
                {
                    node = doubleLeft(node);
                }
            } 

        }
        else 
        {
            Console.WriteLine("Elemento ya existe en el arbol");
        }


        node.height = getMaxHeight(GetHeight(node.left), GetHeight(node.right)) + 1;
        return node;

    }


    public AvlNode rotateleft(AvlNode node1)
    {
        AvlNode node2 = node1.right;
        node1.right = node2.left;
        node2.left = node1;
        node1.height = getMaxHeight(GetHeight(node1.left), this.GetHeight(node1.right)) + 1;
        node2.height = getMaxHeight(GetHeight(node2.right), node1.height) + 1;
        return node2;
    }

    public AvlNode rotateright(AvlNode node2)
    {
        AvlNode node1 = node2.left;
        node2.left = node1.right;
        node1.right = node2;
        node2.height = getMaxHeight(GetHeight(node2.left), GetHeight(node2.right)) + 1;
        node1.height = getMaxHeight(GetHeight(node1.left), node2.height) + 1;
        return node1;
    }

    public AvlNode doubleLeft(AvlNode node)
    {
        node.left = rotateright(node.left);
        return rotateleft(node);
    }

    public AvlNode doubleRight(AvlNode node)
    {
        node.right = rotateleft(node.right);
        return rotateright(node);
    }


    public void treeGraph()
    {
        nodes = "";
        connections = "";
        treeGraphRecursive(root);

        Console.WriteLine("digraph BST {");
        Console.WriteLine("node [shape=rectangle];");
        Console.WriteLine(nodes);
        Console.WriteLine(connections);
        Console.WriteLine("}");

    }

    public void treeGraphRecursive(AvlNode current)
    {
        if(current.left != null)
        {
            treeGraphRecursive(current.left);
            connections += "S"+ Convert.ToString(current.item.ID) + "->" + "S" + Convert.ToString(current.left.item.ID) + "\n";

        }
        nodes += "S"+Convert.ToString(current.item.ID)+"[label="+Convert.ToString(current.item.ID)+"];";
        if(current.right != null)
        {
            treeGraphRecursive(current.right);
            connections += "S"+ Convert.ToString(current.item.ID) + "->" + "S" + Convert.ToString(current.right.item.ID) + "\n";
        }

    }

}

class Program
{
    static void Main()
    {
        Repuestos repuesto1 = new Repuestos(1, "motor", "nuevo", 50.2);
        Repuestos repuesto2 = new Repuestos(2, "motor", "nuevo", 50.2);
        Repuestos repuesto3 = new Repuestos(3, "motor", "nuevo", 50.2);
        Repuestos repuesto4 = new Repuestos(4, "motor", "nuevo", 50.2);
        Repuestos repuesto5 = new Repuestos(5, "motor", "nuevo", 50.2);
        Repuestos repuesto6 = new Repuestos(6, "motor", "nuevo", 50.2);
        Repuestos repuesto14 = new Repuestos(14, "motor", "nuevo", 50.2);
        Repuestos repuesto15 = new Repuestos(15, "motor", "nuevo", 50.2);
        Repuestos repuesto16 = new Repuestos(16, "motor", "nuevo", 50.2);
        AvlTree arbol = new AvlTree();
        arbol.insert(repuesto1);
        arbol.insert(repuesto2);
        arbol.insert(repuesto3);
        arbol.insert(repuesto4);
        arbol.insert(repuesto5);
        arbol.insert(repuesto6);
        arbol.insert(repuesto14);
        arbol.insert(repuesto15);
        arbol.insert(repuesto16);

        //arbol.treeGraphRecursive();


        arbol.treeGraph();
    }
}