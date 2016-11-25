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
using System.Collections.ObjectModel;

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

        ObservableCollection<Evento> eventosAll;
        ObservableCollection<Evento> eventosParticipo;
        ObservableCollection<Evento> eventosMeus;

        public ICommand ClickMeCommand { get; set; }

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
                if (Owner != null)
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

            PreencheListAllEventos();
        }

        void PreencheListAllEventos()
        {
            eventosAll = new ObservableCollection<Evento>(conteinerEventos.colecao);
            listBoxEventosAll.ItemsSource = eventosAll;

           

        }

        void PreencheListAParticipoEventos()
        {
            eventosParticipo = new ObservableCollection<Evento>(conteinerEventos.GetEventos
                (
                x => x.Participa(usuario.Email))
                );
            listBoxEventosParticipo.ItemsSource = eventosParticipo.OrderBy(x => x.Data);
          
        }
        void PreencheListMeusEventos()
        {
            Usuario usuario = conteinerUsuario.curUsuario;
            eventosMeus = new ObservableCollection<Evento>(conteinerEventos.GetEventos(
                x => x.Criador.Email == usuario.Email));
            listBoxEventosMeus.ItemsSource = eventosMeus;
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
            CadastroEvento j = CadastroEvento.Instancia;
            j.Owner = this;
            Hide();
            j.Show();
        }

        private void AbaAllEventos_GotFocus(object sender, RoutedEventArgs e)
        {
            PreencheListAllEventos();
        }

        private void AbaEventoMeus_GotFocus(object sender, RoutedEventArgs e)
        {
            PreencheListMeusEventos();
        }

        private void AbaEventoParticipo_GotFocus(object sender, RoutedEventArgs e)
        {
            PreencheListAParticipoEventos();
        }

        private void btParticiapr_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sender.ToString());
            Button bt = (Button)sender;
            Grid grid = (Grid)bt.Parent;
            Evento ev = (Evento)grid.DataContext;

            ev.AdicionaParticipante(new Participante(usuario.Nome, usuario.Email));
            string tes = conteinerEventos.colecao[0].Nome;
        }
    }
}
