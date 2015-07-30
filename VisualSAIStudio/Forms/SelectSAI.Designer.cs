namespace VisualSAIStudio
{
    partial class SelectSAI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.treeView1 = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cmbType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(121, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(288, 20);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(334, 352);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 20);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.AllColumns.Add(this.olvColumn1);
            this.treeView1.AllColumns.Add(this.olvColumn2);
            this.treeView1.AllColumns.Add(this.olvColumn3);
            this.treeView1.AllColumns.Add(this.olvColumn4);
            this.treeView1.AllColumns.Add(this.olvColumn5);
            this.treeView1.AllColumns.Add(this.olvColumn6);
            this.treeView1.AllColumns.Add(this.olvColumn7);
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7});
            this.treeView1.FullRowSelect = true;
            this.treeView1.IsSearchOnSortColumn = false;
            this.treeView1.Location = new System.Drawing.Point(12, 37);
            this.treeView1.Name = "treeView1";
            this.treeView1.OwnerDraw = true;
            this.treeView1.ShowGroups = false;
            this.treeView1.Size = new System.Drawing.Size(399, 300);
            this.treeView1.TabIndex = 6;
            this.treeView1.UseCompatibleStateImageBehavior = false;
            this.treeView1.UseFiltering = true;
            this.treeView1.UseOverlays = false;
            this.treeView1.View = System.Windows.Forms.View.Details;
            this.treeView1.VirtualMode = true;
            this.treeView1.SelectedIndexChanged += new System.EventHandler(this.treeView1_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "id";
            this.olvColumn1.AutoCompleteEditor = false;
            this.olvColumn1.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Sortable = false;
            this.olvColumn1.Text = "Entry or GUID";
            this.olvColumn1.Width = 100;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "name";
            this.olvColumn2.AutoCompleteEditor = false;
            this.olvColumn2.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Sortable = false;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "map";
            this.olvColumn3.AutoCompleteEditor = false;
            this.olvColumn3.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Sortable = false;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "position_x";
            this.olvColumn4.AutoCompleteEditor = false;
            this.olvColumn4.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Sortable = false;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "position_y";
            this.olvColumn5.AutoCompleteEditor = false;
            this.olvColumn5.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Sortable = false;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "position_z";
            this.olvColumn6.AutoCompleteEditor = false;
            this.olvColumn6.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Sortable = false;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "smartAI";
            this.olvColumn7.AutoCompleteEditor = false;
            this.olvColumn7.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Sortable = false;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Name",
            "Entry",
            "GUID"});
            this.cmbType.Location = new System.Drawing.Point(12, 11);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(103, 21);
            this.cmbType.TabIndex = 7;
            this.cmbType.Text = "Name";
            // 
            // SelectSAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 387);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnOk);
            this.Name = "SelectSAI";
            this.Text = "SelectSAI";
            this.Load += new System.EventHandler(this.SelectSAI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnOk;
        private BrightIdeasSoftware.TreeListView treeView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.ComboBox cmbType;


    }
}