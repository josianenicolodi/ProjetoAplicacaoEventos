using System;

namespace ProjetoAplicacaoEventos
{
    public class Usuario
    {
        string nome;
        string senha;
        string email;
        string endereco;
        string documento;
        DateTime dataNascimento;

        public Usuario()
        {

        }

        public Usuario(string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }

        public Usuario(string nome)
        {
            this.nome = nome;
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

        public string Senha
        {
            get
            {
                return senha;
            }

            set
            {
                senha = value;
            }
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

        public string Documento
        {
            get
            {
                return documento;
            }

            set
            {
                documento = value;
            }
        }

        public DateTime DataNascimento
        {
            get
            {
                return dataNascimento;
            }

            set
            {
                dataNascimento = value;
            }
        }
    }
}