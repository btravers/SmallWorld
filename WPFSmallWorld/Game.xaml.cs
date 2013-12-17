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
        List<Rectangle> unitRectangles;
        Dictionary<Border, Unite> selectedUnits;
        Border selectedUnit;

        public Game()
        {
            InitializeComponent();

            unitRectangles = new List<Rectangle>();
            selectedUnits = new Dictionary<Border, Unite>();

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
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int c = 0; c < h; c++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Case tile = _engine._carte._cases[i, j];
                    var rect = createImage(i, j, tile);
                    mapGrid.Children.Add(rect);
                }
            }

            unitRectangles.Clear();
            displayUnits(_engine._jA);
            displayUnits(_engine._jB);
        }

        private Rectangle createImage(int i, int j, Case tile)
        {
            Rectangle rectangle = new Rectangle();
            
            var image = new Image();
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../" + tile._sourceImage, UriKind.Relative);
            brush.ImageSource = new BitmapImage(uri);
            rectangle.Fill = brush;
            Grid.SetColumn(rectangle, i);
            Grid.SetRow(rectangle, j);
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
            rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);

            return rectangle;
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
                rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
                rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
                rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                mapGrid.Children.Add(rectangle);

                unitRectangles.Add(rectangle);
            }

        }

        public void displaySelectedUnits(List<Unite> units)
        {
            selectedUnits = new Dictionary<Border, Unite>();
            int i = 0;
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../../Textures/gaulois.png", UriKind.Relative);
            if (units[0] is UniteNains)
            {
                uri = new Uri(@"../../Textures/dwarf.png", UriKind.Relative);
            }
            else if (units[0] is UniteVikings)
            {
                uri = new Uri(@"../../Textures/viking.png", UriKind.Relative);
            }
            brush.ImageSource = new BitmapImage(uri);
            foreach (Unite unit in units)
            {
                Border border = new Border();
                border.Background = brush;

                TextBlock unitText = new TextBlock();
                unitText.Text = unit._pm + " point(s) de mouvement \n" + unit._pdv + " point(s) de vie";
                unitText.FontSize = 14;
                unitText.Foreground = Brushes.Red;
                unitText.FontWeight = FontWeights.Bold;
                border.Child = unitText;
                border.Width = 100;
                border.Height = 100;
                border.BorderThickness = new Thickness(3);
                if (i == 0)
                {
                    border.BorderBrush = Brushes.Red;
                    i++;
                    selectedUnit = border;
                }
                else
                {
                    border.BorderBrush = Brushes.Black;
                }

                border.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLefUnitSelectertHandler);

                unitSelecter.Children.Add(border);

                selectedUnits.Add(border, unit);
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

        public void rectangleMouseLeftMapHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);

            selectedUnits.Clear();
            unitSelecter.Children.Clear();
            if (_selection != null)
            {
                _selection.Stroke = Brushes.Black;
            }

            List<Unite> units = _engine.selectCaseInitiale(row,column);

            if (units.Any())
            {
                displaySelectedUnits(units);
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Red;

                _selection = rectangle;
            }
            e.Handled = true;
        }

        public void rectangleMouseRightHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            int column = Grid.GetColumn(rectangle);
            int row = Grid.GetRow(rectangle);

            _engine.selectCaseDestination(row, column);
            _selection.Stroke = Brushes.Black;
            unitRectangles.Clear();
            displayUnits(_engine._jA);
            displayUnits(_engine._jB);
            //TODO afficher les info du joueur

            selectedUnits.Clear();
            unitSelecter.Children.Clear();

            e.Handled = true;
        }

        public void rectangleMouseLefUnitSelectertHandler(object sender, MouseEventArgs e)
        {
            selectedUnit.BorderBrush = Brushes.Black;

            Border border = sender as Border;
            border.BorderBrush = Brushes.Red;
            _engine._uniteSelectionnee = selectedUnits[border];
            selectedUnit = border;
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 4 / 3;
            MessageBox.Show(i.ToString());
        }
    }
}
