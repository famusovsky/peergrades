using System;
    
namespace Matrix_Calculator
{
    /// <summary>
    /// Класс, описывающий действия с матрицами.
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// Количество строк в матрице.
        /// </summary>
        int LinesCount { get; set; }
        /// <summary>
        /// Количество столбцов в матрице.
        /// </summary>
        int ColumnsCount { get; set; }
        /// <summary>
        /// Двумерный массив, в котором хранятся элементы матрицы.
        /// </summary>
        double[,] Body { get; set; }
        /// <summary>
        /// Конструктор, используемый для объявления новой матрицы.
        /// </summary>
        /// <param name="m">Количество строк в матрице</param>
        /// <param name="n">Количество столбцов в матрице</param>
        /// <param name="random">Указатель на то, нужно ли заполнить матрицу случайными числами</param>
        Matrix(int m, int n, bool random)
        {
            LinesCount = m;
            ColumnsCount = n;
            Body = new double[m, n];
            Random rand = new Random();
            if (random) // Заполнение матрицы случайными вещественными  числами от -10 до 10.
            {
                for (int i = 0; i < LinesCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                        Body[i, j] = rand.Next(-10, 9) + rand.NextDouble();
                }
            }
            else // Заполнение матрицы числами, введёнными пользователем.
            {
                for (int i = 0; i < LinesCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        Console.Write($"The matrix element standing on the line №{i+1} and the column №{j+1} = ");
                        Body[i, j] = Correct("element", $"{i}", $"{j}");
                    }

                    Console.WriteLine();
                }
            }
        }
        /// <summary>
        /// Метод, получающий от пользователя информацию о том, каких размеров матрица и как её нужно заполнять
        /// (При сложении, вычитании или умножении данной матрицы на новую матрицу новая изначально обладает
        /// зафиксированным количеством строк или/и столбцов).
        /// </summary>
        /// <param name="linesCount">Количество строк</param>
        /// <param name="columnsCount">Количество столбцов</param>
        /// <returns></returns>
        public static Matrix Input(int linesCount, int columnsCount)
        {
            Random rand = new Random();
            if (linesCount == 0 || columnsCount == 0) // Проверка на наличие незафиксированной размерности.
            {
                // Флаг, указывающий на то, являются ли размерности матрицы случайными числами.
                bool random = true; 
                Console.WriteLine("Do you want to enter the size values of the new matrix yourself?\n" +
                                  "Otherwise, the it will be random numbers up to 10.\n" +
                                  "1 - Yes; other - No");
                if (Console.ReadLine() == "1")
                    random = false;
                // Ввод кол-ва строк, если оно изначально не задано.
                if (linesCount == 0) 
                {
                    Console.Write("Count of Lines = ");
                    if (random)
                    {
                        linesCount = rand.Next(1, 10);
                        Console.WriteLine(linesCount);
                    }
                    else
                        linesCount = (int) Correct("Lines", "", "");
                }
                // Ввод кол-ва столбцов, если оно изначально не задано.
                if (columnsCount == 0)
                {
                    Console.Write("Count of Columns = ");
                    if (random)
                    {
                        columnsCount = rand.Next(1, 10);
                        Console.WriteLine(columnsCount);
                    }
                    else
                        columnsCount = (int) Correct("Columns", "", "");
                }
            }

            Console.WriteLine("Do you want to fill in the new matrix yourself?\n" +
                              "Otherwise, the matrix will be filled with random numbers from -10 to 10.\n" +
                              "1 - Yes; other - No");
            Matrix newMatrix;
            if (Console.ReadLine() == "1")
                // Объявление новой матрицы, заполненной случайными числами.
                newMatrix = new Matrix(linesCount, columnsCount, false);
            else
                // Объявление новой матрицы, значения элементов которой введёт пользователь.
                newMatrix = new Matrix(linesCount, columnsCount, true);

            Console.WriteLine("New matrix:");
            newMatrix.Output();
            return newMatrix;
        }
        /// <summary>
        /// Метод, проверяющий введённое значение на корректность. 
        /// </summary>
        /// <param name="sizeOrElement">Указатель на то, введено значение элемента матрицы или её размерности</param>
        /// <param name="line">Номер строки, которой принадлежит введённый элемент</param>
        /// <param name="column">Номер столбца, которому принадлежит введённый элемент</param>
        /// <returns></returns>
        static double Correct(string sizeOrElement, string line, string column)
        {
            // Я решил не пропускать значения, по модулю большие 100та для элементов и 10ти для размерностей, чтобы
            // наверняка избежать проблем с выводом матриц.
            if (sizeOrElement == "element")
            {
                if (!double.TryParse(Console.ReadLine(), out double variable) || Math.Abs(variable) > 100)
                {
                    if (line != "")
                        Console.Write("The value of the element must be a number from -100 to 100. " +
                                      "Please re input.\nThe matrix element standing on the line " +
                                      $"№{int.Parse(line) + 1} and the column №{int.Parse(column) + 1} = ");
                    return Correct(sizeOrElement, line, column);
                }
                
                return variable;
            }
            else
            {
                if (!int.TryParse(Console.ReadLine(), out int variable) || variable <= 0 || variable > 10)
                {
                    Console.Write($"Count of {sizeOrElement} must be a positive integer up to 10. " +
                                      $"Please re input.\nCount of {sizeOrElement} = ");
                    return Correct(sizeOrElement, line, column);
                }
                
                return variable;
            }
        }
        /// <summary>
        /// Метод, производящий вывод матрицы.
        /// </summary>
        void Output()
        {
            // Я решил выводить числа, округляя их до 3х знаков после запятой, чтобы на консоли  они не в съезжали
            // в другие столбцы
            Console.WriteLine();
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Console.Write(Math.Round(Body[i, j], 3) + "\t");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
        /// <summary>
        /// Метод, выводящий след заданной матрицы.
        /// </summary>
        public void Trace()
        {
            if (LinesCount != ColumnsCount)
            {
                Console.WriteLine("Only square matrices have a trace");
            }
            else
            {
                double trace = 0;
                for (var i = 0; i < LinesCount; i++)
                    trace += Body[i, i];
                Console.WriteLine($"Trace of the given matrix = {Math.Round(trace, 4)}");
            }
        }
        /// <summary>
        /// Метод, выводящий определитель матрицы.
        /// </summary>
        public void Determinant()
        {
            if (LinesCount != ColumnsCount)
            {
                Console.WriteLine("Only square matrices have a determinant");
            }
            else
            {
                // Для упрощения расчётов отдельно рассматриваются матрицы размеров 1x1, 2x2, 3x3 и более.  
                double determinant;
                if (LinesCount == 1)
                    determinant = Body[0, 0];
                else if (LinesCount == 2)
                    determinant = Body[0, 0] * Body[1, 1] - Body[0, 1] * Body[1, 0];
                else if (LinesCount == 3)
                    determinant = Body[0, 0] * Body[1, 1] * Body[2, 2] + Body[0, 1] * Body[1, 2] * Body[2, 0] +
                                  Body[0, 2] * Body[1, 0] * Body[2, 1] -
                                  Body[0, 2] * Body[1, 1] * Body[2, 0] - Body[0, 1] * Body[1, 0] * Body[2, 2] -
                                  Body[0, 0] * Body[1, 2] * Body[2, 1];
                else
                    determinant = DeterminantFinding(Body, LinesCount);
                Console.WriteLine($"Determinant of the matrix is : {determinant}");
            }
        }
        /// <summary>
        /// Метод, осуществляющий поиск определителя матрицы.
        /// </summary>
        /// <param name="matrixBody">Двумерный массив элементов матрицы</param>
        /// <param name="count">Количество строк/столбцов в матрице</param>
        /// <returns></returns>
        static double DeterminantFinding(double[,] matrixBody, int count)
        {
            double determinant = 1;
            // Приводим заданную матрицу к верхнетреугольнуму виду, при котором определитель равен произведению всех
            // элементов главной диагонали.
            for (int i = 0; i < count-1; i++)
            {
                if (matrixBody[i, i] == 0)
                {
                    double temp;
                    for (int k = 0; k < count; k++)
                    {
                        temp = matrixBody[i, k];
                        matrixBody[i, k] = matrixBody[i + 1, k];
                        matrixBody[i + 1, k] = temp;
                    }

                    determinant *= -1;
                }
                else
                    for (int j = i + 1; j < count; j++)
                    {
                        if (matrixBody[j, i] != 0)
                        {
                            double multiplier = matrixBody[j, i] / matrixBody[i, i];
                            for (int k = 0; k < count; k++)
                                matrixBody[j, k] -= (matrixBody[i, k] * multiplier);
                        }
                    }

                determinant *= matrixBody[i, i];
            }

            determinant *= matrixBody[count - 1, count - 1];
            return (determinant); 
        }
        /// <summary>
        /// Метод, осуществляющий транспонирование заданной матрицы.
        /// </summary>
        public void Transposition()
        {
            Matrix resultMatrix = this;
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    resultMatrix.Body[i, j] = Body[j, i];
            }

            Body = resultMatrix.Body;
            Console.WriteLine("The transposition result:");
            Output();
        }
        /// <summary>
        /// Метод, осуществляющий прибавление к заданной матрице новой матрицы.
        /// </summary>
        public void Addition()
        {
            Matrix newMatrix = Input(LinesCount, ColumnsCount);
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    Body[i, j] += newMatrix.Body[i, j];
            }

            Console.WriteLine("The addition result:");
            Output();
        }
        /// <summary>
        /// Метод, осуществляющий вычитание из заданной матрицы новой матрицы.
        /// </summary>
        public void Subtraction()
        {
            Matrix newMatrix = Input(LinesCount, ColumnsCount);
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    Body[i, j] -= newMatrix.Body[i, j];
            }

            Console.WriteLine("The addition result:");
            Output();
        }
        /// <summary>
        /// Метод, осуществляющий умножение заданной матрицы на новую матрицу.
        /// </summary>
        public void MatrixMultiplication()
        {
            Matrix newMatrix = Input(ColumnsCount, 0);
            Matrix resultMatrix = new Matrix(LinesCount, newMatrix.ColumnsCount, true);
            resultMatrix.Body = new double[resultMatrix.LinesCount,resultMatrix.ColumnsCount];
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < newMatrix.ColumnsCount; j++)
                {
                    for (int k = 0; k < ColumnsCount; k++)
                        resultMatrix.Body[i, j] += Body[i, k] * newMatrix.Body[k, j];
                }
            }

            ColumnsCount = resultMatrix.ColumnsCount;
            Body = resultMatrix.Body;
            Console.WriteLine("The matrix multiplication result:");
            Output();
        }
        /// <summary>
        /// Метод, осуществляющий умножение заданной матрицы на число.
        /// </summary>
        public void NumberMultiplication()
        {
            Console.Write("Enter a number: ");
            double variable = Correct("element", "", "");
            for (int i = 0; i < LinesCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                    Body[i, j] *= variable;
            }

            Console.WriteLine("The multiplication by a number result:");
            Output();
        }
    }
}