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

        private Unite _uniteSelectionnee;

        public Partie()
        {
            Random rnd = new Random();
            _premierJoueur = rnd.Next(2);
            this._uniteSelectionnee = null;
        }

        public bool tourJoueurA()
        {
            return (_toursRestant % 2) == 0;
        }

        public bool peutPositionnerUnitesJoueurA(int x, int y)
        {
            if (!(this._carte._cases[x, y] is Eau))
            {
                return true;
            }
            if (_jA._unites.First() is UniteVikings)
            {
                return true;
            }
            return false;
        }

        public void positionnerUnitesJoueurA(int x, int y)
        {
            foreach (Unite unite in this._jA._unites)
            {
                unite.positionner(x, y);
            }
        }

        public bool peutPositionnerUnitesJoueurB(int x, int y)
        {
            if (!(this._carte._cases[x, y] is Eau))
            {
                return true;
            }
            if (_jB._unites.First() is UniteVikings)
            {
                return true;
            }
            return false;
        }

        public void positionnerUnitesJoueurB(int x, int y)
        {
            foreach (Unite unite in this._jB._unites)
            {
                unite.positionner(x, y);
            }
        }

        public void selectCaseInit(int x, int y)
        {
            Joueur joueur;
            if (this.tourJoueurA())
            {
                joueur = this._jA;
            }
            else
            {
                joueur = this._jB;
            }
            
            foreach (Unite unite in joueur._unites)
            {
                if (unite.estSurCase(x, y))
                {
                    this._uniteSelectionnee = unite;
                }
            }

            this._uniteSelectionnee = null;
        }

        public void selectCaseDest(int x, int y)
        {
            if (_uniteSelectionnee.peutDeplacer(x, y))
            {
                Joueur joueur;
                if (this.tourJoueurA())
                {
                    joueur = this._jB;
                }
                else
                {
                    joueur = this._jA;
                }

                List<Unite> adversaires = new List<Unite>();

                foreach (Unite uniteAdvers in joueur._unites)
                {
                    if (uniteAdvers.estSurCase(x, y))
                    {
                        adversaires.Add(uniteAdvers);
                    }
                }

                if (adversaires.Any())
                {
                    Unite meilleur = adversaires.First();
                    foreach (Unite u in adversaires)
                    {
                        if (u._defense > meilleur._defense)
                        {
                            meilleur = u;
                        }
                    }
                    this._uniteSelectionnee.attaquer(meilleur);
                }
                else
                {
                    this._uniteSelectionnee.deplacer(x, y);
                }
            }
        }
    }
}
