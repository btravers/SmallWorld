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
    * La classe MainWindow représente la fenêtre de l'application
    * @author Mickaël Olivier, Benoit Travers
    */
    public partial class MainWindow : Window
    {
        /**
         * Constructeur de la classe
         */
        public MainWindow()
        {
            //On initialise la fenêtre à l'aide du XAML 
            //et on ajoute une référence de cette fenêtre à l'écran de sélection
            InitializeComponent();
            HomeScreen.addReference(this);
            LoadScreen.addReference(this);
            SelectScreen.addReference(this);
            GameScreen.addReference(this);
        }
    }
}
