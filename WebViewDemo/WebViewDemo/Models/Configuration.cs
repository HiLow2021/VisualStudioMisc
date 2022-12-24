using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WebViewDemo.Models
{
    public class Configuration : BindableBase
    {
        private double _Left;
        public double Left
        {
            get { return _Left; }
            set { SetProperty(ref _Left, value); }
        }

        private double _Top;
        public double Top
        {
            get { return _Top; }
            set { SetProperty(ref _Top, value); }
        }

        private double _Width;
        public double Width
        {
            get { return _Width; }
            set { SetProperty(ref _Width, value); }
        }

        private double _Height;
        public double Height
        {
            get { return _Height; }
            set { SetProperty(ref _Height, value); }
        }

        private bool _TopMost;
        public bool TopMost
        {
            get { return _TopMost; }
            set
            {
                SetProperty(ref _TopMost, value);
                PinOpacity = value ? 1 : 0.35;
            }
        }

        private double _PinOpacity = 0.35;
        public double PinOpacity
        {
            get { return _PinOpacity; }
            set { SetProperty(ref _PinOpacity, value); }
        }

        private bool _IsSaveWindowPosition;
        public bool IsSaveWindowPosition
        {
            get { return _IsSaveWindowPosition; }
            set { SetProperty(ref _IsSaveWindowPosition, value); }
        }

        private bool _IsSaveWindowSize;
        public bool IsSaveWindowSize
        {
            get { return _IsSaveWindowSize; }
            set { SetProperty(ref _IsSaveWindowSize, value); }
        }
    }
}
