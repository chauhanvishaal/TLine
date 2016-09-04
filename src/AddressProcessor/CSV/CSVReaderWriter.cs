using System;
using System.IO;
using AddressProcessing.CSV;
using System.Threading.Tasks;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */
    

    public class CSVReaderWriter : ICSVReader, ICSVWriter
    {

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        ICSVReader csvReader;
        ICSVWriter csvWriter;

        StreamReader _readerStream;
        StreamWriter _writerStream;
        string _fileName = "" ;

        public void Open(string fileName, Mode mode)
        {
            
            _fileName = fileName ;

            if (mode == Mode.Read)
            {
                _readerStream = File.OpenText(fileName);
                csvReader = new CSVReader(_readerStream);
            }
            else if (mode == Mode.Write)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                _writerStream = fileInfo.CreateText();
                csvWriter = new CSVWriter(_writerStream);
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Close()
        {
            if (_writerStream != null)
            {
                _writerStream.Close();
            }

            if (_readerStream != null)
            {
                _readerStream.Close();
            }
        }
        
        public bool Read(string column1, string column2)
        {
            return csvReader.Read(column1, column2);
        }

        public bool Read(out string column1, out string column2)
        {
            if (_readerStream == null)
                throw new Exception("File not open");

            return csvReader.Read(out column1, out column2);
        }

        public void Write(params string[] columns)
        {
            if(_writerStream == null)
                throw new Exception("File not open");

            csvWriter.Write(columns);
        }
    }
}
