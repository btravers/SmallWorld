using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public interface IFabriquePeuple
    {
    }

    public class FabriquePeuple : IFabriquePeuple
    {
        public static readonly FabriquePeuple INSTANCE = new FabriquePeuple();

        public Peuple fabriquer(String typePeuple, int nbUnite)
        {
            Peuple peuple;

            switch (typePeuple)
            {
                case "Vikings":
                    peuple = new Vikings(nbUnite);
                    break;
                case "Gaulois":
                    peuple = new Gaulois(nbUnite);
                    break;
                case "Nains":
                    peuple = new Nains(nbUnite);
                    break;
            }

            return peuple;
        }
    }
}
