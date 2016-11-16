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
using ProjetoAplicacaoEventos.Evento;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Usuario usuario;
        CategoriaConteiner categoriaConteiner;

       
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            Inicializar(usuario);
;
        }

        void Inicializar(Usuario usr)
        {
            usuario = usr;
            lbUsuarioNome.Content = "Bem Vindo, " + usuario.Nome;
            categoriaConteiner = CategoriaConteiner.Load(CategoriaConteiner.path);
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
    }
}
