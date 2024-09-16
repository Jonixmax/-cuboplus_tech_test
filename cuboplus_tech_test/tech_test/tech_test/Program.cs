using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

class ProofOfWork    
{
    static void Main()
    {
        string datos = "Tech_test"; // Datos del bloque

        // Probar diferentes niveles de dificultad
        for (int dificultad = 1; dificultad <= 6; dificultad++)
        {
            Console.WriteLine($" dificultad: {dificultad}");
            // Iniciar Proceso 
            Stopwatch crono = Stopwatch.StartNew();
            string hashvalido = Mine(datos, dificultad);
            crono.Stop();

            // Mostrar resultados
            Console.WriteLine($"Hash válido encontrado: {hashvalido}");
            Console.WriteLine($"Tiempo transcurrido: {crono.Elapsed.TotalSeconds} segundos\n");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
        }
    }

    static string Mine(string dato, int dificultad)
    {
        string cantidad = new string('0', dificultad); // Crear la cadena de ceros requeridos
        string hash = "";
        int nonce = 0;

        using (SHA256 sha256 = SHA256.Create())
        {
            while (true)
            {
                string input = dato + nonce;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                hash = ByteArrayToHexString(hashBytes);

                //verificacion del hash
                if (hash.StartsWith(cantidad))
                {
                    return hash;
                }

                nonce++;
            }
        }
    }

    // Función más eficiente para convertir bytes a hexadecimal
    static string ByteArrayToHexString(byte[] bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
}