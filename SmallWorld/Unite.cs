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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int _defense
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int _pdv
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int _pm
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int _x
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int _y
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Unite()
        {
            this._attaque = 2;
            this._defense = 1;
            this._pdv = 5;
            this._pm = 1;
        }

        public void positionner(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public bool estSurCase(int x, int y)
        {
            return (this._x == x && this._y == y);
        }

        public bool peutDeplacer(int x, int y);

        public void deplacer(int x, int y)
        {
            this._x = x;
            this._x = y;
        }

        public void attaquer(Unite adversaire)
        {

        }
    }

    public class UniteVikings : Unite, IUniteVikings
    {
        private static bool _utilise = false;

        public bool peutDeplacer(int x, int y)
        {
            return false;
        }

        public static bool estUtilise()
        {
            return _utilise;
        }

        public static void utilise()
        {
            _utilise = true;
        }
    }

    public class UniteGaulois : Unite, IUniteGaulois
    {
        private static bool _utilise = false;

        public bool peutDeplacer(int x, int y)
        {
            return false;
        }

        public static bool estUtilise()
        {
            return _utilise;
        }

        public static void utilise()
        {
            _utilise = true;
        }
    }

    public class UniteNains : Unite, IUniteNains
    {
        private static bool _utilise = false;

        public bool peutDeplacer(int x, int y)
        {
            return false;
        }

        public static bool estUtilise()
        {
            return _utilise;
        }

        public static void utilise()
        {
            _utilise = true;
        }
    }
}
