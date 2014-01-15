using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SmallWorld;
using WrapperSmallWorld;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestClass]
    public class TestSmallWorld
    {
        public TestSmallWorld()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        [TestMethod]
        public void TestCarteGeneration()
        {
            Carte carte = new Carte(5);
            Assert.AreEqual(carte._width, 5);
        }

        [TestMethod]
        public void TestCarteBordEau()
        {
            Carte carte = new Carte(5);
            FabriqueCase fabrique = new FabriqueCase();
            for (int i = 0; i < 25; i++ )
            {
                carte._cases[i/5,i%5] = fabrique._e;
            }
            Assert.IsTrue(carte.bordEau(1,1));

            for (int i = 0; i < 25; i++)
            {
                carte._cases[i / 5, i % 5] = fabrique._p;
            }
            Assert.IsFalse(carte.bordEau(1, 1));
        }

        [TestMethod]
        public void TestCaseGeneration()
        {
            Case c1 = new Montagne();
            Case c2 = new Plaine();
            Case c3 = new Desert();
            Case c4 = new Eau();
            Case c5 = new Foret();

            Assert.AreEqual(c1.type(), TypeCase.montagne);
            Assert.AreEqual(c2.type(), TypeCase.plaine);
            Assert.AreEqual(c3.type(), TypeCase.desert);
            Assert.AreEqual(c4.type(), TypeCase.eau);
            Assert.AreEqual(c5.type(), TypeCase.foret);
        }

        [TestMethod]
        public void TestCreateurCarteDemo()
        {
            CreateurCarte createur = new CreateurCarteDemo();
            Carte res = createur.construire("vikings", "nains");

            Assert.IsNotNull(createur.Wrapper);
            Assert.AreEqual(res._width, 5);
            Assert.IsNotNull(res._cases);
            Assert.IsNotNull(createur.PosJA);
            Assert.IsTrue(createur.PosJA >= 0 && createur.PosJA < 25);
            Assert.IsNotNull(createur.PosJB);
            Assert.IsTrue(createur.PosJB >= 0 && createur.PosJB < 25);
        }

        [TestMethod]
        public void TestCreateurCartePetite()
        {
            CreateurCarte createur = new CreateurCartePetite();
            Carte res = createur.construire("vikings", "nains");

            Assert.IsNotNull(createur.Wrapper);
            Assert.AreEqual(res._width, 10);
            Assert.IsNotNull(res._cases);
            Assert.IsNotNull(createur.PosJA);
            Assert.IsTrue(createur.PosJA >= 0 && createur.PosJA < 100);
            Assert.IsNotNull(createur.PosJB);
            Assert.IsTrue(createur.PosJB >= 0 && createur.PosJB < 100);
        }

        [TestMethod]
        public void TestCreateurCarteNormale()
        {
            CreateurCarte createur = new CreateurCarteNormale();
            Carte res = createur.construire("vikings", "nains");

            Assert.IsNotNull(createur.Wrapper);
            Assert.AreEqual(res._width, 15);
            Assert.IsNotNull(res._cases);
            Assert.IsNotNull(createur.PosJA);
            Assert.IsTrue(createur.PosJA >= 0 && createur.PosJA < 225);
            Assert.IsNotNull(createur.PosJB);
            Assert.IsTrue(createur.PosJB >= 0 && createur.PosJB < 225);
        }

        [TestMethod]
        public void TestCreateurPartie()
        {
            CreateurPartie createur = new CreateurPartie();
            createur.addPeupleA("vikings");
            createur.addPeupleB("nains");
            createur.TypeCarte = "demo";

            Assert.AreEqual(createur.PeupleA, "vikings");
            Assert.AreEqual(createur.PeupleB, "nains");

            createur.construire();
            Assert.IsNotNull(createur.Monteur);
            Assert.IsTrue(createur.Monteur is MonteurPartieDemo);

            createur.TypeCarte = "normale";
            createur.construire();
            Assert.IsNotNull(createur.Monteur);
            Assert.IsTrue(createur.Monteur is MonteurPartieNormale);

            createur.TypeCarte = "petite";
            createur.construire();
            Assert.IsNotNull(createur.Monteur);
            Assert.IsTrue(createur.Monteur is MonteurPartiePetite);
        }

        [TestMethod]
        public void TestFabriqueCase()
        {
            FabriqueCase fab = new FabriqueCase();

            Assert.IsNotNull(fab._m);
            Assert.IsNotNull(fab._p);
            Assert.IsNotNull(fab._d);
            Assert.IsNotNull(fab._e);
            Assert.IsNotNull(fab._f);

            Assert.IsTrue(fab.getCase(TypeCase.montagne) is Montagne);
            Assert.IsTrue(fab.getCase(TypeCase.plaine) is Plaine);
            Assert.IsTrue(fab.getCase(TypeCase.desert) is Desert);
            Assert.IsTrue(fab.getCase(TypeCase.eau) is Eau);
            Assert.IsTrue(fab.getCase(TypeCase.foret) is Foret);
        }

        [TestMethod]
        public void TestMonteurPartieDemo()
        {
            MonteurPartie monteur = new MonteurPartieDemo();
            Partie res = monteur.monterPartie("nains", "vikings");

            Assert.AreEqual(res._carte._width, 5);
            Assert.AreEqual(res._toursRestant, 5);
            Assert.IsNotNull(res._jA);
            Assert.IsNotNull(res._jB);
            Assert.AreEqual(res._jA._peuple, "Nains");
            Assert.AreEqual(res._jB._peuple, "Vikings");
            Assert.IsNotNull(res._joueur);
            Assert.IsNotNull(res._premierJoueur);
        }

        [TestMethod]
        public void TestMonteurPartiePetite()
        {
            MonteurPartie monteur = new MonteurPartiePetite();
            Partie res = monteur.monterPartie("nains", "vikings");

            Assert.AreEqual(res._carte._width, 10);
            Assert.AreEqual(res._toursRestant, 20);
            Assert.IsNotNull(res._jA);
            Assert.IsNotNull(res._jB);
            Assert.AreEqual(res._jA._peuple, "Nains");
            Assert.AreEqual(res._jB._peuple, "Vikings");
            Assert.IsNotNull(res._joueur);
            Assert.IsNotNull(res._premierJoueur);
        }

        [TestMethod]
        public void TestMonteurPartieNormale()
        {
            MonteurPartie monteur = new MonteurPartieNormale();
            Partie res = monteur.monterPartie("nains", "vikings");

            Assert.AreEqual(res._carte._width, 15);
            Assert.AreEqual(res._toursRestant, 30);
            Assert.IsNotNull(res._jA);
            Assert.IsNotNull(res._jB);
            Assert.AreEqual(res._jA._peuple, "Nains");
            Assert.AreEqual(res._jB._peuple, "Vikings");
            Assert.IsNotNull(res._joueur);
            Assert.IsNotNull(res._premierJoueur);
        }

        [TestMethod]
        public void TestJoueur()
        {
            Joueur j = new Joueur("nains",4);
            Assert.AreEqual(j._peuple, "Nains");
            Assert.AreEqual(j._points, 0);
            Assert.AreEqual(j._unites.Count, 4);

            j.ajouterPoints(5);
            Assert.AreEqual(j._points, 5);
            j.ajouterPoints(5);
            Assert.AreEqual(j._points, 10);
            int i = 0;
            foreach (Unite u in j._unites)
            {
                u._x = i;
                u._y = i;
                i++;
            }
            Assert.AreEqual(j._unites[1], j.obtenirMeilleurUnite(1, 1));
        }


        [TestMethod]
        public void TestNains()
        {
            Peuple p = new Nains();
            Unite res = p.fabriquerUnite();
            Assert.IsTrue(res is UniteNains);
        }

        [TestMethod]
        public void TestVikings()
        {
            Peuple p = new Vikings();
            Unite res = p.fabriquerUnite();
            Assert.IsTrue(res is UniteVikings);
        }

        [TestMethod]
        public void TestGaulois()
        {
            Peuple p = new Gaulois();
            Unite res = p.fabriquerUnite();
            Assert.IsTrue(res is UniteGaulois);
        }

        [TestMethod]
        public void TestUniteNains()
        {
            Unite p = new UniteNains();
            Assert.AreEqual(p._attaque,2);
            Assert.AreEqual(p._defense, 1);
            Assert.AreEqual(p._pdv, 5);
            Assert.AreEqual(p._pm, 1);
            Assert.IsTrue(p.enVie);
            Assert.AreEqual(p.typeUnite(), "nains");

            p.positionner(0, 1, new Plaine());
            Assert.AreEqual(p._x, 0);
            Assert.AreEqual(p._y, 1);
            Assert.IsFalse(p.estSurCase(1,1));
            Assert.IsTrue(p.estSurCase(0, 1));

            Assert.IsTrue(p.peutDeplacer(1,1,new Plaine()));
            Assert.IsFalse(p.peutDeplacer(1,1,new Eau()));

            Assert.AreEqual(p.getPoints(), 0);
        }

        [TestMethod]
        public void TestUniteVikings()
        {
            Unite p = new UniteVikings();
            Assert.AreEqual(p._attaque, 2);
            Assert.AreEqual(p._defense, 1);
            Assert.AreEqual(p._pdv, 5);
            Assert.AreEqual(p._pm, 1);
            Assert.IsTrue(p.enVie);
            Assert.AreEqual(p.typeUnite(), "vikings");

            p.positionner(0, 1, new Plaine());
            Assert.AreEqual(p._x, 0);
            Assert.AreEqual(p._y, 1);
            Assert.IsFalse(p.estSurCase(1, 1));
            Assert.IsTrue(p.estSurCase(0, 1));

            Assert.IsTrue(p.peutDeplacer(1, 1, new Plaine()));
            Assert.IsTrue(p.peutDeplacer(1, 1, new Eau()));

            Assert.AreEqual(p.getPoints(), 1);
        }

        [TestMethod]
        public void TestUniteGaulois()
        {
            Unite p = new UniteGaulois();
            Assert.AreEqual(p._attaque, 2);
            Assert.AreEqual(p._defense, 1);
            Assert.AreEqual(p._pdv, 5);
            Assert.AreEqual(p._pm, 1);
            Assert.IsTrue(p.enVie);
            Assert.AreEqual(p.typeUnite(), "gaulois");

            p.positionner(0, 1, new Plaine());
            Assert.AreEqual(p._x, 0);
            Assert.AreEqual(p._y, 1);
            Assert.IsFalse(p.estSurCase(1, 1));
            Assert.IsTrue(p.estSurCase(0, 1));

            Assert.IsTrue(p.peutDeplacer(1, 1, new Plaine()));
            Assert.IsFalse(p.peutDeplacer(1, 1, new Eau()));

            Assert.AreEqual(p.getPoints(), 2);
        }

        [TestMethod]
        public void TestPartieNainsGaulois()
        {
            MonteurPartie m = new MonteurPartieDemo();
            Partie p = m.monterPartie("nains","gaulois");
            p._jA._name = "j1";
            p._jB._name = "j2";

            Assert.AreEqual(p._joueur == 0, p.joueJoueurA());

            Joueur j = p._jB;
            Joueur ad = p._jA;
            if (p.joueJoueurA())
            {
                j = p._jA;
                ad = p._jB;
            }
            p.selectCaseInitiale(j._unites[0]._x,j._unites[0]._y);
            Assert.IsNotNull(p._uniteSelectionnee);

            ad.Positions = new List<int>();
            foreach (Unite u in ad._unites)
            {
                ad.Positions.Add(u._x*5+u._y);
            }

            List<int> suggestions = p.suggestion();
            foreach (int i in suggestions)
            {
                Assert.IsTrue(p._uniteSelectionnee.estAPortee(i/5,i%5,p._carte));
                Assert.IsTrue(ad.obtenirMeilleurUnite(i/5,i%5) == null);
                Assert.IsTrue(p._uniteSelectionnee.peutDeplacer(i/5,i%5,p._carte._cases[i/5,i%5]));
            }

            int x = suggestions[0] / 5;
            int y = suggestions[0] % 5;
            Unite select = p._uniteSelectionnee;
            p.selectCaseDestination(x, y);
            Assert.AreEqual(select._x, x);
            Assert.AreEqual(select._y, y);
            Assert.AreEqual(select._terrain, p._carte._cases[x, y].type());

            p.joueurSuivant();
            p.selectCaseInitiale(ad._unites[0]._x, ad._unites[0]._y);
            Assert.IsNotNull(p._uniteSelectionnee);

            j.Positions = new List<int>();
            foreach (Unite u in ad._unites)
            {
                j.Positions.Add(u._x * 5 + u._y);
            }

            suggestions = p.suggestion();
            foreach (int i in suggestions)
            {
                Assert.IsTrue(p._uniteSelectionnee.estAPortee(i / 5, i % 5, p._carte));
            }

            x = suggestions[0] / 5;
            y = suggestions[0] % 5;
            select = p._uniteSelectionnee;
            p.selectCaseDestination(x, y);
            Assert.AreEqual(select._x, x);
            Assert.AreEqual(select._y, y);
            Assert.AreEqual(select._terrain, p._carte._cases[x, y].type());

        }

        [TestMethod]
        public void TestPartieNainsVikings()
        {
            MonteurPartie m = new MonteurPartieDemo();
            Partie p = m.monterPartie("nains", "vikings");
            p._jA._name = "j1";
            p._jB._name = "j2";

            Assert.AreEqual(p._joueur == 0, p.joueJoueurA());

            Joueur j = p._jB;
            Joueur ad = p._jA;
            if (p.joueJoueurA())
            {
                j = p._jA;
                ad = p._jB;
            }
            p.selectCaseInitiale(j._unites[0]._x, j._unites[0]._y);
            Assert.IsNotNull(p._uniteSelectionnee);

            ad.Positions = new List<int>();
            foreach (Unite u in ad._unites)
            {
                ad.Positions.Add(u._x * 5 + u._y);
            }

            List<int> suggestions = p.suggestion();
            foreach (int i in suggestions)
            {
                Assert.IsTrue(p._uniteSelectionnee.estAPortee(i / 5, i % 5, p._carte));
                Assert.IsTrue(ad.obtenirMeilleurUnite(i / 5, i % 5) == null);
                Assert.IsTrue(p._uniteSelectionnee.peutDeplacer(i / 5, i % 5, p._carte._cases[i / 5, i % 5]));
            }

            int x = suggestions[0] / 5;
            int y = suggestions[0] % 5;
            Unite select = p._uniteSelectionnee;
            p.selectCaseDestination(x, y);
            Assert.AreEqual(select._x, x);
            Assert.AreEqual(select._y, y);
            Assert.AreEqual(select._terrain, p._carte._cases[x, y].type());

            p.joueurSuivant();
            p.selectCaseInitiale(ad._unites[0]._x, ad._unites[0]._y);
            Assert.IsNotNull(p._uniteSelectionnee);

            j.Positions = new List<int>();
            foreach (Unite u in ad._unites)
            {
                j.Positions.Add(u._x * 5 + u._y);
            }

            suggestions = p.suggestion();
            foreach (int i in suggestions)
            {
                Assert.IsTrue(p._uniteSelectionnee.estAPortee(i / 5, i % 5, p._carte));
            }

            x = suggestions[0] / 5;
            y = suggestions[0] % 5;
            select = p._uniteSelectionnee;
            p.selectCaseDestination(x, y);
            Assert.AreEqual(select._x, x);
            Assert.AreEqual(select._y, y);
            Assert.AreEqual(select._terrain, p._carte._cases[x, y].type());

        }


        [TestMethod]
        public void TestCombat()
        {
            MonteurPartie m = new MonteurPartieDemo();
            Partie p = m.monterPartie("vikings", "nains");

            // On force le joueur A à jouer en premier
            p._joueur = 0;
            p._premierJoueur = 0;

            //On positionne une unité à coté des unités du joueur B
            int x = p._jB._unites[0]._x - 1;
            int y = p._jB._unites[0]._y;
            p._jA._unites[0].positionner(x, y, p._carte._cases[x, y]);

            p.selectCaseInitiale(x, y);
            Assert.IsNotNull(p._uniteSelectionnee);

            p._jB.Positions = new List<int>();
            foreach (Unite u in p._jB._unites)
            {
                p._jB.Positions.Add(u._x * 5 + u._y);
            }

            List<int> suggestions = p.suggestion();
            foreach (int i in suggestions)
            {
                Assert.IsTrue(p._uniteSelectionnee.estAPortee(i / 5, i % 5, p._carte));
            }

            Unite select = p._uniteSelectionnee;
            Unite adversaire = p._jB.obtenirMeilleurUnite(x+1,y);
            // Destination choisie de facon à attaquer
            p.selectCaseDestination(x+1, y);

            Assert.IsFalse(select.enVie==false && adversaire.enVie==false);
            Assert.IsFalse(select._pdv == select.vitaMax && adversaire._pdv == adversaire.vitaMax);

        }

       
    }
}
