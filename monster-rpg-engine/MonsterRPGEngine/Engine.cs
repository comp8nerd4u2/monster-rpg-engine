using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Windows;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using D2D1Factory = SharpDX.Direct2D1.Factory;
using D3D11Device = SharpDX.Direct3D11.Device;
using MonsterRPGEngine.Drawing;
using MonsterRPGEngine.Math;

namespace MonsterRPGEngine {
    /// <summary>
    /// Represents implementation of the game
    /// </summary>
    class Engine {
        public RenderForm gameWindow;

        public DirectManager DM { get; private set; }
        private bool ShouldDisplayFPS = true; //Placeholder until FPS algorithm is added
        private LongMovingAverage FPS = new LongMovingAverage(10);

        long gameTime = 0L;
        Boolean shouldTerminate = false;
        /// <summary>
        /// Load game assets and prepare to start
        /// </summary>
        public void Init() {
            /* 
             * Create a new thread for our game window.
             * Initializing in a new thread complicates our code,
             * but it is worth having seperate UI and Engine threads
             */
            AutoResetEvent DMInitialized = new AutoResetEvent(false);
            Thread.CurrentThread.Name = "EngineThread";
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Thread UIThread = new Thread(() => {
                gameWindow = new RenderForm("MonsterRPGEngine");
                gameWindow.FormClosed += (o, e) => {
                    Terminate();
                };
                //TODO: Learn how to initialize DirectX components
                //Initialize here because WinForms wont let us do it outside UI thread
                DM = new DirectManager(gameWindow);
                DMInitialized.Set();
                //Wow that was a handful. Now we can start our message loop on this thread.
                gameWindow.Show();
                Application.Run(gameWindow);
            });
            UIThread.SetApartmentState(ApartmentState.STA);
            UIThread.Name = "UIThread";
            UIThread.Start();
            DMInitialized.WaitOne();
            DMInitialized.Dispose();
            //TODO: Do some other initialization
            
            
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
            //WARNING: May have to move rendering code to message loop in UIThread if this is causing issues
            Monitor.Enter(DM.DirectXLock);
            WindowRenderTarget renderTarget = DM.RenderTarget;
            if (renderTarget != null) {
                //Begin Draw
                renderTarget.BeginDraw();
                renderTarget.Clear(Color.CornflowerBlue.ToColor4()); //Clear using a common game clear color

                //End Draw
                renderTarget.EndDraw();
            }
            Monitor.Exit(DM.DirectXLock);
        }
        /// <summary>
        /// Game loop
        /// </summary>
        public void Run() {
            //TODO: Add engine logic here
            long tickWait = 1000L / 60L; //Milliseconds per tick
            Stopwatch hiResTimer = new Stopwatch();
            TickProfiler profiler = new TickProfiler();
            hiResTimer.Start();
            long lastTick = 0;
            while (!shouldTerminate) {
                profiler.StartTick();
                CheckInputs();
                Update();
                Render();
                
                if (hiResTimer.ElapsedMilliseconds - lastTick > tickWait) {
                    Console.WriteLine("Tick Lag: " + (hiResTimer.ElapsedMilliseconds - tickWait) + " ms");
                }
                //Wait patiently to perform the next tick
                while (hiResTimer.ElapsedMilliseconds - lastTick < tickWait) {
                    Thread.Yield();
                }
                long tickTime = profiler.StopTick();
                Console.WriteLine("Tick Time: " + tickTime);
                FPS.Add(tickTime);
                lastTick = hiResTimer.ElapsedMilliseconds;
                Console.WriteLine("FPS: " + profiler.FPS);
            }
            Terminate();
        }
        /// <summary>
        /// Terminates the game
        /// </summary>
        public void Terminate() {
            //TODO: Destroy and clean up game objects
            //Release all COM objects
            DM.Dispose();
            Environment.Exit(0);
        }
    }
}
