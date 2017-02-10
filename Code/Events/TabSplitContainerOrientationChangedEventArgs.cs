using System;
using System.Windows.Forms;

namespace CustomControl
{
    public class TabSplitContainerOrientationChangedEventArgs : EventArgs
    {
        #region Fields
        private Orientation _oldOrientation;
        private Orientation _newOrientation;
        #endregion

        #region Properties
        public Orientation OldOrientation
        {
            get { return this._oldOrientation; }
            private set { this._oldOrientation = value; }
        }
        public Orientation NewOrientation
        {
            get { return this._newOrientation; }
            private set { this._newOrientation = value; }
        }
        #endregion

        #region Constructors
        public TabSplitContainerOrientationChangedEventArgs(Orientation oldOrientation, Orientation newOrientation)
            : base()
        {
            this._oldOrientation = oldOrientation;
            this._newOrientation = newOrientation;
        }
        #endregion
    }
}