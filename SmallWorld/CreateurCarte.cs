using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface ICreateurCarte
    {
    }

    public interface ICreateurCarteDemo : ICreateurCarte
    {
    }

    public interface ICreateurCartePetite : ICreateurCarte
    {
    }

    public interface ICreateurCarteNormale : ICreateurCarte
    {
    }

    public abstract class CreateurCarte : ICreateurCarte
    {
        public Carte construire();
    }

    public class CreateurCarteDemo : CreateurCarte, ICreateurCarteDemo
    {
    }

    public class CreateurCartePetite : CreateurCarte, ICreateurCartePetite
    {
    }

    public class CreateurCarteNormale : CreateurCarte, ICreateurCarteNormale
    {
    }
}
