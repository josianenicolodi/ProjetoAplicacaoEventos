using System.Collections.Generic;

namespace ProjetoAplicacaoEventos
{
    public class GerenciadorDeUsuarios
    {
        private static GerenciadorDeUsuarios instancia;

        List<Usuario> Usuarios;

        private GerenciadorDeUsuarios()
        {
            Usuarios = new List<Usuario>();

            Usuarios.Add(new Usuario("Lola", "Lola"));
            Usuarios.Add(new Usuario("Athena", "Athena"));
            Usuarios.Add(new Usuario("Hanna", "Hanna"));
        }

        public static GerenciadorDeUsuarios GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new GerenciadorDeUsuarios();
            }
            return instancia;
        }


        public Usuario GetUsuario(string nome)
        {
            foreach (var usuario in Usuarios)
            {
                if (usuario.Nome == nome)
                {
                    return usuario;

                }
            }
            return new Usuario("Invalido!");
        }


        public Usuario GetUsuarioPerEmail(string email)
        {
            foreach (var usuario in Usuarios)
            {
                if (usuario.Email  == email)
                {
                    return usuario;

                }
            }
            return new Usuario("Invalido!");
        }


        public void AddUsuario(Usuario usuario)
        {
            if (!ExisteUsuario(usuario.Email))
            {
                Usuarios.Add(usuario);                
            }
        }

        /// <summary>
        /// Verifica se um usuario com esse email ja nos esta cadastrado.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExisteUsuario(string email)
        {
            foreach (var item in Usuarios)
            {
                if (item.Email == email)
                {
                    return true;
                }
            }

            return false;
        }

    }
}