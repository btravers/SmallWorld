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
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        /**
         * Reference vers la fenêtre principale
         */ 
        private MainWindow window;

        /**
         * Constructeur
         */ 
        public Home()
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
         * Evenement associé au clic sur le bouton "Nouveau"
         */
        public void nouveau(object sender, RoutedEventArgs e)
        {
            window.SelectScreen.addReference(window);

            //On rend l'UserControl d'accueil invisible
            Visibility = Visibility.Collapsed;

            //On rend l'UserControl de jeu visible
            window.SelectScreen.Visibility = Visibility.Visible;
        }

        /**
         * Evenement associé au clic sur le bouton "Charger"
         */
        public void charger(object sender, RoutedEventArgs e)
        {
            window.LoadScreen.addReference(window);

            //On rend l'UserControl d'accueil invisible
            Visibility = Visibility.Collapsed;

            //On rend l'UserControl de jeu visible
            window.LoadScreen.Visibility = Visibility.Visible;
        }
    }


}
