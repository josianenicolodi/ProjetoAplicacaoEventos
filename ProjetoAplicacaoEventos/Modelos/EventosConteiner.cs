using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ProjetoAplicacaoEventos.Conteiner;

namespace ProjetoAplicacaoEventos.Evento
{
    [System.Serializable]
    public class EventosConteiner : ConteinerXml<EventosConteiner,Evento>
    {
        [XmlIgnore]
        public static string path = "Categorias.xml";
        

        private EventosConteiner()
        {
            colecao = new List<Evento>();
        }

    }
}
