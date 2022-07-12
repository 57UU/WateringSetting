using System.Globalization;
using System.Reflection;
using System.Text;

namespace WateringSetting;
public static  class Language
{
    public static string languageType = "None";
    //due to reflection used in English, all varible must be"a_b_c" struct
    public static string devices_Connected, back, next, parameter_Config, iP_Address,pressure,port,
        about,setting,clean_cache,aPP_setting,advanced_option,connect,auto_scan,use_senser,rotate,horizon,vertical,
        successful,faild,cache_cleaned,great,changes_need_restarting_to_apply,it_seems_you_havenot_connect_device_yet,
        device_setting,new_name,input,change_name,name,status, operation,delete,confirm,following_devices_will_be_deleted,
        unsupported;
    static Language()
    {
        english();
        string setLanguage = Utilities.readValue(Utilities.LanguageKey, "defaultLanguage");
        Utilities.tryDo(() =>  typeof(Language).GetMethod(setLanguage).Invoke(null, null));
           
    }
    public static void defaultLanguage()
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        if (cultureInfo.Name.Contains("en"))
        {
            //english
            //default is english
        }
        else if (cultureInfo.Name.Contains("zh"))
        {
            //chinese
            chinese();
        }
        else if (cultureInfo.Name.Contains("ja"))
        {
            //japanese
            japanese();
        }
        else
        {
            //other language isn't supported
            //use english instead
        }
    }
    public static void english() {
        foreach (FieldInfo fi in typeof(Language).GetFields(BindingFlags.Static | BindingFlags.Public))
        {
            string varibleName = fi.Name;
            StringBuilder sb = new();
            foreach (string i in varibleName.Split('_'))
            {
                sb.Append(char.ToUpper(i[0]));
                sb.Append(i.Substring(1));
                sb.Append(" ");
            }
            fi.SetValue(null, sb.ToString());

        }
        languageType = "english";
        changes_need_restarting_to_apply= "⚠"+changes_need_restarting_to_apply;
        aPP_setting += "⚙🔧";
    }
    public static void chinese()
    {
        devices_Connected = "已连接到的设备"; back = "返回"; next = "下一步"; parameter_Config = "参数设置";
        iP_Address = "IP地址"; pressure = "压力"; port = "端口"; about = "关于"; setting = "设置"; clean_cache = "清除缓存";
        aPP_setting = "APP设置⚙🔧"; advanced_option = "高级设置"; connect = "连接"; auto_scan = "自动扫描中";
        use_senser = "使用传感器"; rotate = "旋转"; horizon = "水平"; vertical = "垂直"; successful = "成功了";
        faild = "失败了"; cache_cleaned = "缓存已被清除"; great = "真是太棒了";changes_need_restarting_to_apply = "⚠重启应用以实现此项更改";
        languageType = "Chinese";it_seems_you_havenot_connect_device_yet = "似乎你还没有连接过设备";device_setting = "设备设置";
        new_name = "新名字";input = "输入";change_name = "更改名字";name = "名字";operation = "操作"; status = "状态";delete = "删除";
        confirm="三思而后行";following_devices_will_be_deleted = "以下设备将被删除";unsupported = "不受支持";
    }
    public static void japanese()
    {
        devices_Connected = "接続されているデバイス";advanced_option = "詳細設定";
        changes_need_restarting_to_apply = "この変更を実行するためのアプリケーションの再起動";languageType = "japanese";
    }
    public static void russian()
    {
        devices_Connected = "подключенное устройство";advanced_option = "Дополнительные настройки";languageType = "russian";
        
    }
    public static void miqimiaomiao()
    {
        chinese();
        devices_Connected = "已连接到的智障"; back = "返回"; next = "下一步"; parameter_Config = "傻逼设置";
        iP_Address = "智障地址"; pressure = "压力"; port = "端口"; about = "智障"; setting = "傻逼"; clean_cache = "缓存滚开";
        aPP_setting = "智障设置⚙🔧"; advanced_option = "高级设置"; connect = "连接"; auto_scan = "自动扫描中";
        use_senser = "使用传感器"; rotate = "旋转"; horizon = "水平"; vertical = "垂直"; successful = "成功了";
        faild = "傻逼了"; cache_cleaned = "缓存已滚开"; great = "真是太傻逼了"; changes_need_restarting_to_apply = "⚠重启傻逼以实现此项更改";
        languageType = "Chinese";
    }

}
