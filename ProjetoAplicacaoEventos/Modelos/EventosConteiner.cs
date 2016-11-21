using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ProjetoAplicacaoEventos.Conteiner
{
    [System.Serializable]
    public class EventosConteiner : ConteinerXml<EventosConteiner,Evento>
    {
        [XmlIgnore]
        public static string path = "Eventos.data";
        

        public EventosConteiner()
        {
            colecao = new List<Evento>();
        }

    }
}
