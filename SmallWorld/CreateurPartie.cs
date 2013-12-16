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
        public MonteurPartie Monteur
        {
            get;
            set;
        }

        public String TypeCarte
        {
            get;
            set;
        }

        public String PeupleA
        {
            get;
            set;
        }

        public String PeupleB
        {
            get;
            set;
        }

        public CreateurPartie()
        {
        }

        public void addPeupleA(String pA)
        {
            this.PeupleA = pA;
        }

        public void addPeupleB(String pB)
        {
            this.PeupleA = pB;
        }

        public Partie construire()
        {
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
