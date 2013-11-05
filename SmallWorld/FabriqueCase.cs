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
    }
}
