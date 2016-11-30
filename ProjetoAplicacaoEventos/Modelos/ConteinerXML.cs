using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjetoAplicacaoEventos.Conteiner
{
    public abstract class ConteinerXml<T, C> : Conteiner<T, C>
    {

        [XmlArray("colecao")]
        [XmlArrayItem("Item")]
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

        #region Save/Load
        public override void Save(string path)
        {
            var serializable = new XmlSerializer(typeof(T));

            try
            {
                if (File.Exists(path))
                    File.Decrypt(path);
                using (var stream = new FileStream(path, FileMode.Create))
                {

                    serializable.Serialize(stream, this);
                    stream.Close();
                }
                    File.Encrypt(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static T Load(string path)
        {
            if (instancia == null)
            {
                if (File.Exists(path))
                {
                    File.Decrypt(path);
                    var serializer = new XmlSerializer(typeof(T));
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        instancia = (T)serializer.Deserialize(stream);
                    }
                        File.Encrypt(path);
                }
                else
                {
                    instancia = (T)Activator.CreateInstance(typeof(T));
                }
            }
            return instancia;
        }
        #endregion
    }
}
