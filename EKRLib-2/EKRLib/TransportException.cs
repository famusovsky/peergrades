#nullable enable
using System;
using System.Runtime.Serialization;

namespace EKRLib
{
    /// <summary>
    /// Класс-наследник класса Exception, описывающий ошибку возникшую при работе с библиотекой EKRLib.
    /// </summary>
    public class TransportException : Exception
    {
        /// <summary>
        /// Конструктор, инициализирующий новый экземпляр класса TransportException.
        /// </summary>
        public TransportException()
        {
        }

        /// <summary>
        /// Конструктор, инициализирующий новый экземпляр класса TransportException с указанным сообщением об ошибке.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку.</param>
        public TransportException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор, инициализирующий новый экземпляр класса TransportException с сериализованными данными.
        /// </summary>
        /// <param name="info">Объект SerializationInfo, хранящий сериализованные данные объекта, относящиеся к выдаваемому исключению.</param>
        /// <param name="context">Объект StreamingContext, содержащий контекстные сведения об источнике или назначении.</param>
        public TransportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Конструктор, инициализирующий новый экземпляр класса TransportException указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
        /// </summary>
        /// <param name="message">Сообщение об ошибке, указывающее причину создания исключения.</param>
        /// <param name="innerException">Исключение, вызвавшее текущее исключение, или пустая ссылка, если внутреннее исключение не задано.</param>
        public TransportException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}