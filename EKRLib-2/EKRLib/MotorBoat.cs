namespace EKRLib
{
    /// <summary>
    /// Класс-наследник класса Transport, описывающий моторную лодку.
    /// </summary>
    public class MotorBoat : Transport
    {
        /// <summary>
        /// Конструктор с параметрами для свойств Model и Power.
        /// </summary>
        /// <param name="model">Модель моторной лодки.</param>
        /// <param name="power">Мощность моторной лодки в л.с.</param>
        public MotorBoat(string model, uint power) : base(model, power)
        {
        }

        /// <summary>
        /// Метод, возвращающий звук, издаваемый моторной лодкой.
        /// </summary>
        /// <returns>Звук, издаваемый моторной лодкой.</returns>
        public override string StartEngine() => $"{Model}: Brrrbrr";

        /// <summary>
        /// Переопределённый метод ToString.
        /// </summary>
        /// <returns>Строка формата "MotorBoat. Model: 'Model', Power: 'Power'".</returns>
        public override string ToString() => "MotorBoat. " + base.ToString();
    }
}