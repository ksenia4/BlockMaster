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

namespace BlockMaster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool drawing = false;
        bool moving = false;
        bool stretching = false;

        Shape CurrentShape;
        Shape TargetShape;
        Point StartPosition;
        Point CurrentPosition;
        

        Polygon CurPoly;

        Condition CurrentCondition;
       
        //++ksu
        StateStore CurrentStateStore;
        //--ksu


        bool action = false;

        int ShapeType = 0;

        Rectangle SelectedBorder = new Rectangle();

        Rectangle StretchController = new Rectangle();

        bool LineSelected = false;

        public MainWindow()
        {
            InitializeComponent();
            SelectedBorder.Visibility = Visibility.Hidden;
            MainCanvas.Children.Add(SelectedBorder);

            StretchController.Visibility = Visibility.Hidden;
            MainCanvas.Children.Add(StretchController);

            StretchController.StrokeThickness = 8;
            StretchController.Stroke = Brushes.Black;
            StretchController.Height = 8;
            StretchController.Width = 8;
            StretchController.MouseDown +=StretchController_MouseDown;

            

            //++ksu
            CurrentStateStore = new StateStore();
            CurrentCondition = new Condition();
            //--ksu

        }

        public double GetDistanceBetweenPoints(Point p, Point q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!action)
            {
                if (SelectedBorder.Visibility == Visibility.Visible)
                {
                    SelectedBorder.Visibility = Visibility.Hidden;
                    StretchController.Visibility = Visibility.Hidden;
                }
                else if(LineSelected)
                {
                    CurrentShape.Stroke = Brushes.Black;
                    LineSelected = false;
                }
                else
                {
                    if (ShapeType == 0)
                    {
                        CurrentShape = new Ellipse();
                    }
                    if (ShapeType == 1)
                    {
                        CurrentShape = new Rectangle();
                    }
                    if (ShapeType == 4)
                    {
                        CurrentShape = new Rectangle();
                        CurrentShape.Name = "Poly";
                        RotateTransform rotateTransform1 =
                        new RotateTransform(45);
                        CurrentShape.RenderTransform = rotateTransform1;
                        
                        //CurPoly = (Polygon)CurrentShape;
                        /*CurPoly = new Polygon();
                        MainCanvas.Children.Add(CurPoly);*/
                    }
                    if (ShapeType != 2 || ShapeType != 3)
                    {
                        StartPosition = Mouse.GetPosition(MainCanvas);
                        CurrentShape.StrokeThickness = 3;
                        CurrentShape.Stroke = Brushes.Blue;

                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                        mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);
                        CurrentShape.Fill = mySolidColorBrush;

                        CurrentShape.Width = 1;
                        CurrentShape.Height = 1;
                        Canvas.SetTop(CurrentShape, StartPosition.Y);
                        Canvas.SetLeft(CurrentShape, StartPosition.X);


                        MainCanvas.Children.Add(CurrentShape);

                        drawing = true;
                    }
                }
            }
            action = false;
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if ( drawing)
            {
                CurrentPosition = Mouse.GetPosition(MainCanvas);

                double offsetWidth = CurrentPosition.X - StartPosition.X;
                double offsetHeight = CurrentPosition.Y - StartPosition.Y;

                

                CurrentShape.Width = Math.Abs(offsetWidth);
                CurrentShape.Height = Math.Abs(offsetHeight);

                if (offsetHeight < 0)
                {
                    Canvas.SetTop(CurrentShape, CurrentPosition.Y);
                }
                else Canvas.SetBottom(CurrentShape, CurrentPosition.Y);

                if (offsetWidth < 0)
                {
                    Canvas.SetLeft(CurrentShape, CurrentPosition.X);
                }
                else Canvas.SetRight(CurrentShape, CurrentPosition.X);

                if (ShapeType == 4)
                {
                    if (offsetWidth!=0)
                    {
                        CurrentShape.Height = CurrentShape.Width;
                    }
                    else if (offsetHeight != 0)
                    {
                        CurrentShape.Width = CurrentShape.Height;
                    }
                }

               // CurrentShape.Re
            }
            if (moving)
            {
                 CurrentPosition = Mouse.GetPosition(MainCanvas);

                double offsetWidth = CurrentPosition.X - StartPosition.X;
                double offsetHeight = CurrentPosition.Y - StartPosition.Y;

                Canvas.SetTop(CurrentShape, Canvas.GetTop(CurrentShape) + offsetHeight);
                Canvas.SetLeft(CurrentShape, Canvas.GetLeft(CurrentShape) + offsetWidth);

                Canvas.SetTop(SelectedBorder, Canvas.GetTop(SelectedBorder) + offsetHeight);
                Canvas.SetLeft(SelectedBorder, Canvas.GetLeft(SelectedBorder) + offsetWidth);

                Canvas.SetTop(StretchController, Canvas.GetTop(StretchController) + offsetHeight);
                Canvas.SetLeft(StretchController, Canvas.GetLeft(StretchController) + offsetWidth);

                StartPosition = CurrentPosition;


            }
            if (stretching)
            {
                StretchController.Visibility = Visibility.Hidden;
                SelectedBorder.Visibility = Visibility.Hidden;

                CurrentPosition = Mouse.GetPosition(MainCanvas);

                double offsetWidth = CurrentPosition.X - StartPosition.X;
                double offsetHeight = CurrentPosition.Y - StartPosition.Y;

                CurrentShape.Width = Math.Abs(offsetWidth);
                CurrentShape.Height = Math.Abs(offsetHeight);

                //CHECK IT

                if (offsetHeight < 0)
                {
                    Canvas.SetTop(CurrentShape, CurrentPosition.Y);
                }
                else Canvas.SetBottom(CurrentShape, CurrentPosition.Y);

                if (offsetWidth < 0)
                {
                    Canvas.SetLeft(CurrentShape, CurrentPosition.X);
                }
                else Canvas.SetRight(CurrentShape, CurrentPosition.X);

                SelectedBorder.Width = Math.Abs(offsetWidth);
                SelectedBorder.Height = Math.Abs(offsetHeight);

                //CHECK IT

                if (offsetHeight < 0)
                {
                    Canvas.SetTop(SelectedBorder, CurrentPosition.Y);
                }
                else Canvas.SetBottom(SelectedBorder, CurrentPosition.Y);

                if (offsetWidth < 0)
                {
                    Canvas.SetLeft(SelectedBorder, CurrentPosition.X);
                }
                else Canvas.SetRight(SelectedBorder, CurrentPosition.X);

                if (CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type == 4)
                {
                    if (offsetWidth != 0)
                    {
                        CurrentShape.Height = CurrentShape.Width;
                        SelectedBorder.Height = SelectedBorder.Width;
                    }
                    else if (offsetHeight != 0)
                    {
                        CurrentShape.Width = CurrentShape.Height;
                        SelectedBorder.Width = CurrentShape.Width;
                    }
                }
            }
        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                Label lab = new Label();
                lab.Content = "Элемент";

                lab.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = lab.DesiredSize;

                Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));
                /*Canvas.SetLeft(lab, Canvas.GetLeft(CurrentShape));
                Canvas.SetTop(lab, Canvas.GetTop(CurrentShape) + (Canvas.GetBottom(CurrentShape)-Canvas.GetTop(CurrentShape))/4.0);*/
                Canvas.SetLeft(lab, center.X-s.Width/2);
                Canvas.SetTop(lab, center.Y-s.Height/2);
                MainCanvas.Children.Add(lab);
                
                //CurrentShape.AddHandler(Shape.MouseDownEvent, new RoutedEventHandler(this.OnShapeClick));

                if (ShapeType == 4)
                {
                    double left = Canvas.GetLeft(CurrentShape); double top = Canvas.GetTop(CurrentShape);
                    CurrentShape.RenderTransform = null;
                }
                
                CurrentShape.MouseDown += new MouseButtonEventHandler(OnShapeClick);
                //lab.MouseDown += new MouseButtonEventHandler(OnShapeClick);

                //++ksu
                string id = Guid.NewGuid().ToString().Replace('-', '_');
                CurrentShape.Name = "S" + id; // здесь нужно генерировать идентификатор
                GBox NewShape = new GBox(CurrentShape, (string)lab.Content, " ", ShapeType);

                if (ShapeType == 4) CurrentShape.RenderTransform = new RotateTransform(45);

                Condition NewCondition = new Condition(CurrentCondition);
                NewCondition.AddElementInCondition(NewShape);

                CurrentStateStore.AddConditionInStore(CurrentCondition);
                CurrentCondition = NewCondition;
                //--ksu
                RegisterName(CurrentShape.Name, CurrentShape);
                drawing = false;
            }
            if (moving)
            {
                moving = false;
                
                //++ksu
                ChangeSizeAndPosition();
                //--ksu
                MainCanvas.Children.Clear();
                MainCanvas.Children.Add(SelectedBorder);
                MainCanvas.Children.Add(StretchController);
                DrawCondition();
            }
            if (stretching)
            {
                action = false;
                stretching = false;
                StretchController.Visibility = Visibility.Visible;
                SelectedBorder.Visibility = Visibility.Visible;

                //Sticky
                
                //--Sticky

                if (CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type != 4)
                {

                    Canvas.SetLeft(StretchController, Canvas.GetLeft(CurrentShape) + CurrentShape.ActualWidth - 4);
                    Canvas.SetTop(StretchController, Canvas.GetTop(CurrentShape) + CurrentShape.ActualHeight - 4);
                }

               
                else
                {

                    double diagonal = Math.Sqrt(Math.Pow(CurrentShape.ActualWidth, 2) + Math.Pow(CurrentShape.ActualHeight, 2));

                    Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));

                    Canvas.SetLeft(SelectedBorder, center.X - diagonal / 2);
                    Canvas.SetTop(SelectedBorder, center.Y - diagonal / 2);
                    SelectedBorder.Height = diagonal;
                    SelectedBorder.Width = diagonal;
                    SelectedBorder.Visibility = Visibility.Visible;
                    StretchController.Visibility = Visibility.Visible;
                    Canvas.SetLeft(StretchController, Canvas.GetLeft(SelectedBorder) + diagonal);
                    Canvas.SetTop(StretchController, Canvas.GetTop(SelectedBorder) + diagonal);


                }

                //++ksu
                ChangeSizeAndPosition();
                //--ksu
                MainCanvas.Children.Clear();
                MainCanvas.Children.Add(SelectedBorder);
                MainCanvas.Children.Add(StretchController);
                DrawCondition();
            }
        }

        private void OnShapeClick(object sender, MouseButtonEventArgs e)
        {
            action = true;
            if (ShapeType == 3)
            {
                TargetShape = (Shape)e.Source;

                string CurrentName = CurrentShape.Name;
                string TargetName = TargetShape.Name;

                CurrentShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                TargetShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                

                double tWidth = TargetShape.DesiredSize.Width;
                double tHeight = TargetShape.DesiredSize.Height;

               //CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type;

               if (CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type == 4)
                {
                    CurrentShape = SelectedBorder;
                }
                Point targetCenter =
                    TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.DesiredSize.Width / 2, TargetShape.DesiredSize.Height / 2));
                if (CurrentCondition.TakeGBoxFromCondition(TargetShape.Name.Substring(1)).Element.Type == 4)
                {
                    Rectangle HitBox = new Rectangle();
                    MainCanvas.Children.Add(HitBox);
                    double diagonal = Math.Sqrt(Math.Pow(TargetShape.DesiredSize.Width, 2) + Math.Pow(TargetShape.DesiredSize.Height, 2));
                   // HitBox.StrokeThickness = 10;
                    //HitBox.Stroke = Brushes.Black;
                    Point center = TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.DesiredSize.Width / 2, TargetShape.DesiredSize.Height / 2));

                    Canvas.SetLeft(HitBox, center.X - diagonal / 2);
                    Canvas.SetTop(HitBox, center.Y - diagonal / 2);
                    HitBox.Height = diagonal;
                    HitBox.Width = diagonal;

                   // HitBox.Visibility = Visibility.Hidden;
                    //HitBox.IsEnabled = false;
                    
                    TargetShape = HitBox;
                    targetCenter.X = Canvas.GetLeft(TargetShape) + diagonal/2;
                    targetCenter.Y = Canvas.GetTop(TargetShape) + diagonal / 2;
                    tWidth = diagonal;
                        tHeight = diagonal;
                }
            
                Point currentCenter =
                    CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.DesiredSize.Width / 2, CurrentShape.DesiredSize.Height / 2));
                

                double distance;
                double optimalDistance;
                Point candidate;
                Point currentOptimal;
                Point targetOptimal;



                candidate = currentCenter; candidate.Y += CurrentShape.DesiredSize.Height / 2;
                optimalDistance = GetDistanceBetweenPoints(candidate, targetCenter);
                currentOptimal = candidate;

                candidate = currentCenter; candidate.Y -= CurrentShape.DesiredSize.Height / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = currentCenter; candidate.X += CurrentShape.DesiredSize.Width / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = currentCenter; candidate.X -= CurrentShape.DesiredSize.Width / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                //now for targeted shape

                candidate = targetCenter; candidate.Y += tHeight / 2;
                optimalDistance = GetDistanceBetweenPoints(candidate, currentOptimal);
                targetOptimal = candidate;

                candidate = targetCenter; candidate.Y -= tHeight / 2;
                distance = GetDistanceBetweenPoints(candidate, currentOptimal);
                if (distance < optimalDistance)
                {
                    targetOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = targetCenter; candidate.X += tWidth / 2;
                distance = GetDistanceBetweenPoints(candidate, currentOptimal);
                if (distance < optimalDistance)
                {
                    targetOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = targetCenter; candidate.X -= tWidth / 2;
                distance = GetDistanceBetweenPoints(candidate, currentOptimal);
                if (distance < optimalDistance)
                {
                    targetOptimal = candidate;
                    optimalDistance = distance;
                }

                

                /*Rectangle R = new Rectangle();
                R.StrokeThickness = 1;
                R.Stroke = Brushes.Black;
                Canvas.SetLeft(R, currentOptimal.X);
                Canvas.SetTop(R, currentOptimal.Y);
                R.Height = 5;
                R.Width = 5;
                MainCanvas.Children.Add(R);

                Rectangle R1 = new Rectangle();
                R1.StrokeThickness = 1;
                R1.Stroke = Brushes.Black;
                Canvas.SetLeft(R1, targetOptimal.X);
                Canvas.SetTop(R1, targetOptimal.Y);
                R1.Height = 5;
                R1.Width = 5;
                MainCanvas.Children.Add(R1);*/

                System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
                line.Name = "L" + CurrentCondition.AmountOfelements.ToString();
                line.Visibility = System.Windows.Visibility.Visible;
                line.StrokeThickness = 2;
                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = currentOptimal.X;
                line.X2 = targetOptimal.X;
                line.Y1 = currentOptimal.Y;
                line.Y2 = targetOptimal.Y;

                MainCanvas.Children.Add(line); 
                TargetShape = null;
                ShapeType = 2;
                string id = Guid.NewGuid().ToString().Replace('-', '_');
                line.Name = "L" + id;
                //Sticky
                Condition NewCondition = new Condition(CurrentCondition);
                GLine gLine = new GLine(CurrentName, TargetName, line.Name);
                NewCondition.AddConnection(CurrentName.Substring(1), TargetName.Substring(1), gLine, line.Name.Substring(1));
                CurrentStateStore.AddConditionInStore(CurrentCondition);
                CurrentCondition = NewCondition;
                //--Sticky
                line.MouseDown += OnLineClick;
                
            }
           
            else if (ShapeType == 2)
            {
                ShapeType = 3;
            }

            Shape S = (Shape)e.Source;

            if (CurrentShape.Name == S.Name)
            {
                moving = true;
                StartPosition = Mouse.GetPosition(MainCanvas);
            }
            SelectedBorder.Visibility = Visibility.Visible;
            
            //MessageBox.Show("Purr");
            CurrentShape = (Shape)e.Source;
            DrawSelectedBorder(CurrentShape.Name, CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type);

            TitleBox.Text = CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Title;

           /* 
            if (CurrentShape.Name != "Poly")
            {
                SelectedBorder.StrokeThickness = 1;
                SelectedBorder.Stroke = Brushes.Black;
                Canvas.SetLeft(SelectedBorder, Canvas.GetLeft(CurrentShape));
                Canvas.SetTop(SelectedBorder, Canvas.GetTop(CurrentShape));
                SelectedBorder.Height = CurrentShape.Height;
                SelectedBorder.Width = CurrentShape.Width;

                StretchController.Visibility = Visibility.Visible;
                Canvas.SetLeft(StretchController, Canvas.GetLeft(CurrentShape) + CurrentShape.ActualWidth - 4);
                Canvas.SetTop(StretchController, Canvas.GetTop(CurrentShape) + CurrentShape.ActualHeight - 4);

                HeightBox.Text = CurrentShape.Height.ToString();
                WidthBox.Text = CurrentShape.Width.ToString();
            }
            else
            {
                SelectedBorder.StrokeThickness = 1;
                SelectedBorder.Stroke = Brushes.Black;
                double diagonal = Math.Sqrt(Math.Pow(CurrentShape.ActualWidth, 2) + Math.Pow(CurrentShape.ActualHeight, 2));

                Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));

                Canvas.SetLeft(SelectedBorder, center.X-diagonal/2);
                Canvas.SetTop(SelectedBorder, center.Y - diagonal / 2);
                SelectedBorder.Height = diagonal;
                SelectedBorder.Width = diagonal;

                StretchController.Visibility = Visibility.Visible;
                Canvas.SetLeft(StretchController, Canvas.GetLeft(SelectedBorder) + diagonal);
                Canvas.SetTop(StretchController, Canvas.GetTop(SelectedBorder) + diagonal);

                HeightBox.Text = CurrentShape.Height.ToString();
                WidthBox.Text = CurrentShape.Width.ToString();
            }*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShapeType = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShapeType = 4;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShapeType = 2;
        }

        private void StretchController_MouseDown(object sender, MouseButtonEventArgs e)
        {
            action = true;
            stretching = true;

                StartPosition.X = Canvas.GetLeft(CurrentShape);
                StartPosition.Y = Canvas.GetTop(CurrentShape);
            
            if (CurrentShape.Name == "Poly")
            {
                SelectedBorder.Visibility = Visibility.Hidden;
            }
        }

        //++ksu
        private void ChangeSizeAndPosition()
        {
            Condition NewCondition = new Condition(CurrentCondition);
            CurrentStateStore.AddConditionInStore(CurrentCondition);

            //Sticky
            GBox CurrentGBox = NewCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1));
            //--Sticky

            double Top = Canvas.GetTop(CurrentShape);
            double Left = Canvas.GetLeft(CurrentShape);
            double Height = CurrentShape.Height;
            double Width = CurrentShape.Width;
            CurrentGBox.SetPositionAndSize(Top, Left, Height, Width);
            
            CurrentCondition = NewCondition;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ShapeType = 1;
        }
        //--ksu

        public void DrawCondition()
        {
            foreach (System.Collections.Generic.KeyValuePair<string,GBox> element in CurrentCondition.BoxIDs)
            {
                DrawShape(element.Value, element.Key);
            }
            foreach (System.Collections.Generic.KeyValuePair<string, GLine> link in CurrentCondition.LineIDs)
            {
                string LinkID;
                LinkID = "L" + link.Value.GetLink().ID.ToString();
                string StartID;
                StartID = "S" + link.Value.GetLink().Start.ToString();
                string EndID;
                EndID = "S" + link.Value.GetLink().End.ToString();

                //CurrentShape = 

                DrawLink(StartID, EndID, LinkID, 0);
            }
            /*CurrentShape = null;
            TargetShape = null;
            SelectedBorder.Visibility = Visibility.Hidden;
            StretchController.Visibility = Visibility.Hidden;*/
        }

        public void DrawShape(GBox gBox, string ID)
        {
            
                if (gBox.Element.Type == 0)
                {
                    CurrentShape = new Ellipse();
                }
                if (gBox.Element.Type == 1 || gBox.Element.Type == 4)
                {
                    CurrentShape = new Rectangle();
                }

                CurrentShape.Name = "S" + ID.ToString();

                CurrentShape.Height = gBox.Element.Height;
                CurrentShape.Width = gBox.Element.Width;

                CurrentShape.StrokeThickness = 3;
                CurrentShape.Stroke = Brushes.Blue;

                SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 255);
                CurrentShape.Fill = mySolidColorBrush;

                MainCanvas.Children.Add(CurrentShape);
                Canvas.SetLeft(CurrentShape, gBox.Element.Left);
                Canvas.SetTop(CurrentShape, gBox.Element.Top);
                CurrentShape.MouseDown += new MouseButtonEventHandler(OnShapeClick);
                MainCanvas.UnregisterName(CurrentShape.Name);
                
                CurrentShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //CurrentShape.Arrange(new Rect(0, 0, CurrentShape.DesiredWidth, CurrentShape.DesiredHeight));
            
            RegisterName(CurrentShape.Name, CurrentShape);
            if (gBox.Element.Type == 4) CurrentShape.RenderTransform = new RotateTransform(45);

            MainCanvas.UpdateLayout();

            Label lab = new Label();
            lab.Content = gBox.Element.Title;

            lab.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            Size s = lab.DesiredSize;

            Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));
            /*Canvas.SetLeft(lab, Canvas.GetLeft(CurrentShape));
            Canvas.SetTop(lab, Canvas.GetTop(CurrentShape) + (Canvas.GetBottom(CurrentShape)-Canvas.GetTop(CurrentShape))/4.0);*/
            Canvas.SetLeft(lab, center.X - s.Width / 2);
            Canvas.SetTop(lab, center.Y - s.Height / 2);
            MainCanvas.Children.Add(lab);

        }

        public void DrawLink(string StartID, string EndID, string Id, int TargetType)
        {
            CurrentShape =(Shape) MainCanvas.FindName(StartID);
            TargetShape = (Shape)MainCanvas.FindName(EndID);

            CurrentShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            TargetShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            /*CurrentShape.Arrange(new Rect(0, 0, CurrentShape.DesiredSize.Width, CurrentShape.DesiredSize.Height));
            TargetShape.Arrange(new Rect(0, 0, TargetShape.DesiredSize.Width, TargetShape.DesiredSize.Height));*/

            /*MainCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            MainCanvas.Arrange(new Rect(0, 0, MainCanvas.DesiredSize.Width, MainCanvas.DesiredSize.Height));*/

            DrawSelectedBorder(CurrentShape.Name, CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type);

            MainCanvas.UpdateLayout();

            string CurrentName = CurrentShape.Name;
            string TargetName = TargetShape.Name;

            double tWidth = TargetShape.DesiredSize.Width;
            double tHeight = TargetShape.DesiredSize.Height;

            if (CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)).Element.Type == 4)
            {
                CurrentShape = SelectedBorder;
            }
            Point targetCenter =
                TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.DesiredSize.Width / 2, TargetShape.DesiredSize.Height / 2));
           /* targetCenter.X += Canvas.GetLeft(TargetShape);
            targetCenter.Y += Canvas.GetTop(TargetShape);*/

            if (CurrentCondition.TakeGBoxFromCondition(TargetShape.Name.Substring(1)).Element.Type == 4)
            {
                Rectangle HitBox = new Rectangle();
                MainCanvas.Children.Add(HitBox);
                double diagonal = Math.Sqrt(Math.Pow(TargetShape.DesiredSize.Width, 2) + Math.Pow(TargetShape.DesiredSize.Height, 2));
                // HitBox.StrokeThickness = 10;
                //HitBox.Stroke = Brushes.Black;
                Point center = TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.DesiredSize.Width / 2, TargetShape.DesiredSize.Height / 2));

                //TargetShape.RenderTransform = null;
               // center.X += Canvas.GetLeft(TargetShape); center.Y += Canvas.GetTop(TargetShape);

                Canvas.SetLeft(HitBox, center.X - diagonal / 2);
                Canvas.SetTop(HitBox, center.Y - diagonal / 2);
                HitBox.Height = diagonal;
                HitBox.Width = diagonal;

                HitBox.Visibility = Visibility.Visible;
                //HitBox.IsEnabled = false;

                TargetShape = HitBox;
                targetCenter.X = Canvas.GetLeft(TargetShape) + diagonal / 2;
                targetCenter.Y = Canvas.GetTop(TargetShape) + diagonal / 2;

               /* targetCenter =
                TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.DesiredSize.Width / 2, TargetShape.DesiredSize.Height / 2));*/

               /* targetCenter.X += Canvas.GetLeft(HitBox);
                targetCenter.Y += Canvas.GetTop(HitBox);*/

                tWidth = diagonal;
                tHeight = diagonal;
            }

            Point currentCenter =
                CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.DesiredSize.Width / 2, CurrentShape.DesiredSize.Height / 2));

           /* currentCenter.X += Canvas.GetLeft(CurrentShape);
            currentCenter.Y += Canvas.GetTop(CurrentShape);*/

            double distance;
            double optimalDistance;
            Point candidate;
            Point currentOptimal;
            Point targetOptimal;



            candidate = currentCenter; candidate.Y += CurrentShape.DesiredSize.Height / 2;
            optimalDistance = GetDistanceBetweenPoints(candidate, targetCenter);
            currentOptimal = candidate;

            candidate = currentCenter; candidate.Y -= CurrentShape.DesiredSize.Height / 2;
            distance = GetDistanceBetweenPoints(candidate, targetCenter);
            if (distance < optimalDistance)
            {
                currentOptimal = candidate;
                optimalDistance = distance;
            }

            candidate = currentCenter; candidate.X += CurrentShape.DesiredSize.Width / 2;
            distance = GetDistanceBetweenPoints(candidate, targetCenter);
            if (distance < optimalDistance)
            {
                currentOptimal = candidate;
                optimalDistance = distance;
            }

            candidate = currentCenter; candidate.X -= CurrentShape.DesiredSize.Width / 2;
            distance = GetDistanceBetweenPoints(candidate, targetCenter);
            if (distance < optimalDistance)
            {
                currentOptimal = candidate;
                optimalDistance = distance;
            }

            //now for targeted shape
            /*targetCenter.X += Canvas.GetLeft(TargetShape);
            targetCenter.Y += Canvas.GetTop(TargetShape);*/

            candidate = targetCenter; candidate.Y += tHeight / 2;
            optimalDistance = GetDistanceBetweenPoints(candidate, currentOptimal);
            targetOptimal = candidate;

            candidate = targetCenter; candidate.Y -= tHeight / 2;
            distance = GetDistanceBetweenPoints(candidate, currentOptimal);
            if (distance < optimalDistance)
            {
                targetOptimal = candidate;
                optimalDistance = distance;
            }

            candidate = targetCenter; candidate.X += tWidth / 2;
            distance = GetDistanceBetweenPoints(candidate, currentOptimal);
            if (distance < optimalDistance)
            {
                targetOptimal = candidate;
                optimalDistance = distance;
            }

            candidate = targetCenter; candidate.X -= tWidth / 2;
            distance = GetDistanceBetweenPoints(candidate, currentOptimal);
            if (distance < optimalDistance)
            {
                targetOptimal = candidate;
                optimalDistance = distance;
            }




            System.Windows.Shapes.Line line = new System.Windows.Shapes.Line();
            line.Name = Id;
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 2;
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.X1 = currentOptimal.X;
            line.X2 = targetOptimal.X;
            line.Y1 = currentOptimal.Y;
            line.Y2 = targetOptimal.Y;

            MainCanvas.Children.Add(line);
            TargetShape = null;
            line.MouseDown += OnLineClick;

        }

        public void DrawSelectedBorder(string SelectedName, int SelectedType)
        {
            SelectedBorder.Visibility = Visibility.Visible;
            //MessageBox.Show("Purr");
            //CurrentShape = (Shape)Application.Current.MainWindow.Co;
            CurrentShape = (Shape)MainCanvas.FindName(SelectedName);
            CurrentShape.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            MainCanvas.UpdateLayout();

            if (SelectedType!=4)
            {
                SelectedBorder.StrokeThickness = 1;
                SelectedBorder.Stroke = Brushes.Black;
                Canvas.SetLeft(SelectedBorder, Canvas.GetLeft(CurrentShape));
                Canvas.SetTop(SelectedBorder, Canvas.GetTop(CurrentShape));
                SelectedBorder.Height = CurrentShape.Height;
                SelectedBorder.Width = CurrentShape.Width;

                StretchController.Visibility = Visibility.Visible;
                Canvas.SetLeft(StretchController, Canvas.GetLeft(CurrentShape) + CurrentShape.DesiredSize.Width - 4);
                Canvas.SetTop(StretchController, Canvas.GetTop(CurrentShape) + CurrentShape.DesiredSize.Height - 4);

                HeightBox.Text = CurrentShape.Height.ToString();
                WidthBox.Text = CurrentShape.Width.ToString();
            }
            else
            {
                SelectedBorder.StrokeThickness = 1;
                SelectedBorder.Stroke = Brushes.Black;
                double diagonal = Math.Sqrt(Math.Pow(CurrentShape.ActualWidth, 2) + Math.Pow(CurrentShape.DesiredSize.Height, 2));

                Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.DesiredSize.Width / 2, CurrentShape.DesiredSize.Height / 2));
                

                Canvas.SetLeft(SelectedBorder, center.X - diagonal / 2);
                Canvas.SetTop(SelectedBorder, center.Y - diagonal / 2);
                SelectedBorder.Height = diagonal;
                SelectedBorder.Width = diagonal;

                StretchController.Visibility = Visibility.Visible;
                Canvas.SetLeft(StretchController, Canvas.GetLeft(SelectedBorder) + diagonal);
                Canvas.SetTop(StretchController, Canvas.GetTop(SelectedBorder) + diagonal);

                HeightBox.Text = CurrentShape.Height.ToString();
                WidthBox.Text = CurrentShape.Width.ToString();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CurrentCondition = CurrentStateStore.TakeConditionFromStore();
            MainCanvas.Children.Clear();
            MainCanvas.Children.Add(SelectedBorder);
            MainCanvas.Children.Add(StretchController);
            DrawCondition();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CurrentCondition = CurrentStateStore.TakeConditionFromDopStore();
            MainCanvas.Children.Clear();
            MainCanvas.Children.Add(SelectedBorder);
            MainCanvas.Children.Add(StretchController);
            DrawCondition();
        }

        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TitleBox.IsFocused)
            {
                GBox CurrentGBox = CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1));
                CurrentGBox.Element.Title = TitleBox.Text;
                MainCanvas.Children.Clear();
                MainCanvas.Children.Add(SelectedBorder);
                MainCanvas.Children.Add(StretchController);
                DrawCondition();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
            //STACK

            Condition NewCondition = new Condition(CurrentCondition);
            if (CurrentShape.Name[0] != 'L')
            {
                NewCondition.DeleteElementFromCondition(CurrentCondition.TakeGBoxFromCondition(CurrentShape.Name.Substring(1)));
            }
            else
            {
                NewCondition.DeleteLineFromCondition(NewCondition.TakeGLineFromCondition(CurrentShape.Name.Substring(1)));
            }
            CurrentStateStore.AddConditionInStore(CurrentCondition);
            CurrentCondition = NewCondition;

            
            MainCanvas.Children.Clear();
            MainCanvas.Children.Add(SelectedBorder);
            MainCanvas.Children.Add(StretchController);
            DrawCondition();

        }

        private void OnLineClick(object sender, MouseButtonEventArgs e)
        {
            action = true;
            CurrentShape = (Line)e.Source;
            Line L = (Line)e.Source;
            WidthBox.Text = "Линия";
            SelectedBorder.Visibility = Visibility.Hidden;

            LineSelected = true;

            CurrentShape.Stroke = Brushes.Blue;
        }

    }
}
