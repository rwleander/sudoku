//  flLog - solver log file

using System;
using System.IO;

namespace sudoku
{
    public class flLog
    {

        //  private properties

        private String fileName = "c:\\sudoku\\data\\solver.log";

        //  constructor - delete file, then create new

        public flLog()
        {
            StreamWriter wrtr;

//  delete the old file

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //  create the new file

            try
            {
                wrtr = File.CreateText(fileName);
                wrtr.Close();
            }
            catch
            {
                throw;
            }            
        }

        //  write a line to the log

        public void WriteLine(String txt)
        {
            StreamWriter wrtr;

            //  open the stream writer

            try
            {
                wrtr = File.AppendText(fileName);
            }
            catch
            {
                throw;
            }

            // write the text then close

            wrtr.WriteLine(txt);
            wrtr.Close();
        }
        
    }
}
