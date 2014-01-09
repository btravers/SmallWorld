using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
     * Interface réalisant un créateur de partie
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICreateurPartie
    {
    }

    /**
     * Classe concrète réalisant la création d'une partie
     * @author Mickaël Olivier, Benoit Travers
     */
    public class CreateurPartie : ICreateurPartie
    {
        /** MonteurPartie correspondant au montage de la partie */
        public MonteurPartie Monteur
        {
            get;
            set;
        }

        /** Type de la carte sous forme de String */
        public String TypeCarte
        {
            get;
            set;
        }

        /** Peuple du joueur A */
        public string PeupleA
        {
            get;
            set;
        }

        /** Peuple du joueur B */
        public string PeupleB
        {
            get;
            set;
        }

        /** 
         * Constructeur de la classe
         */
        public CreateurPartie()
        {
        }

        /**
         * Fonction permettant d'ajouter le peuple du joueur A
         * @param pA le peuple choisi par le joueur A
         */
        public void addPeupleA(string pA)
        {
            this.PeupleA = pA;
        }

        /**
         * Fonction permettant d'ajouter le peuple du joueur B
         * @param pB le peuple choisi par le joueur B
         */
        public void addPeupleB(string pB)
        {
            this.PeupleB = pB;
        }

        /**
         * Fonction permettant de construire la partie
         * @return la partie qui va se jouer à son stade initial
         */
        public Partie construire()
        {
            //En fonction du type de la carte, on adopte le monteur adopté et on contruit la partie avec la bonne carte
            switch (TypeCarte)
            {
                case "demo":
                    this.Monteur = new MonteurPartieDemo();
                    return this.Monteur.monterPartie(PeupleA,PeupleB);
                case "petite":
                    this.Monteur = new MonteurPartiePetite();
                    return this.Monteur.monterPartie(PeupleA,PeupleB);
                case "normale":
                    this.Monteur = new MonteurPartieNormale();
                    return this.Monteur.monterPartie(PeupleA,PeupleB);
                default:
                    throw new Exception("Type de carte introuvable.");
            }
        }
    }
}
