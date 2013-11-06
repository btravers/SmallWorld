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
        public Unite fabriquerUnite();
    }

    public class Vikings : Peuple, IVikings
    {
        public Unite fabriquerUnite()
        {
            return new UniteVikings();
        }
    }

    public class Gaulois : Peuple, IGaulois
    {
        public Unite fabriquerUnite()
        {
            return new UniteGaulois();
        }
    }

    public class Nains : Peuple, INains
    {
        public Unite fabriquerUnite()
        {
            return new UniteNains();
        }
    }
}
