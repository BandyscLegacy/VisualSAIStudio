namespace VisualSAIStudio.SkinableControls
{
    partial class ToolBox
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
            this.components = new System.ComponentModel.Container();
            this.vScroll = new VisualSAIStudio.SkinableControls.SkinableVScrollBar();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // vScroll
            // 
            this.vScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.vScroll.BigStep = 20;
            this.vScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScroll.Location = new System.Drawing.Point(309, 0);
            this.vScroll.Maximum = 100;
            this.vScroll.Minimum = 0;
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(16, 197);
            this.vScroll.SmallStep = 5;
            this.vScroll.TabIndex = 0;
            this.vScroll.Value = 0;
            this.vScroll.ValueChanged += new System.EventHandler(this.vScroll_ValueChanged);
            this.vScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScroll_Scroll);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ToolBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScroll);
            this.Name = "ToolBox";
            this.Size = new System.Drawing.Size(325, 197);
            this.Load += new System.EventHandler(this.ToolBox_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolBox_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ToolBox_MouseDown);
            this.MouseLeave += new System.EventHandler(this.ToolBox_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ToolBox_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableVScrollBar vScroll;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
