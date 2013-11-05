using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IPeuple
    {
    }

    public interface IVikings : IPeuple
    {
    }

    public interface IGaulois : IPeuple
    {
    }

    public interface INains : IPeuple
    {
    }

    public abstract class Peuple : IPeuple
    {
        public List<Unite> _unites
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        
        public Peuple(int nbUnites){
            for (int i = 0; i < nbUnites; i++)
            {
                _unites.Add(new Unite());
            }
        }
    }

    public class Vikings : Peuple, IVikings
    {

    }

    public class Gaulois : Peuple, IGaulois
    {
    }

    public class Nains : Peuple, INains
    {
    }
}
