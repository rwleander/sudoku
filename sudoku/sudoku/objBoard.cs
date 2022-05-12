//  objBoard - represents 9 x 9 grid

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace sudoku
{
    public class objBoard
    {

        //  public properties

        public int[,] grid = new int[9, 9];
        public List<objError> ErrorList = new List<objError>();

        //  constructor - initialize to 0's

        public objBoard()
        {
            int i, j;

            i = 0;
            while (i < 9)
            {
                j = 0;
                while (j < 9)
                {
                    grid[i, j] = 0;
                    j++;
                }
                i++;
            }
        }

        //  copy constructor

        public objBoard(objBoard board1)
        {
            int i, j;

            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    setCell(i, j, board1.getCell(i, j));
                }
            }
        }

        //  format to string

        public override String ToString()
        {
            StringBuilder bldr = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                bldr.AppendLine(formatRow(i));
            }

            return bldr.ToString();
        }

        //  load from string - 9 rows of 9 digits

        public void fromString(String str)
        {
            String[] lines = str.Split((char)10);
            int i = 0;
            int j = 0;
            int n;

            foreach (string line in lines)
            {
                j = 0;
                foreach (char ch in line)
                {
                    if (ch != ' ' && j < 9)
                    {
                        n = ch - '0';
                        if (n > 9) n = 0;
                        setCell(i, j, n);
                        j++;
                    }
                }

                i++;
                if (i >= 9) return;
            }

        }

        // was the board completed successfully

        public bool Success()
        {
            objError err;
            int[] line;
            int i;

            //  clear the list

            ErrorList.Clear();

            //  check each dimension

            for (i = 0; i < 9; i++)
            {

                //  row

                line = getRow(i);
                if (checkLine(line) == false)
                {
                    err = new objError();
                    err.Dimension = "Row";
                    err.n = i + 1;
                    err.Items = formatRow(i);
                    err.index = new Point(i, 0);
                    ErrorList.Add(err);
                }

                //  column

                line = getColumn(i);
                if (checkLine(line) == false)
                {
                    err = new objError();
                    err.Dimension = "Column";
                    err.n = i + 1;
                    err.Items = formatColumn(i);
                    err.index = new Point(0, i);
                    ErrorList.Add(err);
                }

                //  check block

                line = getBlock(i);
                if (checkLine(line) == false)
                {
                    int x = (int)(i / 3) * 3;
                    int y = (int)(i % 3) * 3;
                    err = new objError();
                    err.Dimension = "Block";
                    err.n = i + 1;
                    err.Items = formatBlock(x, y);
                    err.index = new Point(x, y);
                    ErrorList.Add(err); ;
                }
            }

            return (ErrorList.Count == 0);
        }

        //  does board have zeros

        public bool isComplete()
        {
            int i = 0;
            int j = 0;

            while (i < 9)
            {
                j = 0;
                while (j < 9)
                {
                    if (grid[i, j] == 0)
                    {
                        return false;
                    }
                    j++;
                }
                i++;
            }

            return true;
        }

        //  get or set by index

        public int getCell(int i, int j)
        {
            return grid[i, j];
        }

        public void setCell(int i, int j, int n)
        {
            grid[i, j] = n;
        }

        //  get or set block 

        public int[] getBlock(int i, int j)
        {
            int[,] blocks = {
                { 0, 0, 0, 1, 1, 1, 2, 2, 2},
                                { 0, 0, 0, 1, 1, 1, 2, 2, 2},
                                { 0, 0, 0, 1, 1, 1, 2, 2, 2},
                { 3, 3, 3, 4, 4, 4, 5, 5, 5},
                                { 3, 3, 3, 4, 4, 4, 5, 5, 5},
                                { 3, 3, 3, 4, 4, 4, 5, 5, 5},
                { 6, 6, 6, 7, 7, 7, 8, 8, 8},
                { 6, 6, 6, 7, 7, 7, 8, 8, 8},
                { 6, 6, 6, 7, 7, 7, 8, 8, 8}
            };

            int b = blocks[i, j];
            return getBlock(b);
        }

        public int[] getBlock(int b)
        {
            int[] line = new int[9];
            int i, j, n, iStart, jStart;

            //  get top left bound of block

            iStart = b / 3;
            iStart *= 3;
            jStart = b % 3;
            jStart *= 3;

            // copy the array

            n = 0;
            i = 0;
            while (i < 3)
            {
                j = 0;
                while (j < 3)
                {
                    line[n] = grid[iStart + i, jStart + j];
                    n++;
                    j++;
                }
                j = 0;
                i++;
            }

            return line;
        }

        public void setBlock(int b, int[] line)
        {
            int i, j, n, iStart, jStart;

            //  get top left bound of block

            iStart = b / 3;
            iStart *= 3;
            jStart = b % 3;
            jStart *= 3;

            // copy the array

            n = 0;
            i = 0;
            while (i < 3)
            {
                j = 0;
                while (j < 3)
                {
                    grid[iStart + i, jStart + j] = line[n];
                    n++;
                    j++;
                }
                j = 0;
                i++;
            }
        }

        //  get row or column

        public int[] getRow(int r)
        {
            int[] line = new int[9];
            int n = 0;

            while (n < 9)
            {
                line[n] = grid[r, n];
                n++;
            }

            return line;
        }


        public int[] getColumn(int c)
        {
            int[] line = new int[9];
            int n = 0;

            while (n < 9)
            {
                line[n] = grid[n, c];
                n++;
            }

            return line;
        }

        //  format the row data

        public string formatRow(int r)
        {
            int[] row = getRow(r);
            return formatItems(row);
        }

        //  format the column numbers

        public string formatColumn(int r)
        {
            int[] col = getColumn(r);
            return formatItems(col);
        }

        //  format block

        public String formatBlock(int r, int c)
        {
            int[] block = getBlock(r, c);
            return formatItems(block);
        }

        //  shared method for formatRow and formatColumn

        public String formatItems(int[] row)
        {
            string str = "";

            foreach (int n in row)
            {
                if (n > 0)
                {
                    str += n.ToString() + " ";
                }
                else
                {
                    str += "* ";
                }
            }

            return str;
        }


        //  find solutions

        public List<int> getSolutions(int x, int y)
        {
            List<int> missing = new List<int>();
            int[] counter = new int[10];
            int[] line;
            int i = 1;

            //  fill the counter list

            for (i = 0; i < 10; i++)
            {
                counter[i] = 0;
            }

            //  check row 

            line = getRow(x);
            for (i = 0; i < 9; i++)
            {
                counter[line[i]]++;
            }

            //  column

            line = getColumn(y);
            for (i = 0; i < 9; i++)
            {
                counter[line[i]]++;
            }

            //  block

            line = getBlock(x, y);
            for (i = 0; i < 9; i++)
            {
                counter[line[i]]++;
            }

            //  list missing items

            for (i = 1; i < 10; i++)
            {
                if (counter[i] == 0)
                {
                    missing.Add(i);
                }
            }

            return missing;
        }

        //  check line for missing or duplicate values

        public bool checkLine(int[] line)
        {
            SortedList<int, int> numbers = new SortedList<int, int>();
            int i;

            //  set up the list

            for (i = 0; i <= 9; i++)
            {
                numbers.Add(i, 0);
            }

            //  loop through the list and count digits

            foreach (int n in line)
            {
                numbers[n]++;
            }

            //  check counts

            i = 1;
            while (i <= 9)
            {
                if (numbers[i] != 1) return false;
                i++;
            }

            return true;
        }

        //  count empty cells

        public int countEmpty()
        {
            int n = 0;

            for (int i=0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (getCell(i, j) == 0) n++;

            return n;
        }

        //  find next empty cell

        public Point nextEmpty(int x, int y)
        {
            int i = x;
            int j = y;
            int n = 0;

            while (n < 2)
            {
                while (i < 9)
                {
                    while (j < 9)
                    {
                        j++;
                        if (j < 9)
                        {
                            if (getCell(i, j) == 0)
                            {
                                return new Point(i, j);
                            }
                        }
                    }
                    i++;
                    j = -1;
                }
                n++;
                i = 0;
                j = 0;
            }

            return new Point(0, 0);
        }

        //  find previous empty cell

        public Point prevEmpty(int x, int y)
        {
            int i = x;
            int j = y;
            int n = 0;

            while (n < 2)
            {
                while (i >= 0)
                {
                    while (j >= 0)
                    {
                        j--;
                        if (j >= 0)
                        {
                            if (getCell(i, j) == 0)
                            {
                                return new Point(i, j);
                            }
                        }
                    }
                    i--;
                    j = 9;
                }
                n++;
                i = 8;
                j = 9;
            }

            return new Point(0, 0);
        }


    }
}
