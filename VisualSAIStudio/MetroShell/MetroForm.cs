using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI.Docking.Colors;
using WeifenLuo.WinFormsUI.Docking.Themes;
using VisualSAIStudio.MetroShell;

namespace MetroForm
{
    public class MetroForm : Form, IReloadable
    {
        private Dwm.MARGINS dwmMargins;
        private bool _marginOk;

        private bool _aeroEnabled = false;

        public bool AeroEnabled
        {
            get { return _aeroEnabled; }
        }

        public static int LoWord(int dwValue)
        {
            return dwValue & 0xffff;
        }

        public static int HiWord(int dwValue)
        {
            return (dwValue >> 16) & 0xffff;
        }

        protected override void WndProc(ref Message m)
        {
            int WM_NCCALCSIZE = 0x83;
            int WM_NCHITTEST = 0x84;
            IntPtr result = default(IntPtr);

            int dwmHandled = Dwm.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, ref result);

            if (dwmHandled == 1)
            {
                m.Result = result;
                return;
            }

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                WinApi.NCCALCSIZE_PARAMS nccsp = (WinApi.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(WinApi.NCCALCSIZE_PARAMS));

                if (!_marginOk)
                {
                    //Set what client area would be for passing to DwmExtendIntoClientArea. Also remember that at least one of these values NEEDS TO BE > 1, else it won't work.
                    dwmMargins.cyTopHeight = 1;
                    dwmMargins.cxLeftWidth = 1;
                    dwmMargins.cyBottomHeight = 1;
                    dwmMargins.cxRightWidth = 1;
                    _marginOk = true;
                }

                Marshal.StructureToPtr(nccsp, m.LParam, false);

                m.Result = IntPtr.Zero;
            }
            else if (m.Msg == WM_NCHITTEST && m.Result.ToInt32() == 0)
            {
                m.Result = HitTestNCA(m.HWnd, m.WParam, m.LParam);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private IntPtr HitTestNCA(IntPtr hwnd, IntPtr wparam, IntPtr lparam)
        {
            //int HTNOWHERE = 0; // never used
            int HTCLIENT = 1;
            int HTCAPTION = 2;
            int HTGROWBOX = 4;
            int HTSIZE = HTGROWBOX;
            int HTMINBUTTON = 8;
            int HTMAXBUTTON = 9;
            int HTLEFT = 10;
            int HTRIGHT = 11;
            int HTTOP = 12;
            int HTTOPLEFT = 13;
            int HTTOPRIGHT = 14;
            int HTBOTTOM = 15;
            int HTBOTTOMLEFT = 16;
            int HTBOTTOMRIGHT = 17;
            int HTREDUCE = HTMINBUTTON;
            int HTZOOM = HTMAXBUTTON;
            int HTSIZEFIRST = HTLEFT;
            int HTSIZELAST = HTBOTTOMRIGHT;

            Point p = new Point(LoWord(lparam.ToInt32()), HiWord(lparam.ToInt32()));

            Rectangle topleft = RectangleToScreen(new Rectangle(0, 0, dwmMargins.cxLeftWidth, dwmMargins.cxLeftWidth));

            if (topleft.Contains(p))
            {
                return new IntPtr(HTTOPLEFT);
            }

            Rectangle topright = RectangleToScreen(new Rectangle(Width - dwmMargins.cxRightWidth, 0, dwmMargins.cxRightWidth, dwmMargins.cxRightWidth));

            if (topright.Contains(p))
            {
                return new IntPtr(HTTOPRIGHT);
            }

            Rectangle botleft = RectangleToScreen(new Rectangle(0, Height - dwmMargins.cyBottomHeight, dwmMargins.cxLeftWidth, dwmMargins.cyBottomHeight));

            if (botleft.Contains(p))
            {
                return new IntPtr(HTBOTTOMLEFT);
            }

            Rectangle botright = RectangleToScreen(new Rectangle(Width - dwmMargins.cxRightWidth, Height - dwmMargins.cyBottomHeight, dwmMargins.cxRightWidth, dwmMargins.cyBottomHeight));

            if (botright.Contains(p))
            {
                return new IntPtr(HTBOTTOMRIGHT);
            }

            Rectangle top = RectangleToScreen(new Rectangle(0, 0, Width, dwmMargins.cxLeftWidth));

            if (top.Contains(p))
            {
                return new IntPtr(HTTOP);
            }

            Rectangle cap = RectangleToScreen(new Rectangle(0, dwmMargins.cxLeftWidth, Width, dwmMargins.cyTopHeight - dwmMargins.cxLeftWidth));

            if (cap.Contains(p))
            {
                return new IntPtr(HTCAPTION);
            }

            Rectangle left = RectangleToScreen(new Rectangle(0, 0, dwmMargins.cxLeftWidth, Height));

            if (left.Contains(p))
            {
                return new IntPtr(HTLEFT);
            }

            Rectangle right = RectangleToScreen(new Rectangle(Width - dwmMargins.cxRightWidth, 0, dwmMargins.cxRightWidth, Height));

            if (right.Contains(p))
            {
                return new IntPtr(HTRIGHT);
            }

            Rectangle bottom = RectangleToScreen(new Rectangle(0, Height - dwmMargins.cyBottomHeight, Width, dwmMargins.cyBottomHeight));

            if (bottom.Contains(p))
            {
                return new IntPtr(HTBOTTOM);
            }

            return new IntPtr(HTCLIENT);
        }


        private const int BorderWidth = 6;
        private ResizeDirection _resizeDir = ResizeDirection.None;
        private ResizeDirection resizeDir
        {
            get { return _resizeDir; }
            set
            {
                _resizeDir = value;

                //Change cursor
                switch (value)
                {
                    case ResizeDirection.Left:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case ResizeDirection.Right:
                        this.Cursor = Cursors.SizeWE;
                        break;
                    case ResizeDirection.Top:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case ResizeDirection.Bottom:
                        this.Cursor = Cursors.SizeNS;
                        break;
                    case ResizeDirection.BottomLeft:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    case ResizeDirection.TopRight:
                        this.Cursor = Cursors.SizeNESW;
                        break;
                    case ResizeDirection.BottomRight:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    case ResizeDirection.TopLeft:
                        this.Cursor = Cursors.SizeNWSE;
                        break;
                    default:
                        this.Cursor = Cursors.Default;
                        break;
                }
            }
        }


        [SettingsBindable(true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Refresh();
            }
        }
        public string HeaderText { get; set; }
        public string HeaderSubText { get; set; }

        private Font headerFont;
        private Brush headerBrush;
        private Pen titlePen;
        private Brush titleBrush;
        private bool _ShowHeader;
        public bool ShowHeader {
            get
            {
                return _ShowHeader;
            }
            set
            {
                _ShowHeader = value;
                if (value)
                    content.Location = new Point(10, 100);
                else
                    content.Location = new Point(10, 28);
            }
        }
        protected Color _AccentColor;
        protected Pen AccentPen;
        protected Brush AccentBrush;
        public Color AccentColor
        {
            get
            {
                return _AccentColor;
            }
            set
            {
                _AccentColor = value;
                AccentBrush = new SolidBrush(value);
                AccentPen = new Pen(AccentBrush);
            }
        }
        private Bitmap IconResized;
        protected Panel content {get; private set;}
        private Pen WindowControlsPen { get; set; }
        public Color TitleColor { get; set; }
        public Font TitleFont { get; set; }
        //
        // Summary:
        //     Gets or sets the icon for the form.
        //
        // Returns:
        //     An System.Drawing.Icon that represents the icon for the form.
        [AmbientValue("")]
        [Localizable(true)]
        public new Icon Icon 
        {
            get
            {
                return base.Icon;
            }
            set
            {
                base.Icon = value;
                IconResized = new Icon(value, 16, 16).ToBitmap();
            }
        }
        //
        // Summary:
        //     Gets the collection of controls contained within the control.
        //
        // Returns:
        //     A System.Windows.Forms.Control.ControlCollection representing the collection
        //     of controls contained within the control.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new Control.ControlCollection Controls 
        {
            get
            {
                return content.Controls;
            }
        }
        public MetroForm()
            : base()
        {
            TitleColor = Color.Black;
            TitleFont = new Font("Tahoma", 9);
            headerFont = new Font("Calibri Light", 20, FontStyle.Regular);
            this.titleBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionForeColor));
            this.titlePen = new Pen(titleBrush);
            this.headerBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionBackColor));

            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            this.content = new System.Windows.Forms.Panel();
            this.content.Location = new System.Drawing.Point(10, 28);
            base.Controls.Add(this.content);
            this.Resize += Form1_Resized;
            this.Activated += Form1_Activated;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            this.Paint += MetroForm_Paint;
            this.MouseLeave += MetroForm_MouseLeave;
            this.AccentColor = Color.FromArgb(0, 122, 204);
            this.WindowControlsPen = new Pen(new SolidBrush(this.TitleColor));
            ThemeMgr.Instance.RegisterControl(this);
        }

        void MetroForm_MouseLeave(object sender, EventArgs e)
        {
            resizeDir = ResizeDirection.None;
            mouse.X = 0;
            this.Invalidate();
        }

        ~MetroForm()
        {
            ThemeMgr.Instance.UnregisterControl(this);
        }

        private Point mouse;
        private bool mouse_down;

        private void MetroForm_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            
            
            if (ShowHeader)
                e.Graphics.FillRectangle(headerBrush, 0, 0, this.Width, 100);


            if (mouse.Y < 26 && mouse.X > e.ClipRectangle.Width - 102)
            {
                if (mouse.X > e.ClipRectangle.Width - 34)
                {
                    e.Graphics.FillRectangle(mouse_down ? AccentBrush : Brushes.Gray, e.ClipRectangle.Width - 34, 0, 34, 26);
                }
                else if (mouse.X > e.ClipRectangle.Width - 68)
                {
                    e.Graphics.FillRectangle(mouse_down ? AccentBrush : Brushes.Gray, e.ClipRectangle.Width - 68, 0, 34, 26);
                }
                else
                {
                    e.Graphics.FillRectangle(mouse_down ? AccentBrush : Brushes.Gray, e.ClipRectangle.Width - 102, 0, 34, 26);
                }
            }
            //minimize
            e.Graphics.FillRectangle((ShowHeader ? titleBrush : WindowControlsPen.Brush), e.ClipRectangle.Width - 89, 16, 9, 3);


            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //close
            e.Graphics.DrawLine((ShowHeader ? titlePen : WindowControlsPen), e.ClipRectangle.Width - 21, 11, e.ClipRectangle.Width - 14, 18);
            e.Graphics.DrawLine((ShowHeader ? titlePen : WindowControlsPen), e.ClipRectangle.Width - 21, 18, e.ClipRectangle.Width - 14, 11);

            //maximize
            e.Graphics.DrawRectangle((ShowHeader ? titlePen : WindowControlsPen), e.ClipRectangle.Width - 55, 10, 9, 9);
            e.Graphics.DrawRectangle((ShowHeader ? titlePen : WindowControlsPen), e.ClipRectangle.Width - 55, 11, 9, 1);


            if (ShowHeader)
            {
                e.Graphics.DrawString(HeaderText, headerFont, titleBrush, 20, 35);
                e.Graphics.DrawString(HeaderSubText, TitleFont, titleBrush, 20, 70);
            }
            


            if (IconResized != null)
                e.Graphics.DrawImage(IconResized, 5, 5);
            e.Graphics.DrawString(this.Text, this.TitleFont, (ShowHeader ? titleBrush : new SolidBrush(this.TitleColor)), 5 + 16 + 5, 7);

            e.Graphics.DrawRectangle(AccentPen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }

        private void Form1_Resized(object sender, EventArgs e)
        {
            this.content.Size = new Size(this.Width - 20, this.Height - (ShowHeader?112:40));  
        }


        public Panel GetContent()
        {
            return content;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Dwm.DwmExtendFrameIntoClientArea(this.Handle, ref dwmMargins);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse_down = false;
            this.Invalidate();

            switch (GetTtitlebarRegion())
            {
                case TitleBarRegion.Minimize:
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case TitleBarRegion.Maximize:
                    this.WindowState = (this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized);
                    break;
                case TitleBarRegion.Close:
                    this.Close();
                    break;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouse_down = true;
                this.Invalidate();

                if (!(mouse.Y < 26 && mouse.X > this.Width - 102))
                {
                    if (this.Width - BorderWidth > e.Location.X && e.Location.X > BorderWidth && e.Location.Y > BorderWidth && e.Location.Y < this.Height - BorderWidth)
                    {
                        MoveControl(this.Handle);
                    }
                    else
                    {
                        if (this.WindowState != FormWindowState.Maximized)
                        {
                            ResizeForm(resizeDir);
                        }
                    }
                }

            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            //Calculate which direction to resize based on mouse position
            mouse.X = e.X;
            mouse.Y = e.Y;
            mouse_down = (e.Button == System.Windows.Forms.MouseButtons.Left);
            this.Invalidate();

            
            {
                if (e.Location.X < BorderWidth & e.Location.Y < BorderWidth)
                {
                    resizeDir = ResizeDirection.TopLeft;

                }
                else if (e.Location.X < BorderWidth & e.Location.Y > this.Height - BorderWidth)
                {
                    resizeDir = ResizeDirection.BottomLeft;

                }
                else if (e.Location.X > this.Width - BorderWidth & e.Location.Y > this.Height - BorderWidth)
                {
                    resizeDir = ResizeDirection.BottomRight;

                }
                else if (e.Location.X > this.Width - BorderWidth & e.Location.Y < BorderWidth)
                {
                    resizeDir = ResizeDirection.TopRight;

                }
                else if (e.Location.X < BorderWidth)
                {
                    resizeDir = ResizeDirection.Left;

                }
                else if (e.Location.X > this.Width - BorderWidth)
                {
                    resizeDir = ResizeDirection.Right;

                }
                else if (e.Location.Y < BorderWidth)
                {
                    resizeDir = ResizeDirection.Top;

                }
                else if (e.Location.Y > this.Height - BorderWidth)
                {
                    resizeDir = ResizeDirection.Bottom;

                }
                else
                {
                    resizeDir = ResizeDirection.None;
                }
            }

        }

        private TitleBarRegion GetTtitlebarRegion()
        {
            if (mouse.X < 32)
                return TitleBarRegion.Icon;
            else if (mouse.X < this.Width - 102)
                return TitleBarRegion.Title;
            else if (mouse.X < this.Width - 68)
                return TitleBarRegion.Minimize;
            else if (mouse.X < this.Width - 34)
                return TitleBarRegion.Maximize;
            return TitleBarRegion.Close;
        }

        private void MoveControl(IntPtr hWnd)
        {
            ReleaseCapture();
            SendMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void ResizeForm(ResizeDirection direction)
        {
            int dir = -1;
            switch (direction)
            {
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.TopLeft:
                    dir = HTTOPLEFT;
                    break;
                case ResizeDirection.Top:
                    dir = HTTOP;
                    break;
                case ResizeDirection.TopRight:
                    dir = HTTOPRIGHT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
            }

            if (dir != -1)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, dir, 0);
            }

        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xa1;
        private const int HTBORDER = 18;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int HTCAPTION = 2;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;




        public void ReloadTheme()
        {
            this.titleBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionForeColor));
            this.titlePen = new Pen(titleBrush);
            this.headerBrush = new SolidBrush(ThemeMgr.Instance.getColor(WeifenLuo.WinFormsUI.Docking.Colors.IKnownColors.ListSelectionBackColor));
            this.BackColor = ThemeMgr.Instance.getColor(IKnownColors.FormBackground);
            this.TitleColor = ThemeMgr.Instance.getColor(IKnownColors.FormText);
            this.WindowControlsPen = new Pen(new SolidBrush(this.TitleColor));
            this.Refresh();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MetroForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MetroForm";
            this.Load += new System.EventHandler(this.MetroForm_Load);
            this.ResumeLayout(false);

        }

        private void MetroForm_Load(object sender, EventArgs e)
        {

        }


    }
}

namespace VisualSAIStudio.MetroShell
{
    public enum TitleBarRegion
    {
        Icon,
        Title,
        Minimize,
        Maximize,
        Close
    }
}
