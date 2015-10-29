using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPGEngine.Collections {
    public class RevolvingArray<T> {
        private T[] internalArray;
        private int revolvingBase;
        private int revolvingIndex;
        public int Elements { get; private set; }
        public int Capacity { get; private set; }

        public RevolvingArray(int capacity) {
            internalArray = new T[capacity];
            Capacity = capacity;
        }

        private bool IndexOutOfRange(int index) {
            return (index < 0 || index >= internalArray.Length);
        }

        private int WrapIndex(int index) {
            
            if (IndexOutOfRange(index)) {
                int indexOverflow = index - revolvingBase;
                return indexOverflow - 1;
            }
            return index;
        }

        public T this[int index] {
            get {
                if (IndexOutOfRange(index))
                    throw new IndexOutOfRangeException();
                return internalArray[WrapIndex(index + revolvingBase)];
            }
            set {
                if (IndexOutOfRange(index))
                    throw new IndexOutOfRangeException();
                internalArray[WrapIndex(index + revolvingBase)] = value;
            }
        }

        public void Push(T item) {
            this[revolvingIndex] = item;
            revolvingIndex = WrapIndex(revolvingIndex + 1);
            if (revolvingIndex == revolvingBase) //We're full
                revolvingBase = WrapIndex(revolvingBase + 1);
            else if (Elements < Capacity)
                Elements++;
        }

        
    }
}
