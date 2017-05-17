using System.Windows.Forms;

namespace CashJournal.view
{
    partial class ConnectProgress
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
            this.labelSign = new System.Windows.Forms.Label();
            this.sapProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelSign
            // 
            this.labelSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSign.AutoSize = true;
            this.labelSign.Location = new System.Drawing.Point(44, 20);
            this.labelSign.Name = "labelSign";
            this.labelSign.Size = new System.Drawing.Size(235, 16);
            this.labelSign.TabIndex = 0;
            this.labelSign.Text = "Соединение с системой SAP ...";
            // 
            // sapProgress
            // 
            this.sapProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.sapProgress.Location = new System.Drawing.Point(47, 40);
            this.sapProgress.Name = "sapProgress";
            this.sapProgress.Size = new System.Drawing.Size(360, 23);
            this.sapProgress.TabIndex = 1;
            // 
            // ConnectProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 114);
            this.Controls.Add(this.sapProgress);
            this.Controls.Add(this.labelSign);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectProgress";
            this.ShowIcon = false;
            this.Text = "ConnectProgress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSign;
        private System.Windows.Forms.ProgressBar sapProgress;

        // setters and getters
        public ProgressBar GetProgressBar()
        {
            return sapProgress;
        }
    }
}