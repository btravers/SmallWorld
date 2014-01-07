using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
     * Interface définissant une unité
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IUnite
    {
    }

    /**
     * Interface définissant une unité viking
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IUniteVikings : IUnite
    {
    }

    /**
     * Interface définissant une unité gauloise
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IUniteGaulois : IUnite
    {
    }

    /**
     * Interface définissant une unité naine
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IUniteNains : IUnite
    {
    }

    /**
     * Classe abstraite (non instantiable ici) définissant une unité
     * @author Mickael Olivier, Benoit Travers
     */
    public abstract class Unite : IUnite
    {
        /** Les points d'attaque de l'unité */
        public int _attaque
        {
            get;
            set;
        }

        /** Les poitns de défense de l'unité */
        public int _defense
        {
            get;
            set;
        }

        /** Les points de vie de l'unité */
        public int _pdv
        {
            get;
            set;
        }

        /** Les points de mouvement de l'unité (nombre de déplacements sur case adjascente) */
        public double _pm
        {
            get;
            set;
        }

        /** La coordonée en abscisse de l'unité */
        public int _x
        {
            get;
            set;
        }

        /** La coordonée en ordonnée de l'unité */
        public int _y
        {
            get;
            set;
        }

        /** Le type de case sur laquelle est l'unité actuellement */
        public TypeCase _terrain
        {
            get;
            set;
        }

        /** Un booléen permettant de savoir si l'unité passe son tour */
        public bool _passeTour
        {
            get;
            set;
        }

        /** Un booléen permettant de savoir si l'unité est encore en vie ou pas */
        public bool enVie
        {
            get;
            set;
        }

        /** Le nombre de points de vie maximal d'une unité */
        public readonly int vitaMax = 5;

        /** 
         * Fonction donnant le type de l'unité  courante
         * @return le type de l'unité sous forme de String
         */
        public abstract string typeUnite();

        /**
         * Constructeur de base d'une unité (bien que la classe soit abstraite, 
         * les classes qui héritent d'Unité passent par ce constructeur)
         */
        public Unite()
        {
            this._attaque = 2;
            this._defense = 1;
            this._pdv = vitaMax;
            this._pm = 1;
            this.enVie = true;
        }

        /**
         * Fonction permettant de positionne l'unité courante
         * @param x l'abscisse de la coordonée de positionnement
         * @param y l'ordonnée de la coordonée de positionnement
         * @param c la case sur laquelle va se positionner l'unité
         */
        public void positionner(int x, int y, Case c)
        {
            this._x = x;
            this._y = y;
            _terrain = c.type();
        }

        /**
         * Fonction permettant de savoir si l'unité courante est sur la case de coordonées x,y
         * @param x l'abscisse de la coordonée
         * @param y l'ordonnée de la coordonée
         * @return Vrai si l'unité est sur la case x,y, faux sinon
         */
        public bool estSurCase(int x, int y)
        {
            return (this._x == x && this._y == y);
        }

        /**
         * Fonction permettant de savoir si une case est à portée de déplacement d'une unité 
         * @param x l'abscisse de la case que regarde l'unité
         * @param y l'ordonnée de la case que regarde l'unité
         * @param c la case que regarde l'unité
         */
        public abstract bool estAPortee(int x, int y, Carte c);

        /**
         * Fonction permettant de savoir si l'unité peut attaquer une case
         * (on vérifie au préalable la présence d'un ennemi)
         * @param x l'abscisse de la case que désire attaquer l'unité
         * @param y l'ordonnée de la case que désire ttaquer l'unité
         */
        public bool peutAttaquer(int x, int y)
        {
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) == 0;
        }

        /**
         * Fonction permettant de savoir si l'unité peut se déplacer sur la case qu'elle regarde
         * @param x l'abscisse de la case sur laquelle désire se déplacer l'unité
         * @param y l'ordonnée de la case sur laquelle désire se déplacer l'unité
         * @param c la case sur laquelle désire se déplacer l'unité
         * @return Vrai si l'unité peut se déplacer sur la case, faux sinon
         */
        public abstract bool peutDeplacer(int x, int y, Case c);

        /**
         * Fonction permettant de compter le nombre depoints
         * que rapporte cette unité en fin de tour du joueur auquel elle appartient
         * @return le nombre de points ainsi rapporté
         */
        public abstract int getPoints();

        /**
         * Fonction permettant de déplacer l'unité sur la case qu'elle regarde
         * @param x l'abscisse de la case sur laquelle désire se déplacer l'unité
         * @param y l'ordonnée de la case sur laquelle désire se déplacer l'unité
         * @param c la case sur laquelle désire se déplacer l'unité
         */
        public abstract void deplacer(int x, int y, Case c);

        /**
         * Fonction qui permet à l'unité courante d'attaquer une unité adverse
         * @param adversaire l'unité adversaire
         */
        public void attaquer(Unite adversaire)
        {
            Random rnd = new Random();

            //Nombre de combats prédéterminé
            int nb = 3 + rnd.Next(Math.Max(this._pdv, adversaire._pdv)-1);
            Console.WriteLine(nb + " combats auront lieu");

            //Tant que des combats ont toujours lieu
            while ((nb > 0) && this.enVie && adversaire.enVie)
            {
                int forceAttaque = this._attaque * (this._pdv / this.vitaMax);
                int forceDefense = adversaire._defense * (adversaire._pdv / adversaire.vitaMax);
                if (forceDefense == 0)
                {
                    adversaire.enVie = false;
                }
                double ratioAttaqueDefense = (double)forceAttaque / (double)forceDefense;
                double chanceDef = 0.5;
                if(ratioAttaqueDefense > 1)
                {
                    chanceDef = 0.5 * (1 / ratioAttaqueDefense) + 0.5;
                    chanceDef = 1 - chanceDef;
                }
                else if (ratioAttaqueDefense < 1) 
                {
                    chanceDef = 0.5 * ratioAttaqueDefense + 0.5;
                }

                double alea = ((double)rnd.Next(100) / (double)100);

                if (alea < chanceDef)
                {
                    adversaire._pdv--;
                    if (adversaire._pdv < 1)
                    {
                        adversaire.enVie = false;
                    }
                }
                else
                {
                    this._pdv--;
                    if (this._pdv < 1)
                    {
                        this.enVie = false;
                    }
                }
                nb--;
                Console.WriteLine("Points de vie du conquérant " + _pdv);
                Console.WriteLine("Points de vie de l'opposant " + adversaire._pdv);
            }
        }

        /**
         * Fonction permettant à l'unité courante de passer son tour
         */
        public void passerTour()
        {
            this._passeTour = true;
        }
    }

    /** 
     * Classe concrète représentant une unité viking
     * @author Mickael Olivier, Benoit Travers
     */
    public class UniteVikings : Unite, IUniteVikings
    {
        public override string typeUnite()
        {
            return "vikings";
        }

        public override bool estAPortee(int x, int y, Carte c)
        {
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) > -1;
        }

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return true; // TODO
        }

        public override void deplacer(int x, int y, Case c)
        {
            if (x != this._x || y != this._y)
            {
                this._pm--;
            }
            this._x = x;
            this._y = y;
            this._terrain = c.type();
        }

        public override int getPoints()
        {
            int score = 0;
            //TODO
            if(_terrain != TypeCase.eau && _terrain != TypeCase.desert)
            {
                score = 1;
            }

            return score;
        }
    }

    /** 
     * Classe concrète représentant une unité gauloise
     * @author Mickael Olivier, Benoit Travers
     */
    public class UniteGaulois : Unite, IUniteGaulois
    {
        public override string typeUnite()
        {
            return "gaulois";
        }

        public override bool estAPortee(int x, int y, Carte c)
        {
            if ((Math.Abs(this._x - x) + Math.Abs(this._y - y)) == 2 && _pm == 1 && c._cases[x, y] is Plaine)
            {
                if ((this._x == x && c._cases[x, (this._y + y) / 2] is Plaine) || (this._y == y && c._cases[(this._x + x) / 2, y] is Plaine))
                {
                    return true;
                }
                else if (this._x != x && this._y != y && (c._cases[this._x, y] is Plaine || c._cases[x, this._y] is Plaine))
                {
                    return true;
                }
            }

            if (c._cases[x, y] is Plaine && (Math.Abs(this._x - x) + Math.Abs(this._y - y)) == 1)
            {
                return this._pm != 0;
            }
            return (this._pm == 1) && (Math.Abs(this._x - x) + Math.Abs(this._y - y) == 1);
        }

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return ! (c is Eau);
        }

        public override void deplacer(int x, int y, Case c)
        {
            if (Math.Abs(this._x - x) + Math.Abs(this._y - y) == 2)
            {
                this._pm--;
            }
            else
            {
                if (c is Plaine && (x != this._x || y != this._y))
                {
                    this._pm = this._pm - 0.5;
                }
                else
                {
                    if (x != this._x || y != this._y)
                    {
                        this._pm--;
                    }
                }
            }
            this._x = x;
            this._y = y;
            this._terrain = c.type();
        }

        public override int getPoints()
        {
            if (_terrain == TypeCase.plaine)
            {
                return 2;
            }
            else if (_terrain == TypeCase.montagne)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }


    /** 
     * Classe concrète représentant une unité naine
     * @author Mickael Olivier, Benoit Travers
     */
    public class UniteNains : Unite, IUniteNains
    {
        public override string typeUnite()
        {
            return "nains";
        }

        public override bool estAPortee(int x, int y, Carte c)
        {
            if (c._cases[x,y] is Montagne && this._terrain == TypeCase.montagne && this._pm == 1)
            {
                return true;
            }
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) > -1;
        }

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return !(c is Eau);
        }

        public override void deplacer(int x, int y, Case c)
        {
            if (x != this._x || y != this._y)
            {
                this._pm--;
            }
            this._x = x;
            this._y = y;
            this._terrain = c.type();
        }

        public override int getPoints()
        {
            if (_terrain == TypeCase.foret)
            {
                return 2;
            }
            else if (_terrain == TypeCase.plaine)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
