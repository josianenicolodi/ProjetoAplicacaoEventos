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
using System.Text.RegularExpressions;
using ProjetoAplicacaoEventos.Usuarios;


namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for CadastroUsuario.xaml
    /// </summary>
    public partial class CadastroUsuario : Window
    {

        bool valido = true;
        Usuario usuario;
        UsuarioConteiner usuarioConteiner;


        public CadastroUsuario()
        {
            InitializeComponent();

            usuario = new Usuario();
            usuarioConteiner = UsuarioConteiner.Load(UsuarioConteiner.path);

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            btCadastrar.IsEnabled = false;
            valido = true;
            ValidaNome();
            ValidaEmail();
            ValidaEnd();
            ValidaData();
            ValidaDocumento();
            ValidaSenha();

            if (valido)
            {
                SetValueUser();

                if (!usuarioConteiner.Existe(x => x.Email == usuario.Email))
                {
                    //usuarioConteiner.Add(usuario, x => x.Email == usuario.Email);
                    usuarioConteiner.Add(usuario);
                    usuarioConteiner.Save(UsuarioConteiner.path);
                    Login login = new Login();
                    login.Show();
                    this.Close();
                }else
                {
                    lbErrorEmail.Content = "Email já cadastrado!";
                    txEmail.Text = string.Empty;
                }
            }
            btCadastrar.IsEnabled = true;
        }

        private void SetValueUser()
        {
            usuario.Nome = txNome.Text;
            usuario.Email = txEmail.Text;
            usuario.Endereco = txEnd.Text;
            usuario.DataNascimento = dateNasc.SelectedDate.Value;
            usuario.Documento = txDocumento.Text;
            usuario.Senha = txSenha.Text;
        }

        private void ValidaSenha()
        {
            string senha = txSenha.Text;
            if (senha == "")
            {
                valido = false;
                lbErrorSenha.Content = "Campo Senha não pode estar vazio!";

            }
            else if (senha.Length < 6)
            {
                valido = false;
                lbErrorSenha.Content = "Senha deve conter no minimo 6 caracteres";
            }
            else
            {
                lbErrorSenha.Content = "";
                if (senha != txRepitSenha.Text)
                {
                    lbErrorSenha.Content = "Senhas devem ser iguais!";
                }
                else
                {
                    lbErrorSenha.Content = "";
                }
            }
        }

        private void ValidaDocumento()
        {
            string documento = txDocumento.Text;



            if (documento == "")
            {
                valido = false;
                lbErrorDocumento.Content = "Campo Documento não pode estar vazio!";
            }
            else
            {
                lbErrorDocumento.Content = "";
            }
        }

        private void ValidaData()
        {
            if (dateNasc.SelectedDate == null)
            {
                lbErrorData.Content = "Deve selecionar uma data valida!";
            }
            else
            {
                DateTime data = dateNasc.SelectedDate.Value;
            }
        }

        private void ValidaEnd()
        {
            string end = txEnd.Text;

            if (end == "")
            {
                valido = false;
                lbErrorEnd.Content = "Campo endereço não pode estar vazio!";
            }
            else
            {
                lbErrorEnd.Content = string.Empty;

            }
        }

        private void ValidaEmail()
        {
            string email = txEmail.Text;
            if (email == "")
            {
                valido = false;
                lbErrorEmail.Content = "Campo Email nao pode estar vazio!"; ;

            }
            else if (emailInvalido(email))
            {
                valido = false;
                lbErrorEmail.Content = "Email invalido!";
            }
            else
            {
                lbErrorEmail.Content = "";
                txLogin.Text = email;
            }
        }

        public static bool emailInvalido(string email)
        {

            Regex regExpEmail = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            Match match = regExpEmail.Match(email);

            return !(match.Success);
        }


        private void ValidaNome()
        {
            string Nome = txNome.Text;
            if (Nome == "")
            {
                valido = false;
                lbErrorNome.Content = "Campo Nome nao pode estar vazio!";
            }
            else if (Nome.Length < 3)
            {
                valido = false;
                lbErrorNome.Content = "Nome deve conter mais do que 3 caracteres";
            }
            else
            {
                lbErrorNome.Content = "";
            }
        }

        private void txEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidaEmail();
        }
    }
}
