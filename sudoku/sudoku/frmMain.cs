using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace sudoku
{
    public partial class sudoku : Form
    {

        //  private properties

        private objBoard puzzle;
        private TextBox[,] txtItem = new TextBox[9, 9];
        private ContextMenu gridMenu; 

        //  constructor

        public sudoku()
        {
            InitializeComponent();            
            InitializeGrid();
        }

        //  event handlers


            //  new game

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        //  load a game from disk

        private void btnLoad_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not ready");
        }

        //  save to disk

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not ready");
        }

        //  close

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void txtItem_onKeyDown(object sender, KeyEventArgs e)
        {

            //  get the coordinates of the current text box

            TextBox txtBox = (TextBox)sender;
            Point p = (Point)txtBox.Tag;
            int i = p.X;
            int j = p.Y;

            //  handle key presses

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (i > 0)
                    {
                        i--;
                        txtItem[i, j].Focus();
                    }
                    e.Handled = true;
                    break;

                case Keys.Down:
                    if (i < 8)
                    {
                        i++;
                        txtItem[i, j].Focus();
                    }
                    e.Handled = true;
                    break;

                case Keys.Left:
                    if (j > 0)
                    {
                        j--;
                        txtItem[i, j].Focus();
                    }
                    e.Handled = true;
                    break;

                case Keys.Right:
                    if (j < 8)
                    {
                        j++;
                        txtItem[i, j].Focus();
                    }
                    e.Handled = true;
                    break;

                case Keys.PageDown:
                    Point z = puzzle.nextEmpty(i, j);
                    txtItem[z.X, z.Y].Focus();
                    e.Handled = true;
                    break;
            }
                        
        }

        //  build context menu before its displayed

        private void gridMenu_onPopup(System.Object sender, System.EventArgs e)
        {
            ContextMenu cxMenu = (ContextMenu)sender;
            TextBox txtBox = (TextBox)cxMenu.SourceControl;
            Point p = (Point)txtBox.Tag;
            int i = p.X;
            int j = p.Y;

            gridMenu.MenuItems.Clear();
            gridMenu.MenuItems.Add(new MenuItem("Cell " + (i +1).ToString() + ", " + (j + 1).ToString()));
            gridMenu.MenuItems.Add(new MenuItem("Row " + puzzle.formatRow(i)));
            gridMenu.MenuItems.Add(new MenuItem("Column " + puzzle.formatColumn(j)));
            gridMenu.MenuItems.Add(new MenuItem("Block " + puzzle.formatBlock(i, j)));
        }
    
        

        //-----------------------
        //  private methods

        
        //  initialize grid

        private void InitializeGrid()
        {
            int i = 0;
            int j = 0;
            int x = 30;
            int y = 30;

            //  set up the context menu

            gridMenu = new ContextMenu();
            gridMenu.Popup += gridMenu_onPopup;

            //  initialize text boxes

            this.SuspendLayout();
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    txtItem[i, j] = new TextBox();
                    txtItem[i, j].Name = "txtItem" + i.ToString() + j.ToString();
                    txtItem[i, j].Location = new Point(x, y);
                    txtItem[i, j].Size = new Size(30, 18);
                    txtItem[i, j].Text = "";
                    txtItem[i, j].Tag = new Point(i, j);
                    txtItem[i, j].KeyDown += new KeyEventHandler(txtItem_onKeyDown);
                    txtItem[i, j].ContextMenu = gridMenu;
                    this.Controls.Add(txtItem[i, j]);
                    x += 50;
                }

                x = 30;
                y += 35;
            }
            this.ResumeLayout();
        }


        //  set up a new game

        private void NewGame()
        {
            objBuilder bldr = new objBuilder();
            objSolver solver = new objSolver();
            objBoard lastPuzzle;
            frmProgress frm = new frmProgress();
            int solutions = 1;
            int i, j, n;

            //  set up progress window

            frm.pbProgress.Minimum = 0;
            frm.pbProgress.Maximum = 80;
            frm.pbProgress.Value = 0;
            frm.Show();

            //  create the board

            bldr.Step1();
            lastPuzzle = bldr.puzzle;
            frm.pbProgress.Value++;

            while (solutions == 1) 
            {
                lastPuzzle = puzzle;
            bldr.Step2();
            puzzle = bldr.puzzle;
                solutions = solver.Solve(puzzle);
            frm.pbProgress.Value++;
        }
            //  copy to form

            puzzle = lastPuzzle;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    n = puzzle.getCell(i, j);
                    if (n > 0)
                    {
                        txtItem[i, j].Text = n.ToString();
                        txtItem[i, j].ReadOnly = true;
                    }
                    else
                    {
                        txtItem[i, j].Text = "";
                        txtItem[i, j].ReadOnly = false;
                    }
                }
                frm.pbProgress.Value++;
            }

            //  close the form
            
            frm.Close();

        }


    }
}
