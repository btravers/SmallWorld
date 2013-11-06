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
        public List<Unite> _unites
        {
            get;
            set;
        }

        public Joueur(String typePeuple, int nbUnites)
        {
            Peuple peuple = null;
            switch (typePeuple)
            {
                case "vikings":
                    peuple = new Vikings();
                    break;
                case "gaulois":
                    peuple = new Gaulois();
                    break;
                case "nains":
                    peuple = new Nains();
                    break;
            }
            for (int i = 0; i < nbUnites; i++)
            {
                this._unites.Add(peuple.fabriquerUnite());
            }
        }
    }
}
