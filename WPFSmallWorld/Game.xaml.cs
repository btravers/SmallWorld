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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class Game : UserControl
    {
        private Partie engine;
        private Rectangle selection;
        private Grid tempGrid;

        public Game()
        {
            InitializeComponent();

            selection = new Rectangle();
            selection.Stroke = Brushes.Red;
            selection.StrokeThickness = 1;
        }

        public void addReference(Partie _engine)
        {
            engine = _engine;
        }

        public void buildMap()
        {
            //On définit la répartition de la grille
            var w = engine._carte._width;
            var h = engine._carte._width;
            for (int c = 0; c < w; c++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300 / w, GridUnitType.Pixel) });
            }

            for (int c = 0; c < h; c++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(300 / h, GridUnitType.Pixel) });
            }

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    var tile = engine._carte._cases[i, j];
                    var canvas = createImage(i, j, tile);
                    mapGrid.Children.Add(canvas);
                }
            }
        }

        private Grid createImage(int i, int j, Case tile)
        {
            var image = new Image();
            Grid grid = new Grid();

            var uriSource = new Uri(@"/WPFSmallWorld;component/Textures/" + tile._sourceImage, UriKind.Relative);
            image.Source = new BitmapImage(uriSource);
            image.Tag = tile;
            grid.Children.Add(image);

            Grid.SetColumn(grid, i);
            Grid.SetRow(grid, j);

            grid.MouseLeftButtonDown += new MouseButtonEventHandler(Mouse_OnTile);

            return grid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 4 / 3;
            MessageBox.Show(i.ToString());
        }

        private void Mouse_OnTile(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            if (tempGrid != null)
            {
                tempGrid.Children.Remove(selection);
            }
            grid.Children.Add(selection);
            tempGrid = grid;
        }
    }
}
