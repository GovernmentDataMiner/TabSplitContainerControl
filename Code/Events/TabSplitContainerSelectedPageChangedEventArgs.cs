using System;

namespace CustomControl
{
    public class TabSplitContainerSelectedPageChangedEventArgs : EventArgs
    {
        #region Fields
        private TabSplitPageControl selectedPage;
        #endregion

        #region Properties
        public TabSplitPageControl SelectedPage
        {
            get { return this.selectedPage; }
            private set { this.selectedPage = value; }
        }
        #endregion

        #region Constructors
        public TabSplitContainerSelectedPageChangedEventArgs(TabSplitPageControl selectedPage)
        {
            this.selectedPage = selectedPage;
        }
        #endregion
    }
}