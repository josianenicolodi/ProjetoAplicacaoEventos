using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Evento
{
    public enum TIPO_FEIRA
    {
        Organica,
        Modelo,
        Mista
    }

    public class Feira : Evento
    {
        public TIPO_FEIRA _tipoFeira;


    }
}
