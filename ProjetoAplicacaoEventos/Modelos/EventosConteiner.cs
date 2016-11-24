using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace ProjetoAplicacaoEventos.Conteiner
{
    /// <summary>
    /// Responsavel com conter todos os eventos, 
    /// como herda de ConteinerXML possui metodo para
    /// Salvar em disco, ler do disco, adicionor novo evento 
    /// na colecao, pegar um evento espacifico da colecao
    /// 
    /// </summary>
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
