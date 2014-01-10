using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using SmallWorld;

namespace WPFSmallWorld
{
    /**
    * La classe Select représente l'écran de sélection.
    * @author Mickaël Olivier, Benoit Travers
    */
    public partial class Select : UserControl
    {
        private String peupleA; /**Le peuple du joueur A*/
        private String peupleB; /**Le peuple du joueur B*/
        private String carte;   /**La carte*/

        private MainWindow window;

        /**
         * Constructeur
         * Initialise l'UserControl en utilisant les balises définies dans le code xaml
         */
        public Select()
        {
            InitializeComponent();
        }

        /**
         * Ajoute une référence sur la fenêtre principale
         * @param win la fenêtre en question
         */
        public void addReference(MainWindow win)
        {
            window = win;
        }

        /**
         * Evenement associé à la sélection d'un peuple pour empêcher l'aitre joueur
         * de sélectionner le même
         * @param button le RadioButton mirroir pour le Joueur 1
         */
        private void enabledItems1(RadioButton button)
        {
            j1Nains.IsEnabled = true;
            j1Vikings.IsEnabled = true;
            j1Gaulois.IsEnabled = true;

            button.IsEnabled = false;
        }

        /**
         * Evenement associé à la sélection d'un peuple pour empêcher l'aitre joueur
         * de sélectionner le même
         * @param button le RadioButton mirroir pour le Joueur 2
         */
        private void enabledItems2(RadioButton button)
        {
            j2Nains.IsEnabled = true;
            j2Vikings.IsEnabled = true;
            j2Gaulois.IsEnabled = true;

            button.IsEnabled = false;
        }

        /** 
         * Evenement associé à la sélection des nains par le Joueur 1
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j1NainsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Nains);
            peupleA = "nains";
        }

        /** 
         * Evenement associé à la sélection des gaulois par le Joueur 1
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j1GauloisChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Gaulois);
            peupleA = "gaulois";
        }

        /** 
         * Evenement associé à la sélection des vikings par le Joueur 1
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j1VikingsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Vikings);
            peupleA = "vikings";
        }

        /** 
         * Evenement associé à la sélection des nains par le Joueur 2
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j2NainsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Nains);
            peupleB = "nains";
        }

        /** 
         * Evenement associé à la sélection des gaulois par le Joueur 2
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j2GauloisChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Gaulois);
            peupleB = "gaulois";
        }

        /** 
         * Evenement associé à la sélection des vikings par le Joueur 2
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void j2VikingsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Vikings);
            peupleB = "vikings";
        }

        /** 
         * Evenement associé à la sélection de la taille de carte "Démonstration"
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void cDemoChecked(object sender, RoutedEventArgs e)
        {
            carte = "demo";
        }

        /** 
         * Evenement associé à la sélection de la taille de carte "Petite"
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void cPetiteChecked(object sender, RoutedEventArgs e)
        {
            carte = "petite";
        }

        /** 
         * Evenement associé à la sélection de la taille de carte "Normale"
         * @param sender l'objet qui lance l'évenement
         * @param e les RoutedEventArgs associés
         */
        private void cNormaleChecked(object sender, RoutedEventArgs e)
        {
            carte = "normale";
        }

        /** 
         * Fonction chargée de vérifier que les joueurs ont sélectioné assez de paramètres pour lancer la partie
         * @return Vrai si l'on peut lancer une nouvelle partie, faux sinon
         */
        private Boolean verifierValidation()
        {
            return (peupleA != null) && (peupleB != null) && (carte != null);
        }

        /**
         * Evenement associé au clic sur le bouton "Valider"
         */
        public void valider(object sender, RoutedEventArgs e)
        {
            //On vérifie que l'on peut lancer une nouvelle partie
            if (!verifierValidation())
            {
                //Si ce n'est pas le cas on demande aux utilisateurs de choisir la carte et les peuples correctement
                MessageBox.Show("Veuillez sélectionner le type de carte et le peuple des deux joueurs.");
            }

            else
            {
                //Si on peut lancer la partie alors on crée un Créateur de partie auquel on délègue les informations
                //Utiles sur les peuples et la carte choisis
                CreateurPartie createur = new CreateurPartie();
                createur.PeupleA = peupleA;
                createur.PeupleB = peupleB;
                createur.TypeCarte = carte;

                

                //On construit la partie à l'aide de ce créateur
                Partie partie = createur.construire();

                //On rend l'UserControl de sélection invisible
                Visibility = Visibility.Collapsed;

                //On ajoute à la fenêtre principale une référence sur la partie et le nom des joueurs
                window.GameScreen.addReference(partie);
                window.GameScreen.setPlayerNames(j1Name.Text, j2Name.Text);

                //On construit la carte
                window.GameScreen.buildMap();

                //On rend l'UserControl de jeu visible
                window.GameScreen.Visibility = Visibility.Visible;
            }
        }

        /**
         * Evenement associé au clic sur le bouton "Annuler"
         */
        public void annuler(object sender, RoutedEventArgs e)
        {
            //On rend l'UserControl d'accueil invisible
            Visibility = Visibility.Collapsed;

            //On rend l'UserControl de jeu visible
            window.HomeScreen.Visibility = Visibility.Visible;
        }
    }
}
