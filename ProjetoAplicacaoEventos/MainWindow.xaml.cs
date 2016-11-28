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
using System.Timers;
using System.Windows.Threading;
using System.ComponentModel;

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
        DispatcherTimer dtClockTime;


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

            InicializarTimer();

        }

        void InicializarTimer()
        {
            dtClockTime = new DispatcherTimer();

            dtClockTime.Interval = new TimeSpan(0, 1, 0); //in Hour, Minutes, Second.
            dtClockTime.Tick += AlertaEventos;

            dtClockTime.Start();
        }

        void AlertaEventos(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += BuscaEventosAgora;

            worker.RunWorkerCompleted += MostraEventosAgora;
            worker.RunWorkerAsync();
        }

        void MostraEventosAgora(object sender, RunWorkerCompletedEventArgs e)
        {
            if (((List<Evento>)e.Result).Count > 0)
            {
                StringBuilder st = new StringBuilder();
                for (int i = 0; i < ((List<Evento>)e.Result).Count; i++)
                {
                    Evento ev = ((List<Evento>)e.Result)[i];

                    st.Append(ev.Nome);
                    st.Append(" - ");
                }
                st.Append(" ===> Vâo começar agora.");
                MessageBox.Show(st.ToString());
            }
        }

        void BuscaEventosAgora(object sender, DoWorkEventArgs e)
        {
            List<Evento> participo = new List<Evento>(); 
            DateTime agora = Utilitarios.Utilitarios.GetNistTime();
            TimeSpan ts;

            foreach (var item in conteinerEventos.GetEventos(x => x.Participa(usuario.Email)))
            {
                ts = item.Data.Subtract(agora);
                if(ts.Days == 0 && ts.Hours == 00 && ts.Minutes < 10)
                {
                    participo.Add(item);
                }
            }
           
            e.Result = participo;
        }

        void Inicializar(Usuario usr)
        {
            usuario = usr;
            lbUsuarioNome.Content = "Bem Vindo, " + usuario.Nome;
            PrencheCategoriasDrop();
            PreencheListAllEventos();
        }

        void PreencheGenerico(ComboBox cb, ObservableCollection<Evento> cole,CheckBox ckb
            , ListView list,
            Predicate<Evento> TodasCategotias, Predicate<Evento> PorCategoria)
        {

            DateTime agora = Utilitarios.Utilitarios.GetNistTime();
            bool passado = false;

            if (ckb.IsChecked != null)
                passado = ckb.IsChecked.Value;
            string name = cb.SelectedItem.ToString();
            List<Evento> temp = conteinerEventos.colecao;
            if (name == "Todas Categorias")
            {
                temp = conteinerEventos.GetEventos(TodasCategotias);
                if (!passado)
                {
                    cole = new ObservableCollection<Evento>(temp.FindAll(
                            x => DateTime.Compare(x.Data, agora) > 0)
                        );
                }
                else
                {
                    cole = new ObservableCollection<Evento>(temp);
                }
            }
            else
            {
                temp = conteinerEventos.GetEventos(PorCategoria);
                if (passado)
                {
                    cole = new ObservableCollection<Evento>(temp);
                }
                else
                {
                    cole = new ObservableCollection<Evento>(temp.FindAll(
                                 x => DateTime.Compare(x.Data, agora) > 0)
                                 );
                }
            }
            list.ItemsSource = cole.OrderBy(x => x.Data);
        }

        void PreencheListAllEventos()
        {
            string name = cbCategoriasFiltroAll.SelectedItem.ToString();
            PreencheGenerico(cbCategoriasFiltroAll, eventosAll,ckEventosPassado, listBoxEventosAll, x => x is Evento, x => x.Categoria.Nome == name);

        }

        void PreencheListAParticipoEventos()
        {
            string name = cbCategoriasFiltroParti.SelectedItem.ToString();

            PreencheGenerico(cbCategoriasFiltroParti, eventosParticipo,ckEventosPassado1,
                listBoxEventosParticipo, x => x.Participa(usuario.Email), x => x.Participa(usuario.Email) && x.Categoria.Nome == name);

        }
        void PreencheListMeusEventos()
        {
            //Usuario usuario = conteinerUsuario.curUsuario;

            string name = cbCategoriasFiltroAdm.SelectedItem.ToString();

            PreencheGenerico(cbCategoriasFiltroAdm, eventosMeus,ckEventosPassado3,
                listBoxEventosMeus, x => x.Criador.Email == usuario.Email,
                x => (x.Criador.Email == usuario.Email) && (x.Categoria.Nome == name));
        }

        private void PrencheCategoriasDrop()
        {
            cbCategoriasFiltroAdm.Items.Add("Todas Categorias");
            cbCategoriasFiltroAll.Items.Add("Todas Categorias");
            cbCategoriasFiltroParti.Items.Add("Todas Categorias");

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
            Button bt = (Button)sender;
            Grid grid = (Grid)bt.Parent;
            Evento ev = (Evento)grid.DataContext;

            ev.AdicionaParticipante(new Participante(usuario.Nome, usuario.Email));
        }

        private void cbCategoriasFiltroAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreencheListAllEventos();
        }

        private void cbCategoriasFiltroParti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreencheListAParticipoEventos();
        }

        private void cbCategoriasFiltroAdm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreencheListMeusEventos();
        }

        private void btDetalhes_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Button bt = (Button)sender;
            Grid grid = (Grid)bt.Parent;
            Evento evento = (Evento)grid.DataContext;

            DetalhesEvento dt = new DetalhesEvento(evento);
            dt.ShowDialog();
        }

        private void ckEventosPassado_Click(object sender, RoutedEventArgs e)
        {
            PreencheListAllEventos();
        }

        private void ckEventosPassado1_Click(object sender, RoutedEventArgs e)
        {
            PreencheListAParticipoEventos();
        }

        private void ckEventosPassado3_Click(object sender, RoutedEventArgs e)
        {
            PreencheListMeusEventos();
        }
    }
}
