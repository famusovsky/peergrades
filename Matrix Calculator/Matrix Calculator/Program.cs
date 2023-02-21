using System;

namespace Matrix_Calculator
{
    class Program
    {
        /// <summary>
        /// Метод, продолжающий работу калькулятора до ввода пользователем команды "Exit".
        /// </summary>
        static void Main()
        {
            Matrix matrix = Matrix.Input(0, 0);
            while (true)
            {
                matrix = Interface(matrix);
            }
        }
        /// <summary>
        /// Метод, содержащий доступные пользователю команды.
        /// </summary>
        /// <param name="matrix">Матрица, над которой ведётся работа</param>
        /// <returns></returns>
        static Matrix Interface(Matrix matrix)
        {
            Console.Write("List of commands\n" +
                              "0 - New matrix; 1 - Trace of the matrix; 2 - Determinant of the matrix;\n" +
                              "3 - Add a new matrix; 4 - Subtract a new matrix; 5 - Multiply by a new matrix;\n" +
                              "6 - Multiply by a number; 7 - Matrix transposition; Other - Exit\nInput new command: ");
            string input = Console.ReadLine();
            Console.WriteLine();
            if (input == "0")
                matrix = Matrix.Input(0, 0);
            else if (input == "1")
                matrix.Trace();
            else if (input == "2")
                matrix.Determinant();
            else if (input == "3")
                matrix.Addition();
            else if (input == "4")
                matrix.Subtraction();
            else if (input == "5")
                matrix.MatrixMultiplication();
            else if (input == "6")
                matrix.NumberMultiplication();
            else if (input == "7")
                matrix.Transposition();
            else
                Environment.Exit(0);
            return matrix;
        }
    }
}