using System;
using System.IO;
using System.Text;
using EKRLib;

namespace LibraryCheck
{
    /// <summary>
    /// Класс программы, совершающей проверку работоспособности библиотеки EKRLib.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Основной метод программы, совершающей проверку работоспособности библиотеки EKRLib.
        /// </summary>
        // В данном методе меньше 40 строк и в его декомпозиции я не вижу смысла.
        private static void Main()
        {
            ExportInFiles(CreateRandomTransport());
            Console.WriteLine("Повторить работу программы? 1 - да, иначе - нет");
            if (Console.ReadLine() == "1")
                Main();
        }

        /// <summary>
        /// Метод, создающий массив типа Transport[] случайного размера, заполненный случайно-сгенерированными объектами Car и MotorBoat.
        /// </summary>
        /// <returns>Массив типа Transport[] случайного размера, заполненный случайно-сгенерированными объектами Car и MotorBoat.</returns>
        private static Transport[] CreateRandomTransport()
        {
            var rand = new Random();
            var vehicles = new Transport[rand.Next(6, 10 + 1)];
            for (var i = 0; i < vehicles.Length; i++)
            {
                try
                {
                    var power = (uint) rand.Next(10, 100 + 1);
                    var model = "";
                    for (var j = 0; j < 5; j++)
                        model += rand.Next(1, 2 + 1) == 1 ? (char) rand.Next(48, 57 + 1) : (char) rand.Next(65, 90 + 1);
                    vehicles[i] = rand.Next(1, 2 + 1) == 1 ? new Car(model, power) : new MotorBoat(model, power);
                    Console.WriteLine(vehicles[i].StartEngine());
                }
                catch (TransportException e)
                {
                    Console.WriteLine(e.Message);
                    i--;
                }
            }

            return vehicles;
        }

        /// <summary>
        /// Метод, записывающий информацию о транспортных средствах из сформированного массива в текстовые файлы "Cars.txt" и "MotorBoats.txt".
        /// </summary>
        /// <param name="vehicles">Массив типа Transport[] случайного размера, заполненный случайно-сгенерированными объектами Car и MotorBoat.</param>
        private static void ExportInFiles(Transport[] vehicles)
        {
            using var carsWriter = new StreamWriter("Cars.txt");
            using var boatsWriter = new StreamWriter("MotorBoats.txt");
            foreach (var vehicle in vehicles)
            {
                switch (vehicle)
                {
                    case Car:
                        carsWriter.WriteLine(vehicle.ToString(), Encoding.Unicode);
                        break;
                    case MotorBoat:
                        boatsWriter.WriteLine(vehicle.ToString(), Encoding.Unicode);
                        break;
                }
            }
        }
    }
}