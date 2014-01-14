using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SmallWorld;

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

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
        public void TestPartie()
        {
            Partie p = new Partie();
        }
    }
}
