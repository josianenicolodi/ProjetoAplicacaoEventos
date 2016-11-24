using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Conteiner
{
    [System.Serializable]
    public class Participante
    {
        private string nome;
        private string email;

        public Participante()
        {

        }

        public Participante(string nome, string email)
        {
            this.Nome = nome;
            this.Email = email;
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }
    }
}
