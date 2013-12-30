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

        public int _width
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
            this._width = taille;
        }

        public bool bordEau(int x, int y)
        {
            bool test = false;
            if (x + 1 < this._width)
            {
                test |= (this._cases[x + 1, y].type() == TypeCase.eau);
            }
            if (y + 1 < _width)
            {
                test |= (this._cases[x, y + 1].type() == TypeCase.eau);
            }
            if (x - 1 >= 0)
            {
                test |= (this._cases[x - 1, y].type() == TypeCase.eau);
            }
            if (y - 1 >= 0)
            {
                test |= (this._cases[x, y - 1].type() == TypeCase.eau);
            }

            return test;
        }
    }
}
