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
        public abstract Unite fabriquerUnite();
    }

    public class Vikings : Peuple, IVikings
    {
        public override Unite fabriquerUnite()
        {
            return new UniteVikings();
        }
    }

    public class Gaulois : Peuple, IGaulois
    {
        public override Unite fabriquerUnite()
        {
            return new UniteGaulois();
        }
    }

    public class Nains : Peuple, INains
    {
        public override Unite fabriquerUnite()
        {
            return new UniteNains();
        }
    }
}
