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

        public int _points
        {
            get;
            set;
        }

        public String _name
        {
            get;
            set;
        }

        public String _peuple
        {
            get;
            set;
        }

        public Joueur(String typePeuple, int nbUnites)
        {
            Peuple peuple = null;
            this._unites = new List<Unite>();
            switch (typePeuple)
            {
                case "vikings":
                    peuple = new Vikings();
                    _peuple = "Vikings";
                    break;
                case "gaulois":
                    peuple = new Gaulois();
                    _peuple = "Gaulois";
                    break;
                case "nains":
                    peuple = new Nains();
                    _peuple = "Nains";
                    break;
            }
            for (int i = 0; i < nbUnites; i++)
            {
                this._unites.Add(peuple.fabriquerUnite());
            }
        }

        public void ajouterPoints(int pts)
        {
            this._points += pts;
        }

        public Unite obtenirMeilleurUnite(int x, int y)
        {
            List<Unite> unites = new List<Unite>();

            foreach (Unite unite in this._unites)
            {
                if (unite.estSurCase(x, y) && unite.enVie)
                {
                    unites.Add(unite);
                }
            }

            if (unites.Any())
            {
                Unite meilleur = unites.First();

                foreach (Unite unite in unites)
                {
                    if (unite._defense > meilleur._defense)
                    {
                        meilleur = unite;
                    }
                }

                return meilleur;
            }
            else
            {
                return null;
            }
        }

        /*public void perdreUnite(Unite u)
        {
            this._unites.Remove(u);
        }*/
    }
}
