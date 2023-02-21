using System.Text.RegularExpressions;

namespace EKRLib
{
    /// <summary>
    /// Класс, описывающий абстрактное транспортное средство.
    /// </summary>
    public abstract class Transport
    {
        /// <summary>
        /// Поле свойства Model.
        /// </summary>
        private string model;

        /// <summary>
        /// Поле свойства Power.
        /// </summary>
        private uint power;

        /// <summary>
        /// Строковое свойство, описывающее модель транспорта и состоящее только из заглавных латинских символов и цифр и имеющее длину ровно 5 символов.
        /// </summary>
        /// <exception cref="TransportException">Исключение типа TransportException.</exception>
        public string Model
        {
            get => model;
            private set => model = new Regex(@"^[A-Z0-9]{5}$").IsMatch(value)
                ? value
                : throw new TransportException($"Недопустимая модель {value}");
        }

        /// <summary>
        /// Целочисленное свойство, описывающее мощность транспорта в лошадиных силах.
        /// </summary>
        /// <exception cref="TransportException">Исключение типа TransportException.</exception>
        public uint Power
        {
            get => power;
            private set => power = value >= 20
                ? value
                : throw new TransportException("Мощность не может быть меньше 20 л.с.");
        }

        /// <summary>
        /// Конструктор с параметрами для свойств Model и Power.
        /// </summary>
        /// <param name="model">Модель транспорта.</param>
        /// <param name="power">Мощность транспорта в л.с.</param>
        protected Transport(string model, uint power)
        {
            Model = model;
            Power = power;
        }

        /// <summary>
        /// Абстрактный метод, переопределяемый в производных классах для получения звука, издаваемого транспортным средством.
        /// </summary>
        /// <returns>Звук, издаваемый транспортным средством.</returns>
        public abstract string StartEngine();

        /// <summary>
        /// Переопределённый метод ToString.
        /// </summary>
        /// <returns>Строка формата "Model: 'Model', Power: 'Power'".</returns>
        public override string ToString() => $"Model: {Model}, Power: {Power}";
    }
}