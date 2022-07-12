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
    public static Dictionary<string,SingleDevice> devices = new();
    public static ThreadStart refreshList;
    public static bool addDevice(in SmartDeviceInfo smartDevice)
    {
        if (devices.ContainsKey(smartDevice.name))
            return false;
        devices.Add(smartDevice.name,new SingleDevice( smartDevice));
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
    public string status { set; get; }
    public string horizonRotation { set; get; }
    public string verticalRotation { set; get; }
    public string prussure { set; get; }
    public override string ToString()
    {
        return name;
    }

}
public enum DeviceStatus {online,offine,unknown,detecting }