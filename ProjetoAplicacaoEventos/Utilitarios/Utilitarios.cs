using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Utilitarios
{
    public static class Utilitarios
    {

        /// <summary>
        /// Verifica se a string é vazia "". 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
