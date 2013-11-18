using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface ICreateurPartie
    {
    }

    public class CreateurPartie : ICreateurPartie
    {
        public MonteurPartie _monteur
        {
            get;
            set;
        }

        public String _typeCarte
        {
            get;
            set;
        }

        private String _peupleA
        {
            public get;
            private set;
        }

        private String _peupleB
        {
            public get;
            private set;
        }

        public CreateurPartie()
        {
        }

        public void addPeupleA(String pA)
        {
            this._peupleA = pA;
        }

        public void addPeupleB(String pB)
        {
            this._peupleA = pB;
        }

        public void construire()
        {
            switch (_typeCarte)
            {
                case "demo":
                    this._monteur = new MonteurPartieDemo();
                    this._monteur.monterPartie(_peupleA,_peupleB);
                    break;
                case "petite":
                    this._monteur = new MonteurPartiePetite();
                    this._monteur.monterPartie(_peupleA,_peupleB);
                    break;
                case "normale":
                    this._monteur = new MonteurPartieNormale();
                    this._monteur.monterPartie(_peupleA,_peupleB);
                    break;
            }
        }
    }
}
