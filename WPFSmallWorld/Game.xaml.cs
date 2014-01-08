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
    * La classe Game représente l'écran de jeu.
    * @author Mickaël Olivier, Benoit Travers
    */
    public partial class Game : UserControl
    {
        private Partie _engine; /**La partie lancée*/
        private Rectangle _selection; /**Le rectangle de sélection des unités*/
        Dictionary<Point, Rectangle> _unitRectangles; /**Un dictionnaire qui associe un point à un rectangle*/
        Dictionary<Border, Unite> _selectedUnits; /**Un dictionnaire qui associe un objet border pour la sélection de chaus eunité*/
        List<Rectangle> _suggestions; /**Une liste de rectangles qui correspond à la suggestion des cases de déplacement possibles pour une unité*/
        Border _selectedUnit; /**Un border qui correspon à l'unité sélectionnée*/
        private MainWindow window;

        /**
         * Constructeur
         * Initialise l'UserCOntrol en utilisant les balises définies dans le code xaml
         */
        public Game()
        {
            InitializeComponent();

            //On crée les dictionnaires, vides au départ
            _unitRectangles = new Dictionary<Point, Rectangle>();
            _selectedUnits = new Dictionary<Border, Unite>();

            //On crée le collecteur des rectangles de suggestion de destination
            _suggestions = new List<Rectangle>();

            //On crée le rectangle de sélection, auquel on associe un brush rouge de'péaisseur 1
            _selection = new Rectangle();
            _selection.Stroke = Brushes.Red;
            _selection.StrokeThickness = 1;
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
         * Ajoute une référence sur la fenêtre principale
         * @param win la fenêtre en question
         */
        public void addReference(Partie engine)
        {
            _engine = engine;
        }

        /**
         * Ajoute une référence sur les noms des joueurs
         * @param j1Name le nom du joueur 1
         * @param j2Name le nom du joueur 2
         */
        public void setPlayerNames(String j1Name, String j2Name)
        {
            _engine._jA._name = j1Name;
            _engine._jB._name = j2Name;
        }

        /**
         * Fonction de mise à jour de la fenêtre de jeu appelée à la fin du tour de chaque joueur
         */
        private void update()
        {
            //Pour chaque rectangle associé aux Unités
            foreach (Rectangle rect in _unitRectangles.Values)
            {
                //On les supprime de la grille représentant la carte
                mapGrid.Children.Remove(rect);
            }

            //On supprime les rectangles de sélection instantiés
            _unitRectangles.Clear();

            //On redessine les unités pour le joueur A puis pour le joueur B
            displayUnits(_engine._jA);
            displayUnits(_engine._jB);

            //On détermine le joueur actuel.
            String nomJoueur = "Joueur B";
            if (_engine.joueJoueurA())
            {
                nomJoueur = "Joueur A";
            }

            //On affiche sur la fenêtre de jeu le nom du joueur actuel et le nombre de tours restants
            Joueur.Text = nomJoueur;
            Tours.Text = "Tours restants : " + _engine._toursRestant.ToString();

            //On affiche le nom des deux joueurs
            Joueur1.Text = _engine._jA._name + " (" + _engine._jA._peuple + ")";
            Joueur2.Text = _engine._jB._name + " (" + _engine._jB._peuple + ")";

            //On met à jour les points pour chaque joueur
            pointsJ1.Text = "Points : " + _engine._jA._points;
            pointsJ2.Text = "Points : " + _engine._jB._points;

            //On met à jour les unités restantes pour chaque joueur
            unitesJ1.Text = "Unités restantes : " + _engine._jA._unites.Count;
            unitesJ2.Text = "Unités restantes : " + _engine._jB._unites.Count;
        }

        /*
         * Fonction chargée de créer la carte
         */
        public void buildMap()
        {
            //On définit la répartition de la grille
            var w = _engine._carte._width;
            var h = _engine._carte._width;

            //On ajoute pour chaque coordonée une column definition et une rowdefinition qui découpent la carte en carrés de 50x50
            for (int j = 0; j < w; j++)
            {
                mapGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            for (int i = 0; i < h; i++)
            {
                mapGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });

                for (int j = 0; j < w; j++)
                {
                    //On ajoute chaque case à la grille et on les dessine
                    Case tile = _engine._carte._cases[i, j];
                    var rect = createImage(i, j, tile);
                    mapGrid.Children.Add(rect);
                }
            }

            //On met à jour l'écran de jeu une première fois
            update();     
        }

        /*
         * Fonction chargée de créer l'image correspondant à une case
         * @param i la coordonée en abscisse de la case
         * @param j la coordonée en ordonée de la case
         * @param tile le type de case à dessiner
         * @return le rectangle représentant la case contenant l'image voulue
         */
        private Rectangle createImage(int i, int j, Case tile)
        {
            //On crée un rectangle
            Rectangle rectangle = new Rectangle();
            
            //On crée une image que l'on ajoute dans le rectangle
            var image = new Image();
            ImageBrush brush = new ImageBrush();
            var uri = new Uri(@"../" + tile._sourceImage, UriKind.Relative);
            brush.ImageSource = new BitmapImage(uri);
            rectangle.Fill = brush;

            //On place le rectangle sur la grille, en lui donnant un contour noir d'épaisseur 1
            Grid.SetRow(rectangle, i);
            Grid.SetColumn(rectangle, j);
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Black;

            //On associe des listeners à ce rectangle
            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
            rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
            rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
            rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);

            return rectangle;
        }        

        /*
         * Fonction chargée de dessiner les unités du joueur passé en paramètre sur la fenêtre
         * @param j le joueur considéré
         */
        private void displayUnits(Joueur j)
        {
            //On crée un dictionnaire associant points et unités
            Dictionary<Point, List<Unite>> unites = new Dictionary<Point, List<Unite>>();
            j.Positions = new List<int>();
            List<Unite> toRemove = new List<Unite>();

            //Pour chaque unité
            foreach(Unite u in j._unites)
            {
                //Si l'unité est en vie
                if (u.enVie)
                {
                    //On crée un point aux coordonées de l'unité
                    Point p = new Point(u._x, u._y);

                    //Si le dictionnaire des unités ne contient pas déjà ce point
                    if (!unites.ContainsKey(p))
                    {
                        //On ajoute une entrée qui associe le point et une liste d'unités
                        unites.Add(p, new List<Unite>());
                        //On ajoute 
                        j.Positions.Add(u._x*_engine._carte._width+u._y);
                    }
                    //Dans tous les cas on ajoute l'unités dans la liste d'unités associée au point considéré 
                    unites[p].Add(u);
                }
                //Sinon
                else
                {
                    //On l'ajoute à un dictionnaire temporaire chargé de supprimer les unités
                    toRemove.Add(u);
                }
            }

            //Pour chaque unité à supprimer
            foreach (Unite u in toRemove)
            {
                //On la supprime
                j._unites.Remove(u);
            }

            //Pour chaque point dans le dictionnaire unites
            foreach (Point p in unites.Keys)
            {
                //On crée un rectangle qui possède une image de l'unité l'unité
                ImageBrush brush = getImageUri(unites[p][0], unites[p].Count());
                var rectangle = new Rectangle();
                rectangle.Fill = brush;
                Grid.SetRow(rectangle, unites[p][0]._x);
                Grid.SetColumn(rectangle, unites[p][0]._y);

                //On l'entoure d'un bord noir d'épaisseur 1
                rectangle.StrokeThickness = 1;
                rectangle.Stroke = Brushes.Black;

                //On associe des handlers à ce rectangle contenant l'unité
                rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLeftMapHandler);
                rectangle.MouseRightButtonDown += new MouseButtonEventHandler(rectangleMouseRightHandler);
                rectangle.MouseEnter += new MouseEventHandler(mouseEnterHandler);
                rectangle.MouseLeave += new MouseEventHandler(mouseLeaveHandler);

                //On ajoute l'unité à la carte
                mapGrid.Children.Add(rectangle);

                //On ajoute au dictionnaire paramètre de la classe courante le couple point-rectangle
                _unitRectangles.Add(new Point(unites[p][0]._x, unites[p][0]._y), rectangle);
            }

        }

        /**
         * Fonction permettant de définir quel sera l'image associée à l'unité considérée compte tenu du nombre d'unités sur la case
         * @param u l'unité considérée
         * @param nb le nombre d'unités présentes sur la case
         * @return un ImageBrush correspondant à l'unité voulue pour le nombre d'unités sur la case considéré
         */
        private ImageBrush getImageUri(Unite u, int nb)
        {
            //On crée un ImageBrush
            ImageBrush brush = new ImageBrush();

            //On crée l'uri associé à l'image voulue (celà dépend du peuple de l'unite u, du nombre nb)
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

            //On définit la source du brush comme étant cette uri
            brush.ImageSource = new BitmapImage(uri);

            return brush;
        }

        /*
         * Fonction chargée de dessiner les unités considérées
         * @param units la liste d'unités considérée
         */
        public void displaySelectedUnits(List<Unite> units)
        {
            //On crée la liste d'unités sélectionnées
            _selectedUnits = new Dictionary<Border, Unite>();

            //On associe le bon uri pir l'image du premier élément de la liste
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

            //Pour chaque unité dans units
            foreach (Unite unit in units)
            {
                //On crée un border contenant l'ImageBrush crée ci-dessus
                Border border = new Border();
                border.Background = brush;

                //On crée un TextBlock sur lequel on écrit les information sur l'unité considérées
                //(ses points de mouvement, ses points de vie)
                TextBlock unitText = new TextBlock();
                unitText.Text = unit._pm + " points de \nmouvement \n" + unit._pdv + " points \nde vie ";
                unitText.FontSize = 12;
                unitText.Foreground = Brushes.Red;
                unitText.FontWeight = FontWeights.Bold;
                border.Child = unitText;
                border.Width = 75;
                border.Height = 75;
                border.BorderThickness = new Thickness(3);

                //Si l'unité est la première de la liste, on la sélectionne et son bord est rouge
                if (i == 0)
                {
                    border.BorderBrush = Brushes.Red;
                    i++;
                    _selectedUnit = border;
                }
                //Sinon le bord sera noir
                else
                {
                    border.BorderBrush = Brushes.Black;
                }

                //On ajoute un handler au border en question
                border.MouseLeftButtonDown += new MouseButtonEventHandler(rectangleMouseLefUnitSelectertHandler);

                //On l'ajoute au composant unitSelecter de la classe
                unitSelecter.Children.Add(border);

                _selectedUnits.Add(border, unit);
            }
        }

        /**
         * Méthode qui permet d'afficher les rectangles de suggestion de destination
         */
        public void displayDestinations()
        {
            List<int> suggestions = _engine.suggestion();

            foreach (int i in suggestions)
            {
                var rect = new Rectangle();
                rect.StrokeThickness = 5;
                rect.Stroke = Brushes.Green;

                Grid.SetRow(rect, i / _engine._carte._width);
                Grid.SetColumn(rect, i % _engine._carte._width);

                mapGrid.Children.Add(rect);
                _suggestions.Add(rect);
            }
        }

        /**
         * Méthode permettant d'enlever les rectangles de suggestion de destination
         */
        public void deleteDestinations()
        {
            foreach (Rectangle rect in _suggestions)
            {
                mapGrid.Children.Remove(rect);
            }
        }


        /********* Listeners *********/

        /** 
        * Evenement au passage de la souris sur une case
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
        public void mouseEnterHandler(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            rectangle.StrokeThickness = 1;
            rectangle.Stroke = Brushes.Blue;
        }

        /** 
        * Evenement à la sortie de la souris d'une case
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
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

        /** 
        * Evenement au clic gauche de la souris sur une case de la carte
        * Celà correspond à la sélection d'une case sur la carte
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
        public void rectangleMouseLeftMapHandler(object sender, MouseEventArgs e)
        {
            deleteDestinations();

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
                displayDestinations();
            }
            e.Handled = true;
        }

        /** 
        * Evenement au clic droit de la souris sur une case de la carte
        * Celà correspond au déplacement de l'unité sélectionnée si possible
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
        public void rectangleMouseRightHandler(object sender, MouseEventArgs e)
        {
            if (_engine._uniteSelectionnee != null)
            {
                deleteDestinations();

                var rectangle = sender as Rectangle;
                int column = Grid.GetColumn(rectangle);
                int row = Grid.GetRow(rectangle);

                int x = _engine._uniteSelectionnee._x;
                int y = _engine._uniteSelectionnee._y;

                _engine.selectCaseDestination(row, column);
                _selection.Stroke = Brushes.Black;

                _selectedUnits.Clear();
                unitSelecter.Children.Clear();

                update();
            }

            e.Handled = true;
        }

        /** 
        * Evenement associé au clic gauche de la souris sur une case du visualiseur d'unités
        * Celà correspond à la sélection de l'unité pour la case sélectionnée
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
        public void rectangleMouseLefUnitSelectertHandler(object sender, MouseEventArgs e)
        {
            _selectedUnit.BorderBrush = Brushes.Black;

            Border border = sender as Border;
            border.BorderBrush = Brushes.Red;
            _engine._uniteSelectionnee = _selectedUnits[border];
            _selectedUnit = border;
            e.Handled = true;
        }

        /** 
        * Evenement associé au clic gauche de la souris surle bouton "Fin de tour"
        * Celà permet au joueur actuel de finir son tour. On passe alors au joueur suivant s'il reste
        * des tours à jouer après avoir compté les points et mis à jour la fenêtre.
        * @param sender l'objet qui lance l'évenement
        * @param e les RoutedEventArgs associés
        */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Joueur j;
            if (_engine.joueJoueurA())
            {
                j = _engine._jA;
            }
            else
            {
                j = _engine._jB;
            }

            foreach (Unite u in j._unites)
            {
                u._pm = 1;
            }

            _engine.joueurSuivant();

            update();
            Tours.Text = "Tours restants : " + _engine._toursRestant.ToString();

            if (_engine._toursRestant < 1)
            {
                if (_engine.egalite())
                {
                    MessageBox.Show("Egalité !");
                }
                else
                {
                    MessageBox.Show("Le gagnant est " + _engine.gagnant() + " !");
                }
                //TODO : arrêter le jeu ou revenir au menu principal ?
            }
        }

        /**
         * Evenement associé au clic sur le bouton "Sauvegarder"
         */
        public void save(object sender, RoutedEventArgs e)
        {
            Window sauvegarde = new save(_engine);
            sauvegarde.ShowDialog();
        }
    }
}
