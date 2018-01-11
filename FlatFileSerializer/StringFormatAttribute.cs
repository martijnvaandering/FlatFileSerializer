using System;

namespace FlatFileSerializer
{
    public class StringFormatAttribute : Attribute
    {
        public string Format { get; }
        public StringFormatAttribute(string format)
        {
            Format = format;
        }
    }
}
