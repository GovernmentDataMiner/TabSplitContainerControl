using System;

namespace CustomControl
{
    public class TabSplitContainerPanelCollapsedEventArgs : EventArgs
    {
        #region Fields
        private TabSplitPageControl collapsedPage;
        #endregion

        #region Properties
        public TabSplitPageControl CollapsedPage
        {
            get { return this.collapsedPage; }
            private set { this.collapsedPage = value; }
        }
        #endregion

        #region Constructors
        public TabSplitContainerPanelCollapsedEventArgs(TabSplitPageControl collapsedPage)
        {
            this.collapsedPage = collapsedPage;
        }
        #endregion
    }
}
