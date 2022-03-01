using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public enum CellPosition
    {
        LeftTopCell,
        TopCell,
        RightTopcell,
        RightCell,
        RightBottomCell,
        BottomCell,
        LeftBottomCell,
        LeftCell
    }
    public abstract class Box : Button
    {
        public Color defaultColor = Color.Silver;
        public Box()
        {
            Size = new Size(30, 30);
            BackColor = defaultColor;
            MouseDown += Box_MouseDown;
        }
        private void Box_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                if(BackColor != Color.Blue)
                {
                    BackColor = Color.Blue;
                }
                else
                {
                    BackColor = defaultColor;
                }
            }
        }
    }
    public class Cell : Box
    {

    }
    public class Mine : Box
    {
        
    }
    public class Table : MatrixView
    {
        private int _n = 0;
        private Box[,] boxes;
        private int mineCount = 0;
        int cellPadding = 0;
        int tableMargin = 0;
        /*public Table() {
            _n = 0;
        }
        public Table(int n, int MineCount)
        {
            this._n = n;
            this.mineCount = MineCount;
        }*/
        public void CreateGame(int n, int MineCount)
        {
            this._n = n;
            this.mineCount = MineCount;
            CreateTable(n);
            CreateMines(mineCount);
            addControls();
            Trace.WriteLine("table size: " + Size.Width.ToString() + Size.Height.ToString());

        }
        private void addControls()
        {
            Controls.Clear();
            foreach (Box item in boxes)
            {
                Controls.Add(item);
            }
            base.Alignment();
        }
        public void CreateTable(int n)
        {
            boxes = new Box[_n, _n];
            for (int x = 0; x < boxes.Length / _n; x++)
            {
                for (int y = 0; y < boxes.Length / _n; y++)
                {
                    Cell cell = new Cell()
                    {
                        Name = x + "," + y,
                    };
                    cell.Top = y * (cell.Height + cellPadding) + tableMargin;
                    cell.Left = x * (cell.Width + cellPadding) + tableMargin;
                    cell.Click += cellClick;
                    boxes[x, y] = cell;
                    //Controls.Add(boxes[x, y]);
                }
            }
        }
        private void CreateMines(int MineCount)
        {
            int x = 0, y = 0;

            for (int i = 0; i < MineCount; i++)
            {
                int[] coordinates = getRandomMines();
                x = coordinates[0];
                y = coordinates[1];
                Box mine = new Mine() { Name = x + "," + y };
                mine.Location = new Point(x * (mine.Width + cellPadding) + tableMargin, y * (mine.Height + cellPadding) + tableMargin);
                Trace.WriteLine(mine.Location.X);
                mine.Click += mineClick;
                mine.MouseDown += MineRightClick;
                
                boxes[x, y] = mine;
                //Trace.WriteLine(x.ToString() + ", " + y.ToString());
            }
            //Trace.WriteLine("Mines Created");
        }
        private void MineRightClick(object sender, MouseEventArgs e)
        {
            if (isFinished())
            {
                Trace.WriteLine("oyun bitti");
                finishGame();
            }
        }
        int[] getRandomMines()
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(_n);
                y = rand.Next(_n);
            } while (boxes[x, y].GetType() == typeof(Mine));
            return new int[] { x, y };
        }
        public void mineClick(object sender, EventArgs e)
        {
            restartGame();
        }
        public void restartGame()
        {
            boxes = null;
            Controls.Clear();
            CreateGame(_n,mineCount);
        }
        public Boolean isFinished() //tüm kutular mavi işaretlense de kazanılmış durumu döner bunu düzelt
        {
            int count = 0;
            foreach (Box box in boxes)
            {
                if(box.GetType() == typeof(Mine))
                {
                    if(box.BackColor == Color.Blue)
                    {
                        count++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void finishGame()
        {
            foreach (Box box in boxes)
            {
                if(box.GetType() == typeof(Mine))
                {
                    box.BackColor = Color.Pink;
                }
            }    
        }
        public void cellClick(object sender, EventArgs e)
        {
            (sender as Box).Enabled = false;
            //Trace.WriteLine((sender as Button).Name.ToString());
            (sender as Button).BackColor = Color.DarkGray;
            int[] index = getIndexFromName((sender as Box).Name);
            int x = index[0];
            int y = index[1];

            int mineCount = 0;
            Box[] BoxesAround = getBoxesAround(x, y);
            foreach (Box item in BoxesAround)
            {
                if(item != null)
                {
                    if (item.GetType() == typeof(Mine))
                        mineCount++;
                    //Trace.WriteLine(mineCount);
                }
            }
            if(mineCount == 0)
            {
                foreach(Box item in BoxesAround)
                {
                    if (item != null)
                    {
                        Trace.WriteLine(item.Name);
                        item.PerformClick();
                    }
                }
            }
            if (mineCount > 0)
            {
                (sender as Box).Text = mineCount.ToString();
            }
            
        }
        public int[] getIndexFromName(string boxName)
        {
            string[] values = boxName.Split(',');
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            return new int[] { x, y };
        }
        public Box[] getBoxesAround(int x,int y)
        {
            Box[] boxAround = new Box[8];
            if ((x > 0 && x < _n) && (y > 0 && y < _n)) { boxAround[((int)CellPosition.LeftTopCell)] = (boxes[x - 1, y - 1]); }
            if (y > 0 && y < _n) { boxAround[((int)CellPosition.TopCell)] = (boxes[x, y - 1]); }
            if ((x >= 0 && x < _n - 1) && (y > 0 && y < _n)) { boxAround[((int)CellPosition.RightTopcell)] = boxes[x + 1, y - 1]; }
            if (x >= 0 && x < _n - 1) { boxAround[((int)CellPosition.RightCell)] = boxes[x + 1, y]; } 
            if ((x >= 0 && x < _n - 1) && (y >= 0 && y < _n - 1)) { boxAround[((int)CellPosition.RightBottomCell)] = boxes[x + 1, y + 1]; }
            if(y >= 0 && y < _n - 1) { boxAround[((int)CellPosition.BottomCell)] = boxes[x, y + 1]; }
            if ((x > 0 && x < _n) && (y >= 0 && y < _n - 1)) { boxAround[((int)CellPosition.LeftBottomCell)] = boxes[x - 1, y + 1]; }
            if(x > 0 && x < _n) { boxAround[((int)CellPosition.LeftCell)] = boxes[x - 1, y]; }
            return boxAround;
        }
    }
}
