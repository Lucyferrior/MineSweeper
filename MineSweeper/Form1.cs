using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.MinimumSize = groupBox1.Size;
            //this.MinimumSize.Height = groupBox1.Location.Y + groupBox1.Size.Height + table.Size.Height;
        }
        
        public void btnClick(object sender, EventArgs e)
        {
            Trace.WriteLine("main button tıklandı");
        }
        Table table = new Table();
        private void button1_Click(object sender, EventArgs e)
        {
            int gameSize = 0;
            int mineCount = 0;
            int.TryParse(textBoxGameSize.Text,out gameSize);
            int.TryParse(textBoxMineCount.Text,out mineCount);
            if(gameSize >0 && mineCount >= 0)
            {
                table.CreateGame(gameSize, mineCount);
                Controls.Add(table);
            }
            Form1_Resize(sender, new EventArgs());
            Trace.WriteLine(this.Size.Width);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.MinimumSize = new System.Drawing.Size(groupBox1.Size.Width, groupBox1.Location.Y + groupBox1.Size.Height*2 + table.Size.Height);
            groupBox1.Location = new Point(this.Size.Width / 2 - groupBox1.Size.Width/2, groupBox1.Location.Y);
            //table.Location = new Point(this.Size.Width/2 - table.Width/2, this.Size.Height/2- table.Height/2);
            table.Location = new Point(this.Size.Width / 2 - table.Width / 2, groupBox1.Size.Height + groupBox1.Location.Y+25);
        }
    }
}


/*0 lft top
1 top
2 righttop
3 right
4 right bottom
5 bottom
6 leftbottom
7 left*/