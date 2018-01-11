using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileSerializer.Tests
{
    public class DataNorm001ChangeEntryB
    {
        [FixedLengthString(1, 0)]
        public string RecordTypeIndicator { get; set; } = "B";
        [FixedLengthString(1, 1)]
        public string ProcessingFlag { get; set; }
        [FixedLengthString(15, 2)]
        public string ItemCode { get; set; }
        [FixedLengthString(15, 3)]
        public string MatchCode { get; set; }
        [FixedLengthString(15, 4)]
        public string AlternativeItemCode { get; set; }
        [FixedLengthString(8, 5)]
        public string CatalogPage { get; set; }
        [FixedLengthString(8, 6)]
        public string UnknownField { get; set; }
        [FixedLengthString(13, 7)]
        public string GTIN { get; set; }
        [FixedLengthString(12, 8)]
        public string AccessNumber { get; set; }
        [FixedLengthString(10, 9)]
        public string ItemGroup { get; set; }
        [FixedLengthString(2, 10)]
        public int CostType { get; set; }
        [FixedLengthString(2, 11)]
        public int PackagingAmount { get; set; }
        [FixedLengthString(4, 12)]
        public string SupplierReference { get; set; }
        [FixedLengthString(17, 13)]
        public string Reference { get; set; }








    }
}
