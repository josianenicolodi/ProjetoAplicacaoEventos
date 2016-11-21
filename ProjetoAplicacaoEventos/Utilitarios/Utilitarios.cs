using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjetoAplicacaoEventos.Utilitarios
{
    public static class Utilitarios
    {

        public static bool IsEmpty(this string str)
        {
            return (str == string.Empty);
        }

        public static bool IsNull(this string str)
        {
            return (str == null);
        }
    }
}
