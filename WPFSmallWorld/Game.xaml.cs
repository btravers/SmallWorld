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
        Dictionary<Point, Rectangle> _unitRectangles;
        Dictionary<Border, Unite> _selectedUnits;
        Border _selectedUnit;

        public Game()
        {
            InitializeComponent();

            _unitRectangles = new Dictionary<Point, Rectangle>();
            _selectedUnits = new Dictionary<Border, Unite>();

            _selection = new Rectangle();
            _selection.Stroke = Brushes.Red;
            _selection.StrokeThickness = 1;
        }

        public void addReference(Partie engine)
        {
            _engine = engine;
        }

        private void update()
        {
            foreach (Rectangle rect in _unitRectangles.Values)
            {
                mapGrid.Children.Remove(rect);
            }

            _unitRectangles.Clear();
            displayUnits(_engine._jA);
            displayUnits(_engine._jB);

            Tours.Text = "Tours restants : " + _engine._toursRestant.ToString();

            pointsJ1.Text = "Points : " + _engine._jA._points;
            pointsJ2.Text = "Points : " + _engine._jB._points;

            unitesJ1.Text = "Points : " + _engine._jA._unites.Count;
            unitesJ2.Text = "Points : " + _engine._jA._unites.Count;
        }

        public void buildMap()
        {
            //On définit la répartition de la grille
            var w = _engine._carte._width;
            var h = _engine._carte._width;
            for (int j = 0; j < w; j++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int i = 0; i < h; i++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });

                for (int j = 0; j < w; j++)
                {
                    Case tile = _engine._carte._cases[i, j];
                    var rect = createImage(i, j, tile);
                    mapGrid.Children.Add(rect);
                }
            }

            update();
            
        }

        private Rectangle createImage(int i, int j, Case tile)
        {
            Rectangle rectangle = new Rectangle();
            
            var image = new Image();
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../" + tile._sourceImage, UriKind.Relative);
            brush.ImageSource = new BitmapImage(uri);
            rectangle.Fill = brush;
            Grid.SetRow(rectangle, i);
            Grid.SetColumn(rectangle, j);
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
            rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);

            return rectangle;
        }

        

        private void displayUnits(Joueur j)
        {
            Dictionary<Point, List<Unite>> unites = new Dictionary<Point, List<Unite>>();
            foreach(Unite u in j._unites)
            {
                Point p = new Point(u._x,u._y);
                if(! unites.ContainsKey(p))
                {
                    unites.Add(p, new List<Unite>());
                }
                unites[p].Add(u);
            }

            foreach (Point p in unites.Keys)
            {
                ImageBrush brush = getImageUri(unites[p][0], unites[p].Count());
                var rectangle = new Rectangle();
                rectangle.Fill = brush;
                Grid.SetRow(rectangle, unites[p][0]._x);
                Grid.SetColumn(rectangle, unites[p][0]._y);
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;
                rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
                rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
                rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);
                mapGrid.Children.Add(rectangle);

                _unitRectangles.Add(new Point(unites[p][0]._x, unites[p][0]._y), rectangle);
            }

        }

        private ImageBrush getImageUri(Unite u, int nb)
        {
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../../Textures/gaulois.png", UriKind.Relative);
            if (nb == 1)
            {
                if (u is UniteNains)
                {
                    uri = new Uri(@"../../Textures/dwarf.png", UriKind.Relative);
                }
                else if (u is UniteVikings)
                {
                    uri = new Uri(@"../../Textures/viking.png", UriKind.Relative);
                }
            }
            else
            {
                if (u is UniteNains)
                {
                    uri = new Uri(@"../../Textures/dwarfM.png", UriKind.Relative);
                }
                else if (u is UniteVikings)
                {
                    uri = new Uri(@"../../Textures/vikingM.png", UriKind.Relative);
                }
                else
                {
                    uri = new Uri(@"../../Textures/gauloisM.png", UriKind.Relative); 
                }
            }
            brush.ImageSource = new BitmapImage(uri);
            return brush;
        }

        public void displaySelectedUnits(List<Unite> units)
        {
            _selectedUnits = new Dictionary<Border, Unite>();
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
                    _selectedUnit = border;
                }
                else
                {
                    border.BorderBrush = Brushes.Black;
                }

                border.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLefUnitSelectertHandler);

                unitSelecter.Children.Add(border);

                _selectedUnits.Add(border, unit);
            }

            displayDestinations();
        }

        public void displayDestinations()
        {
            
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

            _selectedUnits.Clear();
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

            int x = _engine._uniteSelectionnee._x;
            int y = _engine._uniteSelectionnee._y;

            _engine.selectCaseDestination(row, column);
            _selection.Stroke = Brushes.Black;

            update();

            _selectedUnits.Clear();
            unitSelecter.Children.Clear();

            e.Handled = true;
        }

        public void rectangleMouseLefUnitSelectertHandler(object sender, MouseEventArgs e)
        {
            _selectedUnit.BorderBrush = Brushes.Black;

            Border border = sender as Border;
            border.BorderBrush = Brushes.Red;
            _engine._uniteSelectionnee = _selectedUnits[border];
            _selectedUnit = border;
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _engine.joueurSuivant();

            if (_engine._toursRestant < 1)
            {
                MessageBox.Show("Choix du gagnant");
            }
            else
            {
                Tours.Text = "Tours restants : " + _engine._toursRestant.ToString();
            }
        }
    }
}
