using System.Collections.Generic;

namespace FlatFileSerializer.Tests
{
    [FixedLengthSerializable(Line = 0, Multiline = true)]
    public class ICMHeader
    {
        [FixedLengthString(2, 0)]
        public string Inlogcode { get; set; }
        [FixedLengthString(35, 1)]
        public string Refnr { get; set; }
        [FixedLengthString(35, 2)]
        public string Extraref { get; set; }
        [FixedLengthString(10, 3)]
        public string Project { get; set; }
        [FixedLengthString(10, 4)]
        public string Debnummer { get; set; }
        [FixedLengthString(1, 5)]
        public string Afhaalbezorg { get; set; }
        [FixedLengthString(3, 6)]
        public string Adrescode { get; set; }
        [FixedLengthString(30, 7)]
        public string Varnaam1 { get; set; }
        [FixedLengthString(30, 8)]
        public string Varnaam2 { get; set; }
        [FixedLengthString(30, 9)]
        public string Varstraat { get; set; }
        [FixedLengthString(18, 10)]
        public string Varplaats { get; set; }
        [FixedLengthString(6, 11)]
        public string Varpostcode { get; set; }
        [FixedLengthString(5, 12)]
        public string Varhuisnr { get; set; }
        [FixedLengthString(13, 13)]
        public string Vartelefoon { get; set; }
        [FixedLengthString(10, 14)]
        public string Gafleverdatum { get; set; }
        [FixedLengthString(1, 15)]
        public string Levconditie { get; set; }
        [FixedLengthString(38, 16)]
        public string Levinstruct1 { get; set; }
        [FixedLengthString(38, 17)]
        public string Levinstruct2 { get; set; }
        [FixedLengthString(3, 18), StringFormat("{0:000}")]
        public int Orderregels { get; set; }

        [FixedLengthSerializable]
        public IEnumerable<ICMItem> Items { get; set; }

    }
}
