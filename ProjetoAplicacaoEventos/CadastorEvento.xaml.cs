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
using System.Timers;

namespace ProjetoAplicacaoEventos
{

    // Interacao logica para CadastroEvento.xaml
    // esta classe recebe por herança a classe Window 
    public partial class CadastroEvento : Window
    {

        private static CadastroEvento instancia;

        CategoriaConteiner categoriaConteiner;
        UsuarioConteiner conteinerUsuario;  
        EventosConteiner conteinerEventos;

        Timer myTimer;

        // propriedade que armazena instância
        public static CadastroEvento Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new CadastroEvento();
                    return instancia;
                }
                else
                {
                    return instancia;
                }
            }
        }

        // Construtor da classe do tipo privado
        private CadastroEvento()
        {
            // Inicializa a interface gráfica
            InitializeComponent();

            //instancia de conteiner de usuario, categoria do conteiner e eventos
            conteinerUsuario = UsuarioConteiner.Load(UsuarioConteiner.path);
            categoriaConteiner = CategoriaConteiner.Load(CategoriaConteiner.path);
            conteinerEventos = EventosConteiner.Load(EventosConteiner.path);

            // Adiciona itens no combobox
            foreach (var item in categoriaConteiner.colecao)
            {
                cbCategoria.Items.Add(item.Nome);
            }

            //Determina uma categoria pré selecionada
            cbCategoria.SelectedItem = cbCategoria.Items[0];
            for (int i = 0; i < 24; i++)
            {
                cbHora.Items.Add(i + ":00");
                cbHora.Items.Add(i + ":30");
            }
            cbHora.SelectedItem = cbHora.Items[24];
            //Determina uma hora pré selecionada


            dtbData.DisplayDateStart = Utilitarios.Utilitarios.GetNistTime();
            myTimer = new Timer();
           
        }



        // Metodo que mostra a janela que chamou esta
        private void Window_Closed(object sender, EventArgs e)
        {
            instancia = null;
            //if (Owner != null)
            //{
            //    Owner.Show();
            //}
        }

        // Destrutor da classe
        ~CadastroEvento()
        {
            instancia = null;
        }

        // Metodo que esconde a janela atual e mostra a que a chamou
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

            Participante p = new Participante(conteinerUsuario.curUsuario.Nome, conteinerUsuario.curUsuario.Email);

            evento.Criador = p;
            evento.AdicionaParticipante(p);
            DateTime dia;

            if (dtbData.SelectedDate == null)
            {
                // informar que a data selecionada é inválida
                //lbErrorData.Content = "Deve selecionar uma data valida!";
            }
            else
            {
                dia = dtbData.SelectedDate.Value;
                string[] s = new string[2];
                s = cbHora.SelectedItem.ToString().Split(':');
                TimeSpan hora = new TimeSpan(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), 0);
                dia = dia.Subtract(new TimeSpan(dia.Hour, dia.Minute, dia.Second));
                dia = dia.Add(hora);

                if( DateTime.Compare(dia,DateTime.Now) <= 0)//Valida se a data eh pro futuro
                {
                    lbErroData.Content = "Essa data já passou!";
                    return;
                }

                evento.Data = dia;                
            }

            // Caso todas as validações estejam corretas adiciona o evento ao conteiner
            conteinerEventos.Add(evento);
            // Salva o conteiner
            conteinerEventos.Save(EventosConteiner.path);


            // Mostra a janela anterior e esconde a atual
            Owner.Show();
            Hide();
        }

    }
}
