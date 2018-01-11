namespace FlatFileSerializer.Tests
{
    [FixedLengthSerializable(Line = 1, Separator = ";")]
    public class DataNormRabatEntry
    {
        [FixedLengthString(1, 0)]
        public string Prefix { get { return "R"; } }
        [FixedLengthString(1, 1)]
        public string EmptySeparator { get { return ""; } }
        [FixedLengthString(4, 2)]
        public string DiscountGroup { get; set; }
        [FixedLengthString(1, 3)]
        public string DiscountType { get; set; }
        [FixedLengthString(4, 4)]
        public string DiscountGroup2 { get; set; }
        [FixedLengthString(40, 5)]
        public string DiscountGroupName { get; set; }
    }
}
