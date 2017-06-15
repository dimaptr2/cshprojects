namespace CashJournalPrinting
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
            this.btnProperties = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.labelAtDate = new System.Windows.Forms.Label();
            this.tbxCashBox = new System.Windows.Forms.TextBox();
            this.labelCashBox = new System.Windows.Forms.Label();
            this.tbxCompany = new System.Windows.Forms.TextBox();
            this.labelCompany = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnZ = new System.Windows.Forms.Button();
            this.atDate = new System.Windows.Forms.DateTimePicker();
            this.gbxOutput = new System.Windows.Forms.GroupBox();
            this.dataGridViewOutput = new System.Windows.Forms.DataGridView();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.commandBox.SuspendLayout();
            this.gbxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // commandBox
            // 
            this.commandBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commandBox.Controls.Add(this.buttonInfo);
            this.commandBox.Controls.Add(this.btnProperties);
            this.commandBox.Controls.Add(this.btnPrint);
            this.commandBox.Controls.Add(this.btnX);
            this.commandBox.Controls.Add(this.labelAtDate);
            this.commandBox.Controls.Add(this.tbxCashBox);
            this.commandBox.Controls.Add(this.labelCashBox);
            this.commandBox.Controls.Add(this.tbxCompany);
            this.commandBox.Controls.Add(this.labelCompany);
            this.commandBox.Controls.Add(this.btnExit);
            this.commandBox.Controls.Add(this.btnRead);
            this.commandBox.Controls.Add(this.btnZ);
            this.commandBox.Controls.Add(this.atDate);
            this.commandBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commandBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.commandBox.Location = new System.Drawing.Point(26, 12);
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(267, 683);
            this.commandBox.TabIndex = 1;
            this.commandBox.TabStop = false;
            this.commandBox.Text = "Выбор данных";
            // 
            // btnProperties
            // 
            this.btnProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProperties.Location = new System.Drawing.Point(6, 637);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(255, 36);
            this.btnProperties.TabIndex = 11;
            this.btnProperties.Text = "Свойства ФР";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(6, 288);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(255, 36);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "Распечатать всё";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(6, 162);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(255, 36);
            this.btnX.TabIndex = 9;
            this.btnX.Text = "Открыть смену";
            this.btnX.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // labelAtDate
            // 
            this.labelAtDate.AutoSize = true;
            this.labelAtDate.Location = new System.Drawing.Point(6, 113);
            this.labelAtDate.Name = "labelAtDate";
            this.labelAtDate.Size = new System.Drawing.Size(44, 16);
            this.labelAtDate.TabIndex = 8;
            this.labelAtDate.Text = "Дата";
            // 
            // tbxCashBox
            // 
            this.tbxCashBox.Location = new System.Drawing.Point(9, 85);
            this.tbxCashBox.Name = "tbxCashBox";
            this.tbxCashBox.Size = new System.Drawing.Size(161, 22);
            this.tbxCashBox.TabIndex = 7;
            // 
            // labelCashBox
            // 
            this.labelCashBox.AutoSize = true;
            this.labelCashBox.Location = new System.Drawing.Point(6, 66);
            this.labelCashBox.Name = "labelCashBox";
            this.labelCashBox.Size = new System.Drawing.Size(123, 16);
            this.labelCashBox.TabIndex = 6;
            this.labelCashBox.Text = "Кассовая книга";
            // 
            // tbxCompany
            // 
            this.tbxCompany.Location = new System.Drawing.Point(9, 41);
            this.tbxCompany.Name = "tbxCompany";
            this.tbxCompany.Size = new System.Drawing.Size(161, 22);
            this.tbxCompany.TabIndex = 5;
            // 
            // labelCompany
            // 
            this.labelCompany.AutoSize = true;
            this.labelCompany.Location = new System.Drawing.Point(6, 22);
            this.labelCompany.Name = "labelCompany";
            this.labelCompany.Size = new System.Drawing.Size(164, 16);
            this.labelCompany.TabIndex = 4;
            this.labelCompany.Text = "Балансовая единица";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(6, 330);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(255, 36);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(6, 246);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(255, 36);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "Получить ПКО";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnZ
            // 
            this.btnZ.Location = new System.Drawing.Point(6, 204);
            this.btnZ.Name = "btnZ";
            this.btnZ.Size = new System.Drawing.Size(255, 36);
            this.btnZ.TabIndex = 1;
            this.btnZ.Text = "Закрыть смену";
            this.btnZ.UseVisualStyleBackColor = true;
            this.btnZ.Click += new System.EventHandler(this.btnZ_Click);
            // 
            // atDate
            // 
            this.atDate.CustomFormat = "";
            this.atDate.Location = new System.Drawing.Point(6, 134);
            this.atDate.Name = "atDate";
            this.atDate.Size = new System.Drawing.Size(255, 22);
            this.atDate.TabIndex = 0;
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
            this.gbxOutput.Size = new System.Drawing.Size(863, 683);
            this.gbxOutput.TabIndex = 2;
            this.gbxOutput.TabStop = false;
            this.gbxOutput.Text = "Список приходно-кассовых ордеров";
            // 
            // dataGridViewOutput
            // 
            this.dataGridViewOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOutput.Location = new System.Drawing.Point(6, 22);
            this.dataGridViewOutput.Name = "dataGridViewOutput";
            this.dataGridViewOutput.ReadOnly = true;
            this.dataGridViewOutput.Size = new System.Drawing.Size(851, 655);
            this.dataGridViewOutput.TabIndex = 0;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInfo.Location = new System.Drawing.Point(6, 597);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(255, 36);
            this.buttonInfo.TabIndex = 12;
            this.buttonInfo.Text = "Подробно о состоянии";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // StartUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 707);
            this.Controls.Add(this.gbxOutput);
            this.Controls.Add(this.commandBox);
            this.Name = "StartUI";
            this.ShowIcon = false;
            this.Text = "Кассовая программа \"ООО МК Павловская Слобода\"";
            this.Load += new System.EventHandler(this.StartUI_Load);
            this.commandBox.ResumeLayout(false);
            this.commandBox.PerformLayout();
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
        private System.Windows.Forms.Label labelCompany;
        private System.Windows.Forms.TextBox tbxCashBox;
        private System.Windows.Forms.Label labelCashBox;
        private System.Windows.Forms.TextBox tbxCompany;
        private System.Windows.Forms.Label labelAtDate;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.Button buttonInfo;
    }
}