using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Words.Common;

namespace Words.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string text =
                "Вот дом, Который построил Джек. А это пшеница, Которая в темном чулане хранится В доме, Который построил Джек.";
            IOccurencies occurencies = new Occurencies();
            Dictionary<string, int> result = occurencies.Find(text);
            Assert.IsNotNull(result, "Result is null!");
            Assert.IsTrue(result.Count == 15, "Not 15 unique words");
            Assert.IsTrue(result["Вот"] == 1);
            Assert.IsTrue(result["дом"] == 1);
            Assert.IsTrue(result["Который"] == 2);
            Assert.IsTrue(result["построил"] == 2);
            Assert.IsTrue(result["Джек"] == 2);
        }
    }
}
