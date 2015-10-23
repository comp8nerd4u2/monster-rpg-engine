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
        private RevolvingArray<long> tickTimes = new RevolvingArray<long>(10);
        public long ElapsedTickTime {
            get {
                return profilerTimer.ElapsedMilliseconds - tickStartTime;
            }
        }

        public long FPS { get; private set; }

        public TickProfiler() {
            profilerTimer = Stopwatch.StartNew();
        }

        public void StartTick() {
            tickStartTime = profilerTimer.ElapsedMilliseconds;
        }

        public long StopTick() {
            long tickEndTime = profilerTimer.ElapsedMilliseconds;
            TickSnapshot tickSnapshot = new TickSnapshot(tickStartTime, tickEndTime);
            long tickSpeed = tickEndTime - tickStartTime;
            tickTimes.Push(tickEndTime);
            ema.Push(tickSnapshot.TickSpeed);
            Console.WriteLine(ema.EMA);
            ticks++;
            FPS = (tickSpeed > 0) ? 1000 / tickSpeed : long.MaxValue;
            return tickSpeed;
        }
    }
}
