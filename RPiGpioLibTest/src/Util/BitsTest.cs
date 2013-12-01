using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPi.Gpio.Util;

namespace RPi.Gpio.Test.Util
{
    [TestClass]
    public class BitsTest
    {
        [TestMethod]
        public void SetClearBit()
        {
            Assert.AreEqual<int>(1, Bits.SetClearBit(0, 0, true));
            Assert.AreEqual<int>(0, Bits.SetClearBit(1, 0, false));
            Assert.AreEqual<int>(2, Bits.SetClearBit(2, 0, false));
            Assert.AreEqual<int>(3, Bits.SetClearBit(2, 0, true));
        }

        [TestMethod]
        public void ReadBit()
        {
            Assert.IsTrue(Bits.ReadBit(1, 0));
            Assert.IsFalse(Bits.ReadBit(0, 0));
            Assert.IsFalse(Bits.ReadBit(2, 0));
            Assert.IsTrue(Bits.ReadBit(3, 0));
        }

        [TestMethod]
        public void ClearBitsFromMask()
        {
            Assert.AreEqual<int>(0, Bits.ClearBitsFromMask(1, 1));
            Assert.AreEqual<int>(0, Bits.ClearBitsFromMask(0, 1));
            Assert.AreEqual<int>(2, Bits.ClearBitsFromMask(2, 1));
        }

        [TestMethod]
        public void CompoundMask()
        {
            HashSet<int> bitPositions = new HashSet<int>();
            bitPositions.Add(0);
            Assert.AreEqual<int>(1, Bits.CompoundMask(bitPositions));
            bitPositions.Add(1);
            Assert.AreEqual<int>(3, Bits.CompoundMask(bitPositions));
            bitPositions.Add(2);
            Assert.AreEqual<int>(7, Bits.CompoundMask(bitPositions));
        }

        [TestMethod]
        public void ClearBits()
        {
            HashSet<int> bitPositions = new HashSet<int>();
            bitPositions.Add(0);
            Assert.AreEqual<int>(0, Bits.ClearBits(1, bitPositions));
            Assert.AreEqual<int>(2, Bits.ClearBits(2, bitPositions));
            bitPositions.Add(1);
            Assert.AreEqual<int>(0, Bits.ClearBits(1, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(2, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(3, bitPositions));
            Assert.AreEqual<int>(4, Bits.ClearBits(4, bitPositions));
            Assert.AreEqual<int>(4, Bits.ClearBits(5, bitPositions));
            bitPositions.Add(2);
            Assert.AreEqual<int>(0, Bits.ClearBits(1, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(2, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(3, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(4, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(5, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(6, bitPositions));
            Assert.AreEqual<int>(0, Bits.ClearBits(7, bitPositions));
            Assert.AreEqual<int>(8, Bits.ClearBits(8, bitPositions));
            Assert.AreEqual<int>(8, Bits.ClearBits(9, bitPositions));
        }

        [TestMethod]
        public void SetBits()
        {
            HashSet<int> bitPositions = new HashSet<int>();
            bitPositions.Add(0);
            Assert.AreEqual<int>(1, Bits.SetBits(0, bitPositions));
            Assert.AreEqual<int>(1, Bits.SetBits(1, bitPositions));
            Assert.AreEqual<int>(3, Bits.SetBits(2, bitPositions));
            bitPositions.Add(1);
            Assert.AreEqual<int>(3, Bits.SetBits(1, bitPositions));
            Assert.AreEqual<int>(3, Bits.SetBits(2, bitPositions));
            Assert.AreEqual<int>(3, Bits.SetBits(3, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(4, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(5, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(6, bitPositions));
            bitPositions.Add(2);
            Assert.AreEqual<int>(7, Bits.SetBits(1, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(2, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(3, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(4, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(5, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(6, bitPositions));
            Assert.AreEqual<int>(7, Bits.SetBits(7, bitPositions));
            Assert.AreEqual<int>(15, Bits.SetBits(8, bitPositions));
            Assert.AreEqual<int>(15, Bits.SetBits(9, bitPositions));
        }

        [TestMethod]
        public void ToBitString()
        {
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0000", Bits.ToBitString(0));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0001", Bits.ToBitString(1));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0010", Bits.ToBitString(2));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0011", Bits.ToBitString(3));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0100", Bits.ToBitString(4));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0101", Bits.ToBitString(5));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0110", Bits.ToBitString(6));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-0111", Bits.ToBitString(7));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-1000", Bits.ToBitString(8));
            Assert.AreEqual<string>("0000-0000-0000-0000-0000-0000-0000-1001", Bits.ToBitString(9));
            Assert.AreEqual<string>("0010-0000-1011-0101-1111-1000-0100-0111", Bits.ToBitString(548796487));
            Assert.AreEqual<string>("0111-1111-1111-1111-1111-1111-1111-1111", Bits.ToBitString(2147483647));
            Assert.AreEqual<string>("1111-1111-1111-1111-1111-1111-1111-1111", Bits.ToBitString(-1));
            Assert.AreEqual<string>("1000-0000-0000-0000-0000-0000-0000-0000", Bits.ToBitString(-2147483648));
        }
    }
}
