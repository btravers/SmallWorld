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
    /// Logique d'interaction pour Select.xaml
    /// </summary>
    public partial class Select : UserControl
    {
        private String peupleA;
        private String peupleB;
        private String carte;

        public Select()
        {
            InitializeComponent();
        }
 
        private void enabledItems1(RadioButton button)
        {
            j1Nains.IsEnabled = true;
            j1Vikings.IsEnabled = true;
            j1Gaulois.IsEnabled = true;

            button.IsEnabled = false;
        }

        private void enabledItems2(RadioButton button)
        {
            j2Nains.IsEnabled = true;
            j2Vikings.IsEnabled = true;
            j2Gaulois.IsEnabled = true;

            button.IsEnabled = false;
        }

        private void j1NainsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Nains);
            peupleA = "nains";
        }

        private void j1GauloisChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Gaulois);
            peupleA = "gaulois";
        }

        private void j1VikingsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems2(j2Vikings);
            peupleA = "vikings";
        }

        private void j2NainsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Nains);
            peupleB = "nains";
        }

        private void j2GauloisChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Gaulois);
            peupleB = "gaulois";
        }

        private void j2VikingsChecked(object sender, RoutedEventArgs e)
        {
            enabledItems1(j1Vikings);
            peupleB = "vikings";
        }

        private void cDemoChecked(object sender, RoutedEventArgs e)
        {
            carte = "demo";
        }

        private void cPetiteChecked(object sender, RoutedEventArgs e)
        {
            carte = "petite";
        }

        private void cNormaleChecked(object sender, RoutedEventArgs e)
        {
            carte = "normale";
        }

        public void valider(object sender, RoutedEventArgs e)
        {
            //TODO (ceci ne marche pas)
            /*GlobalWindow.initCreateur();*/
            CreateurPartie createur = new CreateurPartie();
            createur.PeupleA = peupleA;
            createur.PeupleB = peupleB;
            createur.TypeCarte = carte;
            Partie partie = createur.construire();
        }
    }
}
