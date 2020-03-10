using System;
using System.IO;
using System.IO.Compression;

namespace PUCMinasTCC.Util.Util
{
    public class Helper
    {
        /// <summary>
        /// Check if on CPF is valid
        /// </summary>
        /// <param name="vrCPF"></param>
        /// <returns></returns>
        public static bool IsCPF(string vrCPF)
        {
            vrCPF = vrCPF.Trim();

            if (!string.IsNullOrEmpty(vrCPF))
            {
                string valor = vrCPF.Replace(".", "");

                valor = valor.Replace("-", "");

                valor = valor.PadLeft(11, '0');

                bool igual = true;

                for (int i = 1; i < 11 && igual; i++)
                {
                    if (valor[i] != valor[0])
                    {
                        igual = false;
                    }
                }

                if (igual || valor == "12345678909")
                {
                    return false;
                }

                int[] numeros = new int[11];

                for (int i = 0; i < 11; i++)
                {
                    numeros[i] = int.Parse(valor[i].ToString());
                }

                int soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += (10 - i) * numeros[i];
                }

                int resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                    {
                        return false;
                    }
                }

                else if (numeros[9] != 11 - resultado)
                {
                    return false;
                }

                soma = 0;

                for (int i = 0; i < 10; i++)
                {
                    soma += (11 - i) * numeros[i];
                }

                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (numeros[10] != 11 - resultado)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
                return false;
        }

        public static string ReadBinaryFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var cmp = new GZipStream(fs, CompressionMode.Decompress))
                {
                    using (var bs = new BinaryReader(cmp))
                    {
                        if (fs.Length > 0)
                            return bs.ReadString();
                    }
                }
            }
            return null;
        }
        public static void WriteBinaryFile(string fileName, string value)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                using (var cmp = new GZipStream(fs, CompressionMode.Compress))
                {
                    using (var bs = new BinaryWriter(cmp))
                    {
                        bs.Write(value);
                    }
                }
            }
        }
    }
}
