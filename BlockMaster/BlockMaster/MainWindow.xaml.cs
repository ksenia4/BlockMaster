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
        Point StartPosition;
        Point CurrentPosition;
        Condition CurrentCondition;

        int ShapeType = 0;

        Border SelectedBorder = new Border();

        public MainWindow()
        {
            InitializeComponent();
            SelectedBorder.Visibility = Visibility.Hidden;
            SelectedBorder.BorderBrush=Brushes.Black;
            SelectedBorder.BorderThickness = new Thickness(2);
            MainCanvas.Children.Add(SelectedBorder);
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ShapeType == 0)
                CurrentShape = new Ellipse();
            if (ShapeType == 1)
                CurrentShape = new Rectangle();

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

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if ( e.LeftButton == MouseButtonState.Pressed)
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
                Label lab = new Label();
                lab.Content = "Meow";

                lab.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Size s = lab.DesiredSize;

                Point center = CurrentShape.TransformToAncestor(MainCanvas).Transform(new Point(CurrentShape.ActualWidth / 2, CurrentShape.ActualHeight / 2));
                /*Canvas.SetLeft(lab, Canvas.GetLeft(CurrentShape));
                Canvas.SetTop(lab, Canvas.GetTop(CurrentShape) + (Canvas.GetBottom(CurrentShape)-Canvas.GetTop(CurrentShape))/4.0);*/
                Canvas.SetLeft(lab, center.X-s.Width/2);
                Canvas.SetTop(lab, center.Y-s.Height/2);
                MainCanvas.Children.Add(lab);
                
                //CurrentShape.AddHandler(Shape.MouseDownEvent, new RoutedEventHandler(this.OnShapeClick));
                CurrentShape.MouseDown += new MouseButtonEventHandler(OnShapeClick);
                lab.MouseDown += new MouseButtonEventHandler(OnShapeClick);


                drawing = false;
            }
        }

        private void OnShapeClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Purr");
            CurrentShape = (Shape)e.Source;

            SelectedBorder.Visibility = Visibility.Visible;
            Canvas.SetLeft(SelectedBorder, Canvas.GetLeft(CurrentShape));
            Canvas.SetTop(SelectedBorder, Canvas.GetTop(CurrentShape));
            SelectedBorder.Height = CurrentShape.Height;
            SelectedBorder.Width = CurrentShape.Width;
            SelectedBorder.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShapeType = 0;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShapeType = 1;
        }
    }
}
