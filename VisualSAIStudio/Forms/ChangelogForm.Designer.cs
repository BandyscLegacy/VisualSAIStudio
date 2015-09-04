namespace VisualSAIStudio.Forms
{
    partial class ChangelogForm
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
            this.webBrowser1 = new VisualSAIStudio.SkinableControls.SkinableWebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(437, 463);
            this.webBrowser1.TabIndex = 0;
            // 
            // ChangelogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 463);
            this.Controls.Add(this.webBrowser1);
            this.HeaderSubText = "Check if new updates are worth installing :)";
            this.HeaderText = "Changelog";
            this.Name = "ChangelogForm";
            this.ShowHeader = true;
            this.Text = "Changelog";
            this.Load += new System.EventHandler(this.ChangelogForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableControls.SkinableWebBrowser webBrowser1;
    }
}