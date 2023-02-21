using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NotPeerGrade.Models
{
    /// <summary>
    /// Класс, представляющий сообщение.
    /// </summary>
    [DataContract]
    public class Letter
    {
        /// <summary>
        /// Свойство, представляющее тему сообщения.
        /// </summary>
        [DataMember]
        public string Subject { get; set; }

        /// <summary>
        /// Свойство, представляющее текст сообщения.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Свойство, представляющее Email отправителя сообщения.
        /// </summary>
        [DataMember]
        [Required]
        public string SenderId { get; set; }

        /// <summary>
        /// Свойство, представляющее Email получателя сообщения.
        /// </summary>
        [DataMember]
        [Required]
        public string ReceiverId { get; set; }

        /// <summary>
        /// Конструктор, создающий сообщение со случайным темой и текстом.
        /// </summary>
        /// <param name="senderId">Email отправителя сообщения.</param>
        /// <param name="receiverId">Email получателя сообщения.</param>
        public Letter(string senderId, string receiverId)
        {
            var rand = new Random();
            Subject = "";
            Message = "";
            SenderId = senderId;
            ReceiverId = receiverId;

            for (var i = 0; i < rand.Next(5, 10); i++)
            {
                Subject += (char) rand.Next(97, 123);
            }

            for (var i = 0; i < rand.Next(5, 25); i++)
            {
                Message += (char) rand.Next(97, 123);
            }
        }

        // Просто конструктор без параметров, необходимый для десериализации.
        public Letter()
        {
        }
    }
}