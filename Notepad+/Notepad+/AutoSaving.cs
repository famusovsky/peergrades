using System.Windows.Forms;
using System;
using Timer = System.Timers.Timer;

namespace Notepad
{
    /// <summary>
    /// Класс таймера автосохранений.
    /// </summary>
    public class AutoSavingTimer : Timer
    {
        /// <summary>
        /// TabControl, в котором происходят сохранения.
        /// </summary>
        private TabControl tabControl;
        /// <summary>
        /// Номер выбранной переодичности автосохранений.
        /// </summary>
        public int Number { get; set; } = 0;
        /// <summary>
        /// Конструктор данного класса.
        /// </summary>
        /// <param name="tabControl">TabControl, в котором происходят сохранения.</param>
        public AutoSavingTimer(ref TabControl tabControl)
        {
            this.tabControl = tabControl;
            Elapsed += OnTimedEvent;
            AutoReset = true;
            Enabled = false;
        }
        /// <summary>
        /// Метод, устанавливающий периодичность автосохранений.
        /// </summary>
        /// <param name="period">Выбранная периодичность.</param>
        public void SetTimer(int period)
        {
            switch (period)
            {
                case 0:
                    Enabled = false;
                    Number = 0;
                    break;
                case 1:
                    Enabled = true;
                    Interval = 60000;
                    Number = 1;
                    break;
                case 2:
                    Enabled = true;
                    Interval = 5 * 60000;
                    Number = 2;
                    break;
                case 3:
                    Enabled = true;
                    Interval = 10 * 60000;
                    Number = 3;
                    break;
                case 4:
                    Enabled = true;
                    Interval = 30 * 60000;
                    Number = 4;
                    break;
            }
        }
        /// <summary>
        /// Обработчик события срабатывания таймера автосохранений.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Событие.</param>
        private void OnTimedEvent(object sender, EventArgs e)
        {
            foreach (var page in tabControl.TabPages)
            {
                if ((page as TabPage).Name != "")
                    TabExtension.SaveAsFile(page as TabPage);
            }
        }
    }
}