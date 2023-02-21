using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NotPeerGrade.Models
{
    /// <summary>
    /// Класс, представляющий пользователя.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Свойство, представляющее Email пользователя.
        /// </summary>
        [DataMember]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Свойство, представляющее имя пользователя.
        /// </summary>
        [DataMember]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Конструктор, создающий случайного пользователя.
        /// </summary>
        public User()
        {
            var rand = new Random();
            var name = "";
            var email = "";

            for (var i = 0; i < rand.Next(5, 10); i++)
            {
                name += (char) rand.Next(97, 123);
            }

            for (var i = 0; i < rand.Next(5, 10); i++)
            {
                email += (char) rand.Next(97, 123);
            }

            email += "@edu.hse.ru";

            UserName = name;
            Email = email;
        }
    }
}