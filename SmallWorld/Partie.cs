using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using WrapperSmallWorld;

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

        public bool egalite()
        {
            return _jA._points == _jB._points;
        }

        public String gagnant()
        {
            if (_jA._points > _jB._points)
                return _jA._name;
            else
                return _jB._name;
        }

        public bool joueJoueurA()
        {
            return _joueur == 0;
        }

        public void positionnerUnites(Joueur j, int x, int y)
        {
            foreach (Unite unite in j._unites)
            {
                unite.positionner(x, y, this._carte._cases[x, y]);
            }
        }

        public void joueurSuivant()
        {
            this.calculerPoints();
            Joueur j = _jB;
            if (this.joueJoueurA())
            {
                j = _jA;
            }
            foreach (Unite unite in j._unites)
            {
                unite._passeTour = false;
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

        public List<int> suggestion()
        {
            String peuple = _uniteSelectionnee.typeUnite();

            Joueur j = _jA;
            if(joueJoueurA())
            {
                j = _jB;
            }

            int rg = _uniteSelectionnee._x * _carte._width + _uniteSelectionnee._y;
            List<int> carte = new List<int>();
            for (int i = 0; i < _carte._width; i++)
            {
                for (int k = 0; k < _carte._width; k++)
                {
                    carte.Add(_carte._cases[i,k].typeCase());
                }
            }

            return Destinations.destinations(peuple, rg, carte, _carte._width, _uniteSelectionnee._pm, j.Poisitions);
        }

        public void selectCaseDestination(int x, int y)
        {
            if (this._uniteSelectionnee == null)
                return;

            if (_uniteSelectionnee.estAPortee(x, y, _carte))
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
                    if (_uniteSelectionnee.peutAttaquer(x,y))
                    {
                        Console.WriteLine("Un combat a lieu !");
                        this._uniteSelectionnee.attaquer(uniteAdverse);
                        Unite testPresence = joueur.obtenirMeilleurUnite(x, y);
                        if ((testPresence == null) && _uniteSelectionnee.peutDeplacer(x, y, this._carte._cases[x, y]))
                        {
                            this._uniteSelectionnee.deplacer(x, y, this._carte._cases[x, y]);
                        }
                    }
                }
                else
                {
                    if (_uniteSelectionnee.peutDeplacer(x, y, this._carte._cases[x, y]))
                    {
                        this._uniteSelectionnee.deplacer(x, y, this._carte._cases[x, y]);
                    }
                }
            }

            this._uniteSelectionnee = null;
        }

        public void calculerPoints()
        {
            int score = 0;
            Joueur j = _jB;
            if (joueJoueurA())
            {
                j = _jA;
            }

            foreach(Unite u in j._unites)
            {
                score += u.getPoints();
                if (u is UniteVikings && _carte.bordEau(u._x, u._y)  && _carte._cases[u._x, u._y].type() != TypeCase.eau)
                {
                    score++;
                    Console.WriteLine("Un point supplémentaire car unite viking au bord de l'eau !");
                }
            }

            j._points += score;
        }
    }
}
