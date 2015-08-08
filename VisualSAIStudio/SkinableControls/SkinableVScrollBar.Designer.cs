namespace VisualSAIStudio.SkinableControls
{
    partial class SkinableVScrollBar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.drag = new System.Windows.Forms.Label();
            this.ScrollDown = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.drag);
            this.panel1.Location = new System.Drawing.Point(0, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(16, 346);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // drag
            // 
            this.drag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.drag.BackColor = System.Drawing.SystemColors.ControlDark;
            this.drag.Location = new System.Drawing.Point(3, 104);
            this.drag.Name = "drag";
            this.drag.Size = new System.Drawing.Size(10, 76);
            this.drag.TabIndex = 1;
            this.drag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drag_MouseDown);
            this.drag.MouseEnter += new System.EventHandler(this.drag_MouseEnter);
            this.drag.MouseLeave += new System.EventHandler(this.drag_MouseLeave);
            this.drag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drag_MouseMove);
            this.drag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drag_MouseUp);
            // 
            // ScrollDown
            // 
            this.ScrollDown.Interval = 200;
            this.ScrollDown.Tick += new System.EventHandler(this.ScrollDown_Tick);
            // 
            // SkinableVScrollBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SkinableVScrollBar";
            this.Size = new System.Drawing.Size(16, 378);
            this.Load += new System.EventHandler(this.SkinableVScrollBar_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SkinableVScrollBar_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SkinableVScrollBar_MouseDown);
            this.MouseEnter += new System.EventHandler(this.SkinableVScrollBar_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.SkinableVScrollBar_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SkinableVScrollBar_MouseUp);
            this.Resize += new System.EventHandler(this.SkinableVScrollBar_Resize);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label drag;
        protected System.Windows.Forms.Timer ScrollDown;
    }
}
