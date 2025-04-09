using System;
using System.Security.Cryptography;
using System.Text;
namespace clase11.Blockchain
{
    public class Block
    {
        public int Index { get; set; }
        public string Timestamp { get; set; }
        public Usuario Data { get; set; }
        public int Nonce { get; set; }
        public string PreviousHash { get; set; }

        public string Hash { get; set; }
     
    

    public Block(int index, Usuario data, string previousHash)
    {
        Index = index;
        Timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Data = data;
        Nonce = 0;
        PreviousHash = previousHash;
        Hash = CalculateHash();
    }
    private string GenerateHash()
    {
        string rawdata = Index + Timestamp + SerializeUsuario(Data) + Nonce + PreviousHash;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawdata));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

    }
    private string CalculateHash()
    {
        string hash;
        do{
            hash = GenerateHash();
            Nonce++;
        }while(!hash.StartsWith("0000")); // prueba de trabajo de simple
        return hash;
    }
    private string SerializeUsuario(Usuario usuario)
    {
        return $"{usuario.ID}{usuario.Nombres}{usuario.Apellidos}{usuario.Correo}{usuario.Edad}{usuario.Contrasenia}";
    }
}
}