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
            this.dataGridViewReceipt = new System.Windows.Forms.DataGridView();
            this.resultViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.materialNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountPerUnitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tbxTotalAmount = new System.Windows.Forms.TextBox();
            this.labelTotal = new System.Windows.Forms.Label();
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
            this.gbxReceiptUI.Controls.Add(this.labelTotal);
            this.gbxReceiptUI.Controls.Add(this.tbxTotalAmount);
            this.gbxReceiptUI.Controls.Add(this.btnPrint);
            this.gbxReceiptUI.Controls.Add(this.dataGridViewReceipt);
            this.gbxReceiptUI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbxReceiptUI.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gbxReceiptUI.Location = new System.Drawing.Point(12, 12);
            this.gbxReceiptUI.Name = "gbxReceiptUI";
            this.gbxReceiptUI.Size = new System.Drawing.Size(750, 623);
            this.gbxReceiptUI.TabIndex = 0;
            this.gbxReceiptUI.TabStop = false;
            this.gbxReceiptUI.Text = "Просмотр чека";
            // 
            // dataGridViewReceipt
            // 
            this.dataGridViewReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReceipt.AutoGenerateColumns = false;
            this.dataGridViewReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReceipt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.materialNameDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.amountPerUnitDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn});
            this.dataGridViewReceipt.DataSource = this.resultViewBindingSource;
            this.dataGridViewReceipt.Location = new System.Drawing.Point(19, 31);
            this.dataGridViewReceipt.Name = "dataGridViewReceipt";
            this.dataGridViewReceipt.Size = new System.Drawing.Size(716, 483);
            this.dataGridViewReceipt.TabIndex = 0;
            // 
            // resultViewBindingSource
            // 
            this.resultViewBindingSource.DataSource = typeof(CashJournalModel.ResultView);
            // 
            // materialNameDataGridViewTextBoxColumn
            // 
            this.materialNameDataGridViewTextBoxColumn.DataPropertyName = "MaterialName";
            this.materialNameDataGridViewTextBoxColumn.HeaderText = "Материал";
            this.materialNameDataGridViewTextBoxColumn.Name = "materialNameDataGridViewTextBoxColumn";
            this.materialNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Количество";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.Width = 120;
            // 
            // amountPerUnitDataGridViewTextBoxColumn
            // 
            this.amountPerUnitDataGridViewTextBoxColumn.DataPropertyName = "AmountPerUnit";
            this.amountPerUnitDataGridViewTextBoxColumn.HeaderText = "Цена";
            this.amountPerUnitDataGridViewTextBoxColumn.Name = "amountPerUnitDataGridViewTextBoxColumn";
            this.amountPerUnitDataGridViewTextBoxColumn.Width = 120;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "Сумма";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.Width = 120;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(19, 551);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(255, 36);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tbxTotalAmount
            // 
            this.tbxTotalAmount.Location = new System.Drawing.Point(109, 523);
            this.tbxTotalAmount.Name = "tbxTotalAmount";
            this.tbxTotalAmount.ReadOnly = true;
            this.tbxTotalAmount.Size = new System.Drawing.Size(268, 22);
            this.tbxTotalAmount.TabIndex = 6;
            this.tbxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(17, 526);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(82, 16);
            this.labelTotal.TabIndex = 7;
            this.labelTotal.Text = "Сумма ПО";
            // 
            // ReceiptUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 647);
            this.Controls.Add(this.gbxReceiptUI);
            this.Name = "ReceiptUI";
            this.ShowIcon = false;
            this.Text = "Кассовый чек";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn materialNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountPerUnitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox tbxTotalAmount;
        private System.Windows.Forms.Label labelTotal;
    }
}