using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NotPeerGrade.Models;
using System.Runtime.Serialization.Json;

namespace NotPeerGrade.Controllers
{
    /// <summary>
    /// Контроллер класса User.
    /// </summary>
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Генерирует случайный список пользователей.
        /// </summary>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpPost]
        public IActionResult Post()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rand = new Random();
            for (var i = 0; i < rand.Next(1, 10); i++)
            {
                Post(new User());
            }

            return Ok(ReadList());
        }

        /// <summary>
        /// Добавляет информацию о новом пользователе.
        /// </summary>
        /// <param name="user">Новый пользователь.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученного пользователя.</returns>
        [HttpPost("AddUserForm")]
        public IActionResult Post(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = ReadList();
            var list = users.FindAll(x => x.Email.Contains(user.Email));
            if (!list.TrueForAll(x => x.Email != user.Email))
            {
                user.Email = list.Count + 1 + '_' + user.Email;
            }

            users.Add(user);
            users.Sort((x, y) => x.Email[0] > y.Email[0] ? 1 : -1);

            var format = new DataContractJsonSerializer(typeof(List<User>));
            using var fs = new FileStream("Storage/Users.json", FileMode.Create);
            format.WriteObject(fs, users);
            return Ok(user);
        }

        /// <summary>
        /// Добавляет информацию о новом пользователе, полученном из JSON.
        /// </summary>
        /// <param name="user">Новый пользователь.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученного пользователя.</returns>
        [HttpPost("AddUser")]
        public IActionResult PostBody([FromBody] User user) => Post(user);

        /// <summary>
        /// Возвращает список всех пользователей.
        /// </summary>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученный список.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var list = ReadList();

            if (list.Count == 0)
                return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Возвращает пользователя с заданным Email-ом.
        /// </summary>
        /// <param name="email">Email искомого пользователя.</param>
        /// <returns>Объект IActionResult, в случае успеха содержащий полученного пользователя.</returns>
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            var list = ReadList();

            if (list.Count == 0)
                return NoContent();

            var user = list.SingleOrDefault(x => x.Email == email);

            return user == null
                ? NotFound()
                : Ok(user);
        }

        /// <summary>
        /// Метод, совершающий десериализацию списка пользователей из JSON-файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        internal static List<User> ReadList()
        {
            var format = new DataContractJsonSerializer(typeof(List<User>));
            using var fs = new FileStream("Storage/Users.json", FileMode.Open);
            List<User> list;

            try
            {
                list = (List<User>) format.ReadObject(fs);
            }
            catch
            {
                return new List<User>();
            }

            return list;
        }
    }
}