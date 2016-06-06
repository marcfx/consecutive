using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consecutive.Core.BigFileUniqueNumbers
{
    public class BitArrayUint
    {
        readonly BitArray first = new BitArray(int.MaxValue);
        readonly BitArray second = new BitArray(int.MaxValue);
        private bool intMaxValue = false;
        private bool uintMaxValue = false;

        public bool Get(uint index)
        {
            if (index < int.MaxValue)
            {
                return first[GetFirstIndex(index)];
            }
            if (index == int.MaxValue)
            {
                return intMaxValue;
            }
            if (index < uint.MaxValue)
            {
                return second[GetSecondIndex(index)];
            }
            return uintMaxValue;
        }

        public void Set(uint index, bool value)
        {
            if (index < int.MaxValue)
            {
                first[GetFirstIndex(index)] = value;
            }
            else if(index == int.MaxValue)
            {
                intMaxValue = value;
            }
            else if (index < uint.MaxValue)
            {
                second[GetSecondIndex(index)] = value;
            }
            else if(index == uint.MaxValue)
            {
                uintMaxValue = value;
            }
        }

        private static int GetFirstIndex(uint index)
        {
            return (int)index;
        }
        private static int GetSecondIndex(uint index)
        {
            return (int)(index - int.MaxValue-1);
        }
    }
}
