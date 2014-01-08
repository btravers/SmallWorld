using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.IO;
using System.Xml.Serialization;

using WrapperSmallWorld;

namespace SmallWorld
{
    /**
     * Interface définissant une partie
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IPartie
    {
    }

    /**
     * Classe définissant une partie
     * @author Mickael Olivier, Benoit Travers
     */
    [Serializable()]
    public class Partie : IPartie
    {
        /** Le joueur A de cette partie */
        [XmlAttribute()]
        public Joueur _jA
        {
            get;
            set;
        }

        /** Le joueur B de cette partie */
        [XmlAttribute()]
        public Joueur _jB
        {
            get;
            set;
        }

        /** La carte de cette partie */
        [XmlAttribute()]
        public Carte _carte
        {
            get;
            set;
        }

        /** Le nombre de tours restant à jouer pour cette partie */
        [XmlAttribute()]
        public int _toursRestant
        {
            get;
            set;
        }

        /** Le premier joueur au début de la partie choisi au hasard */
        [XmlAttribute()]
        public int _premierJoueur
        {
            get;
            set;
        }

        /** Le joeuur qui joue actuellement */
        [XmlAttribute()]
        public int _joueur
        {
            get;
            set;
        }


        /** L'unité selectionnée par le joueur quand c'est son tour */
        [XmlAttribute()]
        public Unite _uniteSelectionnee
        {
            get;
            set;
        }

        /**
         * Créateur de la partie
         */
        public Partie()
        {
            //On choisi le premier joueur au hasard et aucune unité n'est selectionnée
            Random rnd = new Random();
            this._premierJoueur = rnd.Next(2);
            this._joueur = this._premierJoueur;
            this._uniteSelectionnee = null;
        }

        /**
         * Fonction permettant de savoir si deux joueurs sont à égalité ou non
         * @return Vrai si les deux joeuurs sont à égalité de points, faux sinon
         */
        public bool egalite()
        {
            return _jA._points == _jB._points;
        }

        /**
         * Fonction permettant de définir le gagnant
         * On vérifie avant son appel qu'il n'y a apas d'égalité
         * @return Le nom du gagnant
         */
        public String gagnant()
        {
            if (_jA._points > _jB._points)
                return _jA._name;
            else
                return _jB._name;
        }

        /**
         * Fonction permettant de savoir quel joueur joue
         * @return Vrai si le joueur A joue, faux si c'est le joueur B qui joue
         */
        public bool joueJoueurA()
        {
            return _joueur == 0;
        }

        /**
         * Fonction permettant de positionner les unités d'un joueur à une case de coordonnées x,y au début de la partie
         * @param j le joueur pour lequel il faut positionner les unités
         * @param x l'abscisse de la coordonée de positionnement
         * @param y l'ordonnée de la coordonée de positionnement
         */
        public void positionnerUnites(Joueur j, int x, int y)
        {
            foreach (Unite unite in j._unites)
            {
                unite.positionner(x, y, this._carte._cases[x, y]);
            }
        }

        /*
         * Fonction permettant de définir le joueur suivant à la fin d'un tour de jeu
         */
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

        /**
         * Fonction qui permet de sélectionner les unités sur une case de coordonnées x,y
         * @param x l'abscisse de la coordonée
         * @param y l'ordonnée de la coordonée
         */
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

        /**
         * Fonction qui permet d'obtenir les suggestions de déplacements pour l'unité sélectionnée
         * @return La liste des positions suggérées
         */
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

            return Destinations.destinations(peuple, rg, carte, _carte._width, _uniteSelectionnee._pm, j.Positions);
        }

        /**
         * Fonction chargée de sélectionner la case de destination de l'unité sélectionnée
         * @param x l'abscisse de la cordonnée de destination
         * @param y l'ordonnée de la coordonée de destination
         */
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
                        if ((testPresence == null) && _uniteSelectionnee.peutDeplacer(x, y, this._carte._cases[x, y]) && this._uniteSelectionnee.enVie)
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

        /**
         * Fonction qui permet de calculer les points à la fin d'un tour d'un joueur
         */
        public void calculerPoints()
        {
            //De base le score est nul
            int score = 0;

            //On récupère le bon joueur
            Joueur j = _jB;
            if (joueJoueurA())
            {
                j = _jA;
            }

            //Pour chaque unité dans les unités du joueur ayant fini son tour
            foreach(Unite u in j._unites)
            {
                //On calcule les points
                score += u.getPoints();

                //Si jamais l'unité est viking et au bord de l'eau alors on rajoute un point pour cette unité
                if (u is UniteVikings && _carte.bordEau(u._x, u._y)  && _carte._cases[u._x, u._y].type() != TypeCase.eau)
                {
                    score++;
                }
            }

            //On ajoute aux points du joueur finissant son tour le score ainsi calculé
            j._points += score;
        }

        public void Sauvegarder(string filename)
        {
            
            XmlSerializer serializer = new XmlSerializer(typeof(Partie));
            using (StreamWriter file = new StreamWriter(filename))
            {
                serializer.Serialize(file, this);
            }
        }

        public static Partie Charger(string filename)
        {
            Partie partie;
            XmlSerializer serializer = new XmlSerializer(typeof(Partie));
            using (StreamReader file = new StreamReader(filename))
            {
                partie = serializer.Deserialize(file) as Partie;
            }
            return partie;
        }
    }
}
