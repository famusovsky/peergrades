using System;
using System.IO;

namespace FileManager
{
    class Program
    {
        /// <summary>
        /// Метод, проверяющий доступ к дискам и начинающий работу основной программы.
        /// </summary>
        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.White;
            DirectoryInfo[] directories = null;
            try
            {
                directories = Operations.GetDrivesAsDirectories();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Отсутствует доступ к дискам.");
            }
            if (directories != null)
                Interface(directories);
        }
        /// <summary>
        /// Основной метод программы, вызывающий остальные методы.
        /// </summary>
        /// <param name="directories">Список папок, находящихся в текущей директории.</param>
        static void Interface(DirectoryInfo[] directories)
        {
            var current = 0;
            var buttons = Operations.ButtonsGeneration(directories.Length, ref current);
            FileInfo[] files = null;
            while (true)
            {
                string message = null;
                Output(buttons, directories, files);
                Console.CursorTop = current - 10 < 0 ? 0 : current - 10;
                CheckCommand(ref current, ref buttons, ref directories, ref files, ref message);
                Console.Clear();
                if (message != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            }
        }
        /// <summary>
        /// Метод, выводящий папки/файлы в текущей директории и доступные команды.
        /// </summary>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directories">Папки в текущей директории.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        static void Output(string[] buttons, DirectoryInfo[] directories, FileInfo[] files)
        {
            Console.CursorVisible = false;
            bool isFiles = files != null;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            var commandsList = "W: Вверх, S: Вниз, E: Вернуться к родительской директории\n" +
                               "G: Перейти по адресу к новой директории, Q: Выйти\n";
            if (isFiles)
                commandsList +=
                    "D: Удалить файл, C: Скопировать файл,\n1: Вывод текстового файла, 2: Переместить файл\n" +
                    "3: Конкатенация содержимого с другими файлами, ";
            else
                commandsList += "1: Просмотр папок в выбранной директории, 2: Просмотр файлов в выбранной директории\n"+
                                "3: Просмотр файлов в выбранной папке по заданной маске\n" +
                                "4: Просмотр файлов в выбранной папке и всех её поддиректориях по заданной маске, ";
            commandsList += (isFiles ? "4" : "5") + ": Создать простой текстовый файл";
            Console.WriteLine(commandsList);
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < buttons.Length; i++)
            {
                Console.WriteLine(buttons[i] + "░░" + (isFiles ? files[i].Name : directories[i].Name));
            }
        }
        /// <summary>
        /// Метод, обрабатывающий введённые пользователем команды.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directories">Папки в текущей директории.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        static void CheckCommand(ref int current, ref string[] buttons, ref DirectoryInfo[] directories,
            ref FileInfo[] files, ref string message)
        {
            var input = Console.ReadKey(true).Key.ToString();
            // Так много else if выглядит не очень красиво, но со switch case метод получался значительно длиннее.
            if (input == "W")
                Operations.GoUpOrDown(1, ref current, ref buttons);
            else if (input == "S")
                Operations.GoUpOrDown(2, ref current, ref buttons);
            else if (input == "E")
                Operations.GoBack(ref current, ref buttons, ref directories, ref files);
            else if (input == "G")
                Operations.GoSomewhere(ref current, ref buttons, ref directories, ref files);
            else if (input == "D1" && files == null)
                Operations.GoForwardDirectories(ref current, ref buttons, ref directories, ref message);
            else if (input == "D1")
                Operations.OutputFile(files[current], ref message);
            else if (input == "D2" && files == null)
                Operations.GoForwardFiles(ref current, ref buttons, directories[current], ref files, ref message);
            else if (input == "D2")
                Operations.MoveFile(ref files, ref current, ref buttons, ref message, ref directories);
            else if (input == "D3" && files == null)
                Operations.GoForwardFilesByMask(ref current, ref buttons, directories[current], ref files, ref message);
            else if (input == "D3")
                Operations.Сoncatenation(files[current], ref message);
            else if (input == "D4" && files == null)
                Operations.GoForwardFilesByMaskWithSubs(ref current, ref buttons, directories[current], ref files, ref message);
            else if (input == "D4")
                Operations.CreateFile(ref message, ref buttons, ref current, ref files, files[current].Directory);
            else if (input == "D5" && files == null)
                Operations.CreateFile(ref message, ref buttons, ref current, ref files, directories[current]);
            else if (input == "C" && files != null)
                Operations.CopyFile(files, ref current, ref buttons, ref message);
            else if (input == "D" && files != null)
                Operations.DeleteFile(ref files, ref current, ref buttons, ref message, ref directories);
            else if (input == "Q")
                Operations.Exit();
            else
                message = "Введенная команда отсутствует";
        }
    }
}
