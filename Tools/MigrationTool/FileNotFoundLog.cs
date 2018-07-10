using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MigrationTool
{
    public class FileNotFoundLog
    {
        private static string _logFilePath = @"C:\Users\Mattanapol.K\Documents\NotFound.txt";

        public static void WriteLog(string fileName)
        {
            using (StreamWriter w = File.AppendText(_logFilePath))
            {
                w.WriteLine("Can't Map File\t{0}", fileName);
            }
        }
    }
}
