using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FileManager
{
    /// <summary>
    /// Класс, содержащий действия, выполняемые программой.
    /// </summary>
    public class Operations
    {
        /// <summary>
        /// Класс, возвращающий список корневых папок доступных дисков.
        /// </summary>
        /// <returns>Список корневых папок доступных дисков.</returns>
        public static DirectoryInfo[] GetDrivesAsDirectories()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            DirectoryInfo[] directories = new DirectoryInfo[drives.Length];
            for (int i = 0; i < drives.Length; i++)
                directories[i] = drives[i].RootDirectory;
            return directories;
        }
        /// <summary>
        /// Метод, передвигающий отметку выбранного файла/папки вверх или вниз. 
        /// </summary>
        /// <param name="upOrDown">Указатель на то, какую команду ввёл пользователь (вверх/вниз).</param>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        public static void GoUpOrDown(int upOrDown, ref int current, ref string[] buttons)
        {
            buttons[current] = "▒▒";
            if (upOrDown == 1)
                current = current == 0 ? buttons.Length - 1 : current - 1;
            else
                current = current == buttons.Length - 1 ? 0 : current + 1;
            buttons[current] = "▓▓";
        }
        /// <summary>
        /// Метод, совершающий переход к родительской директории текущей папки/файла.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directories">Папки в текущей директории.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        public static void GoBack(ref int current, ref string[] buttons, ref DirectoryInfo[] directories, 
            ref FileInfo[] files)
        {
            bool isFiles = files != null;
            DirectoryInfo childDirectory;
            if (isFiles)
                childDirectory = files[current].Directory;
            else
                childDirectory = directories[current];
            try
            {
                directories = isFiles
                    ? childDirectory.Parent.GetDirectories()
                    : childDirectory.Parent.Parent.GetDirectories();
            }
            catch (Exception)
            {
                // Так как мы возвращаемся из папки в которой уже находимся в её родительскую директории, то она 
                // существует и к ней есть доступ, т.е. ошибка может возникнуть только если мы находимся в корневой 
                // папке диска.
                directories = GetDrivesAsDirectories();
            }

            files = null;
            buttons = ButtonsGeneration(directories.Length, ref current);
        }
        /// <summary>
        /// Метод, переходящий к списку папок в выбранной директории.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directories">Папки в текущей директории.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void GoForwardDirectories(ref int current, ref string[] buttons, ref DirectoryInfo[] directories,
            ref string message)
        {
            try
            {
                if (directories[current].GetDirectories().Length == 0)
                {
                    message = "В данной директории отсутствуют папки";
                }
                else
                {
                    directories = directories[current].GetDirectories();
                    buttons = ButtonsGeneration(directories.Length, ref current);
                }
            }
            // Так как пользователь может выбирать только среди членов возвращённого Directory.GetDirectories() массива,
            // то выбранная папка существует, и единственная возможная ошибка - это отсутствие доступа.
            catch (Exception)
            {
                message = "Отсутствует доступ к папке";
            }
        }
        /// <summary>
        /// Метод, совершающий переход к набору файлов в выбранной директории.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directory">Выбранная директориия.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void GoForwardFiles(ref int current, ref string[] buttons, DirectoryInfo directory, 
            ref FileInfo[] files, ref string message)
        {
            try
            {
                if (directory.GetFiles().Length != 0)
                {
                    files = directory.GetFiles();
                    buttons = ButtonsGeneration(files.Length, ref current);
                }
                else
                {
                    message = "В папке отсутствуют файлы";
                }
            }
            // Так как пользователь может выбирать только среди членов возвращённого Directory.GetDirectories() массива,
            // то выбранная папка существует, и единственная возможная ошибка - это отсутствие доступа.
            catch (Exception)
            {
                message = "Отсутствует доступ к папке";
            }
        }
        /// <summary>
        /// Метод, совершающий переход к набору файлов, принадлежащих выбранной папке и соответствующих выбранной маске.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directory">Выбранная директориия.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void GoForwardFilesByMask(ref int current, ref string[] buttons, DirectoryInfo directory,
            ref FileInfo[] files, ref string message)
        {
            try
            {
                if (directory.GetFiles().Length != 0)
                {
                    Console.Clear();
                    Console.WriteLine("Будет выведен набор файлов, принадлежащих выбранной папке и соответствующих " +
                                      "введённой маске.\nМаска является регулярным выражением.\n" +
                                      "Т.е., например, при вводе (.docx?)$ будут выведены все файлы формата Word " +
                                      "(.doc или .docx) из выбранной папки.");
                    var regex = RegexInput();
                    files = FindMatches(directory, regex);
                    if (files.Length == 0)
                    {
                        files = null;
                        message = "В папке отсутствуют файлы, соответствующие маске.";
                    }
                    else
                    {
                        buttons = ButtonsGeneration(files.Length, ref current);
                    }
                }
                else
                {
                    message = "В папке отсутствуют файлы";
                }
            }
            catch (Exception)
            {
                message = "Отсутствует доступ к папке";
            }
        }
        /// <summary>
        /// Метод, совершающий переход к набору файлов, принадлежащих выбранной папке и всем её поддиректориям
        /// и соответствующих выбранной маске.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directory">Выбранная директориия.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void GoForwardFilesByMaskWithSubs(ref int current, ref string[] buttons, DirectoryInfo directory,
            ref FileInfo[] files, ref string message)
        {
            try
            {
                Console.Clear();
                Console.WriteLine(
                    "Будет выведен набор файлов, принадлежащих выбранной папке и всем её поддиректориям " +
                    "и соответствующих введённой маске.\nМаска является регулярным выражением.\n" +
                    "Т.е., например, при вводе (.docx?)$ будут выведены все файлы формата Word " +
                    "(.doc или .docx) из выбранной папки.");
                var regex = RegexInput();
                var newFiles = FindMatches(directory, regex);
                foreach (var subdirecory in directory.GetDirectories())
                {
                    var addFiles = FindMatches(subdirecory, regex);
                    if (addFiles.Length != 0)
                    {
                        var oldLength = newFiles.Length;
                        Array.Resize(ref newFiles, oldLength + addFiles.Length);
                        for (int i = 0; i < addFiles.Length; i++)
                            newFiles[oldLength + i] = addFiles[i];
                    }
                }

                files = newFiles.Length == 0 ? null : newFiles;
                if (files == null)
                    message = "В папке и всех её поддиректориях отсутствуют файлы, соответствующие маске.";
                else
                    buttons = ButtonsGeneration(files.Length, ref current);
            }
            catch (Exception)
            {
                message = "Отсутствует доступ к папке";
            }
        }
        /// <summary>
        /// Метод позволяющий пользователю ввести регулярное выражение.
        /// </summary>
        /// <returns>Регулярное выражение</returns>
        static Regex RegexInput()
        {
            Regex regex = null;
            Console.CursorVisible = true;
            Console.WriteLine("\nВведите маску (регулярное выражение.)");
            while (regex == null)
            {
                try
                {
                    regex = new Regex(@$"{Console.ReadLine()}");
                }
                catch
                {
                    Console.WriteLine("Введено некорректное выражение. Пожалуйста, повторите ввод.");
                    regex = null;
                }
            }
            
            Console.CursorVisible = false;
            return regex;
        }
        /// <summary>
        /// Метод, совершающий поиск соответствий регулярному выражению
        /// среди названий файлов, принадлежащих данной папке.
        /// </summary>
        /// <param name="directory">Папка, внутри которой производится поиск.</param>
        /// <param name="regex">Заданное регулярное выражение.</param>
        /// <returns>Массив файлов, назавания которых соответствуют регулярному выражению.</returns>
        static FileInfo[] FindMatches(DirectoryInfo directory, Regex regex)
        {
            var count = 0;
            foreach (var file in directory.GetFiles())
            {
                if (regex.IsMatch(file.Name))
                    count++;
            }
            FileInfo[] files = new FileInfo[count];
            if (count != 0)
            {
                var num = 0;
                foreach (var file in directory.GetFiles())
                {
                    if (regex.IsMatch(file.Name))
                    {
                        files[num] = file;
                        num++;
                    }
                }
            }

            return files;
        }
        /// <summary>
        /// Метод, совершающий переход к папкам/файлам директории, адрес которой вводит пользователь.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="directories">Папки в текущей директории.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        public static void GoSomewhere(ref int current, ref string[] buttons, ref DirectoryInfo[] directories,
            ref FileInfo[] files)
        {
            // В данном методе не могут возникнуть ошибки, так как их наличие проверяется в методе CheckPath, 
            // во время ввода пользователем адреса папки.
            Console.Clear();
            Console.WriteLine("Введите полный путь к директории.");
            var directory = new DirectoryInfo(CheckPath(true));
            while (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0)
            {
                Console.WriteLine("В данной папке отсутствуют папки и файлы.\nПожалуйста, повторите ввод");
                directory = new DirectoryInfo(CheckPath(true));
            }
            if (directory.GetFiles().Length != 0 && directory.GetDirectories().Length != 0)
                Console.WriteLine("Если вы хотите перейти к файлам в данной директории нажмите 1, " +
                                  "если вы хотите перейти к папкам нажмите 2.\nИначе нажмите любую другую клавишу.");
            else
                Console.WriteLine("В данной папке присутствуют только " +
                                  (directory.GetFiles().Length != 0 ? "файлы" : "папки") +
                                  ".\nЕсли вы хотите к ним перейти, нажмите 1, иначе нажмите любую другую клавишу.");
            var input = Console.ReadKey(true).Key.ToString();
            if (input == "D1" && directory.GetFiles().Length != 0)
            {
                files = directory.GetFiles();
                buttons = ButtonsGeneration(directory.GetFiles().Length, ref current);
            }
            else if ((input == "D1" && directory.GetDirectories().Length != 0) ||
                     (input == "D2" && directory.GetFiles().Length != 0 && directory.GetDirectories().Length != 0))
            {
                files = null;
                directories = directory.GetDirectories();
                buttons = ButtonsGeneration(directory.GetDirectories().Length, ref current);
            }
        }
        /// <summary>
        /// Метод, генерирующий отметки, позволяющие пользователю выбрать папку/файл для работы с ним, для текущей
        /// директории.
        /// </summary>
        /// <param name="count">Количество файлов/папок в текущей директории.</param>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <returns>Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</returns>
        public static string[] ButtonsGeneration(int count, ref int current)
        {
            var buttons = new string[count];
            if (count != 0)
            {
                buttons[0] = "▓▓";
                for (int i = 1; i < count; i++)
                    buttons[i] = "▒▒";
            }

            current = 0;
            return buttons;
        }
        /// <summary>
        /// Метод, создающий файл в выбранной пользователем директории.
        /// </summary>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="directory">Директория, в которой будет создан файл.</param>
        public static void CreateFile(ref string message, ref string[] buttons, ref int current, ref FileInfo[] files, 
            DirectoryInfo directory)
        {
            try
            {
                // Проверка на наличие доступа к директории.
                directory.GetFiles();
                Console.Clear();
                Console.WriteLine($"Будет создан файл папке \"{directory.Name}\"");
                Console.WriteLine("Введите имя файла. " +
                                  "Если файл с введённым именем уже существует, то он будет перезаписан");
                Console.CursorVisible = true;
                var name = Console.ReadLine();
                Console.CursorVisible = false;
                var path = $"{directory.FullName}/{name}.txt";
                var encoding = EncodingChoice();
                using (StreamWriter sw = new StreamWriter(path, false, encoding))
                {
                    Console.WriteLine("Если вы хотите заполнить файл, нажмите 1. Иначе нажмите любую другую клавишу");
                    var text = "";
                    if (Console.ReadKey(true).Key.ToString() == "D1")
                        text = WriteText();

                    sw.WriteLine(text);
                }
                FilesListUpdate(ref buttons, ref files, ref current);
            }
            // Так как пользователь может попытаться создать файл только в папке, которая является одним из членов
            // возвращённого Directory.GetDirectories() массива, или в которой пользователь уже находится,
            // то выбранная папка очевидно существует, и единственная возможная ошибка - это отсутствие доступа к ней.
            catch (Exception)
            {
                message = "Отсутствует доступ к директории";
            }
        }
        /// <summary>
        /// Метод, удаляющий выбранный пользователем файл.
        /// </summary>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void DeleteFile(ref FileInfo[] files, ref int current, ref string[] buttons, ref string message,
        ref DirectoryInfo[] directories)
        {
            Console.Clear();
            var file = files[current];
            Console.WriteLine($"Вы точно хотите удалить файл {file.Name}?\n" +
                              "Если да, нажмите 1, иначе нажмите любую иную клавишу");
            try
            {
                if (Console.ReadKey(true).Key.ToString() == "D1")
                {
                    file.Delete();
                    if (files.Length == 1)
                    {
                        GoBack(ref current, ref buttons, ref directories, ref files);
                        message = "Был удалён последний файл в папке.";
                    }
                    else
                    {
                        FilesListUpdate(ref buttons, ref files, ref current); 
                    }
                }
            }
            // Так как пользователь может попытаться удалить только файлы, принадлежащие возвращённому
            // Directory.GetFiles() массиву, то они очевидно существует,
            // и единственная возможная ошибка - это отсутствие возможности их удалить.
            catch
            {
                message = "Данный файл невозможно удалить";
            }
        }
        /// <summary>
        /// Метод, копирующий выбранный пользователем файл.
        /// </summary>
        /// <param name="files">Файлы в текущей директории.</param>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void CopyFile(FileInfo[] files, ref int current, ref string[] buttons, ref string message)
        {
            Console.Clear();
            var file = files[current];
            Console.WriteLine($"Введите полное имя директории, в которую будет скопирован файл {file.Name}. " +
                              $"\nЕсли файл с именем " +
                              $"{Path.GetFileNameWithoutExtension(file.FullName)}_copy{Path.GetExtension(file.FullName)}"
                              + " в ней уже существует, то он будет перезаписан.");
            var path = CheckPath(true);
            var copyName = $"{Path.GetFileNameWithoutExtension(file.FullName)}_copy{Path.GetExtension(file.FullName)}";
            try
            {
                file.CopyTo($"{path}/{copyName}", true);
                FilesListUpdate(ref buttons, ref files, ref current);
            }
            catch (Exception)
            {
                message = "Копирование не удаётся.";
            }
        }
        /// <summary>
        /// Метод, перемещяющий выбранный пользователем файл.
        /// </summary>
        /// <param name="files"></param>
        /// <param name="current"></param>
        /// <param name="buttons"></param>
        /// <param name="message"></param>
        public static void MoveFile(ref FileInfo[] files, ref int current, ref string[] buttons, ref string message, 
            ref DirectoryInfo[] directories)
        {
            Console.Clear();
            var file = files[current];
            Console.WriteLine($"Введите полное имя директории, в которую будет перемещён файл {file.Name}. " +
                              $"\nЕсли файл с именем {file.Name} в ней уже существует, то он будет перезаписан.");
            var path = CheckPath(true);
            try
            {
                file.MoveTo($"{path}/{file.Name}", true);
                if (files.Length == 1)
                {
                    GoBack(ref current, ref buttons, ref directories, ref files);
                    message = "Был перемещён последний файл в папке.";
                }
                else
                {
                    FilesListUpdate(ref buttons, ref files, ref current);
                }
            }
            catch (Exception)
            {
                message = "Переместить файл не удаётся.";
            }
        }
        /// <summary>
        /// Метод, выполняющий конкатенацию содержимого выбранного пользователем файла и ещё одного или более файлов.
        /// </summary>
        /// <param name="file">Выбранный пользователем файл.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void Сoncatenation(FileInfo file, ref string message)
        {
            try
            {
                var text = File.ReadAllText(file.FullName);
                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите полный путь файлу, текст которого будет добавлен к содержимому " +
                                      $"файла {file.Name}");
                    var path = CheckPath(false);
                    text += '\n' + File.ReadAllText(path);
                    Console.WriteLine("Если вы хотите добавить текст из ещё одного файла, нажмите 1, " +
                                      "иначе нажмите любую другую клавишу.");
                } 
                while (Console.ReadKey(true).Key.ToString() == "D1");
                
                Console.Clear();
                Console.WriteLine($"Получен текст:\n{text}\n" +
                                  $"Нажмите 1, если вы хотите, чтобы файл {file.Name} был перезаписан с этим текстом, "+
                                  "иначе нажмите любую другую клавишу.");
                Console.SetCursorPosition(0, 0);
                if (Console.ReadKey().Key.ToString() == "D1")
                    File.WriteAllText(file.FullName, text);
            }
            // Так как пользователь может использовать данную комманду только по отношению к файлу,
            // принадлежащему к возвращённому Directory.GetFiles() массиву,
            // а файлы, адреса которых вводит пользователь, проверяются в CheckPath,
            // то единственная возможная ошибка - это отсутствие доступа.
            catch
            {
                message = "К данному файлу отсутствует доступ.";
            }
        }
        /// <summary>
        /// Метод, выполняющий вывод содержимого выбранного файла.
        /// </summary>
        /// <param name="file">Выбранный пользователем файл.</param>
        /// <param name="message">Сообщение, выводимое пользователю в случае ошибки.</param>
        public static void OutputFile(FileInfo file, ref string message)
        {
            try
            {
                // Проверка на наличие доступа.
                var test = File.ReadAllText(file.FullName);
                Console.Clear();
                var encoding = EncodingChoice();
                Console.WriteLine("Чтобы вернуться к списку файлов нажмите клавишу 'E'\n\n" +
                                  File.ReadAllText(file.FullName, encoding));
                Console.SetCursorPosition(0, 0);
                while (Console.ReadKey(true).Key.ToString() != "E")
                {
                }
            }
            // Так как пользователь может использовать данную комманду только по отношению к файлу,
            // принадлежащему к возвращённому Directory.GetFiles() массиву,
            // то единственная возможная ошибка - это отсутствие доступа.
            catch (Exception)
            {
                message = "Не удаётся получить содержимое файла.";
            }
        }
        /// <summary>
        /// Метод, позволяющий пользователю выбрать кодировку для создания файла, ввода или вывода его содержимого.
        /// </summary>
        /// <returns>Выбранная пользователем кодировка.</returns>
        static Encoding EncodingChoice()
        {
            Encoding encoding = Encoding.UTF8;
            Console.WriteLine("По умолчанию используется кодировка UTF-8.\n" +
                              "Если вы хотите выбрать иную кодировку, нажмите 1, " +
                              "иначе нажмите любую другую клавишу.");
            if (Console.ReadKey(true).Key.ToString() == "D1")
            {
                Console.WriteLine("Выберите кодировку: 1 - UTF-32, 2 - UTF-7, 3 - ASCII." +
                                  "\nИные клавиши - кодировка по умолчанию (UTF-8)");
                string key = Console.ReadKey(true).Key.ToString();
                encoding = key == "D1" ? Encoding.UTF32
                    : key == "D2" ? Encoding.UTF7
                    : key == "D3" ? Encoding.ASCII : encoding;
            }

            return encoding;
        }
        /// <summary>
        /// Метод, позволяющий пользователю ввести текст, для заполнения им созданного файла.
        /// </summary>
        /// <returns>Введённый пользователем текст.</returns>
        static string WriteText()
        {
            Console.WriteLine("Чтобы закончить ввод текста дважды нажмите клавишу Enter");
            Console.CursorVisible = true;
            var input = Console.ReadLine();
            var text = input;
            input = Console.ReadLine();
            while (input != "")
            {
                text += '\n' + input;
                input = Console.ReadLine();
            }

            Console.CursorVisible = false;
            return text;
        }
        /// <summary>
        /// Метод, позволяющий пользователю ввести путь к папке/файлу.
        /// </summary>
        /// <param name="isDirectory">Флаг, указаывающий на то, что пользователь вводит путь к директории.</param>
        /// <returns>Введённый пользователем путь.</returns>
        static string CheckPath(bool isDirectory)
        {
            Console.CursorVisible = true;
            string path = null;
            while (path == null)
            {
                path = Console.ReadLine();
                if (!(isDirectory ? Directory.Exists(path) : File.Exists(path)))
                {
                    Console.WriteLine((isDirectory ? "Директории" : "Файла") +
                        " с таким именем не существует. Пожалуйста, повторите ввод");
                    path = null;
                }
                else
                {
                    // Проверка на возможность работы с файлом/папкой по введённому адресу.
                    try
                    {
                        if (isDirectory)
                        {
                            Directory.GetFiles(path);
                            Directory.GetDirectories(path);
                        }
                        else
                        {
                            var test = File.ReadAllText(path); 
                        }
                    }
                    catch
                    {
                        Console.WriteLine("К данно" + (isDirectory ? "й директории" : "му файлу") +
                                          " отсутствует доступ.");
                        path = null;
                    }
                }
            }

            Console.CursorVisible = false;
            return path;
        }
        /// <summary>
        /// Метод, обновляющий список файлов в текущей директории.
        /// </summary>
        /// <param name="buttons">Отметки, позволяющие пользователю выбрать папку/файл для работы с ним.</param>
        /// <param name="files">Список файлов в текущей директории.</param>
        /// <param name="current">Порядковый номер выбранной пользователем папки/файла.</param>
        public static void FilesListUpdate(ref string[] buttons, ref FileInfo[] files, ref int current)
        {
            if (files != null && files.Length != files[0].Directory.GetFiles().Length)
            {
                files = files[0].Directory.GetFiles();
                buttons = ButtonsGeneration(files.Length, ref current);
            }
        }
        /// <summary>
        /// Метод, завершающий работу программы.
        /// </summary>
        public static void Exit()
        {
            Console.Clear();
            Console.WriteLine("До свидания!");
            Environment.Exit(0);
        }
    }
}