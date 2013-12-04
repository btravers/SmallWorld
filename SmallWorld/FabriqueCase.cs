using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IFabriqueCase
    {
    }

    public class FabriqueCase : IFabriqueCase
    {
        public Montagne _m
        {
            get;
            set;
        }

        public Plaine _p
        {
            get;
            set;
        }

        public Desert _d
        {
            get;
            set;
        }

        public Eau _e
        {
            get;
            set;
        }

        public Foret _f
        {
            get;
            set;
        }

        public FabriqueCase()
        {
            this._m = new Montagne();
            this._p = new Plaine();
            this._d = new Desert();
            this._e = new Eau();
            this._f = new Foret();
        }

        public Case getCase(TypeCase c)
        {
            switch (c)
            {
                case TypeCase.montagne:
                    return this._m;
                case TypeCase.plaine:
                    return this._p;
                case TypeCase.desert:
                    return this._d;
                case TypeCase.eau:
                    return this._e;
                case TypeCase.foret:
                    return this._f;
            }
            return null;
        }
    }
}
