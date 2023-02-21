using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    /// <summary>
    /// Форма, сообщающая об ошибке.
    /// </summary>
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        private string message;
        /// <summary>
        /// Конструктор данной формы.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        public ErrorForm(string message)
        {
            InitializeComponent();
            this.message = message;
        }
        /// <summary>
        /// Обработчик события нажатия на кнопку "Открыть/Закрыть сообщение об ошибке".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void ButtonOnClick(object sender, EventArgs e)
        {
            if (richTextBox.Text == "")
            {
                richTextBox.Text = message;
                button.Text = "Закрыть сообщение об ошибке";
            }
            else
            {
                richTextBox.Text = "";
                button.Text = "Открыть сообщение об ошибке";
            }
        }
    }
}
