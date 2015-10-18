using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MonsterRPG {
    /// <summary>
    /// Represents implementation of the game
    /// </summary>
    class Engine {
        long gameTime = 0L;
        Boolean shouldTerminate = false;
        /// <summary>
        /// Load game assets and prepare to start
        /// </summary>
        public void init() {
            //TODO: Load game assets
        }
        /// <summary>
        /// Check if the user has given input
        /// </summary>
        public void checkInputs() {

        }
        /// <summary>
        /// Do an update tick
        /// </summary>
        public void update() {
            //TODO: Add game logic here
            gameTime++; //Iterate game time
        }
        /// <summary>
        /// Render to the screen
        /// </summary>
        public void render() {
            //TODO: Add render logic here
        }
        /// <summary>
        /// Game loop
        /// </summary>
        public void run() {
            //TODO: Add engine logic here
            long tickFrequency = 1000L / 60L;
            Stopwatch lastTick = new Stopwatch();
            lastTick.Start();
            while (!shouldTerminate) {
                checkInputs();
                update();
                render();
                //Put loop on hold
                while (lastTick.ElapsedMilliseconds < tickFrequency) {
                    Thread.Yield();
                }
                lastTick.Restart();
            }
            terminate();
        }
        /// <summary>
        /// Terminates the game
        /// </summary>
        public void terminate() {
            //TODO: Destroy and clean up game objects
            
        }
    }
}
