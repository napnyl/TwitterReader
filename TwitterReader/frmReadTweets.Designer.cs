namespace TwitterReader
{
    partial class frmReadTweets
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
            this.scrollRead = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblReadTweets = new DevExpress.XtraEditors.LabelControl();
            this.scrollRead.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollRead
            // 
            this.scrollRead.Controls.Add(this.lblReadTweets);
            this.scrollRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollRead.Location = new System.Drawing.Point(0, 0);
            this.scrollRead.Name = "scrollRead";
            this.scrollRead.Size = new System.Drawing.Size(284, 261);
            this.scrollRead.TabIndex = 1;
            // 
            // lblReadTweets
            // 
            this.lblReadTweets.Location = new System.Drawing.Point(12, 12);
            this.lblReadTweets.Name = "lblReadTweets";
            this.lblReadTweets.Size = new System.Drawing.Size(0, 13);
            this.lblReadTweets.TabIndex = 0;
            // 
            // frmReadTweets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.scrollRead);
            this.Name = "frmReadTweets";
            this.Text = "frmReadTweets";
            this.scrollRead.ResumeLayout(false);
            this.scrollRead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lblReadTweets;
        public DevExpress.XtraEditors.XtraScrollableControl scrollRead;

    }
}