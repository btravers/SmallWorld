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
using System.Windows.Shapes;
using System.IO;

using SmallWorld;

namespace WPFSmallWorld
{
    /// <summary>
    /// Logique d'interaction pour save.xaml
    /// </summary>
    public partial class save : Window
    {
        /**
         * La partie en cours
         */
        private Partie partie;

        /**
         * Le nom du fichier de sauvegarde
         */ 
        private String saveName;

        /**
         * Constructeur de la fenêtre save
         * @param p la partie en cours
         */
        public save(Partie p)
        {
            InitializeComponent();
            partie = p;
            saveName = null;
            displayInfo("save1.sav", info1);
            displayInfo("save2.sav", info2);
            displayInfo("save3.sav", info3);
        }

        public void displayInfo(string name, TextBlock info)
        {
            if (File.Exists(name))
            {
                info.Text = " (Sauvgarde utilisée)";
            }
        }

        /**
         * Evenement associé au clic sur le bouton "Valider"
         */
        public void valider(object sender, RoutedEventArgs e)
        {
            if (saveName == null)
            {
                MessageBox.Show("Selectionnez un emplacement");
            }
            else
            {
                partie.Sauvegarder(saveName);
                this.Close();
            }
        }

        /**
         * Evenement associé au clic sur le bouton "Annuler"
         */
        public void annuler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /**
         * Evenement associé à la selection de l'emplacement 1
         */ 
        public void emp1(object sender, RoutedEventArgs e)
        {
            saveName = "save1.sav";
        }

        /**
         * Evenement associé à la selection de l'emplacement 2
         */ 
        public void emp2(object sender, RoutedEventArgs e)
        {
            saveName = "save2.sav";
        }

        /**
         * Evenement associé à la selection de l'emplacement 3
         */ 
        public void emp3(object sender, RoutedEventArgs e)
        {
            saveName = "save3.sav";
        }
    }


}
