using System;
using System.ComponentModel;

namespace CustomControl
{
    public class TabSplitContainerSelectedPageChangingEventArgs : CancelEventArgs
    {
        #region Fields
        private TabSplitPageControl oldPage;
        private TabSplitPageControl newPage;
        #endregion

        #region Properties
        public TabSplitPageControl OldPage
        {
            get { return this.oldPage; }
            private set { this.oldPage = value; }
        }
        public TabSplitPageControl NewPage
        {
            get { return this.newPage; }
            private set { this.newPage = value; }
        }
        #endregion

        #region Constructors
        public TabSplitContainerSelectedPageChangingEventArgs(TabSplitPageControl oldPage, TabSplitPageControl newPage)
        {
            this.oldPage = oldPage;
            this.newPage = newPage;
        }
        #endregion
    }
}