namespace VisualSAIStudio
{
    partial class ScratchWindow
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
            this.scratch1 = new VisualSAIStudio.SkinableControls.SkinableScratch();
            this.SuspendLayout();
            // 
            // scratch1
            // 
            this.scratch1.AllowDrop = true;
            this.scratch1.BackColor = System.Drawing.Color.White;
            this.scratch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scratch1.Location = new System.Drawing.Point(0, 0);
            this.scratch1.Name = "scratch1";
            this.scratch1.Size = new System.Drawing.Size(284, 261);
            this.scratch1.TabIndex = 0;
            this.scratch1.Load += new System.EventHandler(this.scratch1_Load);
            this.scratch1.DragDrop += new System.Windows.Forms.DragEventHandler(this.scratch1_DragDrop);
            this.scratch1.DragOver += new System.Windows.Forms.DragEventHandler(this.scratch1_DragOver);
            this.scratch1.DragLeave += new System.EventHandler(this.scratch1_DragLeave);
            this.scratch1.Paint += new System.Windows.Forms.PaintEventHandler(this.scratch1_Paint);
            this.scratch1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scratch1_MouseDown);
            // 
            // ScratchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.scratch1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ScratchWindow";
            this.Text = "ScratchWindow";
            this.Load += new System.EventHandler(this.ScratchWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableControls.SkinableScratch scratch1;
    }
}