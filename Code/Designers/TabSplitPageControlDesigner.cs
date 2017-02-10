using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CustomControl.Designers
{
    public class TabSplitPageControlDesigner : ScrollableControlDesigner
    {
        #region Methods
        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            base.OnPaintAdornments(pe);

            this.DrawBorder(pe.Graphics);
        }

        private void DrawBorder(Graphics graphics)
        {
            Color color;
            var control = this.Control;
            var clientRectangle = control.ClientRectangle;

            if (control.BackColor.GetBrightness() < 0.5)
                color = Color.White;
            else
                color = Color.Black;

            using (var pen = new Pen(color))
            {
                pen.DashStyle = DashStyle.Dash;

                clientRectangle.Width--;
                clientRectangle.Height--;

                graphics.DrawRectangle(pen, clientRectangle);
            }
        }
        #endregion
    }
}