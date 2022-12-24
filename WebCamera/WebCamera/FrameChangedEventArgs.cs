using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCamera
{
    public class FrameChangedEventArgs:EventArgs
    {
        public Bitmap Frame { get; }

        public FrameChangedEventArgs(Bitmap frame)
        {
            Frame = frame;
        }
    }
}
