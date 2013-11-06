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
            this.addCarte();
            this.addJoueurs(peupleA, peupleB);
            return this._partie;
        }

        private void addNbTours();

        private void addCarte();

        private void addJoueurs(String peupleA, String peupleB);
    }

    public class MonteurPartieDemo : MonteurPartie, IMonteurPartieDemo
    {
        private void addNbTours()
        {
            this._partie._toursRestant = 5;
        }

        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCarteDemo();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 4);
            this._partie._jB = new Joueur(peupleB, 4);
        }
    }

    public class MonteurPartiePetite : MonteurPartie, IMonteurPartiePetite
    {
        private void addNbTours()
        {
            this._partie._toursRestant = 20;
        }

        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCartePetite();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 6);
            this._partie._jB = new Joueur(peupleB, 9);
        }
    }

    public class MonteurPartieNormale : MonteurPartie, IMonteurPartieNormale
    {
        private void addNbTours()
        {
            this._partie._toursRestant = 30;
        }


        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCarteNormale();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 8);
            this._partie._jB = new Joueur(peupleB, 8);
        }
    }
}
