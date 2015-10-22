using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MonsterRPGEngine.Collections;

namespace MonsterRPGEngine {
    class TickProfiler {
        private Stopwatch profilerTimer;
        private long tickStartTime;
        private long ticks;
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
            long tickSpeed = tickEndTime - tickStartTime;
            tickTimes.Push(tickEndTime);

            ticks++;
            FPS = (tickSpeed > 0) ? 1000 / tickSpeed : long.MaxValue;
            return tickSpeed;
        }
    }
}
