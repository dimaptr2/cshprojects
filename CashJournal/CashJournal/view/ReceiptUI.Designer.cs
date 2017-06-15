namespace CashJournal.view
{
    partial class ReceiptUI
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
            this.components = new System.ComponentModel.Container();
            this.gbxReceiptUI = new System.Windows.Forms.GroupBox();
            this.btnDistribution = new System.Windows.Forms.Button();
            this.labelDelivery = new System.Windows.Forms.Label();
            this.tbxDelivery = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.labelTotal = new System.Windows.Forms.Label();
            this.tbxTotalAmount = new System.Windows.Forms.TextBox();
            this.btnPrintDelivery = new System.Windows.Forms.Button();
            this.dataGridViewReceipt = new System.Windows.Forms.DataGridView();
            this.resultViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnPrintView = new System.Windows.Forms.Button();
            this.gbxReceiptUI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxReceiptUI
            // 
            this.gbxReceiptUI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxReceiptUI.Controls.Add(this.btnPrintView);
            this.gbxReceiptUI.Controls.Add(this.btnDistribution);
            this.gbxReceiptUI.Controls.Add(this.labelDelivery);
            this.gbxReceiptUI.Controls.Add(this.tbxDelivery);
            this.gbxReceiptUI.Controls.Add(this.btnClose);
            this.gbxReceiptUI.Controls.Add(this.labelTotal);
            this.gbxReceiptUI.Controls.Add(this.tbxTotalAmount);
            this.gbxReceiptUI.Controls.Add(this.btnPrintDelivery);
            this.gbxReceiptUI.Controls.Add(this.dataGridViewReceipt);
            this.gbxReceiptUI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbxReceiptUI.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbxReceiptUI.Location = new System.Drawing.Point(12, 12);
            this.gbxReceiptUI.Name = "gbxReceiptUI";
            this.gbxReceiptUI.Size = new System.Drawing.Size(1123, 623);
            this.gbxReceiptUI.TabIndex = 0;
            this.gbxReceiptUI.TabStop = false;
            this.gbxReceiptUI.Text = "Просмотр чека";
            // 
            // btnDistribution
            // 
            this.btnDistribution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDistribution.Location = new System.Drawing.Point(530, 551);
            this.btnDistribution.Name = "btnDistribution";
            this.btnDistribution.Size = new System.Drawing.Size(255, 36);
            this.btnDistribution.TabIndex = 11;
            this.btnDistribution.Text = "Распределить";
            this.btnDistribution.UseVisualStyleBackColor = true;
            this.btnDistribution.Click += new System.EventHandler(this.btnDistribution_Click);
            // 
            // labelDelivery
            // 
            this.labelDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDelivery.AutoSize = true;
            this.labelDelivery.Location = new System.Drawing.Point(17, 499);
            this.labelDelivery.Name = "labelDelivery";
            this.labelDelivery.Size = new System.Drawing.Size(79, 16);
            this.labelDelivery.TabIndex = 10;
            this.labelDelivery.Text = "Поставка";
            // 
            // tbxDelivery
            // 
            this.tbxDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDelivery.Location = new System.Drawing.Point(111, 496);
            this.tbxDelivery.Name = "tbxDelivery";
            this.tbxDelivery.ReadOnly = true;
            this.tbxDelivery.Size = new System.Drawing.Size(165, 22);
            this.tbxDelivery.TabIndex = 9;
            this.tbxDelivery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(785, 551);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(255, 36);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(17, 526);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(91, 16);
            this.labelTotal.TabIndex = 7;
            this.labelTotal.Text = "Сумма ПКО";
            // 
            // tbxTotalAmount
            // 
            this.tbxTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxTotalAmount.Location = new System.Drawing.Point(111, 523);
            this.tbxTotalAmount.Name = "tbxTotalAmount";
            this.tbxTotalAmount.ReadOnly = true;
            this.tbxTotalAmount.Size = new System.Drawing.Size(165, 22);
            this.tbxTotalAmount.TabIndex = 6;
            this.tbxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnPrintDelivery
            // 
            this.btnPrintDelivery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintDelivery.Location = new System.Drawing.Point(19, 551);
            this.btnPrintDelivery.Name = "btnPrintDelivery";
            this.btnPrintDelivery.Size = new System.Drawing.Size(255, 36);
            this.btnPrintDelivery.TabIndex = 5;
            this.btnPrintDelivery.Text = "Печать";
            this.btnPrintDelivery.UseVisualStyleBackColor = true;
            this.btnPrintDelivery.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // dataGridViewReceipt
            // 
            this.dataGridViewReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReceipt.Location = new System.Drawing.Point(19, 31);
            this.dataGridViewReceipt.MultiSelect = false;
            this.dataGridViewReceipt.Name = "dataGridViewReceipt";
            this.dataGridViewReceipt.Size = new System.Drawing.Size(1089, 449);
            this.dataGridViewReceipt.TabIndex = 0;
            // 
            // resultViewBindingSource
            // 
            this.resultViewBindingSource.DataSource = typeof(CashJournalModel.ResultView);
            // 
            // btnPrintView
            // 
            this.btnPrintView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrintView.Location = new System.Drawing.Point(275, 551);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(255, 36);
            this.btnPrintView.TabIndex = 12;
            this.btnPrintView.Text = "Просмотр";
            this.btnPrintView.UseVisualStyleBackColor = true;
            // 
            // ReceiptUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 647);
            this.Controls.Add(this.gbxReceiptUI);
            this.Name = "ReceiptUI";
            this.ShowIcon = false;
            this.Text = "Кассовый чек";
            this.Load += new System.EventHandler(this.ReceiptUI_Load);
            this.gbxReceiptUI.ResumeLayout(false);
            this.gbxReceiptUI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxReceiptUI;
        private System.Windows.Forms.DataGridView dataGridViewReceipt;
        private System.Windows.Forms.BindingSource resultViewBindingSource;
        private System.Windows.Forms.Button btnPrintDelivery;
        private System.Windows.Forms.TextBox tbxTotalAmount;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelDelivery;
        private System.Windows.Forms.TextBox tbxDelivery;
        private System.Windows.Forms.Button btnDistribution;
        private System.Windows.Forms.Button btnPrintView;
    }
}