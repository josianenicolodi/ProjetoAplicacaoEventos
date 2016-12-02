using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using ProjetoAplicacaoEventos.Conteiner;

namespace ProjetoAplicacaoEventos.Usuarios
{

    [System.Serializable]
    // Armazena dados de usuários
    public class UsuarioConteiner : ConteinerBinary<UsuarioConteiner,Usuario>
    {
        public static string path = @"Usuarios.xml";
        public Usuario curUsuario;

        // Construtor que inicializa a lista
        public UsuarioConteiner()
        {
            colecao = new List<Usuario>();
        }
    }
}
