using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonsterRPGEngine;

namespace MonsterRPGEngine.Windows {
    class MessageLoopThread {
        public static void StartMessageLoop() {
            Application.Run(Engine.gameWindow);
        }
    }
}
