using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /** 
     * Interface servant à réaliser les cases
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICase
    {
    }

    /** 
     * Interface servant à réaliser les cases montagne
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IMontagne : ICase
    {
    }

    /** 
     * Interface servant à réaliser les cases plaine
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IPlaine : ICase
    {
    }

    /** 
     * Ensemble des interfaces servant à réaliser les cases desert
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IDesert : ICase
    {
    }

    /** 
     * Ensemble des interfaces servant à réaliser les cases eau
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IEau : ICase
    {
    }

    /** 
     * Interface servant à réaliser les cases forêt
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IForet : ICase
    {
    }

    /** 
     * Classe abstraite (on ne peut pas l'instancier directement) représentant une case
     * @author Mickaël Olivier, Benoit Travers
     */
    public abstract class Case : ICase
    {
        /** 
         * L'image source correspondant à la case
         * On l'instancie une fois pour chaque type de case et le poids-mouche se sert du reste
         */
        public String _sourceImage
        {
            get;
            set;
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public abstract int typeCase();

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public abstract TypeCase type();
    }

    /** 
     * Classe concrète représentant une montagne
     * @author Mickaël Olivier, Benoit Travers
     */
    public class Montagne : Case, IMontagne
    {
        /**
         * Constructeur : on associe le bon URI à _sourceImage
         */
        public Montagne()
        {
            _sourceImage = "../Textures/montagne.png";
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public override int typeCase()
        {
            return 0;
        }

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public override TypeCase type()
        {
            return TypeCase.montagne;
        }
    }

    /** 
     * Classe concrète représentant une plaine
     * @author Mickaël Olivier, Benoit Travers
     */
    public class Plaine : Case, IPlaine
    {
        /**
         * Constructeur : on associe le bon URI à _sourceImage
         */
        public Plaine()
        {
            _sourceImage = "../Textures/plaine.png";
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public override int typeCase()
        {
            return 1;
        }

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public override TypeCase type()
        {
            return TypeCase.plaine;
        }
    }

    /** 
     * Classe concrète représentant un désert
     * @author Mickaël Olivier, Benoit Travers
     */
    public class Desert : Case, IDesert
    {
        /**
         * Constructeur : on associe le bon URI à _sourceImage
         */
        public Desert()
        {
            _sourceImage = "../Textures/desert.png";
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public override int typeCase()
        {
            return 2;
        }

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public override TypeCase type()
        {
            return TypeCase.desert;
        }
    }

    /** 
     * Classe concrète représentant un point d'eau
     * @author Mickaël Olivier, Benoit Travers
     */
    public class Eau : Case, IEau
    {
        /**
         * Constructeur : on associe le bon URI à _sourceImage
         */
        public Eau()
        {
            _sourceImage = "../Textures/eau.png";
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public override int typeCase()
        {
            return 3;
        }

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public override TypeCase type()
        {
            return TypeCase.eau;
        }
    }

    /** 
     * Classe concrète représentant une foret
     * @author Mickaël Olivier, Benoit Travers
     */
    public class Foret : Case, IForet
    {
        /**
         * Constructeur : on associe le bon URI à _sourceImage
         */
        public Foret()
        {
            _sourceImage = "../Textures/foret.png";
        }

        /*
         * Fonction retournant sous forme d'integer le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme d'int, eau si la case est une instance de Eau par ex.
         */
        public override int typeCase()
        {
            return 4;
        }

        /*
         * Fonction retournant sous forme de TypeCase (voir enum plus bas) le type de case auquel correspond l'élément courant
         * @return le type de la case courante sous forme de TypeCase, eau si la case est une instance de Eau par ex.
         */
        public override TypeCase type()
        {
            return TypeCase.foret;
        }
    }

    /** Enum représentant un type de case */
    public enum TypeCase
    {
        montagne,
        plaine,
        desert,
        eau,
        foret
    };
}
