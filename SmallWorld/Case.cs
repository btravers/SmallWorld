using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface ICase
    {
    }

    public interface IMontagne : ICase
    {
        //Ajouter la montagne en guise d'image
    }

    public interface IPlaine : ICase
    {
    }

    public interface IDesert : ICase
    {
    }

    public interface IEau : ICase
    {
    }

    public interface IForet : ICase
    {
    }

    public abstract class Case : ICase
    {
        public String _sourceImage
        {
            get;
            set;
        }

        public abstract int typeCase();
    }

    public class Montagne : Case, IMontagne
    {
        public Montagne()
        {
            _sourceImage = "../Textures/montagne.png";
        }

        public override int typeCase()
        {
            return 0;
        }
    }

    public class Plaine : Case, IPlaine
    {
        public Plaine()
        {
            _sourceImage = "../Textures/plaine.png";
        }

        public override int typeCase()
        {
            return 1;
        }
    }

    public class Desert : Case, IDesert
    {
        public Desert()
        {
            _sourceImage = "../Textures/desert.png";
        }

        public override int typeCase()
        {
            return 2;
        }
    }

    public class Eau : Case, IEau
    {
        public Eau()
        {
            _sourceImage = "../Textures/eau.png";
        }

        public override int typeCase()
        {
            return 3;
        }
    }

    public class Foret : Case, IForet
    {
        public Foret()
        {
            _sourceImage = "../Textures/foret.png";
        }

        public override int typeCase()
        {
            return 4;
        }
    }

    public enum TypeCase
    {
        montagne,
        plaine,
        desert,
        eau,
        foret
    };
}
