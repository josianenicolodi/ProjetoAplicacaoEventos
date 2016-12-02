using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjetoAplicacaoEventos.Conteiner;

namespace ProjetoAplicacaoEventos.Conteiner
{
    [System.Serializable]
    public class CategoriaConteiner : ConteinerXml<CategoriaConteiner,Categoria>
    {
        
        public static string path = "Categorias.data";

        public CategoriaConteiner()
        {
            colecao = new List<Categoria>();                    
        }

        public void Populacolecao()
        {
            if(colecao.Count <= 0)
            {
                colecao.AddRange(new List<Categoria>{
                new Categoria() { Nome = "Tecnologia", Descricao = "Eventos sobre tecnologias"},
                new Categoria() { Nome = "Medicina", Descricao = "Eventos sobre Medicina"},
                new Categoria() { Nome = "Doação de Animais", Descricao = "Eventos para adoção e conseguir verbas para ajudar animais"},
              });
            }
        }

        

    }
}
