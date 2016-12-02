using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjetoAplicacaoEventos.Utilitarios
{
    public static class Utilitarios
    {

        private static DateTime ultimaColsulta;

        public static bool IsEmpty(this string str)
        {
            return (str == string.Empty);
        }

        public static bool IsNull(this string str)
        {
            return (str == null);
        }

        public static DateTime GetNistTime()
        {
            if (ultimaColsulta != DateTime.MinValue)
            {
                int value = ultimaColsulta.Subtract(DateTime.Now).Minutes;
                if (value > -1)
                {
                    return ultimaColsulta;
                }
            }

            DateTime dateTime = DateTime.MinValue;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream());
                    string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                    string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                    double milliseconds = Convert.ToInt64(time) / 1000.0;
                    dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
                }
                else
                {
                    dateTime = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                dateTime = DateTime.Now;
            }
            ultimaColsulta = dateTime;
            return dateTime;
        }
    }

    public class Criptografia
    {

        public void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"Lol76352"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Encryption failed! "+e.Message, "Error");
            }
        }

        ///<summary>
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile">Arquivo de criptografado</param>
        ///<param name="outputFile">Arquivo temporario a ser utilizado(decriptografado)</param>
        public void DecryptFile(string inputFile, string outputFile)
        {
            FileStream fsCrypt;
            RijndaelManaged RMCrypto;
            CryptoStream cs;
            FileStream fsOut;

            try
            {
                string password = @"Lol76352"; 

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                using (fsCrypt = new FileStream(inputFile, FileMode.Open))
                {

                    RMCrypto = new RijndaelManaged();

                    cs = new CryptoStream(fsCrypt,
                        RMCrypto.CreateDecryptor(key, key),
                        CryptoStreamMode.Read);

                    fsOut = new FileStream(outputFile, FileMode.Create);

                    int data;
                    while ((data = cs.ReadByte()) != -1)
                        fsOut.WriteByte((byte)data);

                    fsOut.Close();
                    cs.Close();
                    fsCrypt.Close();

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao decryptografar: " + e.Message);
            }
            finally
            {

            }
        }

    }

}
