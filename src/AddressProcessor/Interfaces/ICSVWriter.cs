using System;

namespace AddressProcessing.CSV
{
    interface ICSVWriter
    {
        void Write(params string[] columns);
    }
}
