//  solverTest.cpp - test objSolver methods

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sudoku;

namespace sudokuTest
{
    [TestClass]
    public class SolverTest1
    {

        //  test find first

        [TestMethod]
        public void TestFindFirst()
        {
            objSolver solver = new objSolver();
            objBoard board = new objBoard();

            objSolver.Point p = solver.findFirst(board);

            Assert.AreEqual(0, p.x);
            Assert.AreEqual(0, p.y);
        }

        //  test find next

        [TestMethod]
        public void TestFindNext()
        {
            objSolver solver = new objSolver();
            objBoard board = new objBoard();

            objSolver.Point p = solver.findNext(board, 4, 5);
            Assert.AreEqual(4, p.x);
            Assert.AreEqual(6, p.y);

            p = solver.findNext(board, 4, 8);
            Assert.AreEqual(5, p.x);
            Assert.AreEqual(0, p.y);
        }

        //  test fill obvious

        [TestMethod]
        public void TestFillObvious()
        {
            objSolver solver = new objSolver();
            objBuilder bldr = new objBuilder();
            int n;

            bldr.Step1();
            solver.Puzzle = bldr.puzzle;
            n = solver.Puzzle.getCell(1, 1);
            solver.Puzzle.setCell(1, 1, 0);
            solver.FillObvious();

            Assert.AreEqual(n, solver.Puzzle.getCell(1, 1));
        }

    }
}
