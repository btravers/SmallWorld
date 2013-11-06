﻿using System;
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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Plaine _p
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Desert _d
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Eau _e
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Foret _f
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public FabriqueCase()
        {
            this._m = new Montagne();
            this._p = new Plaine();
            this._d = new Desert();
            this._e = new Eau();
            this._f = new Foret();
        }

        public Case getCase(String c)
        {
            switch (c)
            {
                case "montagne":
                    return this._m;
                case "plaine":
                    return this._p;
                case "desert":
                    return this._d;
                case "eau":
                    return this._e;
                case "foret":
                    return this._f;
            }
            return null;
        }
    }
}
