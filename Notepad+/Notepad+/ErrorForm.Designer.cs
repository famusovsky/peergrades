
using System;

namespace Notepad
{
    partial class ErrorForm
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
            this.Label = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label.Location = new System.Drawing.Point(41, 9);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(257, 35);
            this.Label.TabIndex = 0;
            this.Label.Text = "Произошла ошибка!";
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(12, 47);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(318, 29);
            this.button.TabIndex = 1;
            this.button.Text = "Открыть сообщение об ошибке";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.ButtonOnClick);
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Location = new System.Drawing.Point(12, 87);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(318, 93);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 193);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.button);
            this.Controls.Add(this.Label);
            this.MaximumSize = new System.Drawing.Size(360, 240);
            this.MinimumSize = new System.Drawing.Size(360, 240);
            this.Name = "ErrorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Ошибка";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.RichTextBox richTextBox;
    }
}

