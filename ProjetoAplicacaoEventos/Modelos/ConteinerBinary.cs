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
            using (fs = new FileStream("DataFile.dat", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, colecao);
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
           
        }
    }
}
