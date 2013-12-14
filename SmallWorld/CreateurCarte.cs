using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperSmallWorld;

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
            Carte carte = new Carte(5);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = WrapperCarte.genererCarte(5);
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 5, i / 5] = fab.getCase(c);
                i++;
            }
            return carte;
        }
    }

    public class CreateurCartePetite : CreateurCarte, ICreateurCartePetite
    {
        public override Carte construire()
        {
            Carte carte = new Carte(10);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = WrapperCarte.genererCarte(10);
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 10, i / 10] = fab.getCase(c);
                i++;
            }
            return carte;
        }
    }

    public class CreateurCarteNormale : CreateurCarte, ICreateurCarteNormale
    {
        public override Carte construire()
        {
            Carte carte = new Carte(15);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = WrapperCarte.genererCarte(15);
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 15, i / 15] = fab.getCase(c);
                i++;
            }
            return carte;
        }
    }
}
