namespace FlatFileSerializer
{
    [FixedLengthSerializable]
    public class UGLAddress
    {
        [FixedLengthString(3, 0)]
        public string ADR { get { return "ADR"; } }
        [FixedLengthString(30, 1)]
        public string Name1 { get; set; }
        [FixedLengthString(30, 3)]
        public string Name2 { get; set; }
        [FixedLengthString(30, 4)]
        public string Name3 { get; set; }
        [FixedLengthString(30, 5)]
        public string Address { get; set; }
        [FixedLengthString(3, 6)]
        public string CountryCode { get; set; }
        [FixedLengthString(6, 7)]
        public string ZipCode { get; set; }
        [FixedLengthString(30, 8)]
        public string City { get; set; }
    }
}
