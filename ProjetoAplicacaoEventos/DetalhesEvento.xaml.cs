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
using ProjetoAplicacaoEventos.Conteiner;
using ProjetoAplicacaoEventos.Usuarios;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for DetalhesEvento.xaml
    /// </summary>
    public partial class DetalhesEvento : Window
    {
 
        private Evento evento;
        Usuario usuario;

        public DetalhesEvento(Evento evento)
        {
            InitializeComponent();
            this.evento = evento;
            DataContext = evento;
            usuario = UsuarioConteiner.Load(UsuarioConteiner.path).curUsuario; ;
            lbName.Content = evento.Nome;
            lbLocal.Content = evento.Endereco;
            lbDescricao.Text = evento.Descricao;
            lbCriador.Content = evento.Criador.Nome;
            lbData.Content = evento.Data.ToLongDateString();
            lbHora.Content = evento.Data.TimeOfDay;
            lbCategoria.Content = evento.Categoria.Nome;

            listBox.ItemsSource = evento.Participantes;
        }

        public void OnParticipar()
        {
            evento.AdicionaParticipante(new Participante(usuario.Nome, usuario.Email));
        }

        public void OnRemove()
        {
            evento.RemoveParticipante(new Participante(usuario.Nome, usuario.Email));
        }

        private void btParticipar_Click(object sender, RoutedEventArgs e)
        {
            OnParticipar();
            Close();
        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            OnRemove();
            Close();
        }
    }
}
