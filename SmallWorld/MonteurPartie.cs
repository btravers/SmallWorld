using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
     * Interface réalisant un monteur de partie
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IMonteurPartie
    {
    }

    /**
     * Interface réalisant un créateur de partie de taille "démonstration"
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IMonteurPartieDemo : IMonteurPartie
    {
    }

    /**
     * Interface réalisant un créateur de partie de taille "petite"
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface IMonteurPartiePetite : IMonteurPartie
    {
    }

    /**
     * Interface réalisant un créateur de partie de taille "normale"
     */
    public interface IMonteurPartieNormale : IMonteurPartie
    {
    }

    /**
     * Classe abstraite (non instantiable ici) réalisant le montage d'une partie
     * @author Mickaël Olivier, Benoit Travers
     */
    public abstract class MonteurPartie : IMonteurPartie
    {
        /** La partie qui sera crée */ 
        protected Partie _partie
        {
            get;
            private set;
        }

       /**
        * Fonction qui effectue le montage de la partie 
        * @param peupleA le peuple du joueur A
        * @param peupleB le peuple du joueur B
        */
        public Partie monterPartie(String peupleA, String peupleB)
        {
            //On crée une partie
            this._partie = new Partie();

            //On définit le bon nombre de tours
            this.addNbTours();

            //On définit les joueurs et leurs peuples pour la partie
            this.addJoueurs(peupleA, peupleB);

            //On ajoute la carte et les unités aux bons endroits
            this.addCarte(peupleA, peupleB);

            return this._partie;

        }

        /**
         * Fonction à implémenter chez les fils permettant de définir le nombre de tours de jeu de la partie
         */
        protected abstract void addNbTours();

        /**
         * Fonction à implémenter chez les fils permettant de définir la carte de la partie
         */
        protected abstract void addCarte(String peupleA, String peupleB);

        /**
         * Fonction à implémenter chez les fils permettant de définir les joueurs de la partie
         */
        protected abstract void addJoueurs(String peupleA, String peupleB);
    }

    /**
     * Classe concrète réalisant le montage d'une partie de taille "démonstration"
     * @author Mickaël Olivier, Benoit Travers
     */
    public class MonteurPartieDemo : MonteurPartie, IMonteurPartieDemo
    {
        /**
         * Fonction qui définit le nombre de tours, ici à 5
         */
        protected override void addNbTours()
        {
            this._partie._toursRestant = 5;
        }

        /**
         * Fonction qui définit la carte et positonne les unités des deux joueurs aux bons emplacements de départ
         */
        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCarteDemo();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 5, createurCarte.PosJA % 5);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 5, createurCarte.PosJB % 5);
        }

        /**
         * Fonction qui crée les joueurs, ici avec 4 unités
         */
        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 4);
            this._partie._jB = new Joueur(peupleB, 4);
        }
    }

    /**
    * Classe concrète réalisant le montage d'une partie de taille "petite"
    * @author Mickaël Olivier, Benoit Travers
    */
    public class MonteurPartiePetite : MonteurPartie, IMonteurPartiePetite
    {
        /**
         * Fonction qui définit le nombre de tours, ici à 20
         */
        protected override void addNbTours()
        {
            this._partie._toursRestant = 20;
        }

        /**
         * Fonction qui définit la carte et positonne les unités des deux joueurs aux bons emplacements de départ
         */
        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCartePetite();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 10, createurCarte.PosJA % 10);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 10, createurCarte.PosJB % 10);
        }

        /**
         * Fonction qui crée les joueurs, ici avec 6 unités
         */
        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 6);
            this._partie._jB = new Joueur(peupleB, 6);
        }
    }

    public class MonteurPartieNormale : MonteurPartie, IMonteurPartieNormale
    {
        /**
         * Fonction qui définit le nombre de tours, ici à 30
         */
        protected override void addNbTours()
        {
            this._partie._toursRestant = 30;
        }

        /**
         * Fonction qui définit la carte et positonne les unités des deux joueurs aux bons emplacements de départ
         */
        protected override void addCarte(String peupleA, String peupleB)
        {
            CreateurCarte createurCarte = new CreateurCarteNormale();
            this._partie._carte = createurCarte.construire(peupleA, peupleB);
            this._partie.positionnerUnites(this._partie._jA, createurCarte.PosJA / 15, createurCarte.PosJA % 15);
            this._partie.positionnerUnites(this._partie._jB, createurCarte.PosJB / 15, createurCarte.PosJB % 15);
        }

        /**
         * Fonction qui crée les joueurs, ici avec 8 unités
         */
        protected override void addJoueurs(String peupleA, String peupleB)
        {
            this._partie._jA = new Joueur(peupleA, 8);
            this._partie._jB = new Joueur(peupleB, 8);
        }
    }
}
