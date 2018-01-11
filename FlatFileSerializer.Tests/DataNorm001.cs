using DeWeTechNet.Logic.DataNorm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileSerializer.Tests
{
    [FixedLengthSerializable(Line = 1)]
    public class DataNorm001
    {
        [FixedLengthSerializable(Line = 1)]
        public DataNorm001ChangeEntryA DataNorm001ChangeEntryA { get; set; }
        [FixedLengthSerializable(Line = 2)]
        public DataNorm001ChangeEntryB DataNorm001ChangeEntryB { get; set; }
    }
}
