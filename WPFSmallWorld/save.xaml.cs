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

using SmallWorld;

namespace WPFSmallWorld
{
    /// <summary>
    /// Logique d'interaction pour save.xaml
    /// </summary>
    public partial class save : Window
    {
        private Partie partie;
        private MainWindow main;
        private String saveName;
        private int rg;

        public save(Partie p, MainWindow m)
        {
            InitializeComponent();
            partie = p;
            main = m;
            saveName = null;
            rg = -1;
        }

        /**
         * Evenement associé au clic sur le bouton "Valider"
         */
        public void valider(object sender, RoutedEventArgs e)
        {
            if (rg == -1)
            {
                MessageBox.Show("Selectionnez un emplacement");
            }
            else
            {
                partie.Sauvegarder(saveName);
                Save.Instance.exist[rg] = true;
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

        public void emp1(object sender, RoutedEventArgs e)
        {
            saveName = "save1.xls";
            rg = 0;
        }

        public void emp2(object sender, RoutedEventArgs e)
        {
            saveName = "save2.xls";
            rg = 1;
        }

        public void emp3(object sender, RoutedEventArgs e)
        {
            saveName = "save3.xls";
            rg = 2;
        }
    }


}
