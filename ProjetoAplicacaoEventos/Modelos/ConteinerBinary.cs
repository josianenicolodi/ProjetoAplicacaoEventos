using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Conteiner
{
    [System.Serializable]
    public class ConteinerBinary<T, C> : Conteiner<T, C>
    {
        public List<C> colecao;

        #region DAdO
        public override C Get(Predicate<C> predicado, C defaut)
        {
            if (colecao.Exists(predicado))
            {
                return colecao.Find(predicado);
            }
            return defaut;
        }

        public override List<C> GetTodos(Predicate<C> predicado)
        {
            return colecao.FindAll(predicado);
        }

        public override void Add(C Novo, Predicate<C> predicado)
        {
            if (!colecao.Exists(predicado))
            {
                colecao.Add(Novo);
            }
        }

        public override void Add(C novo)
        {
            colecao.Add(novo);
        }

        public override bool Existe(Predicate<C> condicao)
        {
            return colecao.Exists(condicao);
        }
        #endregion


        public override void Save(string path)
        {
            FileStream fs;

            string temp = @"temp123" + path;
            Utilitarios.Criptografia cr;

            if (File.Exists(path))
            {
                cr = new Utilitarios.Criptografia();
                cr.DecryptFile(path, temp);
            }


            using (fs = new FileStream(temp, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, this);
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            cr = new Utilitarios.Criptografia();
            cr.EncryptFile(temp, path);
            File.Delete(temp);

        }

        public static T Load(string path)
        {
            if (instancia == null)
            {
                if (File.Exists(path))
                {
                    FileStream fs;

                    string temp = @"temp123" + path;
                    Utilitarios.Criptografia cr;

                    cr = new Utilitarios.Criptografia();
                    cr.DecryptFile(path, temp);



                    using (fs = new FileStream(temp, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        try
                        {
                            instancia = (T)formatter.Deserialize(fs);
                        }
                        catch (SerializationException e)
                        {
                            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                            instancia = (T)Activator.CreateInstance(typeof(T));
                        }
                        finally
                        {
                            fs.Close();
                        }
                    }
                    File.Delete(temp);
                }
                else
                {
                    instancia = (T)Activator.CreateInstance(typeof(T));
                }
            }
            return instancia;
        }
    }
}
