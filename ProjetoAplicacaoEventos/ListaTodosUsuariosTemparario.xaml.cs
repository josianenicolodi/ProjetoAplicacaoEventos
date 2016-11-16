using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ProjetoAplicacaoEventos.Usuarios;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for ListaTodosUsuariosTemparario.xaml
    /// </summary>
    public partial class ListaTodosUsuariosTemparario : Window
    {
        private static ListaTodosUsuariosTemparario instancia;

        private ListaTodosUsuariosTemparario()
        {
            InitializeComponent();
        }

        public static ListaTodosUsuariosTemparario Instancia
        {
            get
            {
                if (instancia == null)
                    instancia = new ListaTodosUsuariosTemparario();
                return instancia;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            UsuarioConteiner con = UsuarioConteiner.Load(UsuarioConteiner.path);

            listBox.ItemsSource = con.colecao;
        }
    }
}
