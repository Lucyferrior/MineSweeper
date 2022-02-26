using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class MatrixView : ContainerControl
    {
        public MatrixView()
        {
            BackColor = Color.White;
            this.Size = new Size(250, 250);
            this.Location = new Point(250, 100);
        }
        public void Alignment()
        {
            Box btn = this.Controls[0] as Box;
            int buttonCount = (int)Math.Sqrt(this.Controls.Count);

            int thisSizeX = (btn.Left + btn.Width) * buttonCount;
            int thisSizeY = (btn.Top + btn.Height) * buttonCount;
            Trace.WriteLine(btn.Location.X + ", " + btn.Width + ", " + buttonCount + " = " + thisSizeX);
            Trace.WriteLine(btn.Location.Y + ", " + btn.Height + ", " + buttonCount + " = " + thisSizeY);


            this.Size = new Size(thisSizeX, thisSizeY);
        }
    }
}
