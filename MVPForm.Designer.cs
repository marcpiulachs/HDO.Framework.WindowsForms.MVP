namespace HDO.Framework.WindowsForms.MVP
{
    partial class MVPForm
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
            this.SuspendLayout();
            // 
            // MVPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 256);
            this.Name = "MVPForm";
            this.Text = "MVPForm";
            this.Activated += new System.EventHandler(this.MVPForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MVPForm_FormClosed);
            this.Load += new System.EventHandler(this.MVPForm_Load);
            this.Shown += new System.EventHandler(this.MVPForm_Shown);
            this.Enter += new System.EventHandler(this.MVPForm_Enter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}