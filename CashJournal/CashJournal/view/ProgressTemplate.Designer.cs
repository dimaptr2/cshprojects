using System.Windows.Forms;

namespace CashJournalPrinting.view
{
    partial class ProgressTemplate
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
            this.outputProgress = new System.Windows.Forms.ProgressBar();
            this.labelSign = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // outputProgress
            // 
            this.outputProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputProgress.Location = new System.Drawing.Point(47, 65);
            this.outputProgress.Name = "outputProgress";
            this.outputProgress.Size = new System.Drawing.Size(360, 23);
            this.outputProgress.TabIndex = 3;
            // 
            // labelSign
            // 
            this.labelSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSign.AutoSize = true;
            this.labelSign.Location = new System.Drawing.Point(44, 45);
            this.labelSign.Name = "labelSign";
            this.labelSign.Size = new System.Drawing.Size(188, 15);
            this.labelSign.TabIndex = 2;
            this.labelSign.Text = "Выполняется обработка ...";
            // 
            // ProgressTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 164);
            this.Controls.Add(this.outputProgress);
            this.Controls.Add(this.labelSign);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressTemplate";
            this.ShowIcon = false;
            this.Text = "?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar outputProgress;
        private System.Windows.Forms.Label labelSign;

        public ProgressBar OutputProgress { get => outputProgress; }

    }
}