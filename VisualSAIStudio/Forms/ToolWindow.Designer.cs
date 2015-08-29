namespace VisualSAIStudio
{
    partial class ToolWindow
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
            this.scroll = new VisualSAIStudio.SkinableControls.SkinableVScrollBar();
            this.treeView = new VisualSAIStudio.SkinableControls.SkinableTreeView();
            this.textBox = new VisualSAIStudio.SkinableControls.SkinableTextBox();
            this.SuspendLayout();
            // 
            // scroll
            // 
            this.scroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.scroll.BigStep = 1;
            this.scroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.scroll.Location = new System.Drawing.Point(268, 13);
            this.scroll.Maximum = 100;
            this.scroll.Minimum = 0;
            this.scroll.Name = "scroll";
            this.scroll.Size = new System.Drawing.Size(16, 248);
            this.scroll.SmallStep = 1;
            this.scroll.TabIndex = 2;
            this.scroll.Value = 0;
            this.scroll.ValueChanged += new System.EventHandler(this.scroll_ValueChanged);
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Indent = 7;
            this.treeView.Location = new System.Drawing.Point(0, 13);
            this.treeView.Name = "treeView";
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(284, 248);
            this.treeView.TabIndex = 1;
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterCollapse);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterExpand);
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.NormalColor = System.Drawing.Color.Black;
            this.textBox.Placeholder = null;
            this.textBox.PlaceholderColor = System.Drawing.Color.Gray;
            this.textBox.Size = new System.Drawing.Size(284, 13);
            this.textBox.TabIndex = 0;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // ToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.scroll);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.textBox);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "ToolWindow";
            this.Text = "ToolWindow";
            this.Load += new System.EventHandler(this.ToolWindow_Load);
            this.Resize += new System.EventHandler(this.ToolWindow_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualSAIStudio.SkinableControls.SkinableTextBox textBox;
        private VisualSAIStudio.SkinableControls.SkinableTreeView treeView;
        private SkinableControls.SkinableVScrollBar scroll;
    }
}