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
        Shape CurrentShape;
        Shape TargetShape;
        Point StartPosition;
        Point CurrentPosition;

        Condition CurrentCondition;

        bool action = false;

        int ShapeType = 0;

        Rectangle SelectedBorder = new Rectangle();

        public MainWindow()
        {
            InitializeComponent();
            SelectedBorder.Visibility = Visibility.Hidden;
            MainCanvas.Children.Add(SelectedBorder);
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
                }
                else
                {
                    if (ShapeType == 0)
                        CurrentShape = new Ellipse();
                    if (ShapeType == 1)
                        CurrentShape = new Rectangle();
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


               // CurrentShape.Re
            }
        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (drawing)
            {
                /*Label lab = new Label();
                lab.Content = "Meow";

                lab.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = lab.DesiredSize;

                Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));*/
                /*Canvas.SetLeft(lab, Canvas.GetLeft(CurrentShape));
                Canvas.SetTop(lab, Canvas.GetTop(CurrentShape) + (Canvas.GetBottom(CurrentShape)-Canvas.GetTop(CurrentShape))/4.0);*/
                /*Canvas.SetLeft(lab, center.X-s.Width/2);
                Canvas.SetTop(lab, center.Y-s.Height/2);
                MainCanvas.Children.Add(lab);*/
                
                //CurrentShape.AddHandler(Shape.MouseDownEvent, new RoutedEventHandler(this.OnShapeClick));
                CurrentShape.MouseDown += new MouseButtonEventHandler(OnShapeClick);
                //lab.MouseDown += new MouseButtonEventHandler(OnShapeClick);


                drawing = false;
            }
        }

        private void OnShapeClick(object sender, MouseButtonEventArgs e)
        {
            action = true;
            if (ShapeType == 3)
            {
                TargetShape = (Shape)e.Source;

                Point currentCenter = 
                    CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));
                Point targetCenter =
                    TargetShape.TransformToAncestor(MainCanvas).Transform(new Point(TargetShape.ActualWidth / 2, TargetShape.ActualHeight / 2));

                double distance;
                double optimalDistance;
                Point candidate;
                Point currentOptimal;
                Point targetOptimal;

                candidate = currentCenter; candidate.Y += CurrentShape.ActualHeight / 2;
                optimalDistance = GetDistanceBetweenPoints(candidate, targetCenter);
                currentOptimal = candidate;

                candidate = currentCenter; candidate.Y -= CurrentShape.ActualHeight / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = currentCenter; candidate.X += CurrentShape.ActualWidth / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = currentCenter; candidate.X -= CurrentShape.ActualWidth / 2;
                distance = GetDistanceBetweenPoints(candidate, targetCenter);
                if (distance < optimalDistance)
                {
                    currentOptimal = candidate;
                    optimalDistance = distance;
                }

                //now for targeted shape

                candidate = targetCenter; candidate.Y += TargetShape.ActualHeight / 2;
                optimalDistance = GetDistanceBetweenPoints(candidate, currentOptimal);
                targetOptimal = candidate;

                candidate = targetCenter; candidate.Y -= TargetShape.ActualHeight / 2;
                distance = GetDistanceBetweenPoints(candidate, currentOptimal);
                if (distance < optimalDistance)
                {
                    targetOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = targetCenter; candidate.X += TargetShape.ActualWidth / 2;
                distance = GetDistanceBetweenPoints(candidate, currentOptimal);
                if (distance < optimalDistance)
                {
                    targetOptimal = candidate;
                    optimalDistance = distance;
                }

                candidate = targetCenter; candidate.X -= TargetShape.ActualWidth / 2;
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
            }
            else if (ShapeType == 2)
            {
                ShapeType = 3;
            }
            SelectedBorder.Visibility = Visibility.Visible;
            //MessageBox.Show("Purr");
            CurrentShape = (Shape)e.Source;

            SelectedBorder.StrokeThickness = 1;
            SelectedBorder.Stroke = Brushes.Black;
            Canvas.SetLeft(SelectedBorder, Canvas.GetLeft(CurrentShape));
            Canvas.SetTop(SelectedBorder, Canvas.GetTop(CurrentShape));
            SelectedBorder.Height = CurrentShape.Height;
            SelectedBorder.Width = CurrentShape.Width;

            HeightBox.Text = CurrentShape.Height.ToString();
            WidthBox.Text = CurrentShape.Width.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShapeType = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShapeType = 1;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ShapeType = 2;
        }
    }
}
