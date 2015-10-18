using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPG {
    /// <summary>
    /// Main entry class for our game.
    /// </summary>
    class MonsterRPG {
        private static Engine game = new Engine();

        public static void main(String[] argsv) {
            game.init();
            game.run();
        }
    }
}
