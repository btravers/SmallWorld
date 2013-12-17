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
        private Partie _engine;
        private Rectangle _selection;
        private Grid _tempGrid;
        private List<Rectangle> unitRectangle;

        public Game()
        {
            InitializeComponent();

            unitRectangle = new List<Rectangle>();
            
            _selection = new Rectangle();
            _selection.Stroke = Brushes.Red;
            _selection.StrokeThickness = 1;
        }

        public void addReference(Partie engine)
        {
            _engine = engine;
        }

        public void buildMap()
        {
            //On définit la répartition de la grille
            var w = _engine._carte._width;
            var h = _engine._carte._width;
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
                    var tile = _engine._carte._cases[i, j];
                    var canvas = createImage(i, j, tile);
                    mapGrid.Children.Add(canvas);
                }
            }

            unitRectangle.Clear();
            displayUnits(_engine._jA);
            displayUnits(_engine._jB);
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
            if (_tempGrid != null)
            {
                _tempGrid.Children.Remove(_selection);
            }
            grid.Children.Add(_selection);
            _tempGrid = grid;
        }

        private void displayUnits(Joueur j)
        {
            List<Unite> unites = j._unites;
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../../Textures/gaulois.png", UriKind.Relative);
            if (j._unites[0] is UniteNains)
            {
                uri = new Uri(@"../../Textures/dwarf.png", UriKind.Relative);
            }
            else if (j._unites[0] is UniteVikings)
            {
                uri = new Uri(@"../../Textures/viking.png", UriKind.Relative);
            }
            brush.ImageSource = new BitmapImage(uri);
            foreach (Unite u in unites)
            {
                var rectangle = new Rectangle();
                rectangle.Fill = brush;
                Grid.SetRow(rectangle, u._x);
                Grid.SetColumn(rectangle, u._y);
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
                rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                mapGrid.Children.Add(rectangle);

                unitRectangle.Add(rectangle);
            }

        }


        /********* Listeners *********/

        public void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Blue;
        }

        public void mouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle == _selection)
            {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;
            }
            else
            {
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
            }
        }
    }
}
