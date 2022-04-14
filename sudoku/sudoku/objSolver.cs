//  objSolver - solve a puzzle and determine the number of solutions

using System;
using System.Collections.Generic;

namespace sudoku
{
    public class objSolver
    {

        //  public properties

        public objBoard Puzzle;
        public int Solutions = 0;

        public int logLevel = 0;

        //  private fields

        private objBoard BoardWork;
        private flLog log;

public struct Point
        {
            public int x;
            public int y;
        }
        
        //  main method

        public int Solve(objBoard board)
        {
            Puzzle = new objBoard(board);
            Solutions = 0;

            if (logLevel > 0)
            {
                log = new flLog();
            }

            //  see if there's a single solution

            if (FillObvious() > 0)
            {
                if (Puzzle.Success() == true) return 1;
            }

            //  recursively solve the puzzle




            return Solutions;
        }


        //  fill in cells with single solutions

        public int FillObvious()
        {
            List<int> values;
            Point p;
            int n = 0;

            //  find first empty cell

            p = findFirst(Puzzle );

            //  if logging, print puzzle

            if (logLevel > 0)
            {
                log.WriteLine(DateTime.Now.ToShortTimeString() + " finding obvious cells");
                log.WriteLine(Puzzle.ToString());
                            }

            //  loop through empty cells

            while (p.x < 10)
            {
                values = Puzzle.getSolutions(p.x, p.y);
                if (values.Count == 1)
                {
                    Puzzle.setCell(p.x, p.y, values[0]);
                    if (logLevel > 0)
                    {
                        log.WriteLine(p.x.ToString() + ", " + p.y.ToString() + ": " + values[0].ToString() + "; ");
                    }
                    n++;
                }
                
                p = findNext(Puzzle, p.x, p.y);
            }

            return n;
        }

        //  recursively solve the puzzle

        public void fillRecursively(objBoard thisPuzzle)
        {
            List<int> values;
            Point p;
                        
            //  find first empty cell

            p = findFirst(Puzzle);

            //  if found, try each option

            if (p.x < 10)
            {
                values = Puzzle.getSolutions(p.x, p.y);
                if (values.Count > 0)
                {
                    foreach(int v in values)
                        {
                        thisPuzzle.setCell(p.x, p.y, v);
                        if (logLevel > 0) log.WriteLine("trying " + p.x.ToString() + ", " + p.y.ToString() + ": " + v.ToString());
                        fillRecursively(thisPuzzle);
                    }
                }
            }

            //  if we're at the end, see if it's a solution

            else
            {
                if (thisPuzzle.Success())
                {
                    Solutions++;
                    if (logLevel > 0) log.WriteLine("Found a solution~!");
                }
            }            
        }

            //--------------
            //  helper methods

            //  find first empty cell

            public Point findFirst(objBoard b)
        {
            return findZero(b, 0, 0);
        }

        //  find next open cell

        public Point findNext(objBoard b, int x0, int y0)
        {
            int x = x0;
            int y = y0;

            //  increment 1 

            y++;
            if (y > 8)
            {
                y = 0;
                x++;
            }

            return findZero(b, x, y);
        }

            private Point findZero (objBoard b, int x0, int y0)
            {
                Point p;
                int x = x0;
            int y = y0;

            while (x < 9)
            {
                while (y < 9)                  
                  {
                    if (b.getCell(x, y) == 0)
                    {
                        p = new Point();
                        p.x = x;
                        p.y = y;
                        return p;
                    }

                    y++;
                }                

                x++;
                y = 0;
            }

            // not found

            p = new Point();
            p.x = 10;
            p.y = 10;
            return p;
            }

    }
}
