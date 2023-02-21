using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Notepad
{
    /// <summary>
    /// Класс дополнений к вкладкам TabControl.
    /// </summary>
    static class TabExtension
    {
        /// <summary>
        /// Метод, совершающий расширение данной вкладки.
        /// </summary>
        /// <param name="tabPage">Данная вкладка.</param>
        public static void Extend(this TabPage tabPage)
        {
            tabPage.Text = "Новый текст   ";
            tabPage.Size = new Size(tabPage.Width + 4, tabPage.Height);
            tabPage.FulfillTab();
            tabPage.Tag = "0";
        }
        /// <summary>
        /// Метод, совершающий расширение данной вкладки.
        /// </summary>
        /// <param name="tabPage">Данная вкладка.</param>
        /// <param name="fileName">Файл, открывающийся в данной вкладке.</param>
        public static void Extend(this TabPage tabPage, string fileName)
        {
            tabPage.Text = Path.GetFileNameWithoutExtension(fileName) + "   ";
            tabPage.Name = fileName;
            tabPage.FulfillTab();
            if (Path.GetExtension(fileName) == ".txt")
                (tabPage.Controls[0] as RichTextBox).Text = File.ReadAllText(fileName);
            else
                (tabPage.Controls[0] as RichTextBox).Rtf = File.ReadAllText(fileName);
            tabPage.Tag = "0";
        }
        /// <summary>
        /// Метод, наполняющий контентом данную вкладку.
        /// </summary>
        /// <param name="tabPage">Данная вкладка.</param>
        private static void FulfillTab(this TabPage tabPage)
        { 
            RichTextBox richText = new RichTextBox(); 
            richText.BorderStyle = BorderStyle.None;
            richText.TextChanged += RichTextOnTextChanged;
            richText.Dock = DockStyle.Fill;
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.AddRange(new ContextMenu(richText).contextItems);
            richText.ContextMenuStrip = contextMenu;
            tabPage.Controls.Add(richText);
        }
        /// <summary>
        /// Обработчик события изменения текста в данной вкладке.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private static void RichTextOnTextChanged(object sender, EventArgs e)
        {
            (sender as RichTextBox).Parent.Tag = "1";
        }
        /// <summary>
        /// Метод, совершающий сохранение данной вкладки в виде файла.
        /// </summary>
        /// <param name="tabPage">Данная вкладка.</param>
        public static void SaveAsFile(this TabPage tabPage)
        {
            if (Path.GetExtension(tabPage.Name) == ".txt")
            {
                var richText = (tabPage.Controls[0] as RichTextBox);
                var start = 0;
                var length = 0;
                var isPlain = true;
                if (richText.SelectionLength != 0 | richText.SelectionStart != 0)
                {
                    start = richText.SelectionStart;
                    length = richText.SelectionLength;
                }
                richText.SelectAll();
                if (richText.SelectionFont != null 
                    && richText.SelectionColor != Color.Empty 
                    && richText.SelectionFont.Style == FontStyle.Regular
                    && richText.SelectionColor.ToArgb() == richText.ForeColor.ToArgb())
                {
                    richText.SaveFile(tabPage.Name, RichTextBoxStreamType.PlainText);                
                }
                else
                {
                    File.Delete(tabPage.Name);
                    tabPage.Name = Path.ChangeExtension(tabPage.Name, ".rtf");
                    richText.SaveFile(tabPage.Name, RichTextBoxStreamType.RichText);
                }
                richText.Select(start, length); 
            }
            else
            {
                (tabPage.Controls[0] as RichTextBox).SaveFile(tabPage.Name, RichTextBoxStreamType.RichText);
            }
            tabPage.Tag = "0"; 
        }
        /// <summary>
        /// Метод, совершающий сохранение данной вкладки в виде файла.
        /// </summary>
        /// <param name="tabPage">Данная вкладка.</param>
        /// <param name="fileName">Адрес, по которому сохраняется вкладка.</param>
        public static void SaveAsFile(this TabPage tabPage, string fileName)
        {
            if (Path.GetExtension(fileName) == ".txt")
                (tabPage.Controls[0] as RichTextBox).SaveFile(fileName, RichTextBoxStreamType.PlainText);
            else
                (tabPage.Controls[0] as RichTextBox).SaveFile(fileName, RichTextBoxStreamType.RichText);
            tabPage.Tag = "0";
        }
    }
}

