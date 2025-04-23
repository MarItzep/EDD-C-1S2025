using System;
using System.Security.Cryptography;
using System.Text;

class program
{
    static void Main(string[] args)
    {
        string contrasenia = "password123";
        // generar un hash SHA256

        string hash = GenerarHashSHA256(contrasenia);
        // VERIFICAR si la contraseña coincide con el hash
        string temporal_contrasenia = "password123";
        // hash el que vamos a retornar para el usuario o contrasenia 
        // la contrasenia y la comparamos con la contrasenia de ingreso
        bool coincide = VerificarHashSHA256(temporal_contrasenia, hash);
        if (coincide)
        {
            Console.WriteLine("La contraseña coincide con el hash.");
        }
        else
        {
            Console.WriteLine("La contraseña no coincide con el hash.");
        }

    }

    static string GenerarHashSHA256(string contrasenia)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    static bool VerificarHashSHA256(string contrasenia, string hash)
    {
        string hashContrasenia = GenerarHashSHA256(contrasenia);
        Console.WriteLine($"Contraseña: {contrasenia}");
        Console.WriteLine($"Hash de la contraseña: {hashContrasenia}");
        Console.WriteLine($"Hash a verificar: {hash}");
        // Comparar los hashes

        return hash.Equals(hashContrasenia, StringComparison.OrdinalIgnoreCase);

        
    }
}