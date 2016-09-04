using System;
using System.IO;
using AddressProcessing.CSV;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Diagnostics;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */
    

    public class CSVReaderWriter //: ICSVReader, ICSVWriter
    {

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        ICSVReader csvReader;
        ICSVWriter csvWriter;
        StreamReader _readerStream;
        StreamWriter _writerStream;
        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                //_readerStream = File.OpenText(fileName);
                using (MemoryMappedFile mmFile = MemoryMappedFile.CreateFromFile(fileName))
                {
                    Stream mmvStream = mmFile.CreateViewStream();
                    csvReader = new CSVReader( _readerStream = new StreamReader(mmvStream));
                }
                //csvReader = new CSVReader(_readerStream);
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

        static int rowNum =0;

        public void FastRead()
        {
            
           
            //  using (StreamReader sr = new StreamReader(mmvStream, ASCIIEncoding.ASCII)) {
 
            //    while (!sr.EndOfStream) {
 
            //      String line = sr.ReadLine();
            //      cr.FastRead(line);
                  
                    
            //    }
            //  }            
            //}

        }

        public void Write(params string[] columns)
        {
            if(_writerStream == null)
                throw new Exception("File not open");

            csvWriter.Write(columns);
        }
    }
}
