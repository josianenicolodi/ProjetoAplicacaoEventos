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
using ProjetoAplicacaoEventos.Utilitarios;
using ProjetoAplicacaoEventos.Conteiner;

namespace ProjetoAplicacaoEventos
{
    /// <summary>
    /// Interaction logic for CadastraCategoria.xaml
    /// </summary>
    public partial class CadastraCategoria : Window
    {

        static CadastraCategoria instancia;

        private CadastraCategoria()
        {
            InitializeComponent();
        }

        static public CadastraCategoria Instancia
        {
            get
            {
                if(instancia == null)
                {
                    instancia = new CadastraCategoria();
                }
                return instancia;
            }

        }

        private void ___No_Name__Closed(object sender, EventArgs e)
        {
            instancia = null;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            lbErroNome.Content = string.Empty;
            lbErroDescri.Content = string.Empty;
            txNome.Text = string.Empty;
            txDescricao.Text = string.Empty;
            Owner.Show();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            if(ValidaCampo(txNome, lbErroNome) && ValidaCampo(txDescricao, lbErroDescri))
            {
                CategoriaConteiner categoriaConteiner = CategoriaConteiner.Load(CategoriaConteiner.path);
                categoriaConteiner.Populacolecao();
                if(!categoriaConteiner.Existe(x => x.Nome == txNome.Text))
                {
                    categoriaConteiner.Add(new Categoria()
                    {
                        Nome = txNome.Text,
                        Descricao = txDescricao.Text
                    });
                }
                categoriaConteiner.Save(CategoriaConteiner.path);
                Owner.Show();
                Hide();
            }
           
        }

        private bool ValidaCampo(TextBox campo, Label erroMsg)
        {
            bool ok = true;
            if (campo.Text.IsNull())
            {
                ok = false;
                erroMsg.Content = "Erro, string nula ou invalida!!";
            }
            if (campo.Text.IsEmpty())//txNome.Text == string.Empty)
            {
                ok = false;
                erroMsg.Content = "Campo não pode estar Vazio!!";
            }
            else if (campo.Text.Length < 4)
            {
                ok = false;
                erroMsg.Content = "Campo deve ter mais de 4 caracteres";
            }
            return ok;
        }

        void ClearCampo(Label lb)
        {
            lb.Content = string.Empty;
        }

        private void txNome_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearCampo(lbErroNome);
        }

        private void txDescricao_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearCampo(lbErroDescri);
        }

        private void txDescricao_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
