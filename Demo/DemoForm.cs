using CustomControl;

using DevExpress.XtraBars;
using DevExpress.XtraEditors;

using System;
using System.Windows.Forms;

namespace Demo
{
    public partial class DemoForm : XtraForm
    {
        #region Constructors
        public DemoForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void SetBarCheckItemOrientation()
        {
            barCheckItemOrientationVertical.Checked = (tabSplitContainerControl1.Orientation == Orientation.Vertical);
            barCheckItemOrientationHorizontal.Checked = (tabSplitContainerControl1.Orientation == Orientation.Horizontal);
        }
        #endregion

        #region Events
        private void barCheckItemCollapsePanel1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            var barCheckItem = sender as BarCheckItem;
            if (barCheckItem != null)
                tabSplitContainerControl1.Panel1Collapsed = barCheckItem.Checked;
        }

        private void barCheckItemCollapsePanel2_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            var barCheckItem = sender as BarCheckItem;
            if (barCheckItem != null)
                tabSplitContainerControl1.Panel2Collapsed = barCheckItem.Checked;
        }

        private void barCheckItemOrientationVertical_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            tabSplitContainerControl1.Orientation = Orientation.Vertical;
        }

        private void barCheckItemOrientationHorizontal_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            tabSplitContainerControl1.Orientation = Orientation.Horizontal;
        }

        private void barButtonItemSwapPanels_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void demoForm_Load(object sender, EventArgs e)
        {
            this.SetBarCheckItemOrientation();
        }

        private void tabSplitContainerControl_OrientationChanged(object sender, TabSplitContainerOrientationChangedEventArgs e)
        {
            this.SetBarCheckItemOrientation();
        }

        private void tabSplitContainerControl_PagesSwapped(object sender, TabSplitContainerPanelSwappedEventArgs e)
        {
            //MessageBox.Show("Pages Have Been Swapped!");
        }
        #endregion

        private void barButtonItemSplitterInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MessageBox.Show(tabSplitContainerControl.SplitterSize.ToString());
        }

        private void tabSplitContainerControl_PanelCollapsed(object sender, TabSplitContainerPanelCollapsedEventArgs e)
        {
            var collapsedPanel = e.CollapsedPage;
        }

        private void tabSplitContainerControl_SelectedPageChanged(object sender, TabSplitContainerSelectedPageChangedEventArgs e)
        {
            var selected = e.SelectedPage;
        }

        private void tabSplitContainerControl_SelectedPageChanging(object sender, TabSplitContainerSelectedPageChangingEventArgs e)
        {
            var selectedOld = e.OldPage;
            var selectedNew = e.NewPage;
        }

        private void barButtonItemSelectedPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPage = tabSplitContainerControl1.SelectedPage;

            if (selectedPage == null)
                MessageBox.Show("Selected Page is NULL");
            else
                MessageBox.Show(selectedPage.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }
    }
}