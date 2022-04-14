//  test objBuilder class

using Microsoft.VisualStudio.TestTools.UnitTesting;
using sudoku;

namespace sudokuTest
{
    [TestClass]
    public class BuilderTest 
    {

//  helper methods

private objBuilder CreateBuilder()
{
objBuilder bldr = new objBuilder();
int[] line = {1, 2, 3, 4, 5, 6, 7, 8, 9};
bldr.puzzle.setBlock(0, line);
return bldr;
}

//  block up method

        [TestMethod]
        public void TestBlockUp()
        {
objBuilder bldr = CreateBuilder();
int[] newLine = {4, 5, 6, 7, 8, 9, 1, 2, 3};

bldr.BlockUp(0);
            int[] line = bldr.puzzle.getBlock(0);
            Assert.AreEqual(newLine.ToString(), line.ToString ());
        }

        //  test block down method

        [TestMethod]
        public void TestBlockDown()
        {
            objBuilder bldr = CreateBuilder();
            int[] newLine = { 7, 8, 9, 1, 2, 3, 4, 5, 6};

            bldr.BlockDown(0);
            int[] line = bldr.puzzle.getBlock(0);
            Assert.AreEqual(newLine.ToString(), line.ToString());
        }

        //  test shift block left

        [TestMethod]
        public void TestBlockLeft()
        {
            objBuilder bldr = CreateBuilder();
            int[] newLine = { 2, 3, 1, 5, 6, 4, 8, 9, 7 };

                        bldr.BlockLeft(0);
            int[] line = bldr.puzzle.getBlock(0);
            Assert.AreEqual(newLine.ToString(), line.ToString());
        }

        //  test shift block right

        [TestMethod]
        public void TestBlockRight()
        {
            objBuilder bldr = CreateBuilder();
            int[] newLine = { 3, 1, 2, 6, 4, 5, 9, 7, 8 };

            bldr.BlockRight(0);
            int[] line = bldr.puzzle.getBlock(0);
            Assert.AreEqual(newLine.ToString(), line.ToString());
        }

        //  test step 1

        [TestMethod]
        public void TestStep1()
        {
            objBuilder bldr = new objBuilder();
            bldr.Step1();
            Assert.IsTrue (bldr.puzzle.Success());
        }

        //  test step 2

        [TestMethod]
        public void TestStep2()
        {
            objBuilder bldr = new objBuilder();
            bldr.Step1();
            bldr.Step2();
            Assert.IsFalse (bldr.puzzle.Success());
        }

    }
}
