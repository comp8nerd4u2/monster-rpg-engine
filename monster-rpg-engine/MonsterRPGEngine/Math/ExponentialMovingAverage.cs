using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonsterRPGEngine.Collections;

namespace MonsterRPGEngine.Math {
    public class ExponentialMovingAverage {
        private readonly RevolvingArray<float> samples;

        public ExponentialMovingAverage(int maxSamples) {
            samples = new RevolvingArray<float>(maxSamples);
        }

        public void Push(float sample) {
            samples.Push(sample);
        }

        public float CalculateEMA() {
            float resultEMA = 0;
            for (int i = 0; i < samples.Elements; i++) {
                float multiplier = 2 / (i + 2);
                resultEMA = samples[i] * multiplier + resultEMA * (1 - multiplier);
            }
            return resultEMA;
        }
    }
}
