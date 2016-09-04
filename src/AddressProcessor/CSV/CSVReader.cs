using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace AddressProcessing.CSV
{
    class CSVReader : ICSVReader
    {
        StreamReader _readerStream;

        public CSVReader()
        {

        }

        public CSVReader(StreamReader streamReader)
        {
            _readerStream = streamReader;
        }

        public bool Read(string column1, string column2)
        {
            return Read(column1, column2);
        }

        /// <summary>
        /// Keep for backward compatibility. Use CSVRecord Read() instead
        /// </summary>
        /// <param name="column1"></param>
        /// <param name="column2"></param>
        /// <returns></returns>
        public bool Read(out string column1, out string column2)
        {
            CSVRecord record = Read();
            if (record == null)
            {
                column1 = null;
                column2 = null;
                return false;
            }

            column1 = record.col1;
            column2 = record.col2;

            //Debug.WriteLine("{0}{1}", column1, column2);
            return true;

        }

        /// <summary>
        /// New clients should use this method instead of Read(out string column1, out string column2)
        /// </summary>
        /// <returns></returns>
        public CSVRecord Read()
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;
            CSVRecord fileRecord  = new CSVRecord() ;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = ReadLine();
            line = line.Trim('\0');
            if (string.IsNullOrEmpty(line))
            {
                return null;
            }

            //Debug.WriteLine("[{0:D4}] \"{1}\"", line.Length, line);

            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                return null;
            }
            else
            {
                fileRecord.col1 = columns[FIRST_COLUMN];
                fileRecord.col2 = columns[SECOND_COLUMN];
                
                return fileRecord ;
            }
        }

        private string ReadLine()
        {
            return _readerStream.ReadLine();


        }
    }
}
