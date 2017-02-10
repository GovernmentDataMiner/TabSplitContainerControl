using CustomControl.Designers;

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CustomControl
{
    public enum TabPlacement
    {
        Left,
        Top,
        Middle,
        Right,
        Bottom
    }

    [Designer(typeof(TabSplitContainerControlDesigner))]
    [DefaultEvent("SplitterMoved")]
    public partial class TabSplitContainerControl : XtraUserControl, ISupportInitialize
    {
        #region Event Handlers
        public delegate void SelectedPageChangedEventHandler(object sender, TabSplitContainerSelectedPageChangedEventArgs e);
        public delegate void SelectedPageChangingEventHandler(object sender, TabSplitContainerSelectedPageChangingEventArgs e);
        public delegate void PageCollapsedEventHandler(object sender, TabSplitContainerPanelCollapsedEventArgs e);
        public delegate void PageSwappedEventHandler(object sender, TabSplitContainerPanelSwappedEventArgs e);
        public delegate void OrientationChangedEventHandler(object sender, TabSplitContainerOrientationChangedEventArgs e);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event EventHandler AutoSizeChanged
        {
            add { base.AutoSizeChanged += value; }
            remove { base.AutoSizeChanged -= value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event ControlEventHandler ControlAdded
        {
            add { base.ControlAdded += value; }
            remove { base.ControlAdded -= value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new event ControlEventHandler ControlRemoved
        {
            add { base.ControlRemoved += value; }
            remove { base.ControlRemoved -= value; }
        }

        public event OrientationChangedEventHandler OrientationChanged
        {
            add { this.Events.AddHandler(EVENT_ORIENTATION, value); }
            remove { this.Events.RemoveHandler(EVENT_ORIENTATION, value); }
        }

        public event PageCollapsedEventHandler PanelCollapsed
        {
            add { this.Events.AddHandler(EVENT_COLLAPSED, value); }
            remove { this.Events.RemoveHandler(EVENT_COLLAPSED, value); }
        }

        public event SelectedPageChangedEventHandler SelectedPageChanged
        {
            add { this.Events.AddHandler(EVENT_PAGECHANGED, value); }
            remove { this.Events.RemoveHandler(EVENT_PAGECHANGED, value); }
        }

        public event SelectedPageChangingEventHandler SelectedPageChanging
        {
            add { this.Events.AddHandler(EVENT_PAGECHANGING, value); }
            remove { this.Events.RemoveHandler(EVENT_PAGECHANGING, value); }
        }

        public event PageSwappedEventHandler PanelsSwapped
        {
            add { this.Events.AddHandler(EVENT_SWAPPED, value); }
            remove { this.Events.RemoveHandler(EVENT_SWAPPED, value); }
        }

        public event SplitterEventHandler SplitterMoved
        {
            add { Events.AddHandler(EVENT_MOVED, value); }
            remove { Events.RemoveHandler(EVENT_MOVED, value); }
        }
        #endregion

        #region Fields
        private TabSplitPageControl panel1;
        private TabSplitPageControl panel2;

        private CheckButton buttonHorizontal;
        private CheckButton buttonVertical;

        private SimpleButton buttonCollapseSplit;
        private SimpleButton buttonCollapsePage1;
        private SimpleButton buttonCollapsePage2;
        private SimpleButton buttonSwapPanel;

        private const int DRAW_START = 1;
        private const int DRAW_MOVE = 2;
        private const int DRAW_END = 3;

        private TabSplitPageControl selectedPage;

        private Cursor overrideCursor = null;
        
        private Orientation orientation = Orientation.Vertical;
        private TabPlacement tabPlacement = TabPlacement.Middle;

        private Point anchor = Point.Empty;

        private Rectangle buttonRectangle = Rectangle.Empty;
        private Rectangle splitterRectangle = Rectangle.Empty;
        private Rectangle tabStripRectangle = Rectangle.Empty;
        private Rectangle workingAreaRectangle = Rectangle.Empty;

        private static readonly object EVENT_MOVING = new object();
        private static readonly object EVENT_MOVED = new object();
        private static readonly object EVENT_SWAPPED = new object();
        private static readonly object EVENT_ORIENTATION = new object();
        private static readonly object EVENT_COLLAPSED = new object();
        private static readonly object EVENT_PAGECHANGED = new object();
        private static readonly object EVENT_PAGECHANGING = new object();

        private bool splitterClick = false;
        private bool splitterDrag = false;
        private bool splitBegin = false;
        private bool splitMove = false;
        private bool panelsSwapped = false;
        private bool setSplitterDistance = false;

        private double splitRatio = 0.00f;

        private int borderSize = 1;
        private int splitDistance = 50;
        private int splitterDistance = 50;
        private int splitterDistanceInitial;
        private int splitterIncrement = 1;
        private int panel1MinSize = 25;
        private int panel2MinSize = 25;
        private int splitterWidth = 19;
        private int lastDrawSplit = 1;
        private int minimumTabSize = 28;
        #endregion

        #region Properties
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override bool AutoScroll
        {
            get { return false; }
            set { base.AutoScroll = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Size AutoScrollMargin
        {
            get { return base.AutoScrollMargin; }
            set { base.AutoScrollMargin = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Size AutoScrollMinSize
        {
            get { return base.AutoScrollMinSize; }
            set { base.AutoScrollMinSize = value; }
        }

        [DefaultValue(typeof(Point), "0, 0")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override Point AutoScrollOffset
        {
            get { return base.AutoScrollOffset; }
            set { base.AutoScrollOffset = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Point AutoScrollPosition
        {
            get { return base.AutoScrollPosition; }
            set { base.AutoScrollPosition = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [DefaultValue(BorderStyles.Default)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        public string CollapsePage1ButtonToolTip
        {
            get { return this.buttonCollapsePage1.ToolTip; }
            set { this.buttonCollapsePage1.ToolTip = value; }
        }

        public string CollapsePage2ButtonToolTip
        {
            get { return this.buttonCollapsePage2.ToolTip; }
            set { this.buttonCollapsePage2.ToolTip = value; }
        }

        public string CollapseSplitButtonToolTip
        {
            get { return this.buttonCollapseSplit.ToolTip; }
            set { this.buttonCollapseSplit.ToolTip = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public new Control.ControlCollection Controls
        {
            get { return base.Controls; }
        }

        [DefaultValue(DockStyle.None)]
        public new DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                if (base.Dock != value)
                {
                    base.Dock = value;
                    this.ResizeLayout();
                }
            }
        }

        public string HorizontalButtonToolTip
        {
            get { return this.buttonHorizontal.ToolTip; }
            set { this.buttonHorizontal.ToolTip = value; }
        }

        [DefaultValue(Orientation.Vertical)]
        public Orientation Orientation
        {
            get { return this.orientation; }
            set
            {
                if (this.Orientation != value)
                {
                    var oldOrientation = this.Orientation;

                    this.orientation = value;
                    this.splitDistance = 0;

                    this.SplitterDistance = this.GetRatioedSplitterDistance();
                    this.UpdateLayout();

                    OnOrientationChanged(new TabSplitContainerOrientationChangedEventArgs(oldOrientation, this.orientation));
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public string Page1Text
        {
            get { return this.Panel1.Text; }
            set
            {
                if (this.Panel1.Text != value)
                {
                    this.Panel1.Text = value;
                    this.UpdateLayout();
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        public string Page2Text
        {
            get { return this.Panel2.Text; }
            set
            {
                if (this.Panel2.Text != value)
                {
                    this.Panel2.Text = value;
                    this.UpdateLayout();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public TabSplitPageControl Panel1
        {
            get { return this.panel1; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public TabSplitPageControl Panel2
        {
            get { return this.panel2; }
        }

        [DefaultValue(false)]
        public bool Panel1Collapsed
        {
            get
            {
                if (this.Created)
                    return this.Panel1.Collapsed;

                return false;
            }
            set
            {
                if (value != this.Panel1.Collapsed)
                {
                    if (value && this.Panel2.Collapsed)
                        this.CollapsePanel(this.Panel2, false);

                    this.CollapsePanel(this.Panel1, value);

                    if (value)
                        this.SelectedPage = this.Panel2;
                }
            }
        }

        [DefaultValue(false)]
        public bool Panel2Collapsed
        {
            get
            {
                if (this.Created)
                    return this.Panel2.Collapsed;

                return false;
            }
            set
            {
                if (value != this.Panel2.Collapsed)
                {
                    if (value && this.Panel1.Collapsed)
                        this.CollapsePanel(this.Panel1, false);

                    this.CollapsePanel(this.Panel2, value);

                    if (value)
                        this.SelectedPage = this.Panel1;
                }
            }
        }

        [DefaultValue(25)]
        [RefreshProperties(RefreshProperties.All)]
        public int Panel1MinSize
        {
            get { return this.panel1MinSize; }
            set
            {
                if (this.Panel1MinSize != value)
                {
                    this.panel1MinSize = value;
                    this.ApplyPanel1MinSize(value);
                    this.UpdateLayout();
                }
            }
        }

        [DefaultValue(25)]
        [RefreshProperties(RefreshProperties.All)]
        public int Panel2MinSize
        {
            get { return this.panel2MinSize; }
            set
            {
                if (this.Panel2MinSize != value)
                {
                    this.panel2MinSize = value;
                    this.ApplyPanel2MinSize(value);
                    this.UpdateLayout();
                }
            }
        }

        public TabSplitPageControl SelectedPage
        {
            get { return this.selectedPage; }
            private set
            {
                if (this.Collapsed)
                {
                    if ((this.selectedPage != value) && ((value != null) && ((value == this.Panel1) || (value == this.Panel2))))
                    {
                        this.selectedPage = value;
                        this.SelectPage();
                    }
                }
                else
                {
                    this.selectedPage = null;
                    this.SelectPage();
                }
                
                this.UpdateLayout();
            }
        }

        [DefaultValue(50)]
        public int SplitterDistance
        {
            get { return this.splitDistance; }
            set
            {
                if (value != this.SplitterDistance)
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("SplitterDistance");

                    try
                    {
                        this.setSplitterDistance = true;

                        if (this.Orientation == Orientation.Vertical)
                        {
                            if (value < this.Panel1MinSize)
                                value = this.Panel1MinSize;
                            if (value + this.SplitterWidth > this.Width - this.Panel2MinSize)
                                value = this.Width - this.Panel2MinSize - this.SplitterWidth;
                            if (value < 0)
                                throw new InvalidOperationException();

                            this.splitDistance = value;
                            this.splitterDistance = value;
                            this.Panel1.Width = this.SplitterDistance;
                        }
                        else
                        {
                            if (value < this.Panel1MinSize)
                                value = this.Panel1MinSize;
                            if (value + this.SplitterWidth > this.Height - this.Panel2MinSize)
                                value = this.Height - this.Panel2MinSize - this.SplitterWidth;
                            if (value < 0)
                                throw new InvalidOperationException();

                            this.splitDistance = value;
                            this.splitterDistance = value;
                            this.Panel1.Height = this.SplitterDistance;
                        }
                        this.UpdateLayout();
                    }
                    finally
                    {
                        this.setSplitterDistance = false;

                        if (this.Orientation == Orientation.Vertical)
                            this.splitRatio = Math.Round(((double)this.SplitterDistance) / ((double)this.Width), 2);
                        else
                            this.splitRatio = Math.Round(((double)this.SplitterDistance) / ((double)this.Height), 2);

                        this.OnSplitterMoved(new SplitterEventArgs((this.splitterRectangle.X + (this.splitterRectangle.Width / 2)), (this.splitterRectangle.Y + (this.splitterRectangle.Height / 2)), this.splitterRectangle.X, this.splitterRectangle.Y));
                    }
                }
            }
        }

        [DefaultValue(1)]
        public int SplitterIncrement
        {
            get { return this.splitterIncrement;}
            set
            {
                if (value != this.splitterIncrement)
                {
                    if (value < 1)
                        throw new ArgumentOutOfRangeException("SplitterIncrement");

                    this.splitterIncrement = value;
                }
            }
        }

        [DefaultValue(19)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public int SplitterWidth
        {
            get { return this.splitterWidth; }
        }

        [DefaultValue((string)null)]
        public string SwapPanelButtonToolTip
        {
            get { return this.buttonSwapPanel.ToolTip; }
            set { this.buttonSwapPanel.ToolTip = value; }
        }

        public string VerticalButtonToolTip
        {
            get { return this.buttonVertical.ToolTip; }
            set { this.buttonVertical.ToolTip = value; }
        }

        protected override Size DefaultSize
        {
            get { return new Size(300, 300); }
        }

        private static Color BorderColor
        {
            get
            {
                var color = CommonSkins.GetSkin(UserLookAndFeel.Default)[CommonSkins.SkinTextBorder].Border.All;

                if (color.IsEmpty)
                    color = CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel).TranslateColor(SystemColors.ActiveBorder);

                return color;
            }
        }

        private bool Collapsed
        {
            get { return this.Panel1Collapsed || this.Panel2Collapsed; }
        }

        private static Color ControlColor
        {
            get { return CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel).TranslateColor(SystemColors.Control); }
        }

        private Rectangle[] CollapsePage1Image
        {
            get
            {
                var rectangles = new List<Rectangle>();

                if (this.Orientation == Orientation.Vertical)
                {
                    rectangles.Add(new Rectangle(4, 6, 1, 1));
                    rectangles.Add(new Rectangle(5, 5, 1, 3));
                    rectangles.Add(new Rectangle(6, 4, 1, 5));
                    rectangles.Add(new Rectangle(7, 3, 1, 7));
                }
                else
                {
                    rectangles.Add(new Rectangle(6, 4, 1, 1));
                    rectangles.Add(new Rectangle(5, 5, 3, 1));
                    rectangles.Add(new Rectangle(4, 6, 5, 1));
                    rectangles.Add(new Rectangle(3, 7, 7, 1));
                }

                return rectangles.ToArray();
            }
        }

        private Rectangle[] CollapsePage2Image
        {
            get
            {
                var rectangles = new List<Rectangle>();

                if (this.Orientation == Orientation.Vertical)
                {
                    rectangles.Add(new Rectangle(5, 3, 1, 7));
                    rectangles.Add(new Rectangle(6, 4, 1, 5));
                    rectangles.Add(new Rectangle(7, 5, 1, 3));
                    rectangles.Add(new Rectangle(8, 6, 1, 1));
                }
                else
                {
                    rectangles.Add(new Rectangle(3, 5, 7, 1));
                    rectangles.Add(new Rectangle(4, 6, 5, 1));
                    rectangles.Add(new Rectangle(5, 7, 3, 1));
                    rectangles.Add(new Rectangle(6, 8, 1, 1));
                }

                return rectangles.ToArray();
            }
        }

        private Rectangle[] CollapseSplitImage
        {
            get
            {
                var rectangles = new List<Rectangle>();

                if (this.Orientation == Orientation.Vertical)
                {
                    if (!this.Collapsed || this.Panel1Collapsed)
                    {
                        rectangles.Add(new Rectangle(3, 4, 2, 1));
                        rectangles.Add(new Rectangle(4, 5, 2, 1));
                        rectangles.Add(new Rectangle(6, 4, 2, 1));
                        rectangles.Add(new Rectangle(7, 5, 2, 1));
                        rectangles.Add(new Rectangle(5, 6, 2, 1));
                        rectangles.Add(new Rectangle(8, 6, 2, 1));
                        rectangles.Add(new Rectangle(4, 7, 2, 1));
                        rectangles.Add(new Rectangle(7, 7, 2, 1));
                        rectangles.Add(new Rectangle(3, 8, 2, 1));
                        rectangles.Add(new Rectangle(6, 8, 2, 1));
                    }
                    else
                    {
                        rectangles.Add(new Rectangle(5, 4, 2, 1));
                        rectangles.Add(new Rectangle(8, 4, 2, 1));
                        rectangles.Add(new Rectangle(4, 5, 2, 1));
                        rectangles.Add(new Rectangle(7, 5, 2, 1));
                        rectangles.Add(new Rectangle(3, 6, 2, 1));
                        rectangles.Add(new Rectangle(6, 6, 2, 1));
                        rectangles.Add(new Rectangle(4, 7, 2, 1));
                        rectangles.Add(new Rectangle(7, 7, 2, 1));
                        rectangles.Add(new Rectangle(5, 8, 2, 1));
                        rectangles.Add(new Rectangle(8, 8, 2, 1));
                    }
                }
                else
                {
                    if (!this.Collapsed || this.Panel1Collapsed)
                    {
                        rectangles.Add(new Rectangle(4, 3, 1, 2));
                        rectangles.Add(new Rectangle(4, 6, 1, 2));
                        rectangles.Add(new Rectangle(5, 4, 1, 2));
                        rectangles.Add(new Rectangle(5, 7, 1, 2));
                        rectangles.Add(new Rectangle(6, 5, 1, 2));
                        rectangles.Add(new Rectangle(6, 8, 1, 2));
                        rectangles.Add(new Rectangle(7, 4, 1, 2));
                        rectangles.Add(new Rectangle(7, 7, 1, 2));
                        rectangles.Add(new Rectangle(8, 3, 1, 2));
                        rectangles.Add(new Rectangle(8, 6, 1, 2));
                    }
                    else
                    {
                        rectangles.Add(new Rectangle(4, 5, 1, 2));
                        rectangles.Add(new Rectangle(4, 8, 1, 2));
                        rectangles.Add(new Rectangle(5, 4, 1, 2));
                        rectangles.Add(new Rectangle(5, 7, 1, 2));
                        rectangles.Add(new Rectangle(6, 3, 1, 2));
                        rectangles.Add(new Rectangle(6, 6, 1, 2));
                        rectangles.Add(new Rectangle(7, 4, 1, 2));
                        rectangles.Add(new Rectangle(7, 7, 1, 2));
                        rectangles.Add(new Rectangle(8, 5, 1, 2));
                        rectangles.Add(new Rectangle(8, 8, 1, 2));
                    }
                }
                return rectangles.ToArray();
            }
        }

        private static Color GrayTextColor
        {
            get { return CommonSkins.GetSkin(UserLookAndFeel.Default).TranslateColor(SystemColors.GrayText); }
        }

        private bool IsSplitterMovable
        {
            get
            {
                if (this.Collapsed)
                    return false;

                if (this.Orientation == Orientation.Vertical)
                    return (this.Width >= this.Panel1MinSize + this.SplitterWidth + this.Panel2MinSize);
                else
                    return (this.Height >= this.Panel1MinSize + this.SplitterWidth + this.Panel2MinSize);
            }
        }

        private static Color MouseOverControlColor
        {
            get { return CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel).TranslateColor(SystemColors.Highlight); }
        }

        private Cursor OverrideCursor
        {
            get { return this.overrideCursor; }
            set
            {
                if (this.overrideCursor != value)
                {
                    this.overrideCursor = value;

                    if (this.IsHandleCreated)
                    {
                        var p = new NativeMethods.POINT();
                        var r = new NativeMethods.RECT();

                        NativeMethods.GetCursorPos(out p);
                        NativeMethods.GetWindowRect(new HandleRef(this, this.Handle), out r);

                        if ((r.Left <= p.X && p.X < r.Right && r.Top <= p.Y && p.Y < r.Bottom) || NativeMethods.GetCapture() == this.Handle)
                            NativeMethods.SendMessage(new HandleRef(this, this.Handle), NativeMethods.WM_SETCURSOR, this.Handle, (IntPtr)NativeMethods.HTCLIENT);
                    }
                }
            }
        }

        private static Color SelectedControlColor
        {
            get
            {
                return ControlColor;
            }
        }

        private int SplitterDistanceInternal
        {
            get { return this.splitterDistance; }
            set { this.SplitterDistance = value; }
        }

        private Rectangle[] SwapPageImage
        {
            get
            {
                var rectangles = new List<Rectangle>();

                if (this.Orientation == Orientation.Vertical)
                {
                    rectangles.Add(new Rectangle(8, 2, 1, 1));
                    rectangles.Add(new Rectangle(8, 3, 2, 1));
                    rectangles.Add(new Rectangle(2, 4, 9, 1));
                    rectangles.Add(new Rectangle(8, 5, 2, 1));
                    rectangles.Add(new Rectangle(8, 6, 1, 1));
                    rectangles.Add(new Rectangle(4, 6, 1, 1));
                    rectangles.Add(new Rectangle(3, 7, 2, 1));
                    rectangles.Add(new Rectangle(2, 8, 9, 1));
                    rectangles.Add(new Rectangle(3, 9, 2, 1));
                    rectangles.Add(new Rectangle(4, 10, 1, 1));
                }
                else
                {
                    rectangles.Add(new Rectangle(2, 4, 1, 1));
                    rectangles.Add(new Rectangle(3, 3, 1, 2));
                    rectangles.Add(new Rectangle(4, 2, 1, 9));
                    rectangles.Add(new Rectangle(5, 3, 1, 2));
                    rectangles.Add(new Rectangle(6, 4, 1, 1));
                    rectangles.Add(new Rectangle(6, 8, 1, 1));
                    rectangles.Add(new Rectangle(7, 8, 1, 2));
                    rectangles.Add(new Rectangle(8, 2, 1, 9));
                    rectangles.Add(new Rectangle(9, 8, 1, 2));
                    rectangles.Add(new Rectangle(10, 8, 1, 1));
                }
                return rectangles.ToArray();
            }
        }

        private static Color TextColor
        {
            get { return CommonSkins.GetSkin(UserLookAndFeel.Default).TranslateColor(SystemColors.ControlText); }
        }
        #endregion

        #region Constructors
        public TabSplitContainerControl()
        {
            this.Initialize();

            var styles = (ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint);
            var methodInfo = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            var objectArgs = new object[] { styles, true };

            this.SetStyle(styles, true);

            methodInfo.Invoke(this.Panel1, objectArgs);
            methodInfo.Invoke(this.Panel2, objectArgs);

            this.splitterRectangle = new Rectangle();

            this.DoubleBuffered = true;
            this.UpdateLayout();
        }
        #endregion

        #region Methods
        public void BeginInit()
        {
        }

        public void EndInit()
        {
            this.UpdateLayout();
        }

        internal void UpdateLayout()
        {
            if (this.Created)
            {
                workingAreaRectangle = Rectangle.Empty;

                if (!this.Collapsed)
                {
                    this.tabPlacement = TabPlacement.Middle;
                }
                else if (this.Orientation == Orientation.Horizontal)
                {
                    if (!this.Panel1Collapsed && this.Panel2Collapsed)
                        this.tabPlacement = TabPlacement.Bottom;
                    else
                        this.tabPlacement = TabPlacement.Top;
                }
                else if (!this.Panel1Collapsed && this.Panel2Collapsed)
                {
                    this.tabPlacement = TabPlacement.Right;
                }
                else
                {
                    this.tabPlacement = TabPlacement.Left;
                }

                if (this.Orientation == Orientation.Vertical)
                {
                    if (!this.Collapsed)
                    {
                        this.workingAreaRectangle = new Rectangle(new Point(this.SplitterDistance, this.borderSize), new Size(this.SplitterWidth, (this.Height - (2 * this.borderSize))));

                        this.Panel1.Location = new Point(this.borderSize, this.borderSize);
                        this.Panel1.Size = new Size((workingAreaRectangle.X - this.borderSize), workingAreaRectangle.Height);

                        this.Panel2.Location = new Point((workingAreaRectangle.X + workingAreaRectangle.Width), this.borderSize);
                        this.Panel2.Size = new Size((this.Width - (workingAreaRectangle.X + workingAreaRectangle.Width + this.borderSize)), workingAreaRectangle.Height);
                    }
                    else
                    {
                        if (this.Panel1Collapsed)
                        {
                            this.workingAreaRectangle = new Rectangle(new Point(this.borderSize, this.borderSize), new Size(this.SplitterWidth, (this.Height - (2 * this.borderSize))));

                            var location = new Point(this.borderSize + this.SplitterWidth, this.borderSize);
                            var size = new Size(this.Width - this.SplitterWidth - (2 * this.borderSize), this.Height - (2 * this.borderSize));

                            this.Panel1.Location = location;
                            this.Panel1.Size = size;

                            this.Panel2.Location = location;
                            this.Panel2.Size = size;
                        }
                        else if (this.Panel2Collapsed)
                        {
                            this.workingAreaRectangle = new Rectangle(new Point(this.Width - this.SplitterWidth - this.borderSize, this.borderSize), new Size(this.SplitterWidth, (this.Height - (2 * this.borderSize))));

                            var location = new Point(this.borderSize, this.borderSize);
                            var size = new Size(this.Width - this.SplitterWidth - (2 * this.borderSize), this.Height - (2 * this.borderSize));;

                            this.Panel1.Location = location;
                            this.Panel1.Size = size;

                            this.Panel2.Location = location;
                            this.Panel2.Size = size;
                        }
                    }
                }
                else
                {
                    if (!this.Collapsed)
                    {
                        this.workingAreaRectangle = new Rectangle(new Point(this.borderSize, this.SplitterDistance), new Size((this.Width - (2 * this.borderSize)), this.SplitterWidth));

                        this.Panel1.Location = new Point(this.borderSize, this.borderSize);
                        this.Panel1.Size = new Size(workingAreaRectangle.Width, workingAreaRectangle.Y - this.borderSize);

                        this.Panel2.Location = new Point(this.borderSize, (workingAreaRectangle.Y + workingAreaRectangle.Height));
                        this.Panel2.Size = new Size(workingAreaRectangle.Width, (this.Height - (workingAreaRectangle.Y + workingAreaRectangle.Height + this.borderSize)));
                    }
                    else
                    {
                        if (this.Panel1Collapsed)
                        {
                            this.workingAreaRectangle = new Rectangle(new Point(this.borderSize, this.borderSize), new Size(this.Width - (2 * this.borderSize), this.SplitterWidth));

                            var location = new Point(this.borderSize, this.SplitterWidth + this.borderSize);
                            var size = new Size(this.Width - (2 * this.borderSize), this.Height - this.SplitterWidth - (2 * this.borderSize));

                            this.Panel1.Location = location;
                            this.Panel1.Size = size;

                            this.Panel2.Location = location;
                            this.Panel2.Size = size;
                        }
                        else if (this.Panel2Collapsed)
                        {
                            this.workingAreaRectangle = new Rectangle(new Point(this.borderSize, this.Height - this.SplitterWidth - this.borderSize), new Size(this.Width - (2 * this.borderSize), this.SplitterWidth));

                            var location = new Size(this.Width - (2 * this.borderSize), this.Height - this.SplitterWidth - (2 * this.borderSize));
                            var size = new Point(this.borderSize, this.borderSize);

                            this.Panel1.Size = location;
                            this.Panel1.Location = size;

                            this.Panel2.Size = location;
                            this.Panel2.Location = size;
                        }
                    }
                }

                this.CalcTabStripRectangle();
                this.CalcButtonRectangle();
                this.CalcSplitterRectangle();

                this.PositionTabs();
                this.PositionButtons();
                this.UpdateButtons();

                this.Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.buttonHorizontal.CheckedChanged -= new EventHandler(this.buttonHorizontal_CheckedChanged);
            this.buttonHorizontal.Paint -= new PaintEventHandler(this.buttonHorizontal_Paint);
            this.buttonVertical.CheckedChanged -= new EventHandler(this.buttonVertical_CheckedChanged);
            this.buttonVertical.Paint -= new PaintEventHandler(this.buttonVertical_Paint);
            this.buttonCollapseSplit.Click -= new EventHandler(this.buttonCollapseSplit_Click);
            this.buttonCollapseSplit.Paint -= new PaintEventHandler(this.buttonCollapseSplit_Paint);
            this.buttonCollapsePage1.Click -= new EventHandler(this.buttonCollapsePage1_Click);
            this.buttonCollapsePage1.Paint -= new PaintEventHandler(this.buttonCollapsePage1_Paint);
            this.buttonCollapsePage2.Click -= new EventHandler(this.buttonCollapsePage2_Click);
            this.buttonCollapsePage2.Paint -= new PaintEventHandler(this.buttonCollapsePage2_Paint);
            this.buttonSwapPanel.Click -= new EventHandler(this.buttonSwapPage_Click);
            this.buttonSwapPanel.Paint -= new PaintEventHandler(this.buttonSwapPage_Paint);

            base.Dispose(disposing);
        }

        private void ApplySplitterDistance()
        {
            this.SplitterDistanceInternal = this.splitterDistance;

            this.UpdateLayout();
        }

        private void ApplyPanel1MinSize(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Page1MinSize");

            if (this.Orientation == Orientation.Vertical)
            {
                if (this.DesignMode && this.Width != this.DefaultSize.Width && value + this.Panel2MinSize + this.SplitterWidth > this.Width)
                    throw new ArgumentOutOfRangeException("Page1MinSize");
            }
            else if (this.Orientation == Orientation.Horizontal)
            {
                if (this.DesignMode && this.Height != this.DefaultSize.Height && value + this.Panel2MinSize + this.SplitterWidth > this.Height)
                    throw new ArgumentOutOfRangeException("Page1MinSize");
            }

            this.panel1MinSize = value;

            if (value > this.SplitterDistanceInternal)
                this.SplitterDistanceInternal = value;
        }

        private void ApplyPanel2MinSize(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Page2MinSize");

            if (this.Orientation == Orientation.Vertical)
            {
                if (this.DesignMode && this.Width != this.DefaultSize.Width && value + this.Panel1MinSize + this.SplitterWidth > this.Width)
                    throw new ArgumentOutOfRangeException("Page2MinSize");
            }
            else if (this.Orientation == Orientation.Horizontal)
            {
                if (this.DesignMode && this.Height != this.DefaultSize.Height && value + this.Panel1MinSize + this.SplitterWidth > this.Height)
                    throw new ArgumentOutOfRangeException("Page2MinSize");
            }

            this.panel2MinSize = value;

            if (value > this.Panel2.Width)
                this.SplitterDistanceInternal = this.Panel2.Width + this.SplitterWidth;
        }

        private Rectangle CalcSplitLine(int splitSize, int minWeight)
        {
            var rectangle = new Rectangle();

            switch (this.Orientation)
            {
                case Orientation.Vertical:
                    {
                        rectangle.Width = this.SplitterWidth;
                        rectangle.Height = this.Height;

                        if (rectangle.Width < minWeight)
                            rectangle.Width = minWeight;

                        rectangle.X = this.Panel1.Location.X + splitSize;

                        break;
                    }

                case Orientation.Horizontal:
                    {
                        rectangle.Width = this.Width;
                        rectangle.Height = this.SplitterWidth;

                        if (rectangle.Width < minWeight)
                            rectangle.Width = minWeight;

                        rectangle.Y = this.Panel1.Location.Y + splitSize;

                        break;
                    }
            }
            return rectangle;
        }

        private void CalcButtonRectangle()
        {
            if (this.Orientation == Orientation.Vertical)
                this.buttonRectangle = new Rectangle(this.workingAreaRectangle.X, (this.Height - 79), this.workingAreaRectangle.Width, 79);
            else
                this.buttonRectangle = new Rectangle((this.Width - 79), this.workingAreaRectangle.Y, 79, this.workingAreaRectangle.Height);
        }

        private Size CalculateCommonTabPageSize(Rectangle tabPageRectangle1, Rectangle tabPageRectangle2)
        {
            if (this.Orientation == Orientation.Vertical)
            {
                if (tabPageRectangle1.Height > tabPageRectangle2.Height)
                    return new Size(0, tabPageRectangle1.Height);
                else
                    return new Size(0, tabPageRectangle2.Height);
            }
            else
            {
                if (tabPageRectangle1.Width > tabPageRectangle2.Width)
                    return new Size(tabPageRectangle1.Width, 0);
                else
                    return new Size(tabPageRectangle2.Width, 0);
            }
        }

        private void CalcSplitterRectangle()
        {
            this.splitterRectangle = Rectangle.Empty;

            if (this.Orientation == Orientation.Vertical)
            {
                this.splitterRectangle.Location = new Point(this.tabStripRectangle.X, (this.tabStripRectangle.Height - this.tabStripRectangle.Y));
                this.splitterRectangle.Size = new Size(workingAreaRectangle.Width, (this.Height - (this.tabStripRectangle.Height + this.buttonRectangle.Height)));
            }
            else
            {
                this.splitterRectangle.Location = new Point((this.tabStripRectangle.Width - this.tabStripRectangle.X), this.tabStripRectangle.Y);
                this.splitterRectangle.Size = new Size((this.Width - (this.tabStripRectangle.Width + this.buttonRectangle.Width)), workingAreaRectangle.Height);
            }
        }

        private void CalcTabStripRectangle()
        {
            this.CalcTabPageRectangle(this.Panel1);
            this.CalcTabPageRectangle(this.Panel2);

            var commonSize = this.CalculateCommonTabPageSize(this.Panel1.TabRectangle, this.Panel2.TabRectangle);
            var commonRectangle = new Rectangle();

            if (this.Orientation == Orientation.Vertical)
            {
                commonRectangle = this.Panel1.TabRectangle;
                commonRectangle.Height = commonSize.Height;
                this.Panel1.TabRectangle = commonRectangle;

                commonRectangle = this.Panel2.TabRectangle;
                commonRectangle.Height = commonSize.Height;
                this.Panel2.TabRectangle = commonRectangle;

                var height = commonRectangle.Height * 2;
                if (this.tabPlacement == TabPlacement.Middle)
                    height += buttonSwapPanel.Size.Height + 10;

                this.tabStripRectangle = new Rectangle(this.workingAreaRectangle.X, 0, this.workingAreaRectangle.Width, height);
            }
            else
            {
                commonRectangle = this.Panel1.TabRectangle;
                commonRectangle.Width = commonSize.Width;
                this.Panel1.TabRectangle = commonRectangle;

                commonRectangle = this.Panel2.TabRectangle;
                commonRectangle.Width = commonSize.Width;
                this.Panel2.TabRectangle = commonRectangle;

                var width = commonRectangle.Width * 2;
                if (this.tabPlacement == TabPlacement.Middle)
                    width += buttonSwapPanel.Size.Width + 10;

                this.tabStripRectangle = new Rectangle(0, workingAreaRectangle.Y, width, this.workingAreaRectangle.Height);
            }
        }

        private void CalcTabPageRectangle(TabSplitPageControl tabPage)
        {
            var tabSize = Size.Empty;
            tabSize = tabPage.GetTextSize();

            if (tabSize.Width < minimumTabSize)
                tabSize.Width = minimumTabSize;

            var offset = 3;

            if (this.Orientation == Orientation.Vertical)
            {
                if (tabPage.Selected && (this.tabPlacement == TabPlacement.Left || this.tabPlacement == TabPlacement.Right))
                    offset = 2;
                tabPage.TabRectangle = new Rectangle(0, 0, (this.SplitterWidth - offset), (tabSize.Width + 8));
            }
            else
            {
                if (tabPage.Selected && (this.tabPlacement == TabPlacement.Top || this.tabPlacement == TabPlacement.Bottom))
                    offset = 2;
                tabPage.TabRectangle = new Rectangle(0, 0, (tabSize.Width + 8), (this.SplitterWidth - offset));
            }
        }

        private void CollapsePanel(TabSplitPageControl tabPage, bool collapsing)
        {
            tabPage.Collapsed = collapsing;
            this.OnPanelCollapsed(new TabSplitContainerPanelCollapsedEventArgs(tabPage));
        }

        private void DrawControlBorder(Graphics graphics)
        {
            using (var pen = new Pen(BorderColor))
            {
                var rectangle = this.DisplayRectangle;

                rectangle.Width--;
                rectangle.Height--;

                graphics.DrawRectangle(pen, rectangle);
            }
        }

        private void DrawSplitBar(int mode)
        {
            if (mode != DRAW_START && this.lastDrawSplit != -1)
            {
                this.DrawSplitBarHelper(this.lastDrawSplit);
                this.lastDrawSplit = -1;
            }
            else if (mode != DRAW_START && this.lastDrawSplit == -1)
                return;

            if (mode != DRAW_END)
            {
                if (this.splitMove || this.splitBegin)
                {
                    this.DrawSplitBarHelper(this.splitterDistance);
                    this.lastDrawSplit = this.splitterDistance;
                }
                else
                {
                    this.DrawSplitBarHelper(this.splitterDistance);
                    this.lastDrawSplit = this.splitterDistance;
                }
            }
            else
            {
                if (this.lastDrawSplit != -1)
                    this.DrawSplitBarHelper(lastDrawSplit);

                this.lastDrawSplit = -1;
            }
        }

        private void DrawSplitBarGripper(Graphics graphics)
        {
            if (!this.Collapsed)
            {
                if (this.Orientation == Orientation.Vertical)
                {
                    if (splitterRectangle.Height > 17)
                    {
                        var x = splitterRectangle.X + 7;
                        var y = splitterRectangle.Y + (splitterRectangle.Height / 2) - 8;
                        using (var pen = new Pen(TextColor, 1) { DashStyle = DashStyle.Custom, DashPattern = new float[]{1, 3}})
                        {
                            graphics.DrawLine(pen, x, y, x, (y + 17));
                            graphics.DrawLine(pen, (x + 2), (y + 2), (x + 2), y + 15);
                            graphics.DrawLine(pen, (x + 4), y, (x + 4), y + 17);
                        }
                    }
                }
                else
                {
                    if (splitterRectangle.Width > 17)
                    {
                        var x = splitterRectangle.X + (splitterRectangle.Width / 2) - 8;
                        var y = splitterRectangle.Y + 7;

                        using (var pen = new Pen(TextColor, 1) { DashStyle = DashStyle.Custom, DashPattern = new float[]{1, 3}})
                        {
                            graphics.DrawLine(pen, x, y, (x + 17), y);
                            graphics.DrawLine(pen, (x + 2), (y + 2), (x + 15), (y + 2));
                            graphics.DrawLine(pen, x, (y + 4), (x + 17), (y + 4));
                        }
                    }
                }
            }
        }

        private void DrawSplitBarHelper(int splitSize)
        {
            var rectangle = this.CalcSplitLine(splitSize, 3);
            var parentHandle = new HandleRef(this, this.Handle);
            var dc = NativeMethods.GetDCEx(parentHandle, NativeMethods.NullHandleRef, NativeMethods.DeviceContextValues.Cache | NativeMethods.DeviceContextValues.LockWindowUpdate);
            var halftone = NativeMethods.CreateBrush();
            var saveBrush = NativeMethods.SelectObject(new HandleRef(this, dc), new HandleRef(null, halftone));

            NativeMethods.PatBlt(new HandleRef(this, dc), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, NativeMethods.TernaryRasterOperations.PATINVERT);
            NativeMethods.SelectObject(new HandleRef(this, dc), new HandleRef(null, saveBrush));
            NativeMethods.DeleteObject(new HandleRef(null, halftone));
            NativeMethods.ReleaseDC(parentHandle, new HandleRef(null, dc));
        }

        private void DrawTabPage(Graphics graphics, TabSplitPageControl tabPage, TabPlacement tabPlacement)
        {
            var bounds = tabPage.TabRectangle;

            var color = ControlColor;

            if (tabPage.Selected)
                color = SelectedControlColor;
            if (tabPage.MouseOver)
                color = MouseOverControlColor;

            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, bounds);
            }

            using (var pen = new Pen(BorderColor))
            {
                bounds.Width--;
                bounds.Height--;
                graphics.DrawRectangle(pen, bounds);
            }

            if (!this.Collapsed || tabPage.Selected)
            {
                using (var pen = new Pen(color))
                {
                    switch (tabPlacement)
                    {
                        case TabPlacement.Left:
                            {
                                bounds.Inflate(0, -1);
                                graphics.DrawLine(pen, new Point(bounds.X + bounds.Width, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
                                break;
                            }
                        case TabPlacement.Top:
                            {
                                bounds.Inflate(-1, 0);
                                graphics.DrawLine(pen, new Point(bounds.X, bounds.Y + bounds.Height), new Point(bounds.X + bounds.Width, bounds.Y + bounds.Height));
                                break;
                            }
                        case TabPlacement.Right:
                            {
                                bounds.Inflate(0, -1);
                                graphics.DrawLine(pen, new Point(bounds.X, bounds.Y), new Point(bounds.X, bounds.Y + bounds.Height));
                                break;
                            }
                        case TabPlacement.Bottom:
                            {
                                bounds.Inflate(-1, 0);
                                graphics.DrawLine(pen, new Point(bounds.X, bounds.Y), new Point(bounds.X + bounds.Width, bounds.Y));
                                break;
                            }
                    }
                }
            }

            this.DrawTabPageText(graphics, tabPage, tabPlacement);
        }

        private void DrawTabPageText(Graphics graphics, TabSplitPageControl tabPage, TabPlacement tabPlacement)
        {
            var angle = 0;
            var textRectangle = tabPage.TabRectangle;
            var text = tabPage.Text;

            if (!String.IsNullOrEmpty(text))
            {
                var stringFormat = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter, Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

                switch (tabPlacement)
                {
                    case TabPlacement.Left:
                        {
                            angle = 270;
                            break;
                        }
                    case TabPlacement.Top:
                        {
                            break;
                        }
                    case TabPlacement.Right:
                        {
                            angle = 90;
                            break;
                        }
                    case TabPlacement.Bottom:
                        {
                            break;
                        }
                }

                using (var graphicsCache = new GraphicsCache(graphics))
                {
                    using (var brush = new SolidBrush(TextColor))
                    {
                        graphicsCache.DrawVString(text, this.Font, brush, textRectangle, stringFormat, angle);
                    }
                }
            }
        }

        private void DrawTabStrip(Graphics graphics)
        {
            if (!this.Collapsed)
                this.DrawSplitBarGripper(graphics);

            switch (this.tabPlacement)
            {
                case TabPlacement.Left:
                    {
                        using (var pen = new Pen(BorderColor))
                        {
                            graphics.DrawLine(pen, this.workingAreaRectangle.Width, this.workingAreaRectangle.Y, this.workingAreaRectangle.Width, this.workingAreaRectangle.Height);
                        }
                        break;
                    }
                case TabPlacement.Top:
                    {
                        using (var pen = new Pen(BorderColor))
                        {
                            graphics.DrawLine(pen, this.workingAreaRectangle.X, this.workingAreaRectangle.Height, this.workingAreaRectangle.Width, this.workingAreaRectangle.Height);
                        }
                        break;
                    }
                case TabPlacement.Right:
                    {
                        using (var pen = new Pen(BorderColor))
                        {
                            graphics.DrawLine(pen, this.workingAreaRectangle.X, this.workingAreaRectangle.Y, this.workingAreaRectangle.X, this.workingAreaRectangle.Height);
                        }
                        break;
                    }
                case TabPlacement.Bottom:
                    {
                        using (var pen = new Pen(BorderColor))
                        {
                            graphics.DrawLine(pen, this.workingAreaRectangle.X, this.workingAreaRectangle.Y, this.workingAreaRectangle.Width, this.workingAreaRectangle.Y);
                        }
                        break;
                    }
                case TabPlacement.Middle:
                    {
                        if (this.Orientation == Orientation.Vertical)
                        {
                            using (var pen = new Pen(BorderColor))
                            {
                                graphics.DrawLine(pen, this.workingAreaRectangle.X, this.workingAreaRectangle.Y, this.workingAreaRectangle.X, this.workingAreaRectangle.Height);
                                graphics.DrawLine(pen, (this.workingAreaRectangle.X + this.workingAreaRectangle.Width - 1), this.workingAreaRectangle.Y, (this.workingAreaRectangle.X + this.workingAreaRectangle.Width - 1), this.workingAreaRectangle.Height);
                            }
                        }
                        else
                        {
                            using (var pen = new Pen(BorderColor))
                            {
                                graphics.DrawLine(pen, this.workingAreaRectangle.X, this.workingAreaRectangle.Y, this.workingAreaRectangle.Width, this.workingAreaRectangle.Y);
                                graphics.DrawLine(pen, this.workingAreaRectangle.X, (this.workingAreaRectangle.Y + this.workingAreaRectangle.Height - 1), this.workingAreaRectangle.Width, (this.workingAreaRectangle.Y + this.workingAreaRectangle.Height - 1));
                            }
                        }
                        break;
                    }
            }

            this.DrawTabPage(graphics, this.Panel1, this.GetTabPlacement(this.Panel1));
            this.DrawTabPage(graphics, this.Panel2, this.GetTabPlacement(this.Panel2));
        }

        private int GetSplitterDistance(int x, int y)
        {
            var delta = 0;

            if (this.Orientation == Orientation.Vertical)
                delta = x - anchor.X;
            else
                delta = y - anchor.Y;

            if (this.Orientation == Orientation.Vertical)
                return Math.Max(Math.Min((Math.Max(this.Panel1.Width + delta, borderSize)), this.Width - this.Panel2MinSize), this.Panel1MinSize);
            else
                return Math.Max(Math.Min((Math.Max(this.Panel1.Height + delta, borderSize)), this.Height - this.Panel2MinSize), this.Panel1MinSize);
        }

        private int GetRatioedSplitterDistance()
        {
            if (!this.Collapsed)
                return (this.Orientation == Orientation.Vertical) ? (int)(this.Width * this.splitRatio) : (int)(this.Height * this.splitRatio);

            return this.SplitterDistance;
        }

        private TabSplitPageControl GetTabPage(Point point)
        {
            if (this.Panel1.TabRectangle.Contains(point))
                return this.Panel1;

            if (this.Panel2.TabRectangle.Contains(point))
                return this.Panel2;

            return null;
        }

        private TabPlacement GetTabPlacement(TabSplitPageControl tabPage)
        {
            if (tabPage == this.Panel1)
            {
                if (this.orientation == Orientation.Horizontal)
                {
                    if (this.tabPlacement == TabPlacement.Top)
                        return TabPlacement.Top;
                    return TabPlacement.Bottom;
                }

                if (this.tabPlacement == TabPlacement.Left)
                    return TabPlacement.Left;
                return TabPlacement.Right;
            }

            if (tabPage != this.Panel2)
                return TabPlacement.Top;

            if (this.orientation == Orientation.Horizontal)
            {
                if (this.tabPlacement == TabPlacement.Bottom)
                    return TabPlacement.Bottom;
                return TabPlacement.Top;
            }

            if (this.tabPlacement == TabPlacement.Right)
                return TabPlacement.Right;

            return TabPlacement.Left;
        }

        private void Initialize()
        {
            this.SuspendLayout();

            this.splitterDistance = 50;
            this.panel1MinSize = 25;
            this.panel2MinSize = 25;

            this.buttonHorizontal = new CheckButton();
            this.buttonVertical = new CheckButton();
            this.buttonCollapseSplit = new SimpleButton();
            this.buttonCollapsePage1 = new SimpleButton();
            this.buttonCollapsePage2 = new SimpleButton();
            this.buttonSwapPanel = new SimpleButton();

            this.buttonHorizontal.AllowFocus = false;
            this.buttonHorizontal.GroupIndex = 1;
            this.buttonHorizontal.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonHorizontal.Size = new Size(13, 13);
            this.buttonHorizontal.TabIndex = 0;
            this.buttonHorizontal.TabStop = false;
            this.buttonHorizontal.CheckedChanged += new EventHandler(this.buttonHorizontal_CheckedChanged);
            this.buttonHorizontal.Paint += new PaintEventHandler(this.buttonHorizontal_Paint);

            this.buttonVertical.AllowFocus = false;
            this.buttonVertical.GroupIndex = 1;
            this.buttonVertical.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonVertical.Size = new Size(13, 13);
            this.buttonVertical.TabIndex = 0;
            this.buttonVertical.TabStop = false;
            this.buttonVertical.CheckedChanged += new EventHandler(this.buttonVertical_CheckedChanged);
            this.buttonVertical.Paint += new PaintEventHandler(this.buttonVertical_Paint);

            this.buttonCollapseSplit.AllowFocus = false;
            this.buttonCollapseSplit.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonCollapseSplit.Size = new Size(13, 13);
            this.buttonCollapseSplit.TabIndex = 0;
            this.buttonCollapseSplit.TabStop = false;
            this.buttonCollapseSplit.Click += new EventHandler(this.buttonCollapseSplit_Click);
            this.buttonCollapseSplit.Paint += new PaintEventHandler(this.buttonCollapseSplit_Paint);

            this.buttonCollapsePage1.AllowFocus = false;
            this.buttonCollapsePage1.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonCollapsePage1.Size = new Size(13, 13);
            this.buttonCollapsePage1.TabIndex = 0;
            this.buttonCollapsePage1.TabStop = false;
            this.buttonCollapsePage1.Click += new EventHandler(this.buttonCollapsePage1_Click);
            this.buttonCollapsePage1.Paint += new PaintEventHandler(this.buttonCollapsePage1_Paint);

            this.buttonCollapsePage2.AllowFocus = false;
            this.buttonCollapsePage2.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonCollapsePage2.Size = new Size(13, 13);
            this.buttonCollapsePage2.TabIndex = 0;
            this.buttonCollapsePage2.TabStop = false;
            this.buttonCollapsePage2.Click += new EventHandler(this.buttonCollapsePage2_Click);
            this.buttonCollapsePage2.Paint += new PaintEventHandler(this.buttonCollapsePage2_Paint);

            this.buttonSwapPanel.AllowFocus = false;
            this.buttonSwapPanel.ShowFocusRectangle = DefaultBoolean.False;
            this.buttonSwapPanel.Size = new Size(13, 13);
            this.buttonSwapPanel.TabIndex = 0;
            this.buttonSwapPanel.TabStop = false;
            this.buttonSwapPanel.BorderStyle = BorderStyles.NoBorder;
            this.buttonSwapPanel.Click += new EventHandler(this.buttonSwapPage_Click);
            this.buttonSwapPanel.Paint += new PaintEventHandler(this.buttonSwapPage_Paint);

            this.panel1 = new TabSplitPageControl();
            this.panel1.Text = "Panel1";
            this.panel1.Size = new Size(base.Width, (int)this.splitterDistance);

            this.panel2 = new TabSplitPageControl();
            this.panel2.Text = "Panel2";
            this.panel2.Size = new Size(base.Width, this.panel2MinSize);

            base.Controls.Add(this.buttonSwapPanel);
            base.Controls.Add(this.buttonCollapsePage2);
            base.Controls.Add(this.buttonCollapsePage1);
            base.Controls.Add(this.buttonCollapseSplit);
            base.Controls.Add(this.buttonVertical);
            base.Controls.Add(this.buttonHorizontal);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);

            this.ResumeLayout();
        }

        private void PositionButtons()
        {
            if (this.Orientation == Orientation.Vertical)
            {
                var x = buttonRectangle.X + 3;
                this.buttonVertical.Location = new Point(x, buttonRectangle.Y + (15 * 0) + 3);
                this.buttonHorizontal.Location = new Point(x, buttonRectangle.Y + (15 * 1) + 3);
                this.buttonCollapseSplit.Location = new Point(x, buttonRectangle.Y + (15 * 2) + 3);
                this.buttonCollapsePage1.Location = new Point(x, buttonRectangle.Y + (15 * 3) + 3);
                this.buttonCollapsePage2.Location = new Point(x, buttonRectangle.Y + (15 * 4) + 3);

                buttonSwapPanel.Visible = (!this.Collapsed);
                this.buttonSwapPanel.Location = new Point(x, Panel1.TabRectangle.Y + Panel1.TabRectangle.Height + 2);
            }
            else
            {
                var y = buttonRectangle.Y + 3;
                this.buttonVertical.Location = new Point(buttonRectangle.X + (15 * 0) + 3, y);
                this.buttonHorizontal.Location = new Point(buttonRectangle.X + (15 * 1) + 3, y);
                this.buttonCollapseSplit.Location = new Point(buttonRectangle.X + (15 * 2) + 3, y);
                this.buttonCollapsePage1.Location = new Point(buttonRectangle.X + (15 * 3) + 3, y);
                this.buttonCollapsePage2.Location = new Point(buttonRectangle.X + (15 * 4) + 3, y);

                buttonSwapPanel.Visible = (!this.Collapsed);
                this.buttonSwapPanel.Location = new Point(Panel1.TabRectangle.X + Panel1.TabRectangle.Width + 2, y);
            }
        }

        private void PositionTabs()
        {
            var tabRectangle = Rectangle.Empty;

            if (this.Orientation == Orientation.Vertical)
            {
                if (!this.Collapsed)
                {
                    tabRectangle = this.Panel1.TabRectangle;
                    tabRectangle.Location = new Point(this.tabStripRectangle.X, this.tabStripRectangle.Y + 3);
                    this.Panel1.TabRectangle = tabRectangle;

                    tabRectangle = this.Panel2.TabRectangle;
                    tabRectangle.Location = new Point(this.tabStripRectangle.X + 3, this.Panel1.TabRectangle.Y + this.Panel1.TabRectangle.Height + this.buttonSwapPanel.Height + 4);
                    this.Panel2.TabRectangle = tabRectangle;
                }
                else
                {
                    if (this.Panel1Collapsed)
                    {
                        tabRectangle = this.Panel1.TabRectangle;
                        tabRectangle.Location = new Point(this.tabStripRectangle.X + (this.Panel1.Selected ? 2 : 3), 3);
                        this.Panel1.TabRectangle = tabRectangle;

                        tabRectangle = this.Panel2.TabRectangle;
                        tabRectangle.Location = new Point(this.tabStripRectangle.X + (this.Panel2.Selected ? 2 : 3), this.Panel1.TabRectangle.Y + this.Panel1.TabRectangle.Height - 1);
                        this.Panel2.TabRectangle = tabRectangle;
                    }
                    else
                    {
                        tabRectangle = this.Panel1.TabRectangle;
                        tabRectangle.Location = new Point(this.tabStripRectangle.X, 3);
                        this.Panel1.TabRectangle = tabRectangle;

                        tabRectangle = this.Panel2.TabRectangle;
                        tabRectangle.Location = new Point(this.tabStripRectangle.X, this.Panel1.TabRectangle.Y + this.Panel1.TabRectangle.Height - 1);
                        this.Panel2.TabRectangle = tabRectangle;
                    }
                }
            }
            else
            {
                if (!this.Collapsed)
                {
                    tabRectangle = this.Panel1.TabRectangle;
                    tabRectangle.Location = new Point(this.tabStripRectangle.X + 3, this.tabStripRectangle.Y);
                    this.Panel1.TabRectangle = tabRectangle;

                    tabRectangle = this.Panel2.TabRectangle;
                    tabRectangle.Location = new Point(this.Panel1.TabRectangle.X + this.Panel1.TabRectangle.Width + this.buttonSwapPanel.Width + 4, this.tabStripRectangle.Y + 3);
                    this.Panel2.TabRectangle = tabRectangle;
                }
                else
                {
                    if (this.Panel1Collapsed)
                    {
                        tabRectangle = this.Panel1.TabRectangle;
                        tabRectangle.Location = new Point(3, this.tabStripRectangle.Y + (this.Panel1.Selected ? 2 : 3));
                        this.Panel1.TabRectangle = tabRectangle;

                        tabRectangle = this.Panel2.TabRectangle;
                        tabRectangle.Location = new Point(this.Panel1.TabRectangle.X + this.Panel1.TabRectangle.Width - 1, this.tabStripRectangle.Y + (this.Panel2.Selected ? 2 : 3));
                        this.Panel2.TabRectangle = tabRectangle;
                    }
                    else
                    {
                        tabRectangle = this.Panel1.TabRectangle;
                        tabRectangle.Location = new Point(3, this.tabStripRectangle.Y);
                        this.Panel1.TabRectangle = tabRectangle;

                        tabRectangle = this.Panel2.TabRectangle;
                        tabRectangle.Location = new Point(this.Panel1.TabRectangle.X + this.Panel1.TabRectangle.Width - 1, this.tabStripRectangle.Y);
                        this.Panel2.TabRectangle = tabRectangle;
                    }
                }
            }
        }

        private void ProcessTabSelection(TabSplitPageControl tabPage)
        {
            this.SelectedPage = tabPage;
        }

        private void ResizeLayout()
        {
            if (this.Created)
            {
                this.Panel1.SuspendLayout();
                this.Panel2.SuspendLayout();

                this.SplitterDistance = this.GetRatioedSplitterDistance();

                this.UpdateLayout();

                this.Panel1.ResumeLayout();
                this.Panel2.ResumeLayout();
            }
        }

        private static Image ResizeImage(Image image, int width, int height)
        {
            // This method was to be used to 
            // drawe the image skin for the tab.
            // I've been unable to successfully drawe a rotated devexpress image
            // in the same quality as DevExpress...

            var rectangle = new Rectangle(0, 0, width, height);
            var bitmap = new Bitmap(width, height);

            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, rectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return (Image)bitmap;
        }

        private void SelectPage()
        {
            if (this.Collapsed)
            {
                if (this.selectedPage == this.Panel1)
                {
                    this.Panel1.Selected = true;
                    this.Panel1.Visible = true;

                    this.Panel2.Selected = false;
                    this.Panel2.Visible = false;
                }

                if (this.selectedPage == this.Panel2)
                {
                    this.Panel1.Selected = false;
                    this.Panel1.Visible = false;

                    this.Panel2.Selected = true;
                    this.Panel2.Visible = true;
                }
            }
            else
            {
                this.Panel1.Selected = false;
                this.Panel1.Visible = true;

                this.Panel2.Selected = false;
                this.Panel2.Visible = true;
            }

            this.UpdateLayout();
        }

        private void SetCursor(ref Message message)
        {
            if (message.WParam == this.Handle && ((int)message.LParam & 0x0000ffff) == NativeMethods.HTCLIENT)
            {
                if (this.OverrideCursor != null)
                    Cursor.Current = this.OverrideCursor;
                else
                    Cursor.Current = Cursor;
            }
            else
                this.DefWndProc(ref message);
        }

        private void SplitBegin(int x, int y)
        {
            this.anchor = new Point(x, y);

            this.splitterDistance = this.GetSplitterDistance(x, y);
            this.splitterDistanceInitial = this.splitterDistance;

            this.Capture = true;
            this.DrawSplitBar(DRAW_START);
        }

        private void SplitEnd(bool accept)
        {
            this.DrawSplitBar(DRAW_END);

            if (accept)
                this.ApplySplitterDistance();
            else if (this.splitterDistance != this.splitterDistanceInitial)
            {
                this.splitterClick = false;
                this.splitterDistance = this.SplitterDistanceInternal = this.splitterDistanceInitial;
            }

            this.anchor = Point.Empty;
        }

        private void SplitMove(int x, int y)
        {
            var size = this.GetSplitterDistance(x, y);
            var delta = (size - this.splitterDistanceInitial);
            var mod = delta % this.SplitterIncrement;

            if (this.splitterDistance != size)
            {
                if (this.Orientation == Orientation.Vertical)
                {
                    if (size + this.SplitterWidth <= this.Width - this.Panel2MinSize - this.borderSize)
                        this.splitterDistance = (size - mod);
                }
                else
                {
                    if (size + this.SplitterWidth <= this.Height - this.Panel2MinSize - this.borderSize)
                        this.splitterDistance = (size - mod);
                }
            }

            this.DrawSplitBar(DRAW_MOVE);
        }

        private void SwapPanels()
        {
            this.SuspendLayout();

            var text1 = this.Panel1.Text;
            var text2 = this.Panel2.Text;

            var controlArray1 = this.Panel1.Controls.Cast<Control>().ToArray();
            var controlArray2 = this.Panel2.Controls.Cast<Control>().ToArray();

            this.Panel1.Text = text2;
            this.Panel2.Text = text1;

            this.Panel1.Controls.Clear();
            this.Panel2.Controls.Clear();

            this.Panel1.Controls.AddRange(controlArray2);
            this.Panel2.Controls.AddRange(controlArray1);

            this.panelsSwapped = !this.panelsSwapped;

            this.ResumeLayout();

            this.UpdateLayout();

            this.OnPanelsSwapped(new TabSplitContainerPanelSwappedEventArgs());
        }

        private void UpdateButtons()
        {
            if (this.Orientation == Orientation.Vertical)
                buttonVertical.Checked = true;
            else
                buttonHorizontal.Checked = true;

            this.buttonSwapPanel.Visible = !this.Collapsed;
            this.buttonCollapsePage1.Enabled = !this.Panel1Collapsed;
            this.buttonCollapsePage2.Enabled = !this.Panel2Collapsed;
        }
        #endregion

        #region Events
        public void OnOrientationChanged(TabSplitContainerOrientationChangedEventArgs e)
        {
            var handler = (OrientationChangedEventHandler)this.Events[EVENT_ORIENTATION];
            if (handler != null)
                handler(this, e);
        }

        public void OnPanelCollapsed(TabSplitContainerPanelCollapsedEventArgs e)
        {
            var handler = (PageCollapsedEventHandler)this.Events[EVENT_COLLAPSED];
            if (handler != null)
                handler(this, e);
        }

        public void OnSelectedPageChanged(TabSplitContainerSelectedPageChangedEventArgs e)
        {
            // Currently NOT Working!

            var handler = (SelectedPageChangedEventHandler)this.Events[EVENT_PAGECHANGED];
            if (handler != null)
                handler(this, e);
        }

        public void OnSelectedPageChanging(TabSplitContainerSelectedPageChangingEventArgs e)
        {
            // Currently NOT Working!

            var handler = (SelectedPageChangingEventHandler)this.Events[EVENT_PAGECHANGING];
            if (handler != null)
                handler(this, e);
        }

        public void OnPanelsSwapped(TabSplitContainerPanelSwappedEventArgs e)
        {
            var handler = (PageSwappedEventHandler)this.Events[EVENT_SWAPPED];
            if (handler != null)
                handler(this, e);
        }

        public void OnSplitterMoved(SplitterEventArgs e)
        {
            var handler = (SplitterEventHandler)this.Events[EVENT_MOVED];
            if (handler != null)
                handler(this, e);
        }

        public void OnSplitterMoving(SplitterCancelEventArgs e)
        {
            var handler = (SplitterCancelEventHandler)this.Events[EVENT_MOVING];
            if (handler != null)
                handler(this, e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (!this.setSplitterDistance)
                this.ResizeLayout();

            base.OnLayout(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!this.Enabled)
                return;

            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (this.buttonRectangle.Contains(e.Location))
                    return;

                if (this.tabStripRectangle.Contains(e.Location))
                {
                    var tabPage = this.GetTabPage(e.Location);

                    this.Panel1.MouseOver = false;
                    this.Panel2.MouseOver = false;

                    if (tabPage != null)
                        this.ProcessTabSelection(tabPage);

                    this.Invalidate();
                    this.Update();

                    return;
                }

                if (this.IsSplitterMovable && this.splitterRectangle.Contains(e.Location))
                {
                    //if (e.Button == MouseButtons.Left && e.Clicks == 1)
                    //{
                        //var iContainerControl = this.Parent.GetContainerControl();
                        //if (iContainerControl != null)
                        //{
                        //    var containerControl = iContainerControl as ContainerControl;
                        //    if (containerControl == null)
                        //        iContainerControl.ActiveControl = this;
                        //    else
                        //        containerControl.ActiveControl = this;
                        //}

                        //this.ActiveControl = null;
                        //this.SelectNextControl(this.Panel2, false, true, true, true);

                        this.SplitBegin(e.X, e.Y);
                        this.splitterClick = true;
                    //}
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (!this.Enabled)
                return;

            this.Panel1.MouseOver = false;
            this.Panel2.MouseOver = false;

            this.OverrideCursor = null;

            this.Invalidate();
            this.Update();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.Collapsed)
            {
                this.Panel1.MouseOver = this.Panel1.TabRectangle.Contains(e.Location) && !this.Panel1.Selected;
                this.Panel2.MouseOver = this.Panel2.TabRectangle.Contains(e.Location) && !this.Panel2.Selected;
            }
            else
            {
                this.Panel1.MouseOver = false;
                this.Panel2.MouseOver = false;
            }

            if (this.buttonRectangle.Contains(e.Location) || this.tabStripRectangle.Contains(e.Location))
            {
                if (this.OverrideCursor != null)
                    this.OverrideCursor = null;

                this.Invalidate();
                this.Update();

                return;
            }

            if (this.IsSplitterMovable)
            {
                if (this.Cursor == this.DefaultCursor && this.splitterRectangle.Contains(e.Location))
                {
                    if (this.Orientation == Orientation.Vertical)
                        this.OverrideCursor = Cursors.VSplit;
                    else
                        this.OverrideCursor = Cursors.HSplit;
                }
                else
                    this.OverrideCursor = null;

                if (this.splitterClick)
                {
                    var x = e.X;
                    var y = e.Y;

                    this.splitterDrag = true;
                    this.SplitMove(x, y);

                    if (this.Orientation == Orientation.Vertical)
                    {
                        x = Math.Max(Math.Min(x, this.Width - this.Panel2MinSize), this.Panel1MinSize);
                        y = Math.Max(y, 0);
                    }
                    else
                    {
                        y = Math.Max(Math.Min(y, this.Height - this.Panel2MinSize), this.Panel1MinSize);
                        x = Math.Max(x, 0);
                    }

                    var rectangle = this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
                    var xSplit = rectangle.X;
                    var ySplit = rectangle.Y;

                    var splitterCancelEventArgs = new SplitterCancelEventArgs(x, y, xSplit, ySplit);
                    this.OnSplitterMoving(splitterCancelEventArgs);

                    if (splitterCancelEventArgs.Cancel)
                        this.SplitEnd(false);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!this.Enabled)
                return;

            if (this.IsSplitterMovable && this.splitterClick)
            {
                this.Capture = false;

                if (this.splitterDrag)
                {
                    this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
                    this.SplitEnd(true);
                }
                else
                    this.SplitEnd(false);

                this.splitterClick = false;
                this.splitterDrag = false;
            }

            base.OnMouseUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.DrawControlBorder(e.Graphics);

            this.DrawTabStrip(e.Graphics);
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            this.Panel1.RightToLeft = this.RightToLeft;
            this.Panel2.RightToLeft = this.RightToLeft;

            this.UpdateLayout();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_SETCURSOR:
                    {
                        this.SetCursor(ref m);
                        break;
                    }

                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }

        private void buttonCollapsePage1_Click(object sender, EventArgs e)
        {
            this.Panel1Collapsed = true;
        }

        private void buttonCollapsePage1_Paint(object sender, PaintEventArgs e)
        {
            var color = this.Panel1Collapsed ? GrayTextColor : TextColor;

            using (var brush = new SolidBrush(color))
            {
                e.Graphics.FillRectangles(brush, this.CollapsePage1Image);
            }
        }

        private void buttonCollapsePage2_Click(object sender, EventArgs e)
        {
            this.Panel2Collapsed = true;
        }

        private void buttonCollapsePage2_Paint(object sender, PaintEventArgs e)
        {
            var color = this.Panel2Collapsed ? GrayTextColor : TextColor;

            using (var brush = new SolidBrush(color))
            {
                e.Graphics.FillRectangles(brush, this.CollapsePage2Image);
            }
        }

        private void buttonCollapseSplit_Click(object sender, EventArgs e)
        {
            if (!this.Collapsed)
                this.Panel2Collapsed = true;
            else
            {
                if (this.Panel1Collapsed)
                    this.Panel1Collapsed = false;
                if (this.Panel2Collapsed)
                    this.Panel2Collapsed = false;
                this.SelectedPage = null;
            }
        }

        private void buttonCollapseSplit_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(TextColor))
            {
                e.Graphics.FillRectangles(brush, this.CollapseSplitImage);
            }
        }

        private void buttonHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (buttonHorizontal.Checked && this.Orientation != Orientation.Horizontal)
                this.Orientation = Orientation.Horizontal;
        }

        private void buttonHorizontal_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(TextColor))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(3, 6, 7, 1));
            }
        }

        private void buttonSwapPage_Click(object sender, EventArgs e)
        {
            this.SwapPanels();
        }

        private void buttonSwapPage_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(TextColor))
            {
                e.Graphics.FillRectangles(brush, this.SwapPageImage);
            }
        }

        private void buttonVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (buttonVertical.Checked && this.Orientation != Orientation.Vertical)
                this.Orientation = Orientation.Vertical;
        }

        private void buttonVertical_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new SolidBrush(TextColor))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(6, 3, 1, 7));
            }
        }
        #endregion
    }
}