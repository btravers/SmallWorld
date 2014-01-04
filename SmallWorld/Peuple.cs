using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
     * Interface définissant un peuple
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IPeuple
    {
    }

    /**
     * Interface définissant le peuple "Vikings"
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IVikings : IPeuple
    {
    }

    /**
     * Interface définissant le peuple "Gaulois"
     * @author Mickael Olivier, Benoit Travers
     */
    public interface IGaulois : IPeuple
    {
    }

    /**
     * Interface définissant le peuple "Nains"
     * @author Mickael Olivier, Benoit Travers
     */
    public interface INains : IPeuple
    {
    }

    /**
     * Classe abstraite (non instantiable ici) définissant un peuple
     * @author Mickael Olivier, Benoit Travers
     */
    public abstract class Peuple : IPeuple
    {
        /**
         * Fonction qui crée une unité pour ce peuple
         * @return Une unité pour le peuple courant
         */
        public abstract Unite fabriquerUnite();
    }

    /**
     * Classe concrète définissant le peuple "Vikings"
     * @author Mickael Olivier, Benoit Travers
     */
    public class Vikings : Peuple, IVikings
    {
        /**
         * Fonction qui crée une unité viking
         * @return Une unité  viking
         */
        public override Unite fabriquerUnite()
        {
            return new UniteVikings();
        }
    }

    /**
     * Classe concrète définissant le peuple "Gaulois"
     * @author Mickael Olivier, Benoit Travers
     */
    public class Gaulois : Peuple, IGaulois
    {
        /**
         * Fonction qui crée une unité gauloise
         * @return Une unité gauloise
         */
        public override Unite fabriquerUnite()
        {
            return new UniteGaulois();
        }
    }

    /**
     * Classe concrète définissant le peuple "Nains"
     * @author Mickael Olivier, Benoit Travers
     */
    public class Nains : Peuple, INains
    {
        /**
         * Fonction qui crée une unité naine
         * @return Une unité naine
         */
        public override Unite fabriquerUnite()
        {
            return new UniteNains();
        }
    }
}
