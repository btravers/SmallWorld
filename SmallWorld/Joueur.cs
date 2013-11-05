using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IJoueur
    {
    }

    public class Joueur : IJoueur
    {
        public Peuple _peuple
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Joueur(Peuple peuple)
        {
            this._peuple = peuple;
        }
    }
}
