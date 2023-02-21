using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Mvc;
using NotPeerGrade.Models;

namespace NotPeerGrade.Controllers
{
    /// <summary>
    /// Контроллер класса Letter.
    /// </summary>
    [Route("/api/[controller]")]
    public class LettersController : Controller
    {
        /// <summary>
        /// Генерирует случайный список сообщений.
        /// </summary>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpPost]
        public IActionResult Post()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = UsersController.ReadList();
            if (users.Count == 0)
                return NoContent();
            
            var rand = new Random();
            var letters = new List<Letter>(rand.Next(1, 10));
            for (var i = 0; i < letters.Capacity; i++)
            {
                var sender = users[rand.Next(users.Count)].Email;
                var receiver = users[rand.Next(users.Count)].Email;
                letters.Add(new Letter(sender, receiver));
            }

            var format = new DataContractJsonSerializer(typeof(List<Letter>));
            using var fs = new FileStream("Storage/Letters.json", FileMode.Create);
            format.WriteObject(fs, letters);
            return Ok(letters);
        }

        /// <summary>
        /// Добавляет информацию о новом сообщении.
        /// </summary>
        /// <param name="letter">Отправленное сообщение.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий отправленное сообщение.</returns>
        [HttpPost("AddLetterForm")]
        public IActionResult Post(Letter letter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (UsersController.ReadList().FindIndex(x => x.Email == letter.SenderId) == -1 ||
                UsersController.ReadList().FindIndex(x => x.Email == letter.ReceiverId) == -1)
                return NotFound();

            var format = new DataContractJsonSerializer(typeof(List<Letter>));
            var list = ReadList();
            list.Add(letter);
            using var fs = new FileStream("Storage/Letters.json", FileMode.Create);
            format.WriteObject(fs, list);
            return Ok(letter);
        }

        /// <summary>
        /// Добавляет информацию о новом сообщении, полученном из JSON.
        /// </summary>
        /// <param name="letter">Отправленное сообщение.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий отправленное сообщение.</returns>
        [HttpPost("AddLetter")]
        public IActionResult PostBody([FromBody] Letter letter) => Post(letter);

        /// <summary>
        /// Возвращает список сообщений, отправленных данным пользователем.
        /// </summary>
        /// <param name="senderId">Email пользователя, список сообщений от которого запрашивается.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpGet("from{senderId}")]
        public IActionResult GetBySender(string senderId)
        {
            var letters = ReadList();
            
            if (letters.Count == 0)
                return NoContent();

            var foundLetters = letters.FindAll(x => x.SenderId == senderId);

            return foundLetters.Count == 0
                ? NotFound()
                : Ok(foundLetters);
        }

        /// <summary>
        /// Возвращает список сообщений, отправленных данному пользователелю.
        /// </summary>
        /// <param name="receiverId">Email пользователя, список сообщений которому запрашивается.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpGet("to{receiverId}")]
        public IActionResult GetByReceiver(string receiverId)
        {
            var letters = ReadList();

            if (letters.Count == 0)
                return NoContent();

            var foundLetters = letters.FindAll(x => x.ReceiverId == receiverId);

            return foundLetters.Count == 0
                ? NotFound()
                : Ok(foundLetters);
        }

        /// <summary>
        /// Возвращает список сообщений, отправленных одним из данных пользователей другому.
        /// </summary>
        /// <param name="senderId">Email пользователя, список сообщений от которого запрашивается.</param>
        /// <param name="receiverId">Email пользователя, список сообщений которому запрашивается.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpGet("by{senderId}for{receiverId}")]
        public IActionResult GetByBoth(string senderId, string receiverId)
        {
            var letters = ReadList();

            if (letters.Count == 0)
                return NoContent();

            var foundLetters = letters.FindAll(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            return foundLetters.Count == 0
                ? NotFound()
                : Ok(foundLetters);
        }

        /// <summary>
        /// Метод, совершающий десериализацию списка сообщений из JSON-файла.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        internal static List<Letter> ReadList()
        {
            var format = new DataContractJsonSerializer(typeof(List<Letter>));
            using var fs = new FileStream("Storage/Letters.json", FileMode.Open);
            List<Letter> list;

            try
            {
                list = (List<Letter>) format.ReadObject(fs);
            }
            catch
            {
                return new List<Letter>();
            }

            return list;
        }
    }
}