using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPGEngine.Math {
    class LongMovingAverage {
        private long[] samples;
        private int currentIndex = 0;
        private bool full = false;
        public long Average {
            get {
                long ceiling = (full) ? samples.Length : currentIndex;
                long sum = 0;
                for (int i = 0; i < ceiling; i++)
                    sum += samples[i];
                return sum / ceiling;
            }
        }
        public LongMovingAverage(int maxSamples) {
            samples = new long[maxSamples];
        }
        public void Add(long sample) {
            samples[currentIndex] = sample;
            currentIndex++;
            if (currentIndex >= samples.Length) {
                currentIndex = 0;
                full = true;
            }
        }
    }
}
