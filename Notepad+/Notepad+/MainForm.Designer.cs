
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Notepad
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Save = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.Create = new System.Windows.Forms.ToolStripMenuItem();
            this.Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.Format = new System.Windows.Forms.ToolStripMenuItem();
            this.Font = new System.Windows.Forms.ToolStripMenuItem();
            this.Color = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoSaving = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer0 = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Timer4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Info = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 425);
            this.tabControl.TabIndex = 0;
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_DrawItem);
            this.tabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl_MouseDown);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.Edit,
            this.Format,
            this.Settings,
            this.Info});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 2;
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Save,
            this.SaveAs,
            this.Create});
            this.File.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(59, 24);
            this.File.Text = "Файл";
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(192, 26);
            this.Open.Text = "Открыть";
            this.Open.Click += new System.EventHandler(this.fileOpen_Click);
            // 
            // Save
            // 
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(192, 26);
            this.Save.Text = "Сохранить";
            this.Save.Click += new System.EventHandler(this.FileSaveClick);
            // 
            // SaveAs
            // 
            this.SaveAs.Name = "SaveAs";
            this.SaveAs.Size = new System.Drawing.Size(192, 26);
            this.SaveAs.Text = "Сохранить как";
            this.SaveAs.Click += new System.EventHandler(this.fileSaveAs_Click);
            // 
            // Create
            // 
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(192, 26);
            this.Create.Text = "Создать";
            this.Create.Click += new System.EventHandler(this.fileCreate_Click);
            // 
            // Edit
            // 
            this.Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAll,
            this.Copy,
            this.Cut,
            this.Delete,
            this.Paste});
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(74, 24);
            this.Edit.Text = "Правка";
            // 
            // Format
            // 
            this.Format.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Font,
            this.Color});
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(77, 24);
            this.Format.Text = "Формат";
            // 
            // Font
            // 
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(140, 26);
            this.Font.Text = "Шрифт";
            this.Font.Click += new System.EventHandler(this.FontChange);
            // 
            // Color
            // 
            this.Color.Name = "Color";
            this.Color.Size = new System.Drawing.Size(140, 26);
            this.Color.Text = "Цвет";
            this.Color.Click += new System.EventHandler(this.ColorChange);
            // 
            // Settings
            // 
            this.Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ColorScheme,
            this.AutoSaving});
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(98, 24);
            this.Settings.Text = "Настройки";
            //
            // ColorScheme
            //
            this.ColorScheme.Name = "ColorScheme";
            this.ColorScheme.Click += new EventHandler(ColorSchemeOnClick);
            this.ColorScheme.Size = new System.Drawing.Size(232, 26);
            this.ColorScheme.Text = "Изменить цветовую схему";
            //
            // AutoSaving
            //
            this.AutoSaving.Name = "AutoSaving";
            this.AutoSaving.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.Timer0,
                this.Timer1,
                this.Timer2,
                this.Timer3,
                this.Timer4});
            this.AutoSaving.Size = new System.Drawing.Size(232, 26);
            this.AutoSaving.Text = "Автосохранение";
            //
            // Timer0
            //
            this.Timer0.Name = "Timer0";
            this.Timer0.CheckOnClick = true;
            this.Timer0.Click += new EventHandler(SetTimer);
            this.Timer0.Size = new System.Drawing.Size(232, 26);
            this.Timer0.Text = "Не сохранять";
            //
            // Timer1
            //
            this.Timer1.Name = "Timer1";
            this.Timer1.CheckOnClick = true;
            this.Timer1.Click += new EventHandler(SetTimer);
            this.Timer1.Size = new System.Drawing.Size(232, 26);
            this.Timer1.Text = "Раз в минуту";
            //
            // Timer2
            //
            this.Timer2.Name = "Timer2";
            this.Timer2.CheckOnClick = true;
            this.Timer2.Click += new EventHandler(SetTimer);
            this.Timer2.Size = new System.Drawing.Size(232, 26);
            this.Timer2.Text = "Раз в 5 минут";
            //
            // Timer3
            //
            this.Timer3.Name = "Timer3";
            this.Timer3.CheckOnClick = true;
            this.Timer3.Click += new EventHandler(SetTimer);
            this.Timer3.Size = new System.Drawing.Size(232, 26);
            this.Timer3.Text = "Раз в 10 минут";
            //
            // Timer4
            //
            this.Timer4.Name = "Timer4";
            this.Timer4.CheckOnClick = true;
            this.Timer4.Click += new EventHandler(SetTimer);
            this.Timer4.Size = new System.Drawing.Size(232, 26);
            this.Timer4.Text = "Раз в 30 минут";
            //
            // Info
            //
            this.Info.Name = "Info";
            this.Info.Click += new EventHandler(InfoOnClick);
            this.Info.Size = new System.Drawing.Size(232, 26);
            this.Info.Text = "Справка";
            //
            // SelectAll
            // 
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Click += new EventHandler(SelectAllClick);
            this.SelectAll.Size = new System.Drawing.Size(232, 26);
            this.SelectAll.Text = "Выделить весь текст";
            // 
            // Copy
            // 
            this.Copy.Name = "Copy";
            this.Copy.Click += new EventHandler(CopySelectedClick);
            this.Copy.Size = new System.Drawing.Size(232, 26);
            this.Copy.Text = "Копировать";
            // 
            // Cut
            // 
            this.Cut.Name = "Cut";
            this.Cut.Click += new EventHandler(CutSelectedClick);
            this.Cut.Size = new System.Drawing.Size(232, 26);
            this.Cut.Text = "Вырезать";
            // 
            // Delete
            // 
            this.Delete.Name = "Delete";
            this.Delete.Click += new EventHandler(DeleteClick);
            this.Delete.Size = new System.Drawing.Size(232, 26);
            this.Delete.Text = "Удалить";
            // 
            // Paste
            // 
            this.Paste.Name = "Paste";
            this.Paste.Click += new EventHandler(PasteClick);
            this.Paste.Size = new System.Drawing.Size(232, 26);
            this.Paste.Text = "Вставить";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(360, 240);
            this.Name = "MainForm";
            this.Text = "Notepad+";
            this.Closing += new CancelEventHandler(FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FormKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem Edit;
        private System.Windows.Forms.ToolStripMenuItem Format;
        private System.Windows.Forms.ToolStripMenuItem Settings;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Save;
        private System.Windows.Forms.ToolStripMenuItem SaveAs;
        private System.Windows.Forms.ToolStripMenuItem Create;
        private System.Windows.Forms.ToolStripMenuItem Font;
        private System.Windows.Forms.ToolStripMenuItem Color;
        private ToolStripMenuItem SelectAll;
        private ToolStripMenuItem Copy;
        private ToolStripMenuItem Cut;
        private ToolStripMenuItem Delete;
        private ToolStripMenuItem Paste;
        private ToolStripMenuItem ColorScheme;
        private ToolStripMenuItem AutoSaving;
        private ToolStripMenuItem Timer0;
        private ToolStripMenuItem Timer1;
        private ToolStripMenuItem Timer2;
        private ToolStripMenuItem Timer3;
        private ToolStripMenuItem Timer4;
        private ToolStripMenuItem Info;
    }
}

