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
            this.hScrollBar = new VisualSAIStudio.SkinableControls.SkinableHScrollBar();
            this.vScrollBar = new VisualSAIStudio.SkinableControls.SkinableVScrollBar();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.hScrollBar.BigStep = 20;
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 443);
            this.hScrollBar.Maximum = 100;
            this.hScrollBar.Minimum = 0;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(842, 17);
            this.hScrollBar.SmallStep = 5;
            this.hScrollBar.TabIndex = 1;
            this.hScrollBar.Value = 0;
            this.hScrollBar.ValueChanged += new System.EventHandler(this.hScrollBar_ValueChanged);
            // 
            // vScrollBar
            // 
            this.vScrollBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.vScrollBar.BigStep = 20;
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(825, 0);
            this.vScrollBar.Maximum = 100;
            this.vScrollBar.Minimum = 0;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 443);
            this.vScrollBar.SmallStep = 5;
            this.vScrollBar.TabIndex = 2;
            this.vScrollBar.Value = 0;
            this.vScrollBar.ValueChanged += new System.EventHandler(this.vScrollBar_ValueChanged);
            // 
            // Scratch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.hScrollBar);
            this.Name = "Scratch";
            this.Size = new System.Drawing.Size(842, 460);
            this.Load += new System.EventHandler(this.Scratch_Load);
            this.SizeChanged += new System.EventHandler(this.Scratch_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Scratch_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Scratch_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableControls.SkinableHScrollBar hScrollBar;
        private SkinableControls.SkinableVScrollBar vScrollBar;
    }
}
