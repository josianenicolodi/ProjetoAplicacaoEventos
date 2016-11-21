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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjetoAplicacaoEventos.Usuarios;
using ProjetoAplicacaoEventos.Conteiner;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static MainWindow instancia;

        Usuario usuario;
        CategoriaConteiner categoriaConteiner;
        UsuarioConteiner conteinerUsuario;
        EventosConteiner conteinerEventos;

        public static MainWindow GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MainWindow();
            }
            return instancia;
        }

        
        public MainWindow()
        {
            InitializeComponent();

            categoriaConteiner = CategoriaConteiner.Load(CategoriaConteiner.path);
            conteinerUsuario = UsuarioConteiner.Load(UsuarioConteiner.path);
            usuario = conteinerUsuario.curUsuario;

            conteinerEventos = EventosConteiner.Load(EventosConteiner.path);


            if (usuario == null)
            {
                instancia = null;
                if(Owner != null)
                {
                    Owner.Show();
                    Hide();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
            else
            {
                Inicializar(usuario);
            }
            
        }

        void Inicializar(Usuario usr)
        {
            usuario = usr;
            lbUsuarioNome.Content = "Bem Vindo, " + usuario.Nome;
            PrencheCategoriasDrop();
        }

        private void PrencheCategoriasDrop()
        {
            foreach (var item in categoriaConteiner.colecao)
            {
                cbCategoriasFiltroAdm.Items.Add(item.Nome);
                cbCategoriasFiltroAll.Items.Add(item.Nome);
                cbCategoriasFiltroParti.Items.Add(item.Nome);
            }
            cbCategoriasFiltroAdm.SelectedItem = cbCategoriasFiltroAdm.Items[0];
            cbCategoriasFiltroAll.SelectedItem = cbCategoriasFiltroAll.Items[0];
            cbCategoriasFiltroParti.SelectedItem = cbCategoriasFiltroParti.Items[0];
        }

        private void cbCategoriasFiltro_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            instancia = null;
            Hide();
            Owner.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CadastorEvento j = CadastorEvento.Instancia;
            j.Owner = this;
            Hide();
            j.Show();
        }
    }
}
