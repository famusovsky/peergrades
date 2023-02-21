using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Application = System.Windows.Application;
using MenuItem = System.Windows.Controls.MenuItem;
using MessageBox = System.Windows.MessageBox;

namespace Fractal_Thing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml (класс главной формы).
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Свойство, определяющее глубину рекурсии.
        /// </summary>
        public static int Depth { get; private set; } = 2;

        /// <summary>
        /// Свойство, определяющее список цветов, использующихся при отрисовке на различных итерациях.
        /// </summary>
        public static List<Color> Gradient { get; private set; }

        /// <summary>
        /// Свойство, хранящее массиы доступных фракталов.
        /// </summary>
        public static Fractal[] Fractals { get; private set; }

        /// <summary>
        /// Свойство, определяющее коэффициент изменения размера фрактала относительно стандартного.
        /// </summary>
        public static double Scale { get; private set; } = 1;

        /// <summary>
        /// Констркутор главной формы.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Gradient = new List<Color>(new[] {Colors.Red, Colors.Orange, Colors.Yellow});
            Fractals = new Fractal[]
            {
                new Tree(FirstAngleValue.Value * Math.PI / 180, SecondAngleValue.Value * Math.PI / 180,
                    RelationValue.Value),
                new Koch(), new Carpet(), new Triangle(),
                new Cantor(DistanceValue.Value)
            };
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Выйти".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void QuitClick(object sender, EventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Выйти из приложения?", "", MessageBoxButton.YesNo))
                {
                    case MessageBoxResult.Yes:
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик события изменения типа фрактала.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void FractalTypeChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender == null)
                    return;
                Tree.IsChecked = Koch.IsChecked = Triangle.IsChecked = Carpet.IsChecked = Cantor.IsChecked = false;
                (sender as MenuItem).IsChecked = true;
                var num = int.Parse(((MenuItem) sender).Tag.ToString());
                if (Depth > Fractals[num].MaxDepth)
                    Depth = Fractals[num].MaxDepth;
                DepthValue.Maximum = Fractals[num].MaxDepth;
                DrawFractal(num);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Метод, отрисовывающий выбранный фрактал.
        /// </summary>
        /// <param name="num">Номер выбранного фрактала.</param>
        private void DrawFractal(int num)
        {
            try
            {
                DrawnFractal.Children.Clear();
                switch (num)
                {
                    case 0:
                        Fractals[num].Draw(ref DrawnFractal,
                            new[] {new PointD(250, 300), new PointD(250, 300 - 150 * Scale)}, 0, 0);
                        break;
                    case 1:
                        Fractals[num].Draw(ref DrawnFractal,
                            new[] {new PointD(50, 200), new PointD(50 + 400 * Scale, 200)}, 0, 0);
                        break;
                    case 2:
                        Fractals[num].Draw(ref DrawnFractal, new[] {new PointD(100, 40)}, 0, 300 * Scale);
                        break;
                    case 3:
                        Fractals[num].Draw(ref DrawnFractal, new[] {new PointD(100, 300)}, 0, 300 * Scale);
                        break;
                    case 4:
                        Fractals[num].Draw(ref DrawnFractal, new[] {new PointD(100, 40)}, 0, 300 * Scale);
                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик события изменения глубины.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void DepthChanged(object sender, EventArgs e)
        {
            try
            {
                DepthValue.Value = Depth = (int) DepthValue.Value;
                GradientRemake(Gradient[0], Gradient[^1]);
                Redraw();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик события изменения элементов фрактального дерева (коэффициента отношения длин отрезков и размеров углов наклона).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AngleOrRelationChanged(object sender, EventArgs e)
        {
            try
            {
                Fractals[0] = new Tree(FirstAngleValue.Value * Math.PI / 180, SecondAngleValue.Value * Math.PI / 180,
                    RelationValue.Value);
                DrawFractal(0);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик изменения значения дистанции между отрезками, принадлежащими различным итерациям множества Кантора.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void DistanceValueChanged(object sender, EventArgs e)
        {
            try
            {
                Fractals[4] = new Cantor(DistanceValue.Value);
                DrawFractal(4);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик события нажатия на клавишу.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void KeyPressing(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Down:
                        if (Canvas.GetTop(DrawnFractal) <=
                            ((Panel) Application.Current.MainWindow.Content).ActualHeight)
                            Canvas.SetTop(DrawnFractal, Canvas.GetTop(DrawnFractal) + 20);
                        break;
                    case Key.Up:
                        if (Canvas.GetTop(DrawnFractal) >= -300 * Scale)
                            Canvas.SetTop(DrawnFractal, Canvas.GetTop(DrawnFractal) - 20);
                        break;
                    case Key.Left:
                        if (Canvas.GetLeft(DrawnFractal) >= -300 * Scale)
                            Canvas.SetLeft(DrawnFractal, Canvas.GetLeft(DrawnFractal) - 20);
                        break;
                    case Key.Right:
                        if (Canvas.GetLeft(DrawnFractal) <=
                            ((Panel) Application.Current.MainWindow.Content).ActualWidth)
                            Canvas.SetLeft(DrawnFractal, Canvas.GetLeft(DrawnFractal) + 20);
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Обработчик события изменения коэффициента изменения размера фрактала относительно стандартного
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void ScaleChange(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (Scale + e.Delta * 0.001 <= 5 && Scale + e.Delta * 0.001 >= 0.1)
                {
                    Scale += e.Delta * 0.001;
                    Redraw();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Стартовый цвет" или "Конечный цвет".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void ColorChange(object sender, EventArgs e)
        {
            try
            {
                var colorDialog = new System.Windows.Forms.ColorDialog();
                if (colorDialog.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
                {
                    Color inputColor = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G,
                        colorDialog.Color.B);
                    var newStartColor = Gradient[0];
                    var newEndColor = Gradient[^1];
                    if (((MenuItem) sender).Name == "StartColor")
                        newStartColor = inputColor;
                    else
                        newEndColor = inputColor;
                    GradientRemake(newStartColor, newEndColor);
                    Redraw();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Метод, создающий новый список цветов, использующихся при отрисовке на различных итерациях.
        /// </summary>
        /// <param name="newStartColor">Новый стартовый цвет.</param>
        /// <param name="newEndColor">Новый конечный цвет.</param>
        private void GradientRemake(Color newStartColor, Color newEndColor)
        {
            try
            {
                Gradient.Clear();
                var deltaColor = Color.Multiply(Color.Subtract(newEndColor, newStartColor), 1f / Depth);
                for (int i = 0; i <= Depth; i++)
                {
                    Gradient.Add(Color.Add(newStartColor, Color.Multiply(deltaColor, i)));
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик события выбора множества Кантора.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void CantorChecked(object sender, RoutedEventArgs e)
        {
            Distance.IsEnabled = true;
            Distance.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Обработчик события выбора фрактального дерева.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void TreeChecked(object sender, RoutedEventArgs e)
        {
            Relation.IsEnabled = FirstAngle.IsEnabled = SecondAngle.IsEnabled = true;
            Relation.Visibility = FirstAngle.Visibility = SecondAngle.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Обработчик события снятия выбора с множества Кантора и фрактального дерева.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void Unchecked(object sender, RoutedEventArgs e)
        {
            Relation.IsEnabled = FirstAngle.IsEnabled = SecondAngle.IsEnabled = Distance.IsEnabled = false;
            Relation.Visibility = FirstAngle.Visibility =
                SecondAngle.Visibility = Distance.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Обработчик события изменения размера окна.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void SizeChange(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (e.PreviousSize.Height != 0 && e.PreviousSize.Width != 0)
                {
                    Canvas.SetTop(DrawnFractal,
                        Canvas.GetTop(DrawnFractal) + (e.NewSize.Height - e.PreviousSize.Height) / 2);
                    Canvas.SetLeft(DrawnFractal,
                        Canvas.GetLeft(DrawnFractal) + (e.NewSize.Width - e.PreviousSize.Width) / 2);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Сохранить как".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void SaveAsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var savingDiolog = new System.Windows.Forms.SaveFileDialog();
                savingDiolog.DefaultExt = ".PNG";
                savingDiolog.Filter = "Картинка (.PNG)|*.PNG";
                if (savingDiolog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using var saver = File.Create(savingDiolog.FileName);
                    var dpi = 300;
                    var scaling = dpi / 96d;
                    var image = new RenderTargetBitmap((int) (MainCanvas.ActualWidth * scaling),
                        (int) (MainCanvas.ActualWidth * scaling), dpi, dpi, PixelFormats.Pbgra32);
                    var pngBitmap = new PngBitmapEncoder();
                    image.Render(DrawnFractal);
                    pngBitmap.Frames.Add(BitmapFrame.Create(image));
                    pngBitmap.Save(saver);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Метод, перерисовывающий выбранный фрактал.
        /// </summary>
        private void Redraw()
        {
            try
            {
                foreach (var item in Fractal.Items)
                {
                    if ((item as MenuItem).IsChecked)
                    {
                        var num = int.Parse((item as MenuItem).Tag.ToString());
                        DrawFractal(num);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Справка".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void HelpClick(object sender, RoutedEventArgs e)
        {
            var str = "Перемещение изображения делается с помощью стрелочек на клавиатуре.\n" +
                      "Изменение масштаба делается с помощью скроллинга колёсика на мышке\n" +
                      "(и то, и другое - при выбранном окне программы, конечно).";
            MessageBox.Show(str);
        }
    }
}