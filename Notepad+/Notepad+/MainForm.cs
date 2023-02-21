using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    /// <summary>
    /// Главная форма.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Таймер автосохранений.
        /// </summary>
        private AutoSavingTimer autoSaving;
        /// <summary>
        /// Отметка о текущей цветовой схеме.
        /// </summary>
        private bool isAlternativeScheme = false;

        /// <summary>
        /// Констркутор главной формы.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            autoSaving = new AutoSavingTimer(ref tabControl);
            Timer0.Checked = true;
            try
            {
                var old = System.IO.File.ReadAllLines("info.txt");
                for (int i = 0; i < old.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            var oldSender = new ToolStripMenuItem();
                            oldSender.Name = $"Timer{old[i]}";
                            SetTimer(oldSender, new EventArgs());
                            break;
                        case 1:
                            isAlternativeScheme = (old[i] == "true");
                            if (isAlternativeScheme)
                                ColorSchemeOnClick(new object(), new EventArgs());
                            break;
                        default:
                            var newPage = new TabPage();
                            newPage.Extend(old[i]);
                            tabControl.TabPages.Add(newPage);
                            tabControl.SelectedTab = newPage;
                            break;
                    }
                }

                if (old.Length < 3)
                    fileCreate_Click(new object(), new EventArgs());
            }
            catch
            {
                fileCreate_Click(new object(), new EventArgs());
            }
        }
        /// <summary>
        /// Обработчик события сохранения файла.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void FileSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab.Name == "")
                    fileSaveAs_Click(sender, e);
                else
                    tabControl.SelectedTab.SaveAsFile();
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события сохранения файла как. 
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="Args">Событие.</param>
        private void fileSaveAs_Click(object sender, EventArgs Args)
        {
            try
            {
                var svd = new SaveFileDialog();
                svd.InitialDirectory = @$"С:\Users\{Environment.UserName}\";
                svd.Filter = "Rich Text files (*.rtf)|*.rtf;|Text files (*.txt)|*.txt";
                if (svd.ShowDialog() == DialogResult.OK)
                {
                    tabControl.SelectedTab.SaveAsFile(svd.FileName);
                    if (tabControl.SelectedTab.Name == "")
                    {
                        tabControl.SelectedTab.Name = svd.FileName;
                        tabControl.SelectedTab.Text = Path.GetFileNameWithoutExtension(svd.FileName);
                    }
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события создания файла.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void fileCreate_Click(object sender, EventArgs Args)
        {
            try
            {
                var newPage = new TabPage();
                newPage.Extend();
                tabControl.TabPages.Add(newPage);
                tabControl.SelectedTab = newPage;
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события открытия файла.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void fileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opd = new OpenFileDialog();
                opd.InitialDirectory = @$"С:\Users\{Environment.UserName}\";
                opd.Filter = "Text files (*.txt;*.rtf)|*.txt;*.rtf";
                if (opd.ShowDialog() == DialogResult.OK)
                {
                    var newPage = new TabPage();
                    newPage.Extend(opd.FileName);
                    tabControl.TabPages.Add(newPage);
                    tabControl.SelectedTab = newPage;
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события отрисовки вкладки.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            var closeButton = new Bitmap(@"..\..\..\Images\close.png");
            var tabPage = tabControl.TabPages[e.Index];
            var tabRectangle = tabControl.GetTabRect(e.Index);
            e.Graphics.DrawImage(closeButton, tabRectangle.Right - closeButton.Width - 2,
                tabRectangle.Top + (tabRectangle.Height - closeButton.Height) / 2 - 2);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRectangle, tabPage.ForeColor, TextFormatFlags.Left);
        }
        /// <summary>
        /// Обработчик события нажатия мышкой на вкладку.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < tabControl.TabPages.Count; i++)
            {
                var closeButton = new Bitmap(@"..\..\..\Images\close.png");
                var tabRectangle = tabControl.GetTabRect(i);
                var closeButtonRectangle = new Rectangle(
                    tabRectangle.Right - closeButton.Width - 2,
                    tabRectangle.Top + (tabRectangle.Height - closeButton.Height) / 2,
                    closeButton.Width,
                    closeButton.Height);
                if (closeButtonRectangle.Contains(e.Location))
                {
                    if (tabControl.TabPages[i].Tag == "1")
                        switch (
                            MessageBox.Show(
                                $"В файле \"{(tabControl.TabPages[i]).Text}\" есть несохранённые изменения.\nСохранить?",
                                "", MessageBoxButtons.YesNo))
                        {
                            case DialogResult.Yes:
                                var selectedNum = tabControl.SelectedTab.TabIndex +
                                                  (tabControl.SelectedTab.TabIndex >= i ? -1 : 0);
                                tabControl.SelectTab(i);
                                if (tabControl.TabPages[i].Name != "")
                                    FileSaveClick(sender, e);
                                else
                                    fileSaveAs_Click(sender, e);
                                tabControl.TabPages.RemoveAt(i);
                                tabControl.SelectTab(selectedNum);
                                break;
                            case DialogResult.No:
                                tabControl.TabPages.RemoveAt(i);
                                break;
                        }
                    else
                    {
                        tabControl.TabPages.RemoveAt(i);
                    }
                    if (tabControl.TabPages.Count == 0)
                        fileCreate_Click(new object(), new EventArgs());
                    break;
                }
            }
        }
        /// <summary>
        /// Обработчик события изменения шрифта.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        public void FontChange(object sender, EventArgs e)
        {
            try
            {
                var fontDialog = new FontDialog();
                if (fontDialog.ShowDialog() != DialogResult.Cancel)
                {
                    (tabControl.SelectedTab.Controls[0] as RichTextBox).SelectionFont = fontDialog.Font;
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события изменения цвета шрифта.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        public void ColorChange(object sender, EventArgs e)
        {
            try
            {
                var colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() != DialogResult.Cancel)
                {
                    (tabControl.SelectedTab.Controls[0] as RichTextBox).SelectionColor = colorDialog.Color;
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события выбора всего текста.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void SelectAllClick(object sender, EventArgs e)
        {
            try
            {
                var richText = (tabControl.SelectedTab.Controls[0] as RichTextBox);
                richText.SelectionStart = 0;
                richText.SelectionLength = richText.Text.Length;
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события "Вырезать выделенный текст"
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void CutSelectedClick(object sender, EventArgs e)
        {
            try
            {
                var richText = (tabControl.SelectedTab.Controls[0] as RichTextBox);
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
                var richText = (tabControl.SelectedTab.Controls[0] as RichTextBox);
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
                var richText = (tabControl.SelectedTab.Controls[0] as RichTextBox);
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
        /// <summary>
        /// Обработчик события "Удалить текст".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void DeleteClick(object sender, EventArgs e)
        {
            try
            {
                var richText = (tabControl.SelectedTab.Controls[0] as RichTextBox);
                if (richText.SelectedRtf != null)
                {
                    richText.Rtf = "";
                }
            }
            catch (Exception exception)
            {
                new ErrorForm(exception.Message).ShowDialog();
            }
        }
        /// <summary>
        /// Обработчик события изменения цветовой схемы.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void ColorSchemeOnClick(object sender, EventArgs e)
        {
            menuStrip.BackColor = tabControl.BackColor =
                isAlternativeScheme ? System.Drawing.Color.White : System.Drawing.Color.Chocolate;
            menuStrip.ForeColor =
                isAlternativeScheme ? System.Drawing.Color.Black : System.Drawing.Color.White;
            BackColor = !isAlternativeScheme ? System.Drawing.Color.Bisque : System.Drawing.Color.White;
            foreach (var page in tabControl.TabPages)
            {
                var tabPage = page as TabPage;
                tabPage.BackColor = (tabPage.Controls[0] as RichTextBox).BackColor =
                    !isAlternativeScheme ? System.Drawing.Color.Bisque : System.Drawing.Color.White;
            }
            foreach (var control in menuStrip.Items)
            {
                var menuItem = control as ToolStripMenuItem;
                foreach (var item in menuItem.DropDownItems)
                {
                    var menuOfMenuItem = item as ToolStripMenuItem;
                    menuOfMenuItem.BackColor = isAlternativeScheme ? System.Drawing.Color.White : System.Drawing.Color.Bisque;
                    foreach (var anotherItem in menuOfMenuItem.DropDownItems)
                    {
                        var menuOfMenuOfMenuItem = anotherItem as ToolStripMenuItem;
                        menuOfMenuOfMenuItem.BackColor = 
                            isAlternativeScheme ? System.Drawing.Color.White : System.Drawing.Color.Bisque;
                    }
                }
            }

            isAlternativeScheme = !isAlternativeScheme;
        }
        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void FormClosing(object sender, EventArgs e)
        {
            var sw = new StreamWriter("info.txt", false);
            sw.WriteLine($"{autoSaving.Number}");
            sw.WriteLine($"{isAlternativeScheme}");
            foreach (var page in tabControl.TabPages)
            {
                if ((page as TabPage).Tag as string == "1")
                {
                    switch (
                        MessageBox.Show(
                            $"В файле \"{(page as TabPage).Text}\" есть несохранённые изменения.\nСохранить?",
                            "", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            var selectedNum = tabControl.SelectedTab.TabIndex +
                                              (tabControl.SelectedTab.TabIndex >= (page as TabPage).TabIndex ? -1 : 0);
                            tabControl.SelectTab((page as TabPage).TabIndex);
                            if (tabControl.TabPages[(page as TabPage).TabIndex].Name != "")
                                FileSaveClick(sender, e);
                            else
                                fileSaveAs_Click(sender, e);
                            tabControl.SelectTab(selectedNum);
                            break;
                        case DialogResult.No:
                            break;
                    }
                }

                if ((page as TabPage).Name != "")
                    sw.WriteLine((page as TabPage).Name);
            }

            sw.Close();
        }
        /// <summary>
        /// Обработчик события установки таймера автосохранений.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void SetTimer(object sender, EventArgs e)
        {
            switch ((sender as ToolStripMenuItem).Name)
            {
                case "Timer0":
                    autoSaving.SetTimer(0);
                    Timer0.Checked = true;
                    Timer1.Checked = Timer2.Checked = Timer3.Checked = Timer4.Checked = false;
                    break;
                case "Timer1":
                    autoSaving.SetTimer(1);
                    Timer1.Checked = true;
                    Timer0.Checked = Timer2.Checked = Timer3.Checked = Timer4.Checked = false;
                    break;
                case "Timer2":
                    autoSaving.SetTimer(2);
                    Timer2.Checked = true;
                    Timer1.Checked = Timer0.Checked = Timer3.Checked = Timer4.Checked = false;
                    break;
                case "Timer3":
                    autoSaving.SetTimer(3);
                    Timer3.Checked = true;
                    Timer1.Checked = Timer2.Checked = Timer0.Checked = Timer4.Checked = false;
                    break;
                case "Timer4":
                    autoSaving.SetTimer(4);
                    Timer4.Checked = true;
                    Timer1.Checked = Timer2.Checked = Timer3.Checked = Timer0.Checked = false;
                    break;
            }
        }
        /// <summary>
        /// Обработчик события нажатия горячих клавиш.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                foreach (var page in tabControl.TabPages)
                {
                    if ((page as TabPage).Tag as string == "1")
                    {
                        var selectedNum = tabControl.SelectedTab.TabIndex +
                                          (tabControl.SelectedTab.TabIndex >= (page as TabPage).TabIndex ? -1 : 0);
                        tabControl.SelectTab((page as TabPage).TabIndex);
                        if (tabControl.TabPages[(page as TabPage).TabIndex].Name != "")
                            FileSaveClick(sender, e);
                        else
                            fileSaveAs_Click(sender, e);
                        tabControl.SelectTab(selectedNum);
                    }
                }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                FileSaveClick(new object(), new EventArgs());
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                var lines = System.IO.File.ReadAllLines("info.txt");
                System.IO.File.WriteAllText("info.txt", lines[0] + "\n" + lines[1]);
                new MainForm().ShowDialog();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                fileCreate_Click(new object(), new EventArgs());
            }
            else if (e.Control && e.KeyCode == Keys.Q)
            {
                Close();
            }
        }
        /// <summary>
        /// Обработчик события показа справки.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void InfoOnClick(object sender, EventArgs e)
        {
            MessageBox.Show("ctrl + S => Сохранение текущего документа\n" +
                            "ctrl + A => Сохранение всех открытых в окне документов\n" +
                            "ctrl + N => Создание документа в новой вкладке\n" +
                            "ctrl + M => Создание документа в новом окне\n" +
                            "ctrl + Q => Закрытие приложени");
        }
    }
}
