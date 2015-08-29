namespace VisualSAIStudio
{
    partial class StartPage
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
            this.LoadCreature = new System.Windows.Forms.LinkLabel();
            this.LoadGameObject = new System.Windows.Forms.LinkLabel();
            this.LoadAreaTrigger = new System.Windows.Forms.LinkLabel();
            this.LoadTimedActionList = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // LoadCreature
            // 
            this.LoadCreature.AutoSize = true;
            this.LoadCreature.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadCreature.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadCreature.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LoadCreature.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadCreature.Location = new System.Drawing.Point(59, 146);
            this.LoadCreature.Name = "LoadCreature";
            this.LoadCreature.Size = new System.Drawing.Size(124, 19);
            this.LoadCreature.TabIndex = 0;
            this.LoadCreature.TabStop = true;
            this.LoadCreature.Text = "Load creature SAI";
            this.LoadCreature.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadCreature_LinkClicked);
            // 
            // LoadGameObject
            // 
            this.LoadGameObject.AutoSize = true;
            this.LoadGameObject.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadGameObject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadGameObject.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LoadGameObject.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadGameObject.Location = new System.Drawing.Point(59, 170);
            this.LoadGameObject.Name = "LoadGameObject";
            this.LoadGameObject.Size = new System.Drawing.Size(145, 19);
            this.LoadGameObject.TabIndex = 1;
            this.LoadGameObject.TabStop = true;
            this.LoadGameObject.Text = "Load gameobject SAI";
            this.LoadGameObject.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadGameObject_LinkClicked);
            // 
            // LoadAreaTrigger
            // 
            this.LoadAreaTrigger.AutoSize = true;
            this.LoadAreaTrigger.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadAreaTrigger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadAreaTrigger.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LoadAreaTrigger.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadAreaTrigger.Location = new System.Drawing.Point(59, 194);
            this.LoadAreaTrigger.Name = "LoadAreaTrigger";
            this.LoadAreaTrigger.Size = new System.Drawing.Size(143, 19);
            this.LoadAreaTrigger.TabIndex = 2;
            this.LoadAreaTrigger.TabStop = true;
            this.LoadAreaTrigger.Text = "Load areatrigger SAI";
            // 
            // LoadTimedActionList
            // 
            this.LoadTimedActionList.AutoSize = true;
            this.LoadTimedActionList.Font = new System.Drawing.Font("Calibri Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadTimedActionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadTimedActionList.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.LoadTimedActionList.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(90)))), ((int)(((byte)(160)))));
            this.LoadTimedActionList.Location = new System.Drawing.Point(59, 216);
            this.LoadTimedActionList.Name = "LoadTimedActionList";
            this.LoadTimedActionList.Size = new System.Drawing.Size(149, 19);
            this.LoadTimedActionList.TabIndex = 3;
            this.LoadTimedActionList.TabStop = true;
            this.LoadTimedActionList.Text = "Load timed action list";
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(790, 475);
            this.Controls.Add(this.LoadTimedActionList);
            this.Controls.Add(this.LoadAreaTrigger);
            this.Controls.Add(this.LoadGameObject);
            this.Controls.Add(this.LoadCreature);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "StartPage";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Unknown;
            this.Text = "Start Page";
            this.Load += new System.EventHandler(this.StartPage_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StartPage_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel LoadCreature;
        private System.Windows.Forms.LinkLabel LoadGameObject;
        private System.Windows.Forms.LinkLabel LoadAreaTrigger;
        private System.Windows.Forms.LinkLabel LoadTimedActionList;
    }
}