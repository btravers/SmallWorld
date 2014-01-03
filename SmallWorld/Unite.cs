using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IUnite
    {
    }
    
    public interface IUniteVikings : IUnite
    {
    }

    public interface IUniteGaulois : IUnite
    {
    }

    public interface IUniteNains : IUnite
    {
    }
    
    public abstract class Unite : IUnite
    {

        public int _attaque
        {
            get;
            set;
        }

        public int _defense
        {
            get;
            set;
        }

        public int _pdv
        {
            get;
            set;
        }

        public double _pm
        {
            get;
            set;
        }

        public int _x
        {
            get;
            set;
        }

        public int _y
        {
            get;
            set;
        }

        public TypeCase _terrain
        {
            get;
            set;
        }

        public bool _passeTour
        {
            get;
            set;
        }

        public bool enVie
        {
            get;
            set;
        }

        public readonly int vitaMax = 5;

        public abstract string typeUnite();

        public Unite()
        {
            this._attaque = 2;
            this._defense = 1;
            this._pdv = vitaMax;
            this._pm = 1;
            this.enVie = true;
        }

        public void positionner(int x, int y, Case c)
        {
            this._x = x;
            this._y = y;
            _terrain = c.type();
        }

        public bool estSurCase(int x, int y)
        {
            return (this._x == x && this._y == y);
        }

        public abstract bool estAPortee(int x, int y, Carte c);

        public bool peutAttaquer(int x, int y)
        {
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) > -1;
        }

        public abstract bool peutDeplacer(int x, int y, Case c);

        public abstract int getPoints();

        public abstract void deplacer(int x, int y, Case c);

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
                    chanceDef = 0.5 * ratioAttaqueDefense +0.5;
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

        public void passerTour()
        {
            this._passeTour = true;
        }
    }

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

    public class UniteGaulois : Unite, IUniteGaulois
    {
        public override string typeUnite()
        {
            return "gaulois";
        }

        public override bool estAPortee(int x, int y, Carte c)
        {
            if (c._cases[x, y] is Plaine && (Math.Abs(this._x - x) + Math.Abs(this._y - y)) == 2 && (c._cases[this._x, y] is Plaine || c._cases[x, this._y] is Plaine))
            {
                return this._pm == 1;
            }
            if (c._cases[x, y] is Plaine && (Math.Abs(this._x - x) + Math.Abs(this._y - y)) == 2)
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

    public class UniteNains : Unite, IUniteNains
    {
        public override string typeUnite()
        {
            return "nains";
        }

        public override bool estAPortee(int x, int y, Carte c)
        {
            if (c._cases[x,y] is Montagne && this._terrain == TypeCase.montagne)
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
