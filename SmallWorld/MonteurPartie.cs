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
        public Partie _partie
        {
            get;
            set;
        }

        public Partie monterPartie(String peupleA, String peupleB)
        {
            this._partie = new Partie();
            this.addCarte();
            this.addJoueurs(peupleA, peupleB);
            return this._partie;
        }

        private void addCarte();

        private void addJoueurs(String peupleA, String peupleB);
    }

    public class MonteurPartieDemo : MonteurPartie, IMonteurPartieDemo
    {
        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCarteDemo();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            Peuple pA = FabriquePeuple.INSTANCE.fabriquer(peupleA,4);
            Peuple pB = FabriquePeuple.INSTANCE.fabriquer(peupleB, 4);
            this._partie._jA = new Joueur(pA);
            this._partie._jB = new Joueur(pB);
        }
    }

    public class MonteurPartiePetite : MonteurPartie, IMonteurPartiePetite
    {
        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCartePetite();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            Peuple pA = FabriquePeuple.INSTANCE.fabriquer(peupleA, 6);
            Peuple pB = FabriquePeuple.INSTANCE.fabriquer(peupleB, 6);
            this._partie._jA = new Joueur(pA);
            this._partie._jB = new Joueur(pB);
        }
    }

    public class MonteurPartieNormale : MonteurPartie, IMonteurPartieNormale
    {
        private void addCarte()
        {
            CreateurCarte createurCarte = new CreateurCarteNormale();
            this._partie._carte = createurCarte.construire();
        }

        private void addJoueurs(String peupleA, String peupleB)
        {
            Peuple pA = FabriquePeuple.INSTANCE.fabriquer(peupleA, 8);
            Peuple pB = FabriquePeuple.INSTANCE.fabriquer(peupleB, 8);
            this._partie._jA = new Joueur(pA);
            this._partie._jB = new Joueur(pB);
        }
    }
}
