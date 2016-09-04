using System;
using System.IO;


namespace AddressProcessing.CSV
{
    class CSVWriter : ICSVWriter
    {
        StreamWriter _writerStream;

        public CSVWriter(StreamWriter stream)
        {
            _writerStream = stream;
        }

        public void Write(params string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }

            WriteLine(outPut);
        }

        private void WriteLine(string line)
        {
            _writerStream.WriteLine(line);
        }
    }
}
