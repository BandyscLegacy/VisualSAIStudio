namespace VisualSAIStudio.Forms
{
    partial class SessionsForm
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
            this.components = new System.ComponentModel.Container();
            this.toolBox4 = new VisualSAIStudio.SkinableControls.ToolBox();
            this.contextMenuStrip = new VisualSAIStudio.SkinableControls.SkinableContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBox4
            // 
            this.toolBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBox4.BackColor = System.Drawing.Color.White;
            this.toolBox4.DrawTag = false;
            this.toolBox4.Indent = 17;
            this.toolBox4.ItemPadding = new System.Windows.Forms.Padding(3);
            this.toolBox4.Location = new System.Drawing.Point(15, 22);
            this.toolBox4.Margin = new System.Windows.Forms.Padding(0);
            this.toolBox4.Name = "toolBox4";
            this.toolBox4.ShowTooltip = false;
            this.toolBox4.Size = new System.Drawing.Size(618, 879);
            this.toolBox4.TabIndex = 2;
            this.toolBox4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolBox4_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip.ForeColor = System.Drawing.Color.Black;
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(118, 48);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // SessionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 378);
            this.Controls.Add(this.toolBox4);
            this.HeaderSubText = "\"Sessions\" are group of opene SAI windows. You can save them for later edit.";
            this.HeaderText = "Sessions";
            this.Name = "SessionsForm";
            this.ShowHeader = true;
            this.Text = "Sessions manager";
            this.Load += new System.EventHandler(this.SessionsForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableControls.ToolBox toolBox4;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private SkinableControls.SkinableContextMenuStrip contextMenuStrip;
    }
}