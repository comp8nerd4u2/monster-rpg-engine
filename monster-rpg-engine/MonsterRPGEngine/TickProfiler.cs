using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MonsterRPGEngine.Collections;
using MonsterRPGEngine.Math;

namespace MonsterRPGEngine {
    class TickProfiler {
        private Stopwatch profilerTimer;
        private long tickStartTime;
        private long ticks;
        private ExponentialMovingAverage ema = new ExponentialMovingAverage(60);
        private RevolvingArray<TickSnapshot> tickSnapshots = new RevolvingArray<TickSnapshot>(10);
        public long ElapsedTickTime {
            get {
                return profilerTimer.ElapsedMilliseconds - tickStartTime;
            }
        }

        public float FPS { get; private set; }

        public TickProfiler() {
            profilerTimer = Stopwatch.StartNew();
        }

        public void StartTick() {
            tickStartTime = profilerTimer.ElapsedMilliseconds;
        }

        public long StopTick() {
            long tickEndTime = profilerTimer.ElapsedMilliseconds;
            TickSnapshot tickSnapshot = new TickSnapshot(tickStartTime, tickEndTime);
            tickSnapshots.Push(tickSnapshot);
            ema.Push(tickSnapshot.TickSpeed);
            Console.WriteLine(ema.CalculateEMA());
            ticks++;
            FPS = 1.0F / (((tickSnapshot.TickSpeed > 0) ? tickSnapshot.TickSpeed : 1.0F) / 1000.0F);
            return tickSnapshot.TickSpeed;
        }
    }
}
