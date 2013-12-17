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

        private int _premierJoueur;

        private int _joueur;

        public Unite _uniteSelectionnee
        {
            get;
            set;
        }


        public Partie()
        {
            Random rnd = new Random();
            this._premierJoueur = rnd.Next(2);
            this._joueur = this._premierJoueur;
            this._uniteSelectionnee = null;
        }

        public bool joueJoueurA()
        {
            return _joueur == 0;
        }

        public void positionnerUnites(Joueur j, int x, int y)
        {
            foreach (Unite unite in j._unites)
            {
                unite.positionner(x, y);
            }
        }

        public void joueurSuivant()
        {
            if (this.joueJoueurA())
            {
                foreach (Unite unite in this._jA._unites)
                {
                    unite._passeTour = false;
                }
            }
            else 
            {
                foreach (Unite unite in this._jB._unites)
                {
                    unite._passeTour = false;
                }
            }
            this._joueur = (this._joueur + 1) % 2;
            if (this._joueur == this._premierJoueur)
            {
                this._toursRestant--;
            }
        }

        public List<Unite> selectCaseInitiale(int x, int y)
        {
            this._uniteSelectionnee = null;

            Joueur joueur;
            if (this.joueJoueurA())
            {
                joueur = this._jA;
            }
            else
            {
                joueur = this._jB;
            }

            List<Unite> unitesCase = new List<Unite>(); 

            foreach (Unite unite in joueur._unites)
            {
                if (unite.estSurCase(x, y))
                {
                    if (!unite._passeTour)
                    {
                        unitesCase.Add(unite);
                    }
                }
            }

            if (unitesCase.Any())
            {
                _uniteSelectionnee = unitesCase[0];
            }

            return unitesCase;
        }

        public void selectCaseDestination(int x, int y)
        {
            if (this._uniteSelectionnee == null)
                return;

            if (_uniteSelectionnee.estAPortee(x, y))
            {
                Joueur joueur;
                if (this.joueJoueurA())
                {
                    joueur = this._jB;
                }
                else
                {
                    joueur = this._jA;
                }

                Unite uniteAdverse = joueur.obtenirMeilleurUnite(x, y);

                if (uniteAdverse != null)
                {
                    this._uniteSelectionnee.attaquer(uniteAdverse);
                }
                else
                {
                    if (_uniteSelectionnee.peutDeplacer(x, y, this._carte._cases[x, y]))
                    {
                        this._uniteSelectionnee.deplacer(x, y);
                    }
                }
            }

            this._uniteSelectionnee._pm--;

            this._uniteSelectionnee = null;
        }
    }
}
