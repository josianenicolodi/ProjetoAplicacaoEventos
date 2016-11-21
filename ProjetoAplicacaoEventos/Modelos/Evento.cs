using ProjetoAplicacaoEventos.Usuarios;
using System;
using System.Collections.Generic;
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
    }
}
