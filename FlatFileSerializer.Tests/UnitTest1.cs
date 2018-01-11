<<<<<<< HEAD
﻿using DeWeTechNet.Logic.DataNorm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
=======
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
>>>>>>> 8b14eb0245e4d487337136546137e12251da5d1f
using System.Text.RegularExpressions;

namespace FlatFileSerializer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDeserialize_MultiLine_Ok()
        {
            var serializer = new Serializer<ICMHeader>();
            var obj = serializer.Deserialize(
@"RE
Voorraad
BerendBotje  

0000445
B
001







03-01-2018
1


009
0000002
523457
Branderset Topline 25-30 KW 8738901
000056.00
0000010
555002
Koppeling recht knel 15x15mm k/g
000001.31
0000001
294811
Plooibocht KEN-LOK 90gr. 125 mm
000003.44
0000001
294602
Lengte a 3 m. spiraalbuis gefelst 1
000015.09
0000003
561998
Pijpbeugel verz. Bifix G2 + inlage 
000003.39
0000001
480529
Pompschakelaar 230V
000045.70
0000001
510151
Kamerthermostaat Round Modulation T
000034.42
0000001
520110
Expansievat Flexcon met voeten 35/0
000062.95
0000001
511011
Klokthermostaat Chronotherm Modulat
000116.66
");

            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void TestDeserialize_MultiLine_Trim_Ok()
        {
            var serializer = new Serializer<ICMHeader>();
            var obj = serializer.Deserialize(
@"RE
voorraad
Berend Botje

000000123
B
001







04-01-2018
1


001", true);

            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.Debnummer, "000000123");
        }

        [TestMethod]
        public void TestDeserialize_SingleLine_Ok()
        {
            var serializer = new Serializer<UGLAddress>();
            var obj = serializer.Deserialize("ADRName                          Name2                         Name3                         Street                           PLZ123Worms                         ");
            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.ADR, "ADR");
            Assert.AreEqual(obj.City, "Worms                         ");
        }

        [TestMethod]
        public void TestDeserialize_SingleLine_Trim_Ok()
        {
            var serializer = new Serializer<UGLAddress>();
            var obj = serializer.Deserialize("ADRName                          Name2                         Name3                         Street                           PLZ123Worms                         ", true);
            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.ADR, "ADR");
            Assert.AreEqual(obj.City, "Worms");
        }

        [TestMethod]
        public void TestSerialize_SingleLine_Ok()
        {
            var serializer = new Serializer<UGLAddress>();
            var obj = serializer.Serialize(new UGLAddress()
            {
                City = "Worms",
                CountryCode = "DE",
                Address = "Addr1",
                Name1 = "Name1",
                Name2 = "Name2",
                Name3 = "Name3",
                ZipCode = "PLZ123"
            });

            Assert.IsNotNull(obj);

        }

        [TestMethod]
        public void TestDeserialize_SingleLine_sync_Ok()
        {
            var serializer = new Serializer<UGLAddress>();
            var orig = "ADRName                          Name2                         Name3                         Street                           PLZ123Worms                         ";
            var obj = serializer.Deserialize(orig, true);
            Assert.IsNotNull(obj);
<<<<<<< HEAD
            var newstr = serializer.Serialize(obj,false);
=======
            var newstr = serializer.Serialize(obj);
>>>>>>> 8b14eb0245e4d487337136546137e12251da5d1f
            Assert.IsNotNull(newstr);
            Assert.AreEqual(orig, newstr);
        }

        [TestMethod]
        public void TestSerialize_MultiLine_Ok()
        {
            var serializer = new Serializer<ICMHeader>();
            var input = new ICMHeader();
            foreach (var prop in typeof(ICMHeader).GetProperties())
            {
                try
                {
                    prop.SetValue(input, prop.Name);
                }
                catch { }
            }
            var output = serializer.Serialize(input);
            Assert.IsNotNull(output);
        }


        [TestMethod]
        public void TestSrialize_MultiLine_Sync_Ok()
        {
            var orig = @"RE
voorraad
BerendBotje

00001111
B
001







04-01-2018
1


001";
            var serializer = new Serializer<ICMHeader>();
            var obj = serializer.Deserialize(orig, true);
<<<<<<< HEAD

=======
            
>>>>>>> 8b14eb0245e4d487337136546137e12251da5d1f
            Assert.IsNotNull(obj);
            Assert.AreEqual(obj.Debnummer, "00001111");

            var newStr = serializer.Serialize(obj);
            var origCompare = Regex.Replace(orig, @"\s", string.Empty);
            var newStrCompare = Regex.Replace(newStr, @"\s", string.Empty);
            Assert.AreEqual(origCompare, newStrCompare);
        }
<<<<<<< HEAD

        [TestMethod]
        public void DatanormHeadTest()
        {
            var serializer = new FlatFileSerializer.Serializer<Head>();
            var strHead = serializer.Serialize(new Head()
            {
                CurrencyIndicator = "EUR",
                Date = new DateTime(2018, 1, 10),
                Version = 4,
                InformationText1 = "Datanorm  Rabattsatz       Disk.Nr.: 001",
                InformationText2 = "    Weitergabe der Daten nur mit schrift",
                InformationText3 = "l.Genehmigung",
                RabattEntries = new List<DataNormRabatEntry>() {new DataNormRabatEntry()
            {
                DiscountGroup = "0000",
                DiscountType = "1",
                DiscountGroup2 = "0",
                DiscountGroupName = "Verbrauchsmaterialien"

            },new DataNormRabatEntry()
            {
                DiscountGroup = "0000",
                DiscountType = "1",
                DiscountGroup2 = "0",
                DiscountGroupName = "Verbrauchsmaterialien"

            } }
            }, true);

            Debug.WriteLine(strHead);

            Assert.AreEqual(@"V 100118Datanorm  Rabattsatz       Disk.Nr.: 001    Weitergabe der Daten nur mit schriftl.Genehmigung                      04EUR
R;;0000;1;0;Verbrauchsmaterialien;
R;;0000;1;0;Verbrauchsmaterialien;",
                strHead);
        }

        [TestMethod]
        public void DatanormDiscountTest()
        {
            var serializer = new Serializer<DataNormRabatEntry>();
            var strHead = serializer.Serialize(new DataNormRabatEntry()
            {
                DiscountGroup = "0000",
                DiscountType = "1",
                DiscountGroup2 = "0",
                DiscountGroupName = "Verbrauchsmaterialien"

            }, true);

            //Assert.AreEqual("R;;0000;1;0;Verbrauchsmaterialien;",
            //    strHead);
        }


        [TestMethod]
        public void DebugDateTime()
        {
            Assert.AreEqual("100118", new DateTime(2018, 1, 10).ToString("ddMMyy"));
        }
=======
>>>>>>> 8b14eb0245e4d487337136546137e12251da5d1f
    }
}
