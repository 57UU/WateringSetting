using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text.Json;
using System.Numerics;

namespace WateringSetting;

internal static class Utilities
{
    static Utilities()
    {
        
    }
    /// <summary>
    /// due to some async oparetions(pop up window),a button could be touched for more than one time. 
    /// </summary>
    /// <param name="view">is should be type'View'</param>
    public static async void freezeControl(object view)
    {
        View control = (View)view;
        control.IsEnabled = false;
        await Task.Delay(700);
        control.IsEnabled = true;  
    }


    //Keys
    public static string LanguageKey = "language";
    public static string DevicesFileName = "devices";

    public static void StoreValue(string key,object value)
    {
        tryDo(() => { Preferences.Default.Set(key, value);});
        
    }
    public static T readValue<T>(string key,T defaultValue)
    {
        T value=defaultValue;
        tryDo(()=>value= Preferences.Default.Get(key, defaultValue));
        return value;
    }
    public static void tryDo(in ThreadStart start)
    {
        try
        {
            start();
        }
        catch (Exception e)
        {
            popupMessage($"Error:{e.Message}"); 
        }
    }
    public static void tryDo(ThreadStart start, in string errorMessage)
    {
        try
        {
            start();
        }
        catch (Exception e)
        {
            popupMessage(errorMessage);
        }

    }
    public  static void popupMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;
        var toast = Toast.Make(text, duration, fontSize);
        toast.Show(cancellationTokenSource.Token);
    }
    public async static void readDevices()
    {
        try
        {
            string value = await Utilities.readTextFile(Utilities.DevicesFileName);

            var devicesDict = JsonSerializer.Deserialize<Dictionary<string, SmartDeviceInfo>>(value);
            if (devicesDict != null)
            {
                Dictionary<string, SingleDevice> GUIDict = new();
                foreach (var i in devicesDict)
                {
                    GUIDict.Add(i.Key, new SingleDevice(i.Value));
                }
                devicesDict = null;

                Assets.devices = GUIDict;
                Assets.refreshList();
            }
        }catch(Exception e)
        {
            popupMessage(e.ToString());
        }

    }
    public static void writeDevices()
    {
        tryDo(() =>
        {
            Dictionary<string, SmartDeviceInfo> pairs= new Dictionary<string, SmartDeviceInfo>();
            foreach (var i in Assets.devices)
            {
                pairs.Add(i.Key, i.Value.deviceInfo);
            }
            string jsonString = JsonSerializer.Serialize(pairs);
            pairs = null;
            writeTextFile(Utilities.DevicesFileName, jsonString);
        });
    }
    public  async static void timer(ThreadStart start,int milliseconds) {
        await Task.Delay(milliseconds);
        start();

    }
    public async static Task<string> readTextFile(string filePath)
    {
        Stream fileStream = File.OpenRead
            (Path.Combine(FileSystem.AppDataDirectory, filePath));
        using StreamReader reader = new StreamReader(fileStream);
        var t= await reader.ReadToEndAsync();
        reader.Close();
        fileStream.Close();
        return t;
    }
    public static void writeTextFile(in string filePath,in string text)
    {

        /*        
                Stream fileStream = File.OpenWrite(Path.Combine(FileSystem.AppDataDirectory, filePath));

                StreamWriter writer= new(fileStream);

                await writer.WriteAsync(text);
                writer.Close();
                fileStream.Close();*/
        File.WriteAllTextAsync(Path.Combine(FileSystem.AppDataDirectory, filePath), text);
    }
    public static (float, float, float) transformQuaternion(in Quaternion quaternion)
    {
        float x,y,z;
        float sin = MathF.Sin(MathF.Acos(quaternion.W));
        x=quaternion.X/ sin;
        y=quaternion.Y/ sin;
        z=quaternion.Z/ sin;
        return (x,y,z);
    }



}
