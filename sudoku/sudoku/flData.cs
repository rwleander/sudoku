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

public String load()
        {
            StreamReader rdr;
            String data;

            try
            {
                rdr = File.OpenText(fileName);
            }
            catch (Exception e)
            {
                throw (e);
            }

            data = rdr.ReadToEnd();
            rdr.Close();

            return data;
        }


        //  write the data to t a text file named fileName

        public void Save(objBoard puzzle)
        {
            StreamWriter wrtr;

            try
            {
                wrtr = File.CreateText(fileName);
                wrtr.WriteLine(puzzle.ToString ());
                wrtr.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }

        }


    }
}
