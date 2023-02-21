using System;
using System.Windows.Forms;
namespace Notepad
{
    /// <summary>
    /// Класс, хранящий элементы контекстного меню для конкретного текста.
    /// </summary>
    public class ContextMenu
    {
        /// <summary>
        /// Элементы контекстного меню.
        /// </summary>
        public ToolStripMenuItem[] contextItems { get; }
        /// <summary>
        /// Данный текст.
        /// </summary>
        private RichTextBox richText;
        /// <summary>
        /// Конструктор данного класса.
        /// </summary>
        /// <param name="richTextBox">Данный текст.</param>
        public ContextMenu(RichTextBox richTextBox)
        {
            richText = richTextBox;
            ToolStripMenuItem selectAll = new ToolStripMenuItem("Выбрать весь текст");
            ToolStripMenuItem cutSelected = new ToolStripMenuItem("Вырезать выделенный фрагмент");
            ToolStripMenuItem copySelected = new ToolStripMenuItem("Копировать выделенный фрагмент");
            ToolStripMenuItem paste = new ToolStripMenuItem("Вставить сохранённый в буфере обмена фрагмент");
            ToolStripMenuItem formatSelected = new ToolStripMenuItem("Задать формат выделенного фрагмента текста");
            formatSelected.DropDownItems.AddRange(new FontChangingTools(richText).menuItems);
            selectAll.Click += SelectAllClick;
            cutSelected.Click += CutSelectedClick;
            copySelected.Click += CopySelectedClick;
            paste.Click += PasteClick;
            contextItems = new[] { selectAll, cutSelected, copySelected, paste, formatSelected };
        }
        // Я знаю, что последующие методы также есть и в MainForm, но доделывал я уже совсем незадолго до дедлайна, так
        // что решил не тратить время на попытки положить их в одно место, и оставить так как есть.
        /// <summary>
        /// Обработчик события "Выбрать весь текст".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void SelectAllClick(object sender, EventArgs e)
        {
            try
            {
                richText.SelectAll();
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Вырезать выделенный текст".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void CutSelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (richText.SelectedRtf != "")
                {
                    Clipboard.SetText(richText.SelectedRtf, TextDataFormat.Rtf);
                    richText.SelectedText = "";
                }
                else
                {
                    Clipboard.SetText(richText.Rtf, TextDataFormat.Rtf);
                    richText.Rtf = "";
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Скопировать выделенный текст".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void CopySelectedClick(object sender, EventArgs e)
        {
            try
            {
                if (richText.SelectedRtf != "")
                {
                    Clipboard.SetText(richText.SelectedRtf, TextDataFormat.Rtf);
                }
                else
                {
                    Clipboard.SetText(richText.Rtf, TextDataFormat.Rtf); 
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Вставить скопированный текст".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void PasteClick(object sender, EventArgs e)
        {
            try
            {
                if (richText.SelectedRtf != "")
                {
                    richText.SelectedRtf = Clipboard.GetText(TextDataFormat.Rtf);
                }
                else
                {
                    richText.Rtf += Clipboard.GetText(TextDataFormat.Rtf);
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
    }
}