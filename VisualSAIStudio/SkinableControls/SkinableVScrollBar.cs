using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking.Themes;
using WeifenLuo.WinFormsUI.Docking.Colors;

namespace VisualSAIStudio.SkinableControls
{
    public partial class SkinableVScrollBar : UserControl, WeifenLuo.WinFormsUI.Docking.IReloadable
    {
        public event  EventHandler ValueChanged = delegate { };

        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = Math.Min(Maximum, Math.Max(Minimum, value));
                ValueChanged(this, new EventArgs());
                UpdateDragFromValue();
            }
        }
        private int _Maximum;
        private int _Minimum;
        public int Maximum 
        {
            get
            {
                return _Maximum;
            }
            set
            {
                _Maximum = value;
                if (!dragging)
                    UpdateDragFromValue();
                this.Refresh();
            }
        }
        public int Minimum 
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
                UpdateDragFromValue();
                this.Refresh();
            }
        }
        public int SmallStep { get; set; }
        public int BigStep { get; set; }

        private bool dragging = false;
        protected Point startDragPoint;
        protected Point startDragLocation;

        private bool mouseHovering;
        private bool mouseDown;

        protected Point[] arrowTop = new Point[3];
        protected Point[] arrowBottom = new Point[3];


        private Color DragColor;
        private Color DragHoverColor;
        private Color DragDownColor;
        private Brush ArrowColor;
        private Brush ArrowHoverColor;
        private Brush ArrowDownColor;

        public SkinableVScrollBar()
        {
            InitializeComponent();
            Value = 0;
            Minimum = 0;
            Maximum = 100;
            SmallStep = 5;
            BigStep = 20;
 
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            ThemeMgr.Instance.RegisterControl(this);
            ReloadTheme();

            arrowTop[0] = new Point(8, 4);
            arrowTop[1] = new Point(13, 10);
            arrowTop[2] = new Point(3, 10);

        }

        ~SkinableVScrollBar()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        private void SkinableVScrollBar_Load(object sender, EventArgs e)
        {

        }


        private void drag_MouseDown(object sender, MouseEventArgs e)
        {
            drag.BackColor = DragDownColor;
            dragging = true;
            startDragPoint = panel1.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
            startDragLocation = drag.Location;
        }

        private void drag_MouseUp(object sender, MouseEventArgs e)
        {
            drag.BackColor = DragHoverColor;
            dragging = false;
        }

        private void drag_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point now = panel1.PointToClient(new Point(Cursor.Position.X, Cursor.Position.Y));
                drag.Location = GetNewLocation(now);
                PosToValue();
            }
        }

        protected virtual Point GetNewLocation(Point now)
        {
            return new Point(drag.Location.X, Math.Min(Math.Max(0, startDragLocation.Y + (now.Y - startDragPoint.Y)), (panel1.Size.Height - drag.Size.Height)));
        }

        protected virtual void PosToValue()
        {
            _value = (int)(drag.Location.Y * 1f / (panel1.Size.Height - drag.Size.Height) * (Maximum - Minimum) + Minimum);
            ValueChanged(this, new EventArgs());
        }

        private int Multipler;
        private void SkinableVScrollBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < panel1.Location.Y)
            {
                Multipler = -1;
                Value -= SmallStep;
            }
            else if (e.Y > panel1.Location.Y + panel1.Size.Height)
            {
                Multipler = 1;
                Value += SmallStep;
            }
            ScrollDown.Enabled = true;
            UpdateDragFromValue();
            mouseDown = true;
            this.Refresh();
        }

        protected virtual void UpdateDragFromValue()
        {
            drag.Location = new Point(drag.Location.X, (int)((Value - Minimum) * 1f / (Maximum - Minimum) * (panel1.Size.Height - drag.Size.Height)));
        }

        private void SkinableVScrollBar_MouseUp(object sender, MouseEventArgs e)
        {
            ScrollDown.Enabled = false;
            mouseDown = false;
            this.Refresh();
        }

        private void ScrollDown_Tick(object sender, EventArgs e)
        {
            Value += SmallStep * Multipler;
            UpdateDragFromValue();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Y < panel1.Location.Y)
                Value -= BigStep;
            else if (e.Y > panel1.Location.Y + panel1.Size.Height)
                Value += BigStep;
            UpdateDragFromValue();
        }

        protected virtual void RecalcBottomArrow(Rectangle clip)
        {
            arrowBottom[0] = new Point(8, clip.Height - 4);
            arrowBottom[1] = new Point(13, clip.Height - 10);
            arrowBottom[2] = new Point(3, clip.Height - 10);
        }

        private void SkinableVScrollBar_Paint(object sender, PaintEventArgs e)
        {
            RecalcBottomArrow(e.ClipRectangle);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Point mouse = this.PointToClient(Cursor.Position);

            Brush brush = ArrowColor;
            if (mouse.Y < 16)
            {
                if (mouseDown)
                    brush = ArrowDownColor;
                else if (mouseHovering)
                    brush = ArrowHoverColor;
            }

            e.Graphics.FillPolygon(brush, arrowTop);

            brush = ArrowColor;

            if (mouse.Y > e.ClipRectangle.Height - 16)
            {
                if (mouseDown)
                    brush = ArrowDownColor;
                else if (mouseHovering)
                    brush = ArrowHoverColor;
            }
            e.Graphics.FillPolygon(brush, arrowBottom);

        }

        private void SkinableVScrollBar_MouseEnter(object sender, EventArgs e)
        {
            mouseHovering = true;
            this.Refresh();
        }

        private void SkinableVScrollBar_MouseLeave(object sender, EventArgs e)
        {
            mouseHovering = false;
            this.Refresh();
        }

        private void drag_MouseEnter(object sender, EventArgs e)
        {
            drag.BackColor = DragHoverColor;
        }

        private void drag_MouseLeave(object sender, EventArgs e)
        {
            drag.BackColor = DragColor;
        }

        private void SkinableVScrollBar_Resize(object sender, EventArgs e)
        {
            UpdateDragFromValue();
            UpdateDragSize();
            this.Refresh();
        }

        protected virtual void UpdateDragSize()
        {
            drag.Size = new Size(drag.Size.Width, (int)(0.2f * this.Height));
        }


        public void ReloadTheme()
        {
            BackColor = ThemeMgr.Instance.getColor(IKnownColors.ScrollBarBackground);

            ArrowColor = new SolidBrush(ThemeMgr.Instance.getColor(IKnownColors.ScrollBarArrowBackground));
            ArrowHoverColor = new SolidBrush(ThemeMgr.Instance.getColor(IKnownColors.ScrollBarArrowHoverBackground));
            ArrowDownColor = new SolidBrush(ThemeMgr.Instance.getColor(IKnownColors.ScrollBarArrowDownBackground));

            DragColor = ThemeMgr.Instance.getColor(IKnownColors.ScrollBarDragBackground);
            DragHoverColor = ThemeMgr.Instance.getColor(IKnownColors.ScrollBarDragHoverBackground);
            DragDownColor = ThemeMgr.Instance.getColor(IKnownColors.ScrollBarDragDownBackground);
            this.Refresh();
        }

        private void drag_Click(object sender, EventArgs e)
        {

        }
    }
}
