using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSAIStudio.SkinableControls
{
    public class SkinableHScrollBar : SkinableVScrollBar
    {
        public SkinableHScrollBar()
            : base()
        {
            arrowTop[0] = new Point(4, 8);
            arrowTop[1] = new Point(10, 13);
            arrowTop[2] = new Point(10, 3);

            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(16, 0);
            this.panel1.Size = new System.Drawing.Size(769, 203);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // drag
            // 
            this.drag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.drag.Location = new System.Drawing.Point(0, 3);
            this.drag.Size = new System.Drawing.Size(100, 197);
            // 
            // SkinableHScrollBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "SkinableHScrollBar";
            this.Size = new System.Drawing.Size(801, 203);
            this.Load += new System.EventHandler(this.SkinableHScroll_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override Point GetNewLocation(Point now)
        {
            return new Point(Math.Min(Math.Max(0, startDragLocation.X + (now.X - startDragPoint.X)), (panel1.Size.Width - drag.Size.Width)), drag.Location.Y);
        }

        protected override void PosToValue()
        {
            Value = (int)(drag.Location.X * 1f / (panel1.Size.Width - drag.Size.Width) * (Maximum - Minimum) + Minimum);
        }

        protected override void UpdateDragFromValue()
        {
            drag.Location = new Point((int)((Value - Minimum) * 1f / (Maximum - Minimum) * (panel1.Size.Width - drag.Size.Width)), drag.Location.Y);
        }

        protected override void UpdateDragSize()
        {
            drag.Size = new Size((int)(0.2f * this.Width),drag.Size.Height );
        }

        protected override void RecalcBottomArrow(Rectangle clip)
        {
            arrowBottom[0] = new Point(clip.Width - 4, 8);
            arrowBottom[1] = new Point(clip.Width - 10, 13);
            arrowBottom[2] = new Point(clip.Width - 10, 3);
        }

        private void InitializeComponent()
        {


        }

        private void SkinableHScroll_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
    }
}
