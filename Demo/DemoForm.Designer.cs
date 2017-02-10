namespace Demo
{
    partial class DemoForm
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
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barToolbar = new DevExpress.XtraBars.Bar();
            this.skinBarSubItemSkin = new DevExpress.XtraBars.SkinBarSubItem();
            this.barSubItemAction = new DevExpress.XtraBars.BarSubItem();
            this.barSubItemCollapse = new DevExpress.XtraBars.BarSubItem();
            this.barCheckItemCollapsePage1 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItemCollapsePage2 = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItemOrientation = new DevExpress.XtraBars.BarSubItem();
            this.barCheckItemOrientationVertical = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItemOrientationHorizontal = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItemSelectedPage = new DevExpress.XtraBars.BarButtonItem();
            this.barStatusbar = new DevExpress.XtraBars.Bar();
            this.barStaticItemStatus = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItemCollapsePanel1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCollapsePanel2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSwapPages = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSplitterInfo = new DevExpress.XtraBars.BarButtonItem();
            this.tabSplitContainerControl1 = new CustomControl.TabSplitContainerControl();
            this.richEditControl1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.richEditControl2 = new DevExpress.XtraRichEdit.RichEditControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1.Panel1)).BeginInit();
            this.tabSplitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1.Panel2)).BeginInit();
            this.tabSplitContainerControl1.Panel2.SuspendLayout();
            this.tabSplitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.AllowShowToolbarsPopup = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolbar,
            this.barStatusbar});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.skinBarSubItemSkin,
            this.barStaticItemStatus,
            this.barSubItemCollapse,
            this.barButtonItemCollapsePanel1,
            this.barButtonItemCollapsePanel2,
            this.barCheckItemCollapsePage1,
            this.barCheckItemCollapsePage2,
            this.barSubItemOrientation,
            this.barCheckItemOrientationVertical,
            this.barCheckItemOrientationHorizontal,
            this.barButtonItemSwapPages,
            this.barSubItemAction,
            this.barButtonItemSplitterInfo,
            this.barButtonItemSelectedPage});
            this.barManager.MaxItemId = 16;
            this.barManager.StatusBar = this.barStatusbar;
            // 
            // barToolbar
            // 
            this.barToolbar.BarName = "Toolbar";
            this.barToolbar.DockCol = 0;
            this.barToolbar.DockRow = 0;
            this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.skinBarSubItemSkin),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemAction),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSelectedPage)});
            this.barToolbar.OptionsBar.DrawDragBorder = false;
            this.barToolbar.OptionsBar.UseWholeRow = true;
            this.barToolbar.Text = "Toolbar";
            // 
            // skinBarSubItemSkin
            // 
            this.skinBarSubItemSkin.Caption = "&Skin";
            this.skinBarSubItemSkin.Id = 0;
            this.skinBarSubItemSkin.Name = "skinBarSubItemSkin";
            // 
            // barSubItemAction
            // 
            this.barSubItemAction.Caption = "&Action";
            this.barSubItemAction.Id = 13;
            this.barSubItemAction.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemCollapse),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItemOrientation, true)});
            this.barSubItemAction.Name = "barSubItemAction";
            // 
            // barSubItemCollapse
            // 
            this.barSubItemCollapse.Caption = "&Collapse";
            this.barSubItemCollapse.Id = 4;
            this.barSubItemCollapse.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemCollapsePage1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemCollapsePage2)});
            this.barSubItemCollapse.Name = "barSubItemCollapse";
            // 
            // barCheckItemCollapsePage1
            // 
            this.barCheckItemCollapsePage1.AllowAllUp = true;
            this.barCheckItemCollapsePage1.Caption = "Page &1";
            this.barCheckItemCollapsePage1.GroupIndex = 2;
            this.barCheckItemCollapsePage1.Id = 7;
            this.barCheckItemCollapsePage1.Name = "barCheckItemCollapsePage1";
            this.barCheckItemCollapsePage1.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemCollapsePanel1_CheckedChanged);
            // 
            // barCheckItemCollapsePage2
            // 
            this.barCheckItemCollapsePage2.AllowAllUp = true;
            this.barCheckItemCollapsePage2.Caption = "Page &2";
            this.barCheckItemCollapsePage2.GroupIndex = 2;
            this.barCheckItemCollapsePage2.Id = 8;
            this.barCheckItemCollapsePage2.Name = "barCheckItemCollapsePage2";
            this.barCheckItemCollapsePage2.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemCollapsePanel2_CheckedChanged);
            // 
            // barSubItemOrientation
            // 
            this.barSubItemOrientation.Caption = "&Orientation";
            this.barSubItemOrientation.Id = 9;
            this.barSubItemOrientation.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemOrientationVertical),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItemOrientationHorizontal)});
            this.barSubItemOrientation.Name = "barSubItemOrientation";
            // 
            // barCheckItemOrientationVertical
            // 
            this.barCheckItemOrientationVertical.Caption = "&Vertical";
            this.barCheckItemOrientationVertical.GroupIndex = 1;
            this.barCheckItemOrientationVertical.Id = 10;
            this.barCheckItemOrientationVertical.Name = "barCheckItemOrientationVertical";
            this.barCheckItemOrientationVertical.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemOrientationVertical_CheckedChanged);
            // 
            // barCheckItemOrientationHorizontal
            // 
            this.barCheckItemOrientationHorizontal.Caption = "&Horizontal";
            this.barCheckItemOrientationHorizontal.GroupIndex = 1;
            this.barCheckItemOrientationHorizontal.Id = 11;
            this.barCheckItemOrientationHorizontal.Name = "barCheckItemOrientationHorizontal";
            this.barCheckItemOrientationHorizontal.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItemOrientationHorizontal_CheckedChanged);
            // 
            // barButtonItemSelectedPage
            // 
            this.barButtonItemSelectedPage.Caption = "Selected Page";
            this.barButtonItemSelectedPage.Id = 15;
            this.barButtonItemSelectedPage.Name = "barButtonItemSelectedPage";
            this.barButtonItemSelectedPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSelectedPage_ItemClick);
            // 
            // barStatusbar
            // 
            this.barStatusbar.BarName = "Statusbar";
            this.barStatusbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.barStatusbar.DockCol = 0;
            this.barStatusbar.DockRow = 0;
            this.barStatusbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barStatusbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemStatus)});
            this.barStatusbar.OptionsBar.AllowQuickCustomization = false;
            this.barStatusbar.OptionsBar.DrawDragBorder = false;
            this.barStatusbar.OptionsBar.UseWholeRow = true;
            this.barStatusbar.Text = "Statusbar";
            // 
            // barStaticItemStatus
            // 
            this.barStaticItemStatus.Caption = "Ready";
            this.barStaticItemStatus.Id = 1;
            this.barStaticItemStatus.Name = "barStaticItemStatus";
            this.barStaticItemStatus.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(2, 2);
            this.barDockControlTop.Manager = this.barManager;
            this.barDockControlTop.Size = new System.Drawing.Size(1024, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(2, 492);
            this.barDockControlBottom.Manager = this.barManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(1024, 26);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 28);
            this.barDockControlLeft.Manager = this.barManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 464);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1026, 28);
            this.barDockControlRight.Manager = this.barManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 464);
            // 
            // barButtonItemCollapsePanel1
            // 
            this.barButtonItemCollapsePanel1.Caption = "Panel 1";
            this.barButtonItemCollapsePanel1.Id = 5;
            this.barButtonItemCollapsePanel1.Name = "barButtonItemCollapsePanel1";
            // 
            // barButtonItemCollapsePanel2
            // 
            this.barButtonItemCollapsePanel2.Caption = "Panel 2";
            this.barButtonItemCollapsePanel2.Id = 6;
            this.barButtonItemCollapsePanel2.Name = "barButtonItemCollapsePanel2";
            // 
            // barButtonItemSwapPages
            // 
            this.barButtonItemSwapPages.Caption = "Swap Pages";
            this.barButtonItemSwapPages.Id = 12;
            this.barButtonItemSwapPages.Name = "barButtonItemSwapPages";
            this.barButtonItemSwapPages.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSwapPanels_ItemClick);
            // 
            // barButtonItemSplitterInfo
            // 
            this.barButtonItemSplitterInfo.Caption = "&Splitter Info";
            this.barButtonItemSplitterInfo.Id = 14;
            this.barButtonItemSplitterInfo.Name = "barButtonItemSplitterInfo";
            this.barButtonItemSplitterInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSplitterInfo_ItemClick);
            // 
            // tabSplitContainerControl1
            // 
            this.tabSplitContainerControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabSplitContainerControl1.CollapsePage1ButtonToolTip = "";
            this.tabSplitContainerControl1.CollapsePage2ButtonToolTip = "";
            this.tabSplitContainerControl1.CollapseSplitButtonToolTip = "";
            this.tabSplitContainerControl1.HorizontalButtonToolTip = "";
            this.tabSplitContainerControl1.Location = new System.Drawing.Point(97, 89);
            this.tabSplitContainerControl1.Name = "tabSplitContainerControl1";
            this.tabSplitContainerControl1.Page1Text = "English";
            this.tabSplitContainerControl1.Page2Text = "French";
            // 
            // tabSplitContainerControl1.Panel1
            // 
            this.tabSplitContainerControl1.Panel1.Controls.Add(this.richEditControl1);
            this.tabSplitContainerControl1.Panel1.MouseOver = false;
            this.tabSplitContainerControl1.Panel1.Selected = false;
            // 
            // tabSplitContainerControl1.Panel2
            // 
            this.tabSplitContainerControl1.Panel2.Controls.Add(this.richEditControl2);
            this.tabSplitContainerControl1.Panel2.MouseOver = false;
            this.tabSplitContainerControl1.Panel2.Selected = false;
            this.tabSplitContainerControl1.Size = new System.Drawing.Size(440, 261);
            this.tabSplitContainerControl1.SplitterDistance = 176;
            this.tabSplitContainerControl1.SwapPanelButtonToolTip = "";
            this.tabSplitContainerControl1.TabIndex = 5;
            this.tabSplitContainerControl1.VerticalButtonToolTip = "";
            // 
            // richEditControl1
            // 
            this.richEditControl1.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl1.Appearance.Text.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richEditControl1.Appearance.Text.Options.UseFont = true;
            this.richEditControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl1.Location = new System.Drawing.Point(2, 2);
            this.richEditControl1.MenuManager = this.barManager;
            this.richEditControl1.Name = "richEditControl1";
            this.richEditControl1.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richEditControl1.Size = new System.Drawing.Size(171, 255);
            this.richEditControl1.TabIndex = 0;
            this.richEditControl1.Text = "Hello World, I\'m Panel One";
            // 
            // richEditControl2
            // 
            this.richEditControl2.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
            this.richEditControl2.Appearance.Text.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richEditControl2.Appearance.Text.Options.UseFont = true;
            this.richEditControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richEditControl2.Location = new System.Drawing.Point(2, 2);
            this.richEditControl2.MenuManager = this.barManager;
            this.richEditControl2.Name = "richEditControl2";
            this.richEditControl2.Options.VerticalScrollbar.Visibility = DevExpress.XtraRichEdit.RichEditScrollbarVisibility.Hidden;
            this.richEditControl2.Size = new System.Drawing.Size(240, 255);
            this.richEditControl2.TabIndex = 0;
            this.richEditControl2.Text = "Bonjour tout le monde, je suis Panel One";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(543, 89);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(300, 261);
            this.xtraTabControl1.TabIndex = 10;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(298, 234);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(298, 273);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 520);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.tabSplitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DemoForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tab Split Container Demo";
            this.Load += new System.EventHandler(this.demoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1.Panel1)).EndInit();
            this.tabSplitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1.Panel2)).EndInit();
            this.tabSplitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSplitContainerControl1)).EndInit();
            this.tabSplitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barToolbar;
        private DevExpress.XtraBars.Bar barStatusbar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.SkinBarSubItem skinBarSubItemSkin;
        private DevExpress.XtraBars.BarStaticItem barStaticItemStatus;
        private DevExpress.XtraBars.BarSubItem barSubItemCollapse;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCollapsePanel1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCollapsePanel2;
        private DevExpress.XtraBars.BarCheckItem barCheckItemCollapsePage1;
        private DevExpress.XtraBars.BarCheckItem barCheckItemCollapsePage2;
        private DevExpress.XtraBars.BarSubItem barSubItemOrientation;
        private DevExpress.XtraBars.BarCheckItem barCheckItemOrientationVertical;
        private DevExpress.XtraBars.BarCheckItem barCheckItemOrientationHorizontal;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSwapPages;
        private DevExpress.XtraBars.BarSubItem barSubItemAction;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSplitterInfo;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSelectedPage;
        private CustomControl.TabSplitContainerControl tabSplitContainerControl1;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl1;
        private DevExpress.XtraRichEdit.RichEditControl richEditControl2;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
    }
}