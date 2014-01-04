using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WrapperSmallWorld;



namespace SmallWorld
{
    /** 
     * Interface servant à réaliser le créateur de la carte
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICreateurCarte
    {
    }

    /** 
     * Interface servant à réaliser le créateur d'une carte de taille "démonstrationé
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICreateurCarteDemo : ICreateurCarte
    {
    }

    /** 
     * Interface servant à réaliser le créateur d'une carte de taille "petite"
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICreateurCartePetite : ICreateurCarte
    {
    }

    /** 
     * Interface servant à réaliser le créateur d'une carte de taille "normale"
     * @author Mickaël Olivier, Benoit Travers
     */
    public interface ICreateurCarteNormale : ICreateurCarte
    {
    }

    /** 
     * Classe abstraite (non instantiable) représentant le créateur de la carte
     * @author Mickaël Olivier, Benoit Travers
     */
    public abstract class CreateurCarte : ICreateurCarte
    {
        /** La future position du joueur A au début du jeu */
        public int PosJA
        {
            get;
            set;
        }

        /** La future position du joueur B au début du jeu */
        public int PosJB
        {
            get;
            set;
        }

        /** L'élément d'accès au wrapper de la carte du C++ en CLI */
        public WrapperCarte Wrapper
        {
            get;
            set;
        }

         /** 
         * Fonction à implémenter dans les éléments fils permettant de construire la carte voulue
         * @param PeupleA le peuple du joueur A
         * @param PeupleB le peuple du joueur B
         * @return La carte fabriquée à l'aide de l'algorithme wrappé dans l'élément Wrapper
         */
        public abstract Carte construire(string PeupleA, string PeupleB);
    }

    /** 
    * Classe concrète représentant le créateur d'une carte de taille "démonstration"
    * @author Mickaël Olivier, Benoit Travers
    */
    public class CreateurCarteDemo : CreateurCarte, ICreateurCarteDemo
    {
        /** 
        * Fonction permettant de construire une carte de taille "démonstration"
        * @param PeupleA le peuple du joueur A
        * @param PeupleB le peuple du joueur B
        * @return La carte fabriquée à l'aide de l'algorithme wrappé dans l'élément Wrapper
        */
        public override Carte construire(string PeupleA, string PeupleB)
        {
            //On crée une Carte de taille 5x5 à laquelle on connecte la bonne FabriqueCase et le bon wrapper
            Carte carte = new Carte(5);
            Wrapper = new WrapperCarte(5, PeupleA, PeupleB);
            FabriqueCase fab = new FabriqueCase();

            //On récupère sous forme de liste d'int les cases par le wrapper
            List<int> listeCases = Wrapper.getCarte();

            //Pour chaque case ainsi obtenue, on construit notre carte en passant par la fabrique de cases
            int i = 0;
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i / 5, i % 5] = fab.getCase(c);
                i++;
            }

            //On récupère les positions du joueur A et du joueur B grâce au wrapper
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();

            return carte;
        }
    }

    /** 
    * Classe concrète représentant le créateur d'une carte de taille "petite"
    * @author Mickaël Olivier, Benoit Travers
    */
    public class CreateurCartePetite : CreateurCarte, ICreateurCartePetite
    {
        /** 
        * Fonction permettant de construire une carte de taille "démonstration"
        * @param PeupleA le peuple du joueur A
        * @param PeupleB le peuple du joueur B
        * @return La carte fabriquée à l'aide de l'algorithme wrappé dans l'élément Wrapper
        */
        public override Carte construire(string PeupleA, string PeupleB)
        {
            //On crée une Carte de taille 10x10 à laquelle on connecte la bonne FabriqueCase et le bon wrapper
            Carte carte = new Carte(10);
            Wrapper = new WrapperCarte(10, PeupleA, PeupleB);
            FabriqueCase fab = new FabriqueCase();

            //On récupère sous forme de liste d'int les cases par le wrapper
            List<int> listeCases = Wrapper.getCarte();

            //Pour chaque case ainsi obtenue, on construit notre carte en passant par la fabrique de cases
            int i = 0;
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i / 10, i % 10] = fab.getCase(c);
                i++;
            }

            //On récupère les positions du joueur A et du joueur B grâce au wrapper
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();

            return carte;
        }
    }

    /** 
    * Classe concrète représentant le créateur d'une carte de taille "normale"
    * @author Mickaël Olivier, Benoit Travers
    */
    public class CreateurCarteNormale : CreateurCarte, ICreateurCarteNormale
    {
        /** 
        * Fonction permettant de construire une carte de taille "normale"
        * @param PeupleA le peuple du joueur A
        * @param PeupleB le peuple du joueur B
        * @return La carte fabriquée à l'aide de l'algorithme wrappé dans l'élément Wrapper
        */
        public override Carte construire(string PeupleA, string PeupleB)
        {
            //On crée une Carte de taille 15x15 à laquelle on connecte la bonne FabriqueCase et le bon wrapper
            Carte carte = new Carte(15);
            Wrapper = new WrapperCarte(15, PeupleA, PeupleB);
            FabriqueCase fab = new FabriqueCase();

            //On récupère sous forme de liste d'int les cases par le wrapper
            List<int> listeCases = Wrapper.getCarte();

            //Pour chaque case ainsi obtenue, on construit notre carte en passant par la fabrique de cases
            int i = 0;
            foreach (TypeCase c in listeCases)
            {
                carte._cases[i / 15, i % 15] = fab.getCase(c);
                i++;
            }

            //On récupère les positions du joueur A et du joueur B grâce au wrapper
            PosJA = Wrapper.getPosJA();
            PosJB = Wrapper.getPosJB();

            return carte;
        }
    }
}
