using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WateringSetting;

public static class Logger
{
    static Logger()
    {
        var _fileName= Path.Combine(FileSystem.AppDataDirectory,Utilities.LogFileName, getTime());
        writer = new(File.OpenWrite(_fileName));
    }
    public static string getTime()
    {
        var time= DateTime.Now;
        return $"{time:yyyy}-{time:MM}-{time:dd}_{time:HH}-{time:mm}-{time:ss}";
    }
    private static StreamWriter writer ;
    private  static void writeTextFile(in string text)
    {
        writer.WriteLineAsync(text);
    }
    public static void save()
    {
        writer.FlushAsync();
    }
    //--------------Logger-----------------
    private static LinkedList<Message> Messages = new();
    public static void Info(in string text)
    {
        Log(text, Level.Info);
    }
    public static void Warn(in string text)
    {
        Log(text, Level.Warn);
    }
    public static void Error(in string text)
    {
        Log(text, Level.Error);
    }
    public static void Fatal(in string text)
    {
        Log(text, Level.Fatal);
    }
    public static void Log(in string text,in Level level)
    {
        if(level == Level.Fatal)
        {
            writer.Close();
        }
        var msg = new Message(level, text);
        Messages.AddLast(msg);
        writeTextFile(msg.ToString());
    }


}
public enum Level {Info,Warn,Error,Fatal}
public readonly record struct Message
{
    public readonly DateTime Time = DateTime.Now;
    public readonly Level Level;
    public readonly string Text;
    public readonly string toString;
    public Message(in Level level, in string text)
    {
        this.Level = level;
        this.Text = text;
        toString = $"<{Level}>@{Time:G}->{Text}";
    }
    public override string ToString()
    {
        return toString;
    }
}