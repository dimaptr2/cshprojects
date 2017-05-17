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
            this.btnRead = new System.Windows.Forms.Button();
            this.btnZ = new System.Windows.Forms.Button();
            this.atDate = new System.Windows.Forms.DateTimePicker();
            this.btnExit = new System.Windows.Forms.Button();
            this.gbxOutput = new System.Windows.Forms.GroupBox();
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.commandBox.SuspendLayout();
            this.gbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // commandBox
            // 
            this.commandBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commandBox.Controls.Add(this.btnExit);
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
            this.commandBox.Text = "Выбор данных";
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
            // atDate
            // 
            this.atDate.CustomFormat = "";
            this.atDate.Location = new System.Drawing.Point(6, 32);
            this.atDate.Name = "atDate";
            this.atDate.Size = new System.Drawing.Size(255, 22);
            this.atDate.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 144);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(255, 36);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gbxOutput
            // 
            this.gbxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxOutput.Controls.Add(this.dataGridViewOutput);
            this.gbxOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbxOutput.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbxOutput.Location = new System.Drawing.Point(299, 12);
            this.gbxOutput.Name = "gbxOutput";
            this.gbxOutput.Size = new System.Drawing.Size(626, 495);
            this.gbxOutput.TabIndex = 2;
            this.gbxOutput.TabStop = false;
            this.gbxOutput.Text = "Список приходных ордеров";
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Location = new System.Drawing.Point(6, 22);
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.Size = new System.Drawing.Size(614, 467);
            this.dataGridViewOutput.TabIndex = 0;
            // 
            // StartUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 519);
            this.Controls.Add(this.gbxOutput);
            this.Controls.Add(this.commandBox);
            this.Name = "StartUI";
            this.ShowIcon = false;
            this.Text = "Кассовая программа";
            this.Load += new System.EventHandler(this.StartUI_Load);
            this.commandBox.ResumeLayout(false);
            this.gbxOutput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox commandBox;
        private System.Windows.Forms.DateTimePicker atDate;
        private System.Windows.Forms.Button btnZ;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox gbxOutput;
        private System.Windows.Forms.DataGridView dataGridViewOutput;
    }
}