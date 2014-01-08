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

using System.IO;
using SmallWorld;

namespace WPFSmallWorld
{
    /// <summary>
    /// Logique d'interaction pour Load.xaml
    /// </summary>
    public partial class Load : UserControl
    {
        private MainWindow window;

        public Load()
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
         * Evenement associé au clic sur le bouton "Sauvegarde 1"
         */
        public void save1(object sender, RoutedEventArgs e)
        {
            open("save1.sav");
        }

        /**
         * Evenement associé au clic sur le bouton "Sauvegarde 2"
         */
        public void save2(object sender, RoutedEventArgs e)
        {
            open("save2.sav");
        }

        /**
         * Evenement associé au clic sur le bouton "Sauvegarde 3"
         */
        public void save3(object sender, RoutedEventArgs e)
        {
            open("save3.sav");
        }

        private void open(String name)
        {
            if (!File.Exists(name))
            {
                MessageBox.Show("Aucune sauvegarde à cet emplacement.");
            }
            else
            {
                Partie partie = Partie.Charger(name);

                //On rend l'UserControl de sélection invisible
                Visibility = Visibility.Collapsed;

                window.GameScreen.addReference(window);

                //On ajoute à la fenêtre principale une référence sur la partie et le nom des joueurs
                window.GameScreen.addReference(partie);

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
            window.HomeScreen.addReference(window);

            //On rend l'UserControl d'accueil invisible
            Visibility = Visibility.Collapsed;

            //On rend l'UserControl de jeu visible
            window.HomeScreen.Visibility = Visibility.Visible;
        }
    }
}
