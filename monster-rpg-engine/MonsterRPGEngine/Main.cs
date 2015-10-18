using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPGEngine {
    /// <summary>
    /// Main entry class for our game.
    /// </summary>
    class Program {
        private static Engine engine = new Engine();

        public static void Main(String[] argsv) {
            engine.Init();
            engine.Run();
        }
    }
}
