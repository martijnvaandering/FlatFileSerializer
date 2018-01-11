namespace FlatFileSerializer.Tests
{
    [FixedLengthSerializable(Separator = ";")]
    public class DataNorm001ChangeEntryA
    {
        [FixedLengthString(1, 0)]
        public string RecordTypeIndicator { get; set; } = "A";
        [FixedLengthString(1, 1)]
        public string ChangeFlag { get; set; }
        [FixedLengthString(15, 2)]
        public string ItemCode { get; set; }
        [FixedLengthString(2, 3)]
        public string TextType { get; set; }
        [FixedLengthString(40, 4)]
        public string Description1 { get; set; }
        [FixedLengthString(40, 5)]
        public string Description2 { get; set; }
        [FixedLengthString(2, 6)]
        public string PriceType { get; set; }
        [FixedLengthString(1, 7)]
        public string UnitQuantityMultiplier { get; set; }
        [FixedLengthString(2, 9)]
        public string UnitCode { get; set; }
        [FixedLengthString(8, 10)]
        public string Price { get; set; }
        [FixedLengthString(4, 11)]
        public string DiscountGroup { get; set; }
        [FixedLengthString(3, 12)]
        public string ItemGroup { get; set; }
        [FixedLengthString(4, 13)]
        public string LongTextKey { get; set; }





    }
}
