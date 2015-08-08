using VisualSAIStudio.SkinableControls;
namespace VisualSAIStudio.Forms
{
    partial class CodePreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodePreview));
            this.text = new VisualSAIStudio.SkinableControls.SkinableFastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.text)).BeginInit();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.text.AutoScrollMinSize = new System.Drawing.Size(59, 14);
            this.text.CharHeight = 14;
            this.text.CharWidth = 8;
            this.text.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.text.IsReplaceMode = false;
            this.text.Language = FastColoredTextBoxNS.Language.SQL;
            this.text.Location = new System.Drawing.Point(0, 0);
            this.text.Name = "text";
            this.text.Paddings = new System.Windows.Forms.Padding(0);
            this.text.Size = new System.Drawing.Size(284, 261);
            this.text.TabIndex = 0;
            this.text.Text = "dfdf";
            this.text.Zoom = 100;
            // 
            // CodePreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.text);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "CodePreview";
            this.Text = "SQLPreview";
            this.Load += new System.EventHandler(this.SQLPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.text)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SkinableFastColoredTextBox text;
    }
}