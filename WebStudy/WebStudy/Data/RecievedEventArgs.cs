using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStudy.Data
{
    public class RecievedEventArgs : EventArgs
    {
        public string UserName { get; }
        public string Message { get; }

        public RecievedEventArgs(string userName, string message)
        {
            UserName = userName;
            Message = message;
        }
    }
}
