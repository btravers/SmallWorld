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
    }

    public class Montagne : Case, IMontagne
    {
    }

    public class Plaine : Case, IPlaine
    {
    }

    public class Desert : Case, IDesert
    {
    }

    public class Eau : Case, IEau
    {
    }

    public class Foret : Case, IForet
    {
    }
}
