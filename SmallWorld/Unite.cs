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

        public int _pm
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

        public Unite()
        {
            this._attaque = 2;
            this._defense = 1;
            this._pdv = vitaMax;
            this._pm = 1;
            this.enVie = true;
        }

        /*public abstract bool peutPositionner(int x, int y, Case c);*/

        public void positionner(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public bool estSurCase(int x, int y)
        {
            return (this._x == x && this._y == y);
        }

        public bool estAPortee(int x, int y)
        {
            // TODO gérer la téléportation des nains
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) > -1;
        }

        public bool peutAttaquer(int x, int y)
        {
            return this._pm - Math.Abs(this._x - x) - Math.Abs(this._y - y) > -1;
        }

        public abstract bool peutDeplacer(int x, int y, Case c);

        public void deplacer(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public void attaquer(Unite adversaire)
        {
            // TODO
            Random rnd = new Random();
            int nb = 3 + rnd.Next(Math.Max(this._pdv, adversaire._pdv)+2);

            while ((nb > 0) && this.enVie && adversaire.enVie)
            {
                int forceAttaque = this._attaque * (this._pdv / this.vitaMax);
                int forceDefense = adversaire._defense * (adversaire._pdv / adversaire.vitaMax);
                if (forceDefense == 0)
                {
                    this.enVie = false;
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
                    if (adversaire._pdv == 0)
                    {
                        adversaire.enVie = false;
                    }
                }
                else
                {
                    this._pdv--;
                    if (this._pdv == 0)
                    {
                        this.enVie = false;
                    }
                }
                nb--;
            }

        }

        public void passerTour()
        {
            this._passeTour = true;
        }
    }

    public class UniteVikings : Unite, IUniteVikings
    {
        /*public override bool peutPositionner(int x, int y, Case c)
        {
            return true;
        }*/

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return true; // TODO
        }
    }

    public class UniteGaulois : Unite, IUniteGaulois
    {
        /*public override bool peutPositionner(int x, int y, Case c)
        {
            if (c is Eau)
            {
                return false;
            }
            return true;
        }*/

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return ! (c is Eau); // TODO
        }
    }

    public class UniteNains : Unite, IUniteNains
    {
        /*public override bool peutPositionner(int x, int y, Case c)
        {
            if(c is Eau)
            {
                return false;
            }
            return true;
        }*/

        public override bool peutDeplacer(int x, int y, Case c)
        {
            return !(c is Eau); // TODO
        }
    }
}
