using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonsterRPGEngine.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPGEngine.Collections.Tests {
    [TestClass()]
    public class RevolvingArrayTests {

        [TestMethod()]
        public void Push_Test() {
            Trace.WriteLine("Starting push test");
            int[] testArray = {
                10, 2, 4, 7, 1, 8, 9
            };
            Trace.WriteLine("Test Array: " + string.Join(",", testArray));
            int[] expectedArray = {
                4, 7, 1, 8, 9
            };
            Trace.WriteLine("Expected Array: " + string.Join(",", expectedArray));
            RevolvingArray<int> test = new RevolvingArray<int>(5);
            for (int i = 0; i < testArray.Length; i++) {
                test.Push(testArray[i]);
            }

        }

    }
}