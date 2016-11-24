using ProjetoAplicacaoEventos.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Conteiner
{
    [System.Serializable]
    public class Evento 
    {
        private string nome;
        private string endereco;
        private string descricao;

        private Categoria categoria;
        private DateTime data;
        private Usuario criador;
        public List<Participante> participantes;


        public Evento()
        {
            participantes = new List<Participante>();
        }
        #region Propriedades
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

        public string Endereco
        {
            get
            {
                return endereco;
            }

            set
            {
                endereco = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public Categoria Categoria
        {
            get
            {
                return categoria;
            }

            set
            {
                categoria = value;
            }
        }

        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }

        public Usuario Criador
        {
            get
            {
                return criador;
            }

            set
            {
                //if (criador != null)
                criador = value;
            }
        }

       
        public List<Participante> Participantes
        {
            get
            {
                return participantes;
            }

            set
            {
                participantes = value;
            }
        }
        #endregion

        public bool Participa(string email)
        {
            return participantes.Find(x => x.Email == email) != null;
        }

        public void AdicionaParticipante(Participante participante)
        {
            participantes.Add(participante);
        }

        public override string ToString()
        {
            StringBuilder st = new StringBuilder();
            st.Append(nome);
            st.Append(" em: ");
            st.Append(data.ToShortDateString());

            return st.ToString();
        }
    }
   
}
