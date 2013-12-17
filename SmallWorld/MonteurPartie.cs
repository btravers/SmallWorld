using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IMonteurPartie
    {
    }

    public interface IMonteurPartieDemo : IMonteurPartie
    {
    }

    public interface IMonteurPartiePetite : IMonteurPartie
    {
    }

    public interface IMonteurPartieNormale : IMonteurPartie
    {
    }

    public abstract class MonteurPartie : IMonteurPartie
    {
        protected Partie _partie
        {
            get;
            private set;
        }

        public Partie monterPartie(String peupleA, String peupleB)
        {
            this._partie = new Partie();
            this.addNbTours();
            this.addJoueurs(peupleA, peupleB);
            this.addCarte(peupleA, peupleB);
            return this._partie;

        }

        protected abstract void addNbTours();

        protected abstract void addCarte(String peupleA, String peupleB);

        protected abstract void addJoueurs(String peupleA, String peupleB);
    }

    public class MonteurPartieDemo : MonteurPartie, IMonteurPartieDemo
    {
        protected override void addNbTours()
        {
            this._partie._toursRestant = 5;
        }

        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCarteDemo();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 5, createurCarte.PosJA % 5);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 5, createurCarte.PosJB % 5);
        }

        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 4);
            this._partie._jB = new Joueur(peupleB, 4);
        }
    }

    public class MonteurPartiePetite : MonteurPartie, IMonteurPartiePetite
    {
        protected override void addNbTours()
        {
            this._partie._toursRestant = 20;
        }

        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCartePetite();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 10, createurCarte.PosJA % 10);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 10, createurCarte.PosJB % 10);
        }

        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 6);
            this._partie._jB = new Joueur(peupleB, 9);
        }
    }

    public class MonteurPartieNormale : MonteurPartie, IMonteurPartieNormale
    {
        protected override void addNbTours()
        {
            this._partie._toursRestant = 30;
        }


        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCarteNormale();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 15, createurCarte.PosJA % 15);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 15, createurCarte.PosJB % 15);
        }

        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 8);
            this._partie._jB = new Joueur(peupleB, 8);
        }
    }
}
