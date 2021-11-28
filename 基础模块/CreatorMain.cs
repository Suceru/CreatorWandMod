using Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public static class CreatorMain
    {
        public static List<Point3> Position;
        public static string version = "2.2.10.L.31B";
        //75shiK
        public static int Numversion = 922117620;
        public static int[] Sumnumversion = new int[] { 902021003, 902021011, 902021002, 902021012, 902021013, 922107401, 922107502, 922107504, 922117620 };
        public static int[] Donotnumversion = new int[] { 922107501, 902021003, 902021011, 902021002, 902021012, 902021013, 922107401 };
        public static string password = "456321";
        public static bool canUse = true;
        public static bool professional = false;
        //得到公共存储库的目录，然后并上一个cache
        public static readonly string CacheDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Cache");/* Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Cache";*/
        private static string sdcard;
        public static readonly string Export_wMod_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "wMod");
        public static readonly string Export_oMod_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "oMod");
        public static readonly string Export_CopyFile_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "Copy");
        public static readonly string Export_OnekeyFile_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "OneKey");
        //一键文件在cache里面的cachefile.od
        public static readonly string OneKeyFile = System.IO.Path.Combine(CreatorMain.CacheDirectory, "CacheFile.od");
        //复制文件在cache里面的cachefile.cd
        public static readonly string CopyFile = System.IO.Path.Combine(CreatorMain.CacheDirectory, "CacheFile.cd");
        //特殊复制文件在cache里面的cachefile.sd
        public static readonly string SpecialCopyFile = System.IO.Path.Combine(CreatorMain.CacheDirectory, "CacheFile.sd");
        public static readonly string Export_ModFile_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "Mod");
        public static string SCLanguage_Directory = System.IO.Path.Combine(CreatorMain.Sdcard, "Mod", "CreatorAPI" + version + ".xml");
        private static string ROSversion;
        public static bool Penetrate = false;
        public static List<int> PenetrateBlocksID = new List<int>();
        public static bool LightWorld = false;
        public static List<int> LightWorldBlockID = new List<int>();
        public static Language Language;

        public static Language Language_EnumFormString(string String)
        {
            var Enum_a = Enum.GetValues(typeof(Language));
            foreach (var Enum_b in Enum_a)
            {
                if (Language.GetName(typeof(Language), Enum_b) == String)
                {
                    return (Language)System.Enum.ToObject(typeof(Language), Enum_b);
                }
            }
            return Language.zh_CN;
        }
        public static string Display_Key_Dialog(string Key)
        {
            foreach (var e in CreatorAPI.CreatorDisplayDataDialog)
            {
                if (e.Attribute("Text").Value == Key)
                {
                    var replacement = e.Attribute("DisplayName").Value.Replace("\\n", "\n");
                    return replacement;
                }
            }

            return Key;

        }
        public static string Display_Key_UI(string Language, string Name, string Key)
        {
            IEnumerable<XElement> f;
            foreach (var e in CreatorAPI.CreatorDisplayDataUI)
            {
                if (e.Name.ToString() == Name.ToString())
                {
                    f = e.Elements("CreatorDisplayData").Where(xe => xe.Attribute("Language").Value == Language);
                    foreach (var h in f)
                    {
                        if (h.Attribute("Text").Value == Key)
                        {
                            var replacement = h.Attribute("DisplayName").Value.Replace("\\n", "\n");
                            return replacement;
                        }
                    }

                    break;
                }
            }

            return Key;

        }
        public static string Path_str(string path1, string path2)
        {
            return path1 + "/" + path2;
        }
        public static string Sdcard
        {
            get
            {
                if (CreatorMain.sdcard == null)
                {
                    string str1;



                    switch (Environment.OSVersion.Platform.ToString())
                    {
                        case "Windows":
                        case "windows":
                            str1 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "CreatorMod");
                            break;
                        case "Android":
                        case "android":
                        case "Unix":
                        case "unix":
                            str1 = System.IO.Path.Combine(Storage.GetSystemPath("config:/"));
                            break;
                        default:
                            str1 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "CreatorMod");
                            break;

                    }


                    CreatorMain.sdcard = str1;
                }
                return CreatorMain.sdcard;
            }
        }
        public static string Language_Directory
        {
            get
            {
                string str11;
                switch (Environment.OSVersion.Platform.ToString())
                {
                    case "Windows":
                    case "windows":
                        str11 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ModSettings.xml");
                        /*str11 = Directory.GetCurrentDirectory() + "/ModSettings.xml";*//*Environment.CurrentDirectory + "/CreatorMod";*/
                        break;
                    case "Android":
                    case "android":
                    case "Unix":
                    case "unix":
                        //  str1 = Window.Activity.GetExternalFilesDir((string)null).AbsolutePath;

                        //  str1 = Window.Activity.GetExternalFilesDir((string)null).AbsolutePath;
                        str11 = System.IO.Path.Combine(Storage.GetSystemPath("config:/"), "ModSettings.xml");
                        /* str11  = Storage.GetSystemPath("config:/")+ "ModSettings.xml";*/
                        break;
                    default:
                        str11 = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ModSettings.xml");
                        break;

                }


                CreatorMain.ROSversion = str11;
                return ROSversion;
            }
        }
        public enum CreateType
        {
            X,
            Y,
            Z,
        }

        public static class Math
        {
            //开始到结束这个是起初没有的内容
            public static void StarttoEnd(ref Point3 Start, ref Point3 End)
            {
                if (Start.X > End.X)
                {
                    int x = Start.X;
                    Start.X = End.X;
                    End.X = x;
                }
                if (Start.Y > End.Y)
                {
                    int y = Start.Y;
                    Start.Y = End.Y;
                    End.Y = y;
                }
                //<=返回
                if (Start.Z <= End.Z)
                {
                    return;
                }

                int z = Start.Z;
                Start.Z = End.Z;
                End.Z = z;
            }
            public static void StartEnd(ref Point3 Start, ref Point3 End)
            {
                if (Start.X < End.X)
                {
                    int x = Start.X;
                    Start.X = End.X;
                    End.X = x;
                }
                if (Start.Y < End.Y)
                {
                    int y = Start.Y;
                    Start.Y = End.Y;
                    End.Y = y;
                }
                if (Start.Z >= End.Z)
                {
                    return;
                }

                int z = Start.Z;
                Start.Z = End.Z;
                End.Z = z;
            }
        }
    }
}
