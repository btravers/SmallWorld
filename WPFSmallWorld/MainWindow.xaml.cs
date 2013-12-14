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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CreateurPartie createur
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void initCreateur()
        {
            /*createur = new CreateurPartie();
            createur.addPeupleA(SelectScreen.peupleA);
            createur.addPeupleB(SelectScreen.peupleB);
            createur._typeCarte = SelectScreen.carte;
            createur.construire();*/

            MessageBox.Show("Test");

            /*SelectScreen.Visibility = Visibility.Collapsed;
            GameScreen.Visibility = Visibility.Visible;*/
        }
    }
}
