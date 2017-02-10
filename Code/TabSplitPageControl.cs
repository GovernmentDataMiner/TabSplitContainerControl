using CustomControl.Designers;

using DevExpress.XtraEditors;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControl
{
    [Designer(typeof(TabSplitPageControlDesigner))]
    [ToolboxItem(false)]
    public partial class TabSplitPageControl : XtraScrollableControl, ISupportInitialize
    {
        #region Event Handlers
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler AutoSizeChanged
        {
            add { base.AutoSizeChanged += value; }
            remove { base.AutoSizeChanged -= value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new event EventHandler DockChanged
        {
            add { base.DockChanged += value; }
            remove { base.DockChanged -= value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler LocationChanged
        {
            add { base.LocationChanged += value; }
            remove { base.LocationChanged -= value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler Resize
        {
            add { base.Resize += value; }
            remove { base.Resize -= value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler TabIndexChanged
        {
            add { base.TabIndexChanged += value; }
            remove { base.TabIndexChanged -= value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler TabStopChanged
        {
            add { base.TabStopChanged += value; }
            remove { base.TabStopChanged -= value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler VisibleChanged
        {
            add { base.VisibleChanged += value; }
            remove { base.VisibleChanged -= value; }
        }
        #endregion

        #region Fields
        private bool collapsed = false;
        private bool selected = false;
        private bool mouseOver = false;
        private string text = String.Empty;
        private Rectangle tabRectangle = Rectangle.Empty;
        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public bool Collapsed
        {
            get { return this.collapsed; }
            set
            {
                if (this.collapsed != value)
                    this.collapsed = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        protected override Padding DefaultMargin
        {
            get { return new Padding(0, 0, 0, 0); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set { base.Dock = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new DockPaddingEdges DockPadding
        {
            get { return base.DockPadding; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public new int Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Point Location
        {
            get { return base.Location; }
            set { base.Location = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Size MaximumSize
        {
            get { return base.MaximumSize; }
            set { base.MaximumSize = value; }
        }

        public bool MouseOver
        {
            get { return this.mouseOver; }
            set
            {
                if (this.mouseOver != value)
                    this.mouseOver = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        public bool Selected
        {
            get { return this.selected; }
            set
            {
                if (this.selected != value)
                    this.selected = value;

                if (value)
                    this.mouseOver = false;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        public new string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(false)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        internal Rectangle TabRectangle
        {
            get { return this.tabRectangle; }
            set { this.tabRectangle = value; }
        }
        #endregion

        #region Constructors
        public TabSplitPageControl()
            : base()
        {
            this.Padding = new Padding(2);
        }

        public TabSplitPageControl(IContainer container)
            : this()
        {
            container.Add(this);
        }
        #endregion

        #region Methods
        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }

        public Size GetTextSize()
        {
            var lines = 1;
            var size = Size.Empty;
            var sizeF = SizeF.Empty;
            var text = this.Text;

            if (String.IsNullOrEmpty(text))
                text = "A";

            var stringArray = GetStrings(text, new char[] { '\n' });
            var length = stringArray.Length;

            if ((lines > 0) && (lines <= stringArray.Length))
                length = lines;

            using (var graphics = this.CreateGraphics())
            {
                for (int i = 0; i < length; i++)
                {
                    sizeF = graphics.MeasureString(stringArray[i], this.Font);
                    if (size.Width < sizeF.Width)
                        size.Width = (int)sizeF.Width;
                }
                return new Size(size.Width, (int)sizeF.Height * length);
            }
        }

        private static string[] GetStrings(string text, char[] delimiters)
        {
            return text.Split(delimiters);
        }

        private void UpdateParent()
        {
            if (base.Parent is TabSplitContainerControl)
                (base.Parent as TabSplitContainerControl).UpdateLayout();
        }
        #endregion

        #region Events
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.Cursor != Cursors.Default)
                this.Cursor = Cursors.Default;

            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (base.Visible && base.DesignMode)
            {
                var clientRectangle = base.ClientRectangle;

                clientRectangle.Width--;
                clientRectangle.Height--;

                using (var pen = new Pen(Color.Gray, 1f) { DashStyle = DashStyle.Dot })
                {
                    e.Graphics.DrawRectangle(pen, clientRectangle);
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.UpdateParent();

            base.OnTextChanged(e);
        }
        #endregion
    }
}