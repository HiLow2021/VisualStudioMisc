using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WebCamera
{
    public class WebCamera : IDisposable
    {
        private CancellationTokenSource tokenSource;
        private CancellationToken token;
        private VideoCapture camera;

        public bool IsOpened { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int Fps { get; private set; }

        public event EventHandler<FrameChangedEventArgs> FrameChanged;

        public void Open(int index, int frameWidth, int frameHeight, int fps)
        {
            if (IsOpened)
            {
                return;
            }

            camera = new VideoCapture(index)
            {
                FrameWidth = frameWidth,
                FrameHeight = frameHeight,
                Fps = fps,
            };
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Fps = fps;

            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            IsOpened = true;
            Task.Run(MainTaskAsync).ContinueWith(x =>
            {
                camera?.Dispose();
                IsOpened = false;
            });
        }

        public void Close()
        {
            if (!IsOpened)
            {
                return;
            }

            tokenSource?.Cancel();
            tokenSource?.Dispose();
        }

        public void Dispose()
        {
            Close();
        }

        private async Task MainTaskAsync()
        {
            while (!token.IsCancellationRequested)
            {
                using (var mat = new Mat())
                {
                    camera.Read(mat); // Webカメラの読み取り（バッファに入るまでブロックされる)

                    var frame = mat.Empty() ? null : mat.ToBitmap();

                    FrameChanged?.Invoke(this, new FrameChangedEventArgs(frame));
                }

                await Task.Delay(10, token);
            }
        }
    }
}
