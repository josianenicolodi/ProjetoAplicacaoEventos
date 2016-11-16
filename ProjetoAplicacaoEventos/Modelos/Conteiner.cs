using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjetoAplicacaoEventos.Conteiner
{
    public abstract class ConteinerXml<T,C>
    {
        [XmlIgnore]
        protected static T instancia;  
          
        [XmlArray("colecao")]
        [XmlArrayItem("Item")]   
        public List<C> colecao;
        

        #region DAO
        public C Get(Predicate<C> predicado, C defaut)
        {
            if (colecao.Exists(predicado))
            {
                return colecao.Find(predicado);
            }
            return defaut;
        }

        public void Add(C Novo, Predicate<C> predicado)
        {
            if (!colecao.Exists(predicado))
            {
                colecao.Add(Novo);
            }
        }

        public void Add(C novo)
        {
            colecao.Add(novo);
        }

        public bool Existe(Predicate<C> condicao)
        {
            return colecao.Exists(condicao);
        }
        #endregion

        #region Save/Load
        public void Save(string path)
        {
            var serializable = new XmlSerializer(typeof(T));

            try
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    serializable.Serialize(stream, this);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);//Aqui sera lançada uma exeção presonalizada que gravara em um txt
            }
        }


        public static T Load(string path)
        {
            if (instancia == null)
            {
                if (File.Exists(path))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        instancia = (T)serializer.Deserialize(stream);                       
                    }
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
