namespace CashJournal
{
    partial class StartUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.commandBox = new System.Windows.Forms.GroupBox();
            this.atDate = new System.Windows.Forms.DateTimePicker();
            this.btnZ = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.commandBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandBox
            // 
            this.commandBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commandBox.Controls.Add(this.btnRead);
            this.commandBox.Controls.Add(this.btnZ);
            this.commandBox.Controls.Add(this.atDate);
            this.commandBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commandBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.commandBox.Location = new System.Drawing.Point(26, 12);
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(267, 495);
            this.commandBox.TabIndex = 1;
            this.commandBox.TabStop = false;
            this.commandBox.Text = "Печать приходных ордеров";
            // 
            // atDate
            // 
            this.atDate.Location = new System.Drawing.Point(6, 32);
            this.atDate.Name = "atDate";
            this.atDate.Size = new System.Drawing.Size(255, 22);
            this.atDate.TabIndex = 0;
            // 
            // btnZ
            // 
            this.btnZ.Location = new System.Drawing.Point(6, 60);
            this.btnZ.Name = "btnZ";
            this.btnZ.Size = new System.Drawing.Size(255, 36);
            this.btnZ.TabIndex = 1;
            this.btnZ.Text = "Z-отчёт";
            this.btnZ.UseVisualStyleBackColor = true;
            this.btnZ.Click += new System.EventHandler(this.btnZ_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(6, 102);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(255, 36);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Получить ПО";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // StartUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 519);
            this.Controls.Add(this.commandBox);
            this.Name = "StartUI";
            this.Text = "Кассовая программа";
            this.Load += new System.EventHandler(this.StartUI_Load);
            this.commandBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox commandBox;
        private System.Windows.Forms.DateTimePicker atDate;
        private System.Windows.Forms.Button btnZ;
        private System.Windows.Forms.Button btnRead;
    }
}