using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPGEngine {
    class TickSnapshot {
        public readonly long StartTime;
        public readonly long EndTime;
        public readonly float TickSpeed;
        public TickSnapshot(long startTime, long endTime) {
            StartTime = startTime;
            EndTime = endTime;
            TickSpeed = EndTime - StartTime;
        }
    }
}
