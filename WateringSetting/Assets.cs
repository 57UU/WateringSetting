using System.Text.Json.Serialization;

namespace WateringSetting;

internal static class Assets
{
    /// <summary>
    /// store the connected devices,Tips:invoke refreshList after changes
    /// </summary>
    //public static IList<SmartDeviceInfo> devices = new List<SmartDeviceInfo>();
    public static LinkedList<SmartDeviceInfo> deviceInfosRank = new();
    public static Dictionary<string,SingleDevice> devices = new();
    public static ThreadStart refreshList;
    public static bool addDevice(in SmartDeviceInfo smartDevice)
    {
        if (devices.ContainsKey(smartDevice.name))
            return false;
        var DeviceGUI = new SingleDevice(smartDevice);
        devices.Add(smartDevice.name, DeviceGUI);
        deviceInfosRank.AddLast(smartDevice);
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
    [JsonInclude]
    public float horizonRotation { set; get; }
    [JsonInclude]
    public float verticalRotation { set; get; }
    [JsonInclude]
    public float pressure { set; get; }
public override string ToString()
    {
        return name;
    }

}
public enum DeviceStatus {online,offine,unknown,detecting }