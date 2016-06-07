using System.Collections;
using System.Collections.Generic;

namespace Consecutive.Core.BitmaskSort
{
    public class BitArrayUInt
    {
        readonly BitArray _first = new BitArray(int.MaxValue);
        readonly BitArray _second = new BitArray(int.MaxValue);
        private bool _intMaxValue = false;
        private bool _uintMaxValue = false;

        public bool Get(uint index)
        {
            if (index < int.MaxValue)
            {
                return _first[GetFirstIndex(index)];
            }
            if (index == int.MaxValue)
            {
                return _intMaxValue;
            }
            if (index < uint.MaxValue)
            {
                return _second[GetSecondIndex(index)];
            }
            return _uintMaxValue;
        }

        public void Set(uint index, bool value)
        {
            if (index < int.MaxValue)
            {
                _first[GetFirstIndex(index)] = value;
            }
            else if(index == int.MaxValue)
            {
                _intMaxValue = value;
            }
            else if (index < uint.MaxValue)
            {
                _second[GetSecondIndex(index)] = value;
            }
            else if(index == uint.MaxValue)
            {
                _uintMaxValue = value;
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

        public IEnumerable<uint> GetValuesFromArray()
        {
            for (uint valuesFromArray = 0; valuesFromArray < uint.MaxValue; valuesFromArray++)
            {
                if (!Get(valuesFromArray))
                {
                    continue;
                }
                yield return valuesFromArray;
            }
        }
    }
}
