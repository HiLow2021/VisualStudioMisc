using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using Prism.Mvvm;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WebCamera.WPF.ViewModels
{
    class WebCameraViewModel : BindableBase, IDisposable
    {
        private readonly Timer timer = new Timer();
        private readonly VideoCapture camera;

        private WriteableBitmap _Frame;
        public WriteableBitmap Frame
        {
            get { return _Frame; }
            set { SetProperty(ref _Frame, value); }
        }

        public WebCameraViewModel()
        {
            camera = new VideoCapture(1)
            {
                FrameWidth = 800,
                FrameHeight = 600,
                Fps = 30,
            };
            timer.Interval = 100;
            timer.Elapsed += (sender, e) =>
            {
                using (var frame = new Mat())
                {
                    camera.Read(frame); // Webカメラの読み取り（バッファに入るまでブロックされる)

                    Application.Current?.Dispatcher?.Invoke(new Action(() =>
                    {
                        if (!frame.Empty())
                        {
                            Frame = frame.ToWriteableBitmap();
                        }
                    }));
                }
            };

            timer.Start();
        }

        public void Dispose()
        {
            timer?.Stop();
            camera?.Dispose();
        }
    }
}
