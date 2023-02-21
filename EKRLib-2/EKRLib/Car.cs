namespace EKRLib
{
    /// <summary>
    /// Класс-наследник класса Transport, описывающий машину.
    /// </summary>
    public class Car : Transport
    {
        /// <summary>
        /// Конструктор с параметрами для свойств Model и Power.
        /// </summary>
        /// <param name="model">Модель машины.</param>
        /// <param name="power">Мощность машины в л.с.</param>
        public Car(string model, uint power) : base(model, power)
        {
        }

        /// <summary>
        /// Метод, возвращающий звук, издаваемый машиной.
        /// </summary>
        /// <returns>Звук, издаваемый машиной.</returns>
        public override string StartEngine() => $"{Model}: Vroom";

        /// <summary>
        /// Переопределённый метод ToString.
        /// </summary>
        /// <returns>Строка формата "Car. Model: 'Model', Power: 'Power'".</returns>
        public override string ToString() => "Car. " + base.ToString();
    }
}