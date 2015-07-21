namespace VisualSAIStudio
{
    partial class Scratch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(825, 0);
            this.vScrollBar.Maximum = 1000;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 460);
            this.vScrollBar.SmallChange = 10;
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.Tag = false;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 443);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(825, 17);
            this.hScrollBar.TabIndex = 1;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            this.hScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar_ValueChanged);
            // 
            // Scratch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Name = "Scratch";
            this.Size = new System.Drawing.Size(842, 460);
            this.Load += new System.EventHandler(this.Scratch_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Scratch_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
    }
}
