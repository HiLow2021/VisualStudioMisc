using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace DeviceSearcher
{
    public static class DeviceSearcher
    {
        public static IList<DeviceInfo> GetDeviceInfos()
        {
            var queryString = @"Select * From Win32_PnPEntity";

            using (var searcher = new ManagementObjectSearcher(queryString))
            using (var collection = searcher.Get())
            {
                var deviceInfos = new List<DeviceInfo>();

                foreach (var baseObject in collection)
                {
                    var deviceInfo = new DeviceInfo()
                    {
                        PNPClass = GetPropertyValue(baseObject, "PNPClass"),
                        DeviceID = GetPropertyValue(baseObject, "DeviceID"),
                        PnpDeviceID = GetPropertyValue(baseObject, "PNPDeviceID"),
                        Description = GetPropertyValue(baseObject, "Description"),
                        Caption = GetPropertyValue(baseObject, "Caption")
                    };

                    deviceInfos.Add(deviceInfo);
                }

                return deviceInfos;
            }

            string GetPropertyValue(ManagementBaseObject baseObject, string propertyName)
            {
                return (string)baseObject.GetPropertyValue(propertyName) ?? string.Empty;
            }
        }

        public static IList<DeviceInfo> GetUSBDevices()
        {
            return FilterByClass(GetDeviceInfos(), "usb");
        }

        public static IList<DeviceInfo> GetWebCameras()
        {
            return FilterByClass(GetDeviceInfos(), "camera");
        }

        public static IList<DeviceInfo> GetSerialPorts()
        {
            return FilterByClass(GetDeviceInfos(), "ports");
        }

        private static IList<DeviceInfo> FilterByClass(IList<DeviceInfo> deviceInfos, string pnpClassName)
        {
            return deviceInfos.Where(x => x.PNPClass.Equals(pnpClassName, StringComparison.OrdinalIgnoreCase)).ToArray();
        }
    }
}
