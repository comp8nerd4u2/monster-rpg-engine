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
        public void Init() {
            //TODO: Load game assets
        }
        /// <summary>
        /// Check if the user has given input
        /// </summary>
        public void CheckInputs() {

        }
        /// <summary>
        /// Do an update tick
        /// </summary>
        public void Update() {
            //TODO: Add game logic here
            gameTime++; //Iterate game time
        }
        /// <summary>
        /// Render to the screen
        /// </summary>
        public void Render() {
            //TODO: Add render logic here
        }
        /// <summary>
        /// Game loop
        /// </summary>
        public void Run() {
            //TODO: Add engine logic here
            long tickFrequency = 1000L / 60L;
            Stopwatch lastTick = new Stopwatch();
            lastTick.Start();
            while (!shouldTerminate) {
                CheckInputs();
                Update();
                Render();
                //Put loop on hold
                while (lastTick.ElapsedMilliseconds < tickFrequency) {
                    Thread.Yield();
                }
                lastTick.Restart();
            }
            Terminate();
        }
        /// <summary>
        /// Terminates the game
        /// </summary>
        public void Terminate() {
            //TODO: Destroy and clean up game objects
            
        }
    }
}
