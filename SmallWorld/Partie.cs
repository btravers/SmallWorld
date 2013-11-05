using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IPartie
    {
    }

    public class Partie : IPartie
    {
        public Joueur _jA
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Carte _carte
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Joueur _jB
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
