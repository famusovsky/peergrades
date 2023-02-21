using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Fractal_Thing
{
    /// <summary>
    /// Точка с параметрами X и Y типа double.
    /// </summary>
    /// <param name="X">Параметр X точки.</param>
    /// <param name="Y">Параметр Y точки.</param>
    public record PointD(double X, double Y);

    /// <summary>
    /// Базовый класс фрактала.
    /// </summary>
    public abstract class Fractal
    {
        /// <summary>
        /// Свойство, определяющее максимальную глубину рекурсии.
        /// </summary>
        public virtual int MaxDepth => 5;

        /// <summary>
        /// Метод, совершающий отрисовку фрактала.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="points">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="additional">Дополнительные данные.</param>
        public virtual void Draw(ref Canvas canvas, PointD[] points, int step, double additional)
        {
        }

        /// <summary>
        /// Метод, создающий объект класса Line.
        /// </summary>
        /// <param name="start">Стартовая точка линии.</param>
        /// <param name="finish">Конечная точка линии.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <returns></returns>
        protected static Line CreateALine(PointD start, PointD finish, int step)
        {
            var newLine = new Line();
            newLine.X1 = start.X;
            newLine.Y1 = start.Y;
            newLine.X2 = finish.X;
            newLine.Y2 = finish.Y;
            newLine.Stroke = new SolidColorBrush(MainWindow.Gradient[step]);
            newLine.StrokeThickness = 2;
            return newLine;
        }
    }

    /// <summary>
    /// Класс линии Коха (наследник фрактала).
    /// </summary>
    public class Koch : Fractal
    {
        /// <summary>
        /// Метод, совершающий отрисовку линии Коха.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="p">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="realStep">Шаг на котором изначально была создана текущая линия.</param>
        public override void Draw(ref Canvas canvas, PointD[] p, int step, double realStep)
        {
            if (MainWindow.Depth - step == 0)
            {
                canvas.Children.Add(CreateALine(p[0], p[1], (int) realStep));
                return;
            }

            var deltaX = (p[1].X - p[0].X) / 3;
            var deltaY = (p[1].Y - p[0].Y) / 3;
            var p3 = new PointD(p[0].X + deltaX, p[0].Y + deltaY);
            var p4 = new PointD(p[1].X - deltaX, p[1].Y - deltaY);
            var p5 = new PointD((p[0].X + p[1].X) / 2 - (p[0].Y - p[1].Y) / 2 / Math.Sqrt(3),
                (p[0].Y + p[1].Y) / 2 - (p[1].X - p[0].X) / 2 / Math.Sqrt(3));
            Draw(ref canvas, new[] {p[0], p3}, step + 1, realStep);
            Draw(ref canvas, new[] {p4, p[1]}, step + 1, realStep);
            Draw(ref canvas, new[] {p3, p5}, step + 1, step + 1);
            Draw(ref canvas, new[] {p5, p4}, step + 1, step + 1);
        }
    }

    /// <summary>
    /// Класс фрактального дерева (наследник фрактала).
    /// </summary>
    public class Tree : Fractal
    {
        /// <summary>
        /// Угол наклона первого отрезка.
        /// </summary>
        public double Angle1 { get; }

        /// <summary>
        /// Угол наклона второго отрезка.
        /// </summary>
        public double Angle2 { get; }

        /// <summary>
        /// Коэффициент отношения длин отрезков на различных итерациях.
        /// </summary>
        public double Relation { get; }

        /// <summary>
        /// Свойство, определяющее максимальную глубину рекурсии.
        /// </summary>
        public override int MaxDepth => 7;

        /// <summary>
        /// Конструктор экземпляра фрактального дерева.
        /// </summary>
        /// <param name="angle1">Угол наклона первого отрезка.</param>
        /// <param name="angle2">Угол наклона второго отрезка.</param>
        /// <param name="relation">Коэффициент отношения длин отрезков на различных итерациях.</param>
        public Tree(double angle1, double angle2, double relation)
        {
            Angle1 = angle1;
            Angle2 = angle2;
            Relation = relation;
        }

        /// <summary>
        /// Метод, совершающий отрисовку фрактального дерева.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="p">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="angle0">Угол наклона отрезка из предыдущей итерации.</param>
        public override void Draw(ref Canvas canvas, PointD[] p, int step, double angle0)
        {
            canvas.Children.Add(CreateALine(p[0], p[1], step));
            if (MainWindow.Depth - step == 0)
            {
                return;
            }

            var delta = Math.Sqrt((p[1].X - p[0].X) * (p[1].X - p[0].X) + (p[1].Y - p[0].Y) * (p[1].Y - p[0].Y)) *
                        Relation;
            var currentAngle1 = angle0 + Angle1;
            var currentAngle2 = angle0 - Angle2;
            var p3 = new PointD(p[1].X + delta * Math.Sin(currentAngle1), p[1].Y - delta * Math.Cos(currentAngle1));
            var p4 = new PointD(p[1].X + delta * Math.Sin(currentAngle2), p[1].Y - delta * Math.Cos(currentAngle2));
            Draw(ref canvas, new[] {p[1], p3}, step + 1, currentAngle1);
            Draw(ref canvas, new[] {p[1], p4}, step + 1, currentAngle2);
        }
    }

    /// <summary>
    /// Класс ковра Серпинского (наследник фрактала).
    /// </summary>
    public class Carpet : Fractal
    {
        /// <summary>
        /// Свойство, определяющее максимальную глубину рекурсии.
        /// </summary>
        public override int MaxDepth => 4;

        /// <summary>
        /// Метод, совершающий отрисовку ковра Серпинского.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="p">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="length">Длина стороны текущего квадрата.</param>
        public override void Draw(ref Canvas canvas, PointD[] p, int step, double length)
        {
            var newLength = length / 3;
            if (step == 0)
            {
                canvas.Children.Add(CreateARectangle(p[0].X, p[0].Y, length,
                    new SolidColorBrush(MainWindow.Gradient[step])));
                Draw(ref canvas, new[] {new PointD(p[0].X, p[0].Y)}, step + 1, length);
            }
            else
            {
                canvas.Children.Add(CreateARectangle(p[0].X + newLength, p[0].Y + newLength, newLength,
                    new SolidColorBrush(MainWindow.Gradient[step])));
                if (MainWindow.Depth - step == 0)
                {
                    return;
                }

                Draw(ref canvas, new[] {new PointD(p[0].X, p[0].Y)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X + newLength, p[0].Y)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X + 2 * newLength, p[0].Y)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X, p[0].Y + newLength)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X + 2 * newLength, p[0].Y + newLength)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X, p[0].Y + 2 * newLength)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X + newLength, p[0].Y + 2 * newLength)}, step + 1, newLength);
                Draw(ref canvas, new[] {new PointD(p[0].X + 2 * newLength, p[0].Y + 2 * newLength)}, step + 1,
                    newLength);
            }
        }

        /// <summary>
        /// Метод, создающий объект класса Rectangle.
        /// </summary>
        /// <param name="x">Параметр X левого верхнего угла квадрата.</param>
        /// <param name="y">Параметр Y левого верхнего угла квадрата.</param>
        /// <param name="length">Длина стороны квадрата.</param>
        /// <param name="brushes">Цвет квадрата.</param>
        /// <returns></returns>
        internal static Rectangle CreateARectangle(double x, double y, double length, Brush brushes)
        {
            var newSquire = new Rectangle();
            newSquire.Height = newSquire.Width = length;
            newSquire.Stroke = newSquire.Fill = brushes;
            Canvas.SetLeft(newSquire, x);
            Canvas.SetTop(newSquire, y);
            return newSquire;
        }
    }

    /// <summary>
    /// Класс треугольника Серпинского (наследник фрактала).
    /// </summary>
    public class Triangle : Fractal
    {
        /// <summary>
        /// Метод, совершающий отрисовку треугольника Серпинского.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="p">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="length">Длина стороны текущего треугольника.</param>
        public override void Draw(ref Canvas canvas, PointD[] p, int step, double length)
        {
            if (step == 0)
            {
                canvas.Children.Add(CreateALine(p[0], new PointD(p[0].X + length, p[0].Y), step));
                canvas.Children.Add(CreateALine(p[0],
                    new PointD(p[0].X + length / 2, p[0].Y - length * Math.Sqrt(3) / 2), step));
                canvas.Children.Add(CreateALine(new PointD(p[0].X + length / 2, p[0].Y - length * Math.Sqrt(3) / 2),
                    new PointD(p[0].X + length, p[0].Y), step));
                Draw(ref canvas, p, step + 1, length);
            }
            else
            {
                canvas.Children.Add(CreateALine(new PointD(p[0].X + length / 4, p[0].Y - length * Math.Sqrt(3) / 4),
                    new PointD(p[0].X + length * 3 / 4, p[0].Y - length * Math.Sqrt(3) / 4), step));
                canvas.Children.Add(CreateALine(new PointD(p[0].X + length / 4, p[0].Y - length * Math.Sqrt(3) / 4),
                    new PointD(p[0].X + length / 2, p[0].Y), step));
                canvas.Children.Add(CreateALine(new PointD(p[0].X + length * 3 / 4, p[0].Y - length * Math.Sqrt(3) / 4),
                    new PointD(p[0].X + length / 2, p[0].Y), step));
                if (MainWindow.Depth - step == 0)
                {
                    return;
                }

                Draw(ref canvas, p, step + 1, length / 2);
                Draw(ref canvas, new[] {new PointD(p[0].X + length / 2, p[0].Y)}, step + 1, length / 2);
                Draw(ref canvas, new[] {new PointD(p[0].X + length / 4, p[0].Y - length * Math.Sqrt(3) / 4)}, step + 1,
                    length / 2);
            }
        }
    }

    /// <summary>
    /// Класс множества Кантора (наследник фрактала).
    /// </summary>
    public class Cantor : Fractal
    {
        /// <summary>
        /// Свойство, определяющее максимальную глубину рекурсии.
        /// </summary>
        public override int MaxDepth => 9;

        /// <summary>
        /// Выбранное расстояние между отрезками, отрисовающимися на разных итерациях.
        /// </summary>
        public double Distance { get; }

        /// <summary>
        /// Конструктор экземпляра множества Кантора.
        /// </summary>
        /// <param name="distance">Выбранное расстояние между отрезками, отрисовающимися на разных итерациях.</param>
        public Cantor(double distance)
        {
            Distance = distance;
        }

        /// <summary>
        /// Метод, совершающий отрисовку множества Кантора.
        /// </summary>
        /// <param name="canvas">Холст, на котором происходит отрисовка фрактала.</param>
        /// <param name="p">Массив точек, необходимых для отрисовки.</param>
        /// <param name="step">Текущий шаг рекурсии.</param>
        /// <param name="length">Длина стороны текущей прямой.</param>
        public override void Draw(ref Canvas canvas, PointD[] p, int step, double length)
        {
            if (MainWindow.Depth - step == 0)
            {
                return;
            }

            canvas.Children.Add(CreateALine(new PointD(p[0].X, p[0].Y + step * Distance * MainWindow.Scale),
                new PointD(p[0].X + length, p[0].Y + step * Distance * MainWindow.Scale), step));
            Draw(ref canvas, new[] {new PointD(p[0].X, p[0].Y + Distance * MainWindow.Scale)}, step + 1, length / 3);
            Draw(ref canvas, new[] {new PointD(p[0].X + length * 2 / 3, p[0].Y + Distance * MainWindow.Scale)},
                step + 1, length / 3);
        }
    }
}