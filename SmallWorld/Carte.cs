using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface ICarte
    {
    }

    public class Carte : ICarte
    {
        public FabriqueCase _fabrique
        {
            get;
            set;
        }

        public Case[,] _cases
        {
            get;
            set;
        }

        public Carte(int taille)
        {
            this._cases = new Case[taille,taille];
        }
    }
}
