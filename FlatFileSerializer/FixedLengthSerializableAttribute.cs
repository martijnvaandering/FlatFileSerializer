using System;

namespace FlatFileSerializer
{
    public class FixedLengthSerializableAttribute : Attribute
    {
        public int Line { get; set; }
        public int Order { get; set; }
        public bool Multiline { get; set; }
        public string Separator { get; set; }
    }
}
