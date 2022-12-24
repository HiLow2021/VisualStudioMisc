using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceSearcher
{
    public class DeviceInfo
    {
        public string PNPClass { get; internal set; }
        public string DeviceID { get; internal set; }
        public string PnpDeviceID { get; internal set; }
        public string Description { get; internal set; }
        public string Caption { get; internal set; }
    }
}
