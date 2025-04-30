using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using clase11.Blockchain;

class Program
{
    static void Main(string[] args)
    {
        string path = "usuarios.json"; // Asegúrate de que esté en el directorio correcto
        string jsonData = File.ReadAllText(path);
        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(jsonData);

        // Encriptar contraseñas
        foreach (var user in usuarios)
        {
            user.Contrasenia = EncryptSHA256(user.Contrasenia);
        }

        Blockchain blockchain = new Blockchain();

        foreach (var user in usuarios)
        {
            blockchain.AddBlock(user);
        }

        foreach (var block in blockchain.Chain)
        {
            Console.WriteLine("--------- BLOQUE ---------");
            Console.WriteLine($"Index: {block.Index}");
            Console.WriteLine($"Timestamp: {block.Timestamp}");
            Console.WriteLine($"Usuario: {block.Data.Nombres} {block.Data.Apellidos}");
            Console.WriteLine($"Correo: {block.Data.Correo}");
            Console.WriteLine($"Edad: {block.Data.Edad}");
            Console.WriteLine($"Contraseña (hash): {block.Data.Contrasenia}");
            Console.WriteLine($"Nonce: {block.Nonce}");
            Console.WriteLine($"Previous Hash: {block.PreviousHash}");
            Console.WriteLine($"Hash: {block.Hash}");
            Console.WriteLine();
        }

        // Serializar la blockchain a JSON
        string blockchainJson = JsonConvert.SerializeObject(blockchain.Chain, Formatting.Indented);

        // Guardar la blockchain en un archivo JSON
        string blockchainFilePath = "blockchain.json";
        File.WriteAllText(blockchainFilePath, blockchainJson);
        Console.WriteLine($"Blockchain guardada en: {blockchainFilePath}");

        Console.WriteLine("Archivo DOT generado:");
        string dotFilePath = "blockchain.dot";
        blockchain.GenerarDot(dotFilePath);
        Console.WriteLine($"Archivo DOT generado: {dotFilePath}");
    }

    static string EncryptSHA256(string text)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
