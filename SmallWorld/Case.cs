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
    }

    public class Montagne : Case, IMontagne
    {
        public Montagne()
        {
            _sourceImage = "../Textures/montagne.png";
        }
    }

    public class Plaine : Case, IPlaine
    {
        public Plaine()
        {
            _sourceImage = "../Textures/plaine.png";
        }
    }

    public class Desert : Case, IDesert
    {
        public Desert()
        {
            _sourceImage = "../Textures/desert.png";
        }
    }

    public class Eau : Case, IEau
    {
        public Eau()
        {
            _sourceImage = "../Textures/eau.png";
        }
    }

    public class Foret : Case, IForet
    {
        public Foret()
        {
            _sourceImage = "../Textures/foret.png";
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
