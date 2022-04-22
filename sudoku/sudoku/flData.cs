//  load and save puzzles

using System;
using System.IO;

namespace sudoku
{
    internal class flData
    {

        public String fileName = "";

        //  constructors

        public flData()
        {
            fileName = "";
        }

        public flData(String fl)
        {
            fileName = fl;
        }

        //  load data from file

public void load()
        {
        }


        //  write the data to t a text file named fileName

        public void Save(String data)
        {
            StreamWriter wrtr;

            try
            {
                wrtr = File.CreateText(fileName);
                wrtr.WriteLine(data);
                wrtr.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }


    }
}
