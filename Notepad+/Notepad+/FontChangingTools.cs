using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    /// <summary>
    /// Класс, хранящий инструменты для изменения стиля шрифта в конкретной вкладке.
    /// </summary>
    class FontChangingTools
    {
        /// <summary>
        /// Инструменты для изменения стиля шрифта.
        /// </summary>
        public ToolStripItem[] menuItems { get; set; }
        /// <summary>
        /// Текущий текст.
        /// </summary>
        RichTextBox currentText;
        /// <summary>
        /// Констректор данного класса.
        /// </summary>
        /// <param name="text">Текст в данной вкладке.</param>
        public FontChangingTools(RichTextBox text)
        {
            ToolStripMenuItem cursive = new ToolStripMenuItem("Курсив");
            ToolStripMenuItem bold = new ToolStripMenuItem("Жирный");
            ToolStripMenuItem underline = new ToolStripMenuItem("Подчёркнутый");
            ToolStripMenuItem strikeout = new ToolStripMenuItem("Зачёркнутый");
            cursive.Click += CursiveClick;
            bold.Click += BoldClick;
            underline.Click += UnderlineClick;
            strikeout.Click += StrikeoutClick;
            menuItems = new[] {cursive, bold, underline, strikeout};
            currentText = text;
        }
        /// <summary>
        /// Обработчик события "Сделать шрифт жирным". 
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void BoldClick(object sender, EventArgs e)
        {
            try
            {
                ChangingFormatOfHardText(FontStyle.Bold);
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Сделать шрифт зачёркнутым". 
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void StrikeoutClick(object sender, EventArgs e)
        {
            try
            {
                ChangingFormatOfHardText(FontStyle.Strikeout);
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Сделать шрифт подчёркнутым". 
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void UnderlineClick(object sender, EventArgs e)
        {
            try
            {
                ChangingFormatOfHardText(FontStyle.Underline);
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Сделать шрифт курсивным". 
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void CursiveClick(object sender, EventArgs e)
        {
            try
            {
                ChangingFormatOfHardText(FontStyle.Italic);
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Метод, изменяющий шрифт текста, содержащего различные шрифты.
        /// </summary>
        /// <param name="newStyle">Новый шрифт.</param>
        private void ChangingFormatOfHardText(FontStyle newStyle)
        {
            var start = currentText.SelectionStart;
            var length = currentText.SelectionLength;
            var check = true;
            for (int i = start; i < start + length; i++)
            {
                currentText.Select(i, 1);
                if (currentText.SelectionFont.Style != newStyle)
                {
                    check = false;
                    break;
                }
            }
            for (int i = start; i < start + length; i++)
            {
                currentText.Select(i, 1);
                if (check)
                {
                    currentText.SelectionFont =
                        new Font(currentText.SelectionFont, currentText.SelectionFont.Style ! ^ newStyle);
                }
                else
                {
                    currentText.SelectionFont =
                        new Font(currentText.SelectionFont, currentText.SelectionFont.Style ^ newStyle);
                }
            }
            currentText.Select(start, length);
        }
    }
}