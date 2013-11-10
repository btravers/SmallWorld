﻿using System;
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

        public Unite()
        {
            this._attaque = 2;
            this._defense = 1;
            this._pdv = 5;
            this._pm = 1;
        }

        public bool peutPositionner(int x, int y, Case c);

        public void positionner(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public bool estSurCase(int x, int y)
        {
            return (this._x == x && this._y == y);
        }

        public bool peutDeplacer(int x, int y, Case c);

        public void deplacer(int x, int y)
        {
            this._x = x;
            this._x = y;
        }

        public void attaquer(Unite adversaire)
        {
            // TODO
        }

        public void passerTour()
        {
            this._passeTour = true;
        }
    }

    public class UniteVikings : Unite, IUniteVikings
    {
        private static bool _utilise = false;

        public bool peutPositionner(int x, int y, Case c)
        {
            return true;
        }

        public bool peutDeplacer(int x, int y)
        {
            return false; // TODO
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

        public bool peutPositionner(int x, int y, Case c)
        {
            if (c is Eau)
            {
                return false;
            }
            return true;
        }

        public bool peutDeplacer(int x, int y)
        {
            return false; // TODO
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

        public bool peutPositionner(int x, int y, Case c)
        {
            if(c is Eau)
            {
                return false;
            }
            return true;
        }

        public bool peutDeplacer(int x, int y)
        {
            return false; // TODO
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
