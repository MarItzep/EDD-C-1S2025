using System.Collections.Generic;
using System.IO;
using System.Text;

namespace clase11.Blockchain
{
public class Blockchain
{
    public List<Block> Chain { get; private set; }
    public Blockchain()
    {
        Chain = new List<Block>();
        // Crear el bloque génesis
        var genesisBlock = new Block(0, new Usuario(), "0000");
        Chain.Insert(0, genesisBlock);
    }
    public void AddBlock(Usuario data)
    {
        int index = Chain[0].Index + 1;
        string previousHash = Chain[0].Hash;
        var newBlock = new Block(index, data, previousHash);
        Chain.Insert(0, newBlock);
   
    }

    public void GenerarDot(string rutaArchivo)
    {   
  
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph Blockchain {");
        dot.AppendLine("    node [shape=record, style=filled, fontname=\"Arial\"];");

        for (int i = 0; i < Chain.Count; i++)
        {
            Block block = Chain[i];
            Usuario user = block.Data;

            // Usamos solo partes del hash para que no sea tan largo el label
            string hashShort = block.Hash.Substring(0, 6);
            string prevHashShort = block.PreviousHash.Length >= 6 ? block.PreviousHash.Substring(0, 6) : block.PreviousHash;

            dot.AppendLine($"    Block{i} [label=\"{{ INDEX: {block.Index} | TIMESTAMP: {block.Timestamp} | ID: {user.ID} | Nombre: {user.Nombres}| contrasenia: {user.Contrasenia} | HASH: {hashShort} | PREV: {prevHashShort} }}\"];");

            if (i > 0)
            {
                dot.AppendLine($"    Block{i} -> Block{i - 1};");
            }
        }

        dot.AppendLine("}");

        File.WriteAllText(rutaArchivo, dot.ToString());
        Console.WriteLine($"Archivo DOT generado: {rutaArchivo}");
    }

}
}