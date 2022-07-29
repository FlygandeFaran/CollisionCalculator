using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace CollisionCalc
{
    class CSVwriter
    {

        public void WriteToCSV(string fileName, List<string> strOut)
        {
            StreamWriter sw = new StreamWriter(fileName, false);
            foreach (string text in strOut)
            {
                sw.Write(text);
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

    }
}
