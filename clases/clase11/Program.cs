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
        // path to the file
        string path = "usuarios.json";
        // check if the file exists
        string jsonData = File.ReadAllText(path);
        //Console.WriteLine(jsonData);
        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(jsonData);
        foreach (var usuario in usuarios)
        {
            // Encriptar la contraseña
            usuario.Contrasenia = EncriptacionSHa256(usuario.Contrasenia);
        }
        foreach (var usuario in usuarios)
        {
            Console.WriteLine($"ID: {usuario.ID}");
            Console.WriteLine($"Nombres: {usuario.Nombres}");
            Console.WriteLine($"Apellidos: {usuario.Apellidos}");
            Console.WriteLine($"Correo: {usuario.Correo}");
            Console.WriteLine($"Edad: {usuario.Edad}");
            Console.WriteLine($"Contrasenia: {usuario.Contrasenia}");
            Console.WriteLine();
        }
            Blockchain blockchain = new Blockchain();
            foreach (var user in usuarios)
            {
                blockchain.AddBlock(user);
            }
            foreach (var block in blockchain.Chain)
            {
                Console.WriteLine("----------------BLOQUE------------------------");
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Timestamp: {block.Timestamp}");
                Console.WriteLine($"ID: {block.Data.ID} {block.Data.Nombres}{block.Data.Apellidos}");
                Console.WriteLine($"Correo: {block.Data.Correo}");
                Console.WriteLine($"Edad: {block.Data.Edad}");
                Console.WriteLine($"Contrasenia: {block.Data.Contrasenia}");
                Console.WriteLine($"Data: {block.Data.Nombres} {block.Data.Apellidos}");
                Console.WriteLine($"Nonce: {block.Nonce}");
                Console.WriteLine($"PreviousHash: {block.PreviousHash}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine();
            }
            // Generar el archivo .dot
            string rutaArchivo = "blockchain.dot";
            //GenerarDot
            blockchain.GenerarDot(rutaArchivo);
            Console.WriteLine($"Archivo .dot generado en: {rutaArchivo}");
    }
    // blockchain




    static string EncriptacionSHa256(string text)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower(); 
        
        }
    }
}