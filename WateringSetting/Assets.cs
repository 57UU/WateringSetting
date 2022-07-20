global using WateringSetting.PopupContent;


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace WateringSetting;

internal static class Assets
{
    /// <summary>
    /// store the connected devices,Tips:invoke refreshList after changes
    /// </summary>
    //public static IList<SmartDeviceInfo> devices = new List<SmartDeviceInfo>();
    public static LinkedList<string> deviceInfosRank = new();
    public static Dictionary<string,SingleDevice> devices = new();
    public static ThreadStart refreshList;
    public static bool addDevice(in SmartDeviceInfo smartDevice)
    {
        if (devices.ContainsKey(smartDevice.name))
            return false;
        var DeviceGUI = new SingleDevice(smartDevice);
        devices.Add(smartDevice.name, DeviceGUI);
        deviceInfosRank.AddLast(smartDevice.name);
        return true;
    }
    public static List<SmartDeviceInfo> beingdeletedDevices=new();

    static Assets()
    {
        
    }


}
public class SmartDeviceInfo
{
    public string ip { set; get; }
    public int port { set; get; }
    public string name { set; get; }
    public DeviceStatus status { set; get; }
    public float horizonRotation=0;
    public float verticalRotation=0;
    public float pressure = 0;
    public override string ToString()
    {
        return name;
    }

}
public enum DeviceStatus {online,offine,unknown,detecting }