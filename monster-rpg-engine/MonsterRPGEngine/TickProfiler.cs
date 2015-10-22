using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MonsterRPGEngine {
    class TickProfiler {
        private Stopwatch profilerTimer;
        private long baseTime;
        private long ticks;

        public long FPS { get; private set; }

        public TickProfiler() {
            profilerTimer = Stopwatch.StartNew();
        }

        public void StartTick() {
            baseTime = profilerTimer.ElapsedMilliseconds;
        }

        public long StopTick() {
            ticks++;
            long tickSpeed = profilerTimer.ElapsedMilliseconds - baseTime;
            FPS = (tickSpeed > 0) ? 1000 / tickSpeed : long.MaxValue;
            return profilerTimer.ElapsedMilliseconds - baseTime;
        }
    }
}
