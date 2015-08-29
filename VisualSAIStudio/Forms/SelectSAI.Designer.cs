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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.listView = new BrightIdeasSoftware.FastObjectListView();
            this.EntryOrGuid = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.nName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cmbType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.listView)).BeginInit();
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
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // listView
            // 
            this.listView.AllColumns.Add(this.EntryOrGuid);
            this.listView.AllColumns.Add(this.nName);
            this.listView.AllColumns.Add(this.olvColumn1);
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EntryOrGuid,
            this.nName,
            this.olvColumn1});
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(12, 37);
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(397, 309);
            this.listView.TabIndex = 8;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.VirtualMode = true;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // EntryOrGuid
            // 
            this.EntryOrGuid.AspectName = "entry";
            this.EntryOrGuid.Text = "EntryOrGuid";
            this.EntryOrGuid.Width = 111;
            // 
            // nName
            // 
            this.nName.AspectName = "name";
            this.nName.Text = "Name";
            this.nName.Width = 125;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "sai";
            this.olvColumn1.Text = "Has SAI";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Entry",
            "GUID"});
            this.cmbType.Location = new System.Drawing.Point(12, 11);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(103, 21);
            this.cmbType.TabIndex = 7;
            this.cmbType.Text = "Entry";
            // 
            // SelectSAI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 387);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnOk);
            this.Name = "SelectSAI";
            this.Text = "SelectSAI";
            this.Load += new System.EventHandler(this.SelectSAI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnOk;
        private BrightIdeasSoftware.FastObjectListView listView;
        private System.Windows.Forms.ComboBox cmbType;
        private BrightIdeasSoftware.OLVColumn EntryOrGuid;
        private BrightIdeasSoftware.OLVColumn nName;
        private BrightIdeasSoftware.OLVColumn olvColumn1;


    }
}