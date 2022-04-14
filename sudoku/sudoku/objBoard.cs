﻿//  objBoard - represents 9 x 9 grid

using System;
using System.Collections.Generic;
using System.Text;

namespace sudoku
{
    public class objBoard
    {

        //  private properties

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
                    setCell(i, j,board1.getCell(i, j));
                }
            }
        }

        //  format to string

        public override  String ToString ()
        {
            StringBuilder bldr = new StringBuilder();
            int i, j, n;

            //  copy the board to the string builder

            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    n = getCell(i, j);
                    if (n == 0)
                    {
                        bldr.Append ("*"); 
                    }
                    else
                    {
                        bldr.Append(n.ToString());
                    }
                    if (j < 8)
                    {
                        bldr.Append(", ");
                    }
                }
                bldr.AppendLine();
            }
            return bldr.ToString();
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

for (i=0; i< 9; i++)
            {

                //  row

                line = getRow(i);
                if (checkLine(line) == false)
                {
                    err = new objError();
                    err.Dimension = "Row";
                    err.n = i + 1;
                    err.Items = line.ToString();
                    ErrorList.Add(err);
                }

                //  column

                line = getColumn(i);
                if (checkLine(line) == false)
                {
                    err = new objError();
                    err.Dimension = "Column";
                    err.n = i + 1;
                    err.Items = line.ToString();
                    ErrorList.Add(err);
                }

                //  check block

                line = getBlock(i);
                if (checkLine(line) == false)
                {
                    err = new objError();
                    err.Dimension = "Block";
                    err.n = i + 1;
                    err.Items = line.ToString();
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
            int[,] blocks= {
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
            for (i=0; i<9; i++)
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
                
    }
}