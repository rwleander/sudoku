//  BoardTest - test objBoard class

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sudoku;


namespace sudokuTest
{
    [TestClass]
    public class BoardTest
    {

        // test copy constructor

        [TestMethod]
        public void TestCopy()
        {
            objBoard board1 = new objBoard();
            int[] line1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            board1.setBlock(1, line1);

            objBoard board2 = new objBoard(board1);
            int[] line2 = board2.getBlock(0);
             
            Assert.AreEqual(line1.ToString(), line2.ToString());
        }
        
        //  test setBlock

        [TestMethod]
        public void TestSetBlock()
        {
            objBoard board = new objBoard();
            int[] line = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            board.setBlock(4, line);

            Assert.AreEqual (1, board.getCell(3, 3));
            Assert.AreEqual (3, board.getCell(3, 5));
            Assert.AreEqual (5, board.getCell(4, 4));
            Assert.AreEqual (7, board.getCell(5, 3));
            Assert.AreEqual (9, board.getCell(5, 5));
        }

        //  test getBlock

        [TestMethod]
        public void TestGetBlock()
        {
            objBoard board = new objBoard();
            int[] line; 

            board.setCell(3, 3, 1);
            board.setCell(3, 5, 2);
            board.setCell(5, 3, 3);
            board.setCell(5, 5, 4);

            line = board.getBlock(4);

            Assert.AreEqual(1, line[0]);
            Assert.AreEqual(2, line[2]);
            Assert.AreEqual(3, line[6]);
            Assert.AreEqual(4, line[8]);
        }

        //  test alternative get block method

        [TestMethod]
        public void TestGetBlock2()
        {
            objBoard board = new objBoard();
            int[] line;

            board.setCell(3, 3, 1);
            board.setCell(3, 5, 2);
            board.setCell(5, 3, 3);
            board.setCell(5, 5, 4);

            line = board.getBlock(5, 5);

            Assert.AreEqual(1, line[0]);
            Assert.AreEqual(2, line[2]);
            Assert.AreEqual(3, line[6]);
            Assert.AreEqual(4, line[8]);
        }

        //  test getRow

        [TestMethod]
        public void TestGetRow()
        {
            objBoard board = new objBoard();
            int[] line;

            board.setCell(3, 0, 1);
            board.setCell(3, 2, 2);
            board.setCell(3, 6, 3);
            board.setCell(3, 8, 4);

            line = board.getRow(3);

            Assert.AreEqual(1, line[0]);
            Assert.AreEqual(2, line[2]);
            Assert.AreEqual(3, line[6]);
            Assert.AreEqual(4, line[8]);
        }

        //  test get column

        [TestMethod]
        public void TestGetColumn()
        {
            objBoard board = new objBoard();
            int[] line;

            board.setCell(0, 3, 1);
            board.setCell(2, 3, 2);
            board.setCell(6, 3, 3);
            board.setCell(8, 3, 4);

            line = board.getColumn(3);

            Assert.AreEqual(1, line[0]);
            Assert.AreEqual(2, line[2]);
            Assert.AreEqual(3, line[6]);
            Assert.AreEqual(4, line[8]);
        }

        //  test if complete

        [TestMethod]
        public void TestCheckBlock()
        {

            //  test false

            objBoard board = new objBoard();
            Assert.AreEqual(false, board.isComplete());

            int[] line = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 9; i++)
            {
                board.setBlock(i, line);
            }
            Assert.AreEqual(true, board.isComplete());
        }

        //  test check line

        [TestMethod]
        public void TestCheckLine()
        {
            objBoard board = new objBoard();
            int[] lineTrue = { 9, 7, 5, 3, 1, 8, 6, 4, 2};
            int[] lineFalse1 = {9, 7, 5, 3, 1, 8, 6, 4, 6};
            int[] lineFalse2 = { 9, 7, 5, 3, 1, 8, 6, 4, 0 };

            Assert.AreEqual(true, board.checkLine(lineTrue));
            Assert.AreEqual(false, board.checkLine(lineFalse1));
            Assert.AreEqual(false, board.checkLine(lineFalse2));
        }

        //  test check complete

        [TestMethod]
        public void TestSuccess()
        {
            objBoard board = new objBoard();

            Assert.AreEqual(false, board.Success());
        }

        //  check get solutions

        [TestMethod]
        public void TestGetSolutions()
        {
            objBoard board = new objBoard();
            int[] line = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            board.setBlock(0, line);

            board.setCell(2, 2, 0);
            List<int> missing = board.getSolutions(2, 2);

            Assert.AreEqual(1, missing.Count);
            Assert.AreEqual(9, missing[0]);
        }

        }
    }
