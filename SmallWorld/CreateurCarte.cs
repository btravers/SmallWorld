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
        public int PosJA{
            get;
            set;
        }

        public int PosJB
        {
            get;
            set;
        }

        public WrapperCarte Wrapper
        {
            get;
            set;
        }

        public abstract Carte construire(string PeupleA, string PeupleB);
    }

    public class CreateurCarteDemo : CreateurCarte, ICreateurCarteDemo
    {
        public override Carte construire(string PeupleA, string PeupleB)
        {
            Carte carte = new Carte(5);
            Wrapper = new WrapperCarte(5, PeupleA, PeupleB);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = Wrapper.getCarte();
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 5, i / 5] = fab.getCase(c);
                i++;
            }
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();
            return carte;
        }
    }

    public class CreateurCartePetite : CreateurCarte, ICreateurCartePetite
    {
        public override Carte construire(string PeupleA, string PeupleB)
        {
            Carte carte = new Carte(10);
            Wrapper = new WrapperCarte(10, PeupleA, PeupleB);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = Wrapper.getCarte();
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 10, i / 10] = fab.getCase(c);
                i++;
            }
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();
            return carte;
        }
    }

    public class CreateurCarteNormale : CreateurCarte, ICreateurCarteNormale
    {
        public override Carte construire(string PeupleA, string PeupleB)
        {
            Carte carte = new Carte(15);
            Wrapper = new WrapperCarte(15, PeupleA, PeupleB);
            int i = 0;
            FabriqueCase fab = new FabriqueCase();
            List<int> listeCases = Wrapper.getCarte();
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i % 15, i / 15] = fab.getCase(c);
                i++;
            }
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();
            return carte;
        }
    }
}
