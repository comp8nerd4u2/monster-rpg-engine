using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using D2D1Factory = SharpDX.Direct2D1.Factory;
using D3D11Device = SharpDX.Direct3D11.Device;


namespace MonsterRPGEngine.Drawing {
    class DirectManager : IDisposable {
        private Form displayWindow;

        public readonly object DirectXLock = new object();
        public D2D1Factory Factory { get; private set; }
        public WindowRenderTarget RenderTarget { get; private set; }

        /// <summary>
        /// Creates a new DirectManger and attaches to the display window
        /// </summary>
        /// <param name="displayWindow">Window to create DirectX resources for</param>
        public DirectManager(Form displayWindow) {
            this.displayWindow = displayWindow;
            displayWindow.Size = new System.Drawing.Size(800, 600);
            displayWindow.FormBorderStyle = FormBorderStyle.FixedSingle;
            displayWindow.MaximizeBox = false;
            Monitor.Enter(DirectXLock);
            Factory = new D2D1Factory(FactoryType.SingleThreaded, DebugLevel.Error); //Creates DirectX 2D1 resources for us
            Size2F dpi = Factory.DesktopDpi; //Get our DPI
            RenderTargetProperties renderTargetProperties = new RenderTargetProperties() {
                DpiX = dpi.Width,
                DpiY = dpi.Height,
                MinLevel = FeatureLevel.Level_DEFAULT, //We really dont need any of the advanced features
                PixelFormat = new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Unknown), //RGBA Pixel Format, Don't know if we should change this
                Type = RenderTargetType.Hardware, //Render using the hardware
                Usage = RenderTargetUsage.None //None of the usages suit the purpose
            };
            HwndRenderTargetProperties hwndRenderTargetProperties = new HwndRenderTargetProperties() {
                Hwnd = displayWindow.Handle,
                PixelSize = new Size2(800, 600),
                PresentOptions = PresentOptions.Immediately //Immediate mode graphics
            };
            RenderTarget = new WindowRenderTarget(Factory, renderTargetProperties, hwndRenderTargetProperties); //Use this to draw to the window
            Monitor.Exit(DirectXLock);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                Monitor.Enter(DirectXLock);
                RenderTarget.Dispose();
                Factory.Dispose();
                RenderTarget = null;
                Factory = null;
                Monitor.Exit(DirectXLock);
                displayWindow.Dispose();

                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DirectManager() {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
