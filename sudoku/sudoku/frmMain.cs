using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

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
            String data = "";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                flData fl = new flData(dlgOpen.FileName);

                try
                {
                    data = fl.load();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString());
                }

                puzzle = new objBoard();
                puzzle.fromString(data);
                loadPage();
            }
        }

        //  save to disk

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                flData fl = new flData(dlgSave.FileName);
                try
                {
                    fl.Save(puzzle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


        }

        //  check board for errors

        private void btnCheck_Click(object sender, EventArgs e)
        {
            int i, j;

            if (puzzle.Success() == true)
            {
                MessageBox.Show("Not ready");
                return;
            }

            frmErrors frm = new frmErrors();
            frm.LoadData(puzzle.ErrorList);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                i = frm.err.index.X;
                j = frm.err.index.Y;
                txtItem[i, j].Focus();
            }
            frm.Close(); ;




        }

        //  close

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        public void txtItem_onKeyDown(object sender, KeyEventArgs e)
        {
            Point z;

            //  get the coordinates of the current text box

            TextBox txtBox = (TextBox)sender;
            Point p = (Point)txtBox.Tag;
            int i = p.X;
            int j = p.Y;

            //  handle key presses

            switch (e.KeyCode)
            {
                case Keys.Space:
                    puzzle.setCell(i, j, 0);
                    txtItem[i, j].Text = " ";
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad1:
                case Keys.D1:
                    handleKeystroke(i, j, 1);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad2:
                case Keys.D2:
                    handleKeystroke(i, j, 2);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad3:
                case Keys.D3:
                    handleKeystroke(i, j, 3);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad4:
                case Keys.D4:
                    handleKeystroke(i, j, 4);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad5:
                case Keys.D5:
                    handleKeystroke(i, j, 5);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad6:
                case Keys.D6:
                    handleKeystroke(i, j, 6);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad7:
                case Keys.D7:
                    handleKeystroke(i, j, 7);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.NumPad8:
                case Keys.D8:
                    e.SuppressKeyPress = true;
                    handleKeystroke(i, j, 8);
                    e.Handled = true;
                    break;

                case Keys.NumPad9:
                case Keys.D9:
                    handleKeystroke(i, j, 9);
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;

                case Keys.Home:
                    txtItem[0, 0].Focus();
                    e.Handled = true;
                    break;

                case Keys.End:
                    txtItem[8, 8].Focus();
                    e.Handled = true;
                    break;

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
                    z = puzzle.nextEmpty(i, j);
                    txtItem[z.X, z.Y].Focus();
                    e.Handled = true;
                    break;

                case Keys.PageUp:
                    z = puzzle.prevEmpty(i, j);
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
            gridMenu.MenuItems.Add(new MenuItem("Cell " + (i + 1).ToString() + ", " + (j + 1).ToString()));
            gridMenu.MenuItems.Add(new MenuItem("Row " + puzzle.formatRow(i)));
            gridMenu.MenuItems.Add(new MenuItem("Column " + puzzle.formatColumn(j)));
            gridMenu.MenuItems.Add(new MenuItem("Block " + puzzle.formatBlock(i, j)));
            gridMenu.MenuItems.Add(new MenuItem("location " + txtBox.Location.ToString()));
        }



        //-----------------------
        //  private methods


        //  initialize grid

        private void InitializeGrid()
        {
            int i = 0;
            int j = 0;
            int x = 100;
            int y = 100;
            int tb = 10;

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
                    txtItem[i, j].Size = new Size(20, 14);
                    txtItem[i, j].TextAlign = HorizontalAlignment.Center;
                    txtItem[i, j].Text = "";
                    txtItem[i, j].TabIndex = tb;
                    txtItem[i, j].Tag = new Point(i, j);
                    txtItem[i, j].KeyDown += new KeyEventHandler(txtItem_onKeyDown);
                    txtItem[i, j].ContextMenu = gridMenu;
                    this.Controls.Add(txtItem[i, j]);
                    x += 60;
                    if (j == 2 || j == 5) x += 20;
                    tb++;
                }

                x = 100;
                y += 35;
                if (i == 2 || i == 5) y += 10;
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
            loadPage();

            frm.Close();
        }

        //  load the puzzle to the page

        public void loadPage()
        {
            int i, j, n;

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
            }

            txtItem[0, 0].Focus();
        }

        //  handle keystrokes

        void handleKeystroke(int i, int j, int n)
        {
            bool inRow = false;
            String ch;


            int[] row = puzzle.getRow(i);
            if (row.Contains(n)) inRow = true;

            int[] col = puzzle.getColumn(j);
            if (col.Contains(n)) inRow = true;

            int[] block = puzzle.getBlock(i, j);
            if (block.Contains(n)) inRow = true;

            if (inRow == true)
            {
                SystemSounds.Beep.Play();
            }
            else
            {
                puzzle.setCell(i, j, n);
            }
            ch = puzzle.getCell(i, j).ToString();
            if (ch == "0") ch = " ";
            txtItem[i, j].Text = ch;

            if (puzzle.Success() == true)
            {
                MessageBox.Show(this, "congratulations - you solved the puzzle", "Sudoku", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}
