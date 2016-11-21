using ProjetoAplicacaoEventos.Usuarios;
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
using ProjetoAplicacaoEventos.Utilitarios;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for CadastorEvento.xaml
    /// </summary>
    public partial class CadastorEvento : Window
    {

        private static CadastorEvento instancia;

        CategoriaConteiner categoriaConteiner;
        UsuarioConteiner conteinerUsuario;
        EventosConteiner conteinerEventos;

        public static CadastorEvento Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CadastorEvento();
                    return instancia;
                }
                else
                {
                    return instancia;
                }
            }
        }


        private CadastorEvento()
        {
            InitializeComponent();

            conteinerUsuario = UsuarioConteiner.Load(UsuarioConteiner.path);
            categoriaConteiner = CategoriaConteiner.Load(CategoriaConteiner.path);
            conteinerEventos = EventosConteiner.Load(EventosConteiner.path);


            foreach (var item in categoriaConteiner.colecao)
            {
                cbCategoria.Items.Add(item.Nome);
            }
            cbCategoria.SelectedItem = cbCategoria.Items[0];
            for (int i = 0; i < 24; i++)
            {
                cbHora.Items.Add(i + ":00");
                cbHora.Items.Add(i + ":30");
            }
            cbHora.SelectedItem = cbHora.Items[24];
        }




        private void Window_Closed(object sender, EventArgs e)
        {
            instancia = null;
            if (Owner != null)
            {
                Owner.Show();
            }
        }

        ~CadastorEvento()
        {
            instancia = null;
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Hide();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

            Evento evento = new Evento();

            if (!txNome.Text.IsEmpty())
            {
                evento.Nome = txNome.Text;
            }
            if (!txEndereco.Text.IsEmpty())
            {
                evento.Endereco = txEndereco.Text;
            }
            if (!txDescri.Text.IsEmpty())
            {
                evento.Descricao = txDescri.Text;
            }

            evento.Categoria = categoriaConteiner.Get(
                x => x.Nome == cbCategoria.SelectedItem.ToString(),
                new Categoria() { Nome = "Indefinida" });

            evento.Criador = conteinerUsuario.curUsuario;
            DateTime d;

            if (dtbData.SelectedDate == null)
            {
                //lbErrorData.Content = "Deve selecionar uma data valida!";
            }
            else
            {
                d = dtbData.SelectedDate.Value;
                string[] s = new string[2];
                s = cbHora.SelectedItem.ToString().Split(':');
                TimeSpan t = new TimeSpan(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), 0);
                evento.Data = d.Add(t);                
            }


            conteinerEventos.Add(evento);
            conteinerEventos.Save(EventosConteiner.path);



            Owner.Show();
            Hide();
        }

    }
}
