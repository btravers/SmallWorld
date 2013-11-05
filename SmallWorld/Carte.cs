using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface ICarte
    {
    }

    public class Carte : ICarte
    {
        public FabriqueCase _fabrique
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Case[][] _cases
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
