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

namespace MonsterRPGEngine {
    /// <summary>
    /// Represents implementation of the game
    /// </summary>
    class Engine {
        public static RenderForm gameWindow;

        private static D3D11Device device;
        private static SwapChain swapChain;
        private static RenderTarget renderTarget;
        private static Boolean ShouldDisplayFPS = true; //Placeholder until FPS algorithm is added

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
            Thread.CurrentThread.Name = "EngineThread";
            Thread UIThread = new Thread(() => {
                gameWindow = new RenderForm("MonsterRPGEngine");
                gameWindow.FormClosed += (o, e) => {
                    Terminate();
                };
                //TODO: Learn how to initialize DirectX components
                //Initialize here because WinForms wont let us do it outside UI thread
                //Describing our swap chain allows us to tell DirectX exactly how we want to render and transition each frame
                SwapChainDescription swapChainDesc = new SwapChainDescription() {
                    BufferCount = 2, //Double buffer
                    Usage = Usage.RenderTargetOutput, //We're using this swap chain as a RenderTarget
                    OutputHandle = gameWindow.Handle, //Pass handle of our game window
                    IsWindowed = true, //Yep
                    ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.B8G8R8A8_UNorm), //Automatic resolution resizing, Refresh rate, Pixel color format
                    SampleDescription = new SampleDescription(1, 0), //No Multisampling
                    Flags = SwapChainFlags.AllowModeSwitch, //Allow us to switch modes
                    SwapEffect = SwapEffect.Discard //Assuming this means buffer will be cleared when it is moved offscreen
                };
                //We need to create a Direct3D11 device because we can't create a swap chain for Direct2D1 without it. Direct2D1 has no Device object.
                D3D11Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport, swapChainDesc, out device, out swapChain);
                Surface backbuffer = Surface.FromSwapChain(swapChain, 0); //Grab the backbuffer to use as a RenderTarget
                //Need a Direct2D1 factory just to make a new RenderTarget
                D2D1Factory factory = new D2D1Factory(FactoryType.SingleThreaded, DebugLevel.Error);
                Size2F dpi = factory.DesktopDpi;
                renderTarget = new RenderTarget(factory, backbuffer, new RenderTargetProperties() {
                    DpiX = dpi.Width, //Horizontal DPI
                    DpiY = dpi.Height, //Vertical DPI
                    MinLevel = FeatureLevel.Level_DEFAULT, //Just make sure we are using a hardware accelerated render target
                    PixelFormat = new PixelFormat(Format.Unknown, AlphaMode.Ignore), //Who cares? our swap chain already knows this stuff
                    Type = RenderTargetType.Default, //Again swap chain has this
                    Usage = RenderTargetUsage.None //Yeah that swap chain though
                });
                //Wow that was a handful. Now we can start our message loop on this thread.
                gameWindow.Show();
                Application.Run(gameWindow);
            });
            UIThread.SetApartmentState(ApartmentState.STA);
            UIThread.Name = "UIThread";
            UIThread.Start();
            //TODO: Replace with cleaner multithreading code. This is ugly
            while (gameWindow == null || renderTarget == null) { }
            
            
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
            Console.Clear();
            Console.WriteLine("Game Time: " + gameTime);
            //Begin Draw
            renderTarget.BeginDraw();
            renderTarget.Clear(Color.CornflowerBlue.ToColor4()); //Clear using a common game clear color
            
            //End Draw
            renderTarget.EndDraw();
            //Swap Backbuffer
            swapChain.Present(0, PresentFlags.None);
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
                if (lastTick.ElapsedMilliseconds > tickFrequency) {
                    Console.WriteLine("Tick Lag: " + (lastTick.ElapsedMilliseconds - tickFrequency) + " ms");
                }
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
            //Release all COM objects
            device.Dispose();
            swapChain.Dispose();
            renderTarget.Dispose();
            Environment.Exit(0);
        }
    }
}
