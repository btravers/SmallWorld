using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
     * Interface définissant un joueur
     */
    public interface IJoueur
    {
    }

    /**
     * Classe définissant un joueur
     */
    public class Joueur : IJoueur
    {
        /** La liste d'unités de ce joueur */
        public List<Unite> _unites
        {
            get;
            set;
        }

        /** ??? */
        public List<int> Poisitions
        {
            get;
            set;
        }

        /** Le nombre de points actuel de ce joueur */
        public int _points
        {
            get;
            set;
        }

        /** Le nom du joueur */
        public String _name
        {
            get;
            set;
        }

        /** Le peuple du joueur sous forme de String */
        public String _peuple
        {
            get;
            set;
        }

        /**
         * Constructeur
         * @param typePeuple le peuple chosi sous forme de String
         * @param nbUnites le nombre d'unités à fabriquer pour ce joueur
         */
        public Joueur(String typePeuple, int nbUnites)
        {
            //On construit les éléments de base
            Peuple peuple = null;
            this._unites = new List<Unite>();

            //On choisit le bon peuple et on le construit
            switch (typePeuple)
            {
                case "vikings":
                    peuple = new Vikings();
                    _peuple = "Vikings";
                    break;
                case "gaulois":
                    peuple = new Gaulois();
                    _peuple = "Gaulois";
                    break;
                case "nains":
                    peuple = new Nains();
                    _peuple = "Nains";
                    break;
            }

            //On ajoute des unités fabriqués dans la liste prévue à cet effet
            for (int i = 0; i < nbUnites; i++)
            {
                this._unites.Add(peuple.fabriquerUnite());
            }
        }

        /**
         * Fonction permettant d'ajouter des points au joueur courant
         * @param pts le nombre de points à ajouter au score de ce joueur
         */
        public void ajouterPoints(int pts)
        {
            this._points += pts;
        }

        /**
         * Fonction permettant d'obtenir l'unité dont la santé est la meilleure sur la case de coordonées x, y
         * @param x l'abscisse de la coordonée passée en paramètre
         * @param y l'ordonnée de la coordonée passée en paramètre
         * @return L'unité recherchée
         */
        public Unite obtenirMeilleurUnite(int x, int y)
        {
            //Liste d'unités temporaire
            List<Unite> unites = new List<Unite>();

            //Pour chaque unité dans la liste d'unités du joueur courant
            foreach (Unite unite in this._unites)
            {
                //Si l'unité est sur la case x,y et est valide
                if (unite.estSurCase(x, y) && unite.enVie)
                {
                    //On l'ajoute à la liste temporaire
                    unites.Add(unite);
                }
            }

            //S'il y a des unités dans la liste temporaire
            if (unites.Any())
            {
                //Alors on recherche la meilleure unité, par défaut la première de la liste
                Unite meilleur = unites.First();

                //On parcourt la liste pour comparer les unités 
                foreach (Unite unite in unites)
                {
                    //Si on trouve une meilleure unité, on met à jour l'Unité que l'on rendra
                    if (unite._defense*unite._pdv/unite.vitaMax > meilleur._defense*meilleur._pdv/meilleur.vitaMax)
                    {
                        meilleur = unite;
                    }
                }

                return meilleur;
            }
            //Sinon ne rien retourner
            else
            {
                return null;
            }
        }

        /*public void perdreUnite(Unite u)
        {
            this._unites.Remove(u);
        }*/
    }
}
