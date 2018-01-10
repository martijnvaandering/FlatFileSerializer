namespace FlatFileSerializer.Tests
{
    [FixedLengthSerializable(Order = 2, Multiline = true)]
    public class ICMItem
    {
        [FixedLengthString(7, 0)]
        public string Aantalart { get; set; }
        [FixedLengthString(13, 1)]
        public string Artnr { get; set; }
        [FixedLengthString(38, 2)]
        public string Artomschr { get; set; }
        [FixedLengthString(9, 3)]
        public string Verkoopprijs { get; set; }
    }
}
