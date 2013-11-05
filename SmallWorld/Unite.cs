using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IUnite
    {
    }

    public class Unite : IUnite
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
            this._pdv = 2;
            this._pm = 1;
        }
    }
}
