using System;
using System.Net.Mime;

namespace bullycows
{
    class Program
    {
        //"Лаунчер" игры, позволяющий пользователю ознакомиться с правилами и начать игру
        static void Main(string[] args)
        {
            Console.WriteLine("Вам предложено сыграть в игру 'Быки и Коровы'" + "\nВы хотите увидеть правила игры?");
            if (Yes_No())
            {
                Console.WriteLine("В начале игры Компьютер задумывает число из N различных цифр." +
                                  "\nКаждый ход вы вводите N-значное число, в ответ на что компьютер показывает количество быков и коров" +
                                  "\n(быки - отгаданные цифры, стоящие на своих местах, коровы - отгаданные цифры, стоящие не на своих местах)" +
                                  "\nВаша задача - отгадать заданное число за наименьшее количество ходов.");
            }

            Console.WriteLine("Вы хотите начать игру? В любой момент вы можете ввести 'exit' для завершения игры.");
            if (Yes_No())
                Game();
            else
                Exit();
            Game();
        }

        //Метод, содержащий непосредственно основной алгоритм игры
        static void Game()
        {
            int N = -1;
            string input = null;
            Console.WriteLine("Введите кол-во цифр в числе (Допустимы значения от 0 до 10)");
            //Получение от пользователя значения N, с его последующей проверкой на корректность
            while (N == -1)
            {
                input = Console.ReadLine();
                if (Correctness(input, 11, 0, 2, 1, "amount")) N = int.Parse(input);
            }

            //Объявление угадываемого числа в виде массива
            int[] number = new int[N];
            //(заполнение массива -1ми, чтобы затем программа могла поставить 0 на случайную позицию внутри числа)
            for (int i = 0; i < N; i++) number[i] = -1;
            //и его заполнение случайными неповторяющимися цифрами
            for (int i = 0; i < N; i++) number[i] = Create(number, i);
            Int64 moves_count = 0;
            bool win = false;
            Console.WriteLine($"\nЗагадано {N}-значное число.");
            //Цикл, продолжащийся пока пользователь не угадает число
            while (!win) 
            {
                moves_count++;
                bool correct = false;
                Console.WriteLine($"\nХод {moves_count}" + $"\nВведите положительное {N}-значное число, состоящее их неповторяющихся цифр.");
                //Ввод пользователем своей догадки, с последующей проверкой её на корректность
                while (!correct) 
                {
                    input = Console.ReadLine();
                    if (Correctness(input, (int) Math.Pow(10, N), (int) Math.Pow(10, N - 1), N, N, "number"))
                        correct = true;
                }

                //Вывод поздравления и предложения начать игру заново в случае победы пользователя
                if (BullsCNT(input, number))
                {
                    Console.WriteLine($"\n:-)\nПоздравляем, вы нашли загаданное число за {moves_count} ходов!\n:-) " +
                                      "\n\nЖелаете начать игру заново?");
                    if (Yes_No())
                        Game();
                    else
                        Exit();
                    Game();
                }
            }
        }

        //Проверка введённого пользователем числа на корректность
        static bool Correctness(string input, Int64 max, int min, int max_size, int min_size, string amount_or_number)
        {
            Int64 int_input;
            //Вызов "Защиты от дурака" с предложением пользователю выйти из игры, в случае ввода им ключевого слова "exit"
            if (input == "exit")
            {
                Exit();
                Console.WriteLine("Пожалуйста, повторите ввод.");
            }
            //Проверка введённого значения на соответствие критериям "Целое число от 0 до 10" для значения N, и "N-значное целое число" для отгадки пользователя
            else if (!Int64.TryParse(input, out int_input) || input[0]=='0' ||
                     (amount_or_number == "amount" && (int_input >= max || int_input < min)) ||
                     (amount_or_number == "number" && (input.Length < min_size || input.Length > max_size)))
                Wrong();
            //Проверка введённой отгадки пользвателя на отсутствие повторяющихся цифр
            else if (amount_or_number == "number")
            {
                bool unique = true;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = i + 1; j < input.Length; j++)
                        if (input[i] == input[j])
                        {
                            unique = false;
                            break;
                        }

                    if (!unique) break;
                }

                if (unique) return true;
                else Wrong();
            }
            else
                return true;

            return false;
        }

        //Метод, заполняющий элементы массива различными случайными значениями
        static int Create(int[] number, int i)
        {
            bool unique = false;
            int value = 0;
            Random rand = new Random();
            //Цикл находящий случайную цифру от 1 до 10, ещё не появившуюся в массиве
            while (!unique)
            {
                //т.к. число не может начинаться с нуля, то нулевому элементу массива присваивается значение от 1 до 10
                if (i == 0) value = rand.Next(1, 10);
                else value = rand.Next(0, 10);
                for (int j = 0; j < number.Length; j++)
                {
                    if (value != number[j])
                    {
                        unique = true;
                    }
                    else
                    {
                        unique = false;
                        break;
                    }
                }
            }

            return value;
        }

        //Метод, подсчитывающий кол-во быков и коров, и возвращающий true в случае победы пользователя
        static bool BullsCNT(string input, int[] number)
        {
            int bulls = 0, cows = 0;
            for (int i = 0; i < input.Length; i++)
            {
                //Подсчёт соответствий в искомом числе для каждого символа введённого пользователем значения
                int digit = input[i] - '0';
                for (int j = 0; j < input.Length; j++)
                    if (digit == number[j])
                    {
                        if (i == j)
                            bulls++;
                        else
                            cows++;
                    }
            }

            Console.WriteLine($"{bulls} быков и {cows} коров");
            if (bulls == input.Length) return true;
            else return false;
        }

        //"Защита от дурака", вызываемая в случае попытки пользователя выйти из игры
        static void Exit()
        {
            Console.WriteLine("Вы точно хотите выйти?");
            if (Yes_No())
            {
                Console.WriteLine("До свидания.");
                System.Environment.Exit(0);
            }
        }

        //Метод для получения от пользователя ответа в формате "да или нет"
        static bool Yes_No()
        {
            Console.WriteLine("1-Да\nОстальные символы-Нет");
            if (Console.ReadLine() == "1")
                return true;
            else
                return false;
        }

        //Просто вывод фразы о недопустимом введённом значении, сделан чтобы дважды не прописывать один и тот же код
        static void Wrong()
        {
            Console.WriteLine("Введено недопустимое значение." +
                              "\nПожалуйста, повторите ввод, либо введите 'exit' для завершения игры");
        }
    }
}