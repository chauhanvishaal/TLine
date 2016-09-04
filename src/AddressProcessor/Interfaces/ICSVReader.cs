using System;

namespace AddressProcessing.CSV
{
    interface ICSVReader
    {
        bool Read(out string column1, out string column2);
        bool Read(string column1, string column2);
    }
}