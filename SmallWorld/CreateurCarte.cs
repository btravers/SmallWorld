﻿using System;
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
        public abstract Carte construire();
    }

    public class CreateurCarteDemo : CreateurCarte, ICreateurCarteDemo
    {
        public override Carte construire()
        {
            Carte carte = new Carte(5); // TODO
            return carte;
        }
    }

    public class CreateurCartePetite : CreateurCarte, ICreateurCartePetite
    {
        public override Carte construire()
        {
            Carte carte = new Carte(10); // TODO
            return carte;
        }
    }

    public class CreateurCarteNormale : CreateurCarte, ICreateurCarteNormale
    {
        public override Carte construire()
        {
            Carte carte = new Carte(15); // TODO
            return carte;
        }
    }
}
