using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAplicacaoEventos.Conteiner
{

    public interface Salvavel
    {
        
    }

    public abstract class Conteiner<T, C>
    {

        protected static T instancia;


        #region DAdO
        public virtual C Get(Predicate<C> predicado, C defaut)
        {
            return defaut;
        }

        public virtual List<C> GetTodos(Predicate<C> predicado)
        {
            return new List<C>();
        }

        public virtual void Add(C Novo, Predicate<C> predicado) { }

        public virtual void Add(C novo) { }

        public virtual bool Existe(Predicate<C> condicao)
        {
            return false;
        }
        #endregion

        #region Save/Load
        public abstract void Save(string path);

       //nao pensei em como fazer o load  ser statico e virtul
       //pensei em fazer uma interface e tal

        #endregion
    }
}
