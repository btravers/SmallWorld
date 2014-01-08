using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml.Serialization;

namespace SmallWorld
{
    /**
     * Interface qui définit une fabrique de cases
     */
    public interface IFabriqueCase
    {
    }

    /**
     * Classe qui définit une fabrique de cases
     */
    [Serializable()]
    public class FabriqueCase : IFabriqueCase
    {
        /** Instance de montagne */
        [XmlAttribute()]
        public Montagne _m
        {
            get;
            set;
        }

        /** Instance de plaine */
        [XmlAttribute()]
        public Plaine _p
        {
            get;
            set;
        }

        /** Instance de desert */
        [XmlAttribute()]
        public Desert _d
        {
            get;
            set;
        }

        /** Instance d'eau */
        [XmlAttribute()]
        public Eau _e
        {
            get;
            set;
        }

        /** Instance de foret */
        [XmlAttribute()]
        public Foret _f
        {
            get;
            set;
        }

        /**
         * Contructeur de la classe
         */
        public FabriqueCase()
        {
            this._m = new Montagne();
            this._p = new Plaine();
            this._d = new Desert();
            this._e = new Eau();
            this._f = new Foret();
        }

        /**
         * Fonction qui permet d'obtenir la case voulue
         * @param c le type de la case voulue
         * @return Case une référence sur la case recherchée
         */
        public Case getCase(TypeCase c)
        {
            switch (c)
            {
                case TypeCase.montagne:
                    return this._m;
                case TypeCase.plaine:
                    return this._p;
                case TypeCase.desert:
                    return this._d;
                case TypeCase.eau:
                    return this._e;
                case TypeCase.foret:
                    return this._f;
            }
            return null;
        }
    }
}
