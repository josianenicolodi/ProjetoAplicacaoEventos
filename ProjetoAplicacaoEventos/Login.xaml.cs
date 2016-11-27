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
using System.Text.RegularExpressions;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private static Login instancia;

        UsuarioConteiner conteinerUsuario;

        public static Login Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Login();
                }
                return instancia;
            }
        }

        public Login()
        {
            InitializeComponent();
            conteinerUsuario = UsuarioConteiner.Load(UsuarioConteiner.path);
            LbError.Content = Utilitarios.Utilitarios.GetNistTime().ToString();
        }

        private void txNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            LbError.Content = "";
        }


        private void btCriarconta_Click_1(object sender, RoutedEventArgs e)
        {
            CadastroUsuario cadastro = new CadastroUsuario();
            cadastro.Owner = this;
            cadastro.ShowDialog();
            
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            Logar();
        }

        private void Logar()
        {
            string email;
            string senha;

            email = txEmail.Text;
            senha = txSenha.Password;


            Usuario user;

            if (conteinerUsuario.Existe(x => x.Email == email))
            {

                user = conteinerUsuario.Get(x => x.Email == email, new Usuario("Invalido!"));

                if (senha == user.Senha)
                {
                    conteinerUsuario.curUsuario = user;

                    MainWindow janela = MainWindow.GetInstancia();

                    janela.Owner = this;
                    Hide();
                    janela.Show();
                }
                else
                {
                    LbError.Content = "Senha Incorreta!";
                }
            }
            else
            {
                LbError.Content = "Usuario não cadastrado!";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ListaTodosUsuariosTemparario j = ListaTodosUsuariosTemparario.Instancia;
 
            j.Owner = this;
            Hide();
            j.Show();
            //CadastraCategoria.Instancia.Show();
        }

        private void winLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Logar();
            }
        }

        private void winLogin_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txEmail_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        public static bool emailInvalido(string email)
        {

            Regex regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            Match match = regExpEmail.Match(email);

            return !(match.Success);
        }

        private void txEmail_LostFocus(object sender, RoutedEventArgs e)
        {

            if (emailInvalido(txEmail.Text))
            {
                lbLOginErro.Content = "Email invalido!";
            }
            else
            {
                lbLOginErro.Content = string.Empty;
            }
        }
    }
}
