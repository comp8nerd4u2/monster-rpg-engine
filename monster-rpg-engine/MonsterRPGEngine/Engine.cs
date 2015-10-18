using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using SharpDX.Windows;

namespace MonsterRPGEngine {
    /// <summary>
    /// Represents implementation of the game
    /// </summary>
    class Engine {
        public static RenderForm gameWindow = new RenderForm("MonsterRPGEngine");

        long gameTime = 0L;
        Boolean shouldTerminate = false;
        /// <summary>
        /// Load game assets and prepare to start
        /// </summary>
        public void Init() {
            //TODO: Create the game window
            
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
            long tickFrequency = 1000L / 60L; //60 ticks per second
            Stopwatch lastTick = new Stopwatch();
            lastTick.Start();
            while (!shouldTerminate) {
                CheckInputs();
                Update();
                Render();
                //Wait patiently to perform the next tick
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
