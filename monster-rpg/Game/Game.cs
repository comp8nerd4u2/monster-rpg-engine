using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterRPG {
    /// <summary>
    /// Represents implementation of the game
    /// </summary>
    class Game {
        long gameTime = 0L;
        /// <summary>
        /// Load game assets and prepare to start
        /// </summary>
        public void init() {
            //TODO: Load game assets
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
            while (true) {
                update();
                render();
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
