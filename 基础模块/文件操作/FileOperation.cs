// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.FileOperation
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*文件操作*/
/*namespace CreatorModAPI-=  public static class FileOperation*/
using System;
using System.IO;

namespace CreatorModAPI
{
    public static class FileOperation
    {
        public static bool IsFileInUse(this string file)
        {
            bool flag = true;
            try
            {
                new FileStream(file, FileMode.Open).Dispose();
                flag = false;
            }
            catch
            {
            }
            return flag;
        }

        public static Stream CreateStream(this Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            memoryStream.Position = 0L;
            stream.Dispose();
            return memoryStream;
        }

        public static bool Delete(this string directory)
        {
            if (File.Exists(directory))
            {
                if (directory.IsFileInUse())
                {
                    return false;
                }

                File.Delete(directory);
                return true;
            }
            foreach (string directory1 in Directory.GetDirectories(directory))
            {
                directory1.Delete();
            }

            foreach (string file in Directory.GetFiles(directory))
            {
                if (file.IsFileInUse())
                {
                    return false;
                }

                File.Delete(file);
            }
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory);
            }

            return true;
        }

        public static FileStream CreateFile(this string file)
        {
            try
            {
                return new FileStream(file, FileMode.Create);
            }
            catch (DirectoryNotFoundException ex)
            {
                string[] strArray = file.Split('/', (char)StringSplitOptions.None);
                string path = "";
                for (int index = 0; index < strArray.Length - 1; ++index)
                {
                    if (index == strArray.Length - 1)
                    {
                        path += strArray[index];
                        break;
                    }
                    path = path + strArray[index] + "/";
                }
                Directory.CreateDirectory(path);
                if (ex == null) { };
                return new FileStream(file, FileMode.Create);

            }
        }
    }
}
