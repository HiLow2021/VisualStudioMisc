using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WebViewDemo.Models
{
    public class WebInformation : BindableBase
    {
        private Uri _CurrentUri;
        public Uri CurrentUri
        {
            get { return _CurrentUri; }
            set { SetProperty(ref _CurrentUri, value); }
        }
        
        private string _CurrentContent;
        public string CurrentContent
        {
            get { return _CurrentContent; }
            set { SetProperty(ref _CurrentContent, value); }
        }
    }
}
