using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CustomControl.Designers
{
    public class TabSplitContainerControlDesigner : ParentControlDesigner
    {
        #region Methods
        public override bool CanParent(Control control)
        {
            return control is Control;
        }

        public override bool CanParent(ControlDesigner controlDesigner)
        {
            if (controlDesigner != null && controlDesigner.Control is Control)
                return true;

            return false;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (this.Control is TabSplitContainerControl)
            {
                var control = (TabSplitContainerControl)this.Control;
                if (control != null)
                {
                    this.EnableDesignMode(control.Panel1, "Panel1");
                    this.EnableDesignMode(control.Panel2, "Panel2");
                }
            }
        }
        #endregion
    }
}