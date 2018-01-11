using FlatFileSerializer;
using FlatFileSerializer.Tests;
using System;
using System.Collections.Generic;

namespace DeWeTechNet.Logic.DataNorm
{
    [FixedLengthSerializable(Line = 0)]
    public class Head
    {
        [FixedLengthString(1, 0)]
        public string RecordTypeIndicator { get; set; } = "V";
        [FixedLengthString(1, 1, IgnoreTrim = true)]
        public string EmptySeparator { get; set; } = " ";
        [FixedLengthString(6, 2), StringFormat("ddMMyy")]
        public DateTime Date { get; set; }
        [FixedLengthString(40, 3, IgnoreTrim = true)]
        public string InformationText1 { get; set; }
        [FixedLengthString(40, 4, IgnoreTrim = true)]
        public string InformationText2 { get; set; }
        [FixedLengthString(35, 5, IgnoreTrim = true)]
        public string InformationText3 { get; set; }
        [FixedLengthString(2, 6), StringFormat("{0:00}")]
        public int Version { get; set; }
        [FixedLengthString(3, 7, IgnoreTrim = true)]
        public string CurrencyIndicator { get; set; }

        [FixedLengthSerializable]
        public IEnumerable<DataNormRabatEntry> RabattEntries { get; set; }

    }
}
