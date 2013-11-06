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
            get;
            set;
        }

        public Joueur _jB
        {
            get;
            set;
        }

        public Carte _carte
        {
            get;
            set;
        }

        public int _toursRestant
        {
            get;
            set;
        }

        private int _premierJouer;

        public Partie()
        {
            Random rnd = new Random();
            _premierJouer = rnd.Next(2);
        }

        public bool tourJoueurA()
        {
            return (_toursRestant % 2) == 0;
        }

        
    }
}
