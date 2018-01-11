using System;

namespace FlatFileSerializer
{
    public class FixedLengthStringAttribute : Attribute
    {
        public int Length { get; set; }
        public int Order { get; set; }
        public string Filler { get; set; }
<<<<<<< HEAD
        public bool IgnoreTrim { get; set; }
=======
>>>>>>> 8b14eb0245e4d487337136546137e12251da5d1f

        public FixedLengthStringAttribute(int length)
        {
            Length = length;
        }

        public FixedLengthStringAttribute(int length, int order) : this(length)
        {
            Order = order;
        }

        public FixedLengthStringAttribute(int length, int order, string filler) : this(length, order)
        {
            Filler = filler;
        }
    }
}
