using System;
using System.Collections.Generic;
using System.Text;

namespace WebStudy.Hubs
{
    public static class NotificationMessageType
    {
        public static string Receive { get; } = nameof(Receive);
        public static string Register { get; } = nameof(Register);
        public static string Send { get; } = nameof(Send);
    }
}
