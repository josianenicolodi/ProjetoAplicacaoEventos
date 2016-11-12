using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;



namespace ProjetoAplicacaoEventos.Usuarios
{

    


    [System.Serializable]
    class UsuarioConteiner
    {
        string path  = @"Usuarios.xml";
        public List<Usuario> usuarios;

        public void Save()
        {
            var serializable = new XmlSerializer(typeof(UsuarioConteiner));

            try
            {
                using(var stream = new FileStream(path, FileMode.Create))
                {
                    serializable.Serialize(stream, this);
                    stream.Close();
                }
            }
            catch (Exception e)
            {

               //Aqui vamos lançar uma exeção
            }
        }

        //public static UsuarioConteiner Carrega()
        //{

        //}
    }
}
