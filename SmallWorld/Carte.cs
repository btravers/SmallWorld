using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    /**
    * La classe ICarte représente l'interface servant à réaliser une carte
    * @author Mickaël Olivier, Benoit Travers
    */
    public interface ICarte
    {
    }

    /**
    * La classe Carte sert à réaliser la carte de notre jeu
    * @author Mickaël Olivier, Benoit Travers
    */
    public class Carte : ICarte
    {
        /** La FabriqueCase qui nous sert à créer les cases en passant par un poids-mouche */
        public FabriqueCase _fabrique
        {
            get;
            set;
        }

        /** La largeur de notre carte (on considère qu'elle est carrée)*/
        public int _width
        {
            get;
            set;
        }

        /** Le tableau de cases que contient notre carte */
        public Case[,] _cases
        {
            get;
            set;
        }

        /** Le constructeur de carte
         *  @param taille la largeur de la carte
         */
        public Carte(int taille)
        {
            this._cases = new Case[taille,taille];
            this._width = taille;
        }

        /** Un prédicat qui nous permet de savoir si une coordonée donnée corespond à une case au bord de l'eau
         *  @param x l'abcisse de la coordonée passée en paramètre
         *  @param y l'ordonée de la coordonée passée en paramètre
         */
        public bool bordEau(int x, int y)
        {
            bool test = false;
            if (x + 1 < this._width)
            {
                test |= (this._cases[x + 1, y].type() == TypeCase.eau);
            }
            if (y + 1 < _width)
            {
                test |= (this._cases[x, y + 1].type() == TypeCase.eau);
            }
            if (x - 1 >= 0)
            {
                test |= (this._cases[x - 1, y].type() == TypeCase.eau);
            }
            if (y - 1 >= 0)
            {
                test |= (this._cases[x, y - 1].type() == TypeCase.eau);
            }

            return test;
        }
    }
}
