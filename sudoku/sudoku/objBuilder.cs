//  objBuilder - create a new game board

using System;
using System.Collections.Generic;

namespace sudoku
{
    public class objBuilder
    {

        //  public properties

            public         objBoard puzzle = new objBoard ();

        //  private fields

        private Random rnd = new Random();


        //  method to create a board

        //  step 1 - fill the board

        public void Step1()
        {
            int i;

            //  fill all blocks with the same set of rrandom numbers

            int[] line = getRandom();

            for (i = 0; i < 9; i++)
            { 
            puzzle.setBlock(i, line);
        }

            //  shift columns up or down 

            if (CoinToss())
            {
                BlockUp(1);
                BlockUp(4);
                BlockUp(7);
                BlockDown(2);
                BlockDown(5);
                BlockDown(8);
            }
            else
            {
                BlockDown(1);
                BlockDown(4);
                BlockDown(7);
                BlockUp(2);
                BlockUp(5);
                BlockUp(8);
            }

            //  now shift rows left or right randomly

            if (CoinToss())
            {
                BlockLeft(3);
                BlockLeft(4);
                BlockLeft(5);
                BlockRight(6);
                BlockRight(7);
                BlockRight(8);
            }
            else
            {
                BlockRight(3);
                BlockRight(4);
                BlockRight(5);
                BlockLeft(6);
                BlockLeft(7);
                BlockLeft(8);
            }
        }

        //  step 2 - remove a pair of numbers

        public void Step2()
        {
            int i = GetRandom();
            int j = GetRandom();

            while (puzzle.getCell(i, j) == 0)
            {
                i = GetRandom();
                j = GetRandom();
            }

            puzzle.setCell(i, j, 0);
            puzzle.setCell(10 - i, 10 - j, 0);            
        }

        
        //  shift block up one row

        public void BlockUp(int n)
        {
            int[] line = puzzle.getBlock(n);
            int i, x, y, z;

            x = line[0];
            y = line[1];
            z = line[2];
            for (i = 3; i < 9; i++)
            {
                line[i - 3] = line[i];
            }
            line[6] = x;
            line[7] = y;
            line[8] = z;            
            puzzle.setBlock(n, line);
        }

        //  shift block down one row

        public void BlockDown(int n)
        {
            int[] line = puzzle.getBlock(n);
            int i, x, y, z;

            x = line[6];
            y = line[7];
            z = line[8];
            for (i = 5; i >= 0; i--)
            {
                line[i + 3] = line[i];
            }
            line[0] = x;
            line[1] = y;
            line[2] = z;            
            puzzle.setBlock(n, line);
        }

        //  shift block left

        public void BlockLeft(int n)
        {
            int[] line = puzzle.getBlock(n);
            int i, x, y, z;

            x = line[0];
            y = line[3];
            z = line[6];
            for (i = 1; i < 9; i++)
            {
                line[i - 1] = line[i];
            }
            line[2] = x;
            line[5] = y;
            line[8] = z;
            puzzle.setBlock(n, line);
        }

        //  shift block right

        public void BlockRight(int n)
        {
            int[] line = puzzle.getBlock(n);
            int i, x, y, z;

            x = line[2];
            y = line[5];
            z = line[8];
            for (i = 7; i >= 0; i--)
            {
                line[i + 1] = line[i];
            }
            line[0] = x;
            line[3] = y;
            line[6] = z;
            puzzle.setBlock(n, line);
        }


        //  create a list of nine random digits

        public int[] getRandom()
        {
            int[] line = new int[9];
SortedList<int, int> nums = new SortedList<int, int>();
            int n = 1;

            //  fill the numbers list with random sort keys

            while (n <= 9)
            {
                nums.Add(rnd.Next(), n);
                n++;
            }

            //  now copy to line array

            n = 0;
            while (n < 9)
            {
                line[n] = nums.Values[n];
                n++;
            }

            return line;            
        }

        //  get a random number from 0 to 8

        public int GetRandom()
        {
return rnd.Next(8);            
        }

        //  random coin toss - return true or false

        public bool CoinToss()
        {
            return (rnd.Next() > (int.MaxValue / 2)); 
        }


    }
}
