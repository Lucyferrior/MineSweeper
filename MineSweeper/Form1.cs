﻿using System;
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
            
            Table _table = new Table();
            _table.CreateGame(10,2);
            Controls.Add(_table);
        }
        public void btnClick(object sender, EventArgs e)
        {
            Trace.WriteLine("main button tıklandı");
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