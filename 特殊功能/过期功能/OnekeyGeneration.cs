// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.OnekeyGeneration
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*一键界面*/
/*namespace CreatorModAPI-=  public static class OnekeyGeneration*/
using Engine;
using Engine.Serialization;
using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreatorModAPI
{
    public static class OnekeyGeneration
    {
        public static void CreateOnekey(
          CreatorAPI creatorAPI,
          string directory,
          string path,
          Point3 Start,
          Point3 End,
          Point3 position)
        {
            int num = 0;
            CreatorMain.Math.StartEnd(ref Start, ref End);
            FileStream fileStream = new FileStream(directory + "/" + path, FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream, true);
            engineBinaryWriter.Write(End.X - position.X);
            engineBinaryWriter.Write(End.Y - position.Y);
            engineBinaryWriter.Write(End.Z - position.Z);
            engineBinaryWriter.Write(Start.X - position.X);
            engineBinaryWriter.Write(Start.Y - position.Y);
            engineBinaryWriter.Write(Start.Z - position.Z);
            for (int x = End.X; x <= Start.X; ++x)
            {
                for (int y = End.Y; y <= Start.Y; ++y)
                {
                    for (int z = End.Z; z <= Start.Z; ++z)
                    {
                        engineBinaryWriter.Write(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z));
                        ++num;
                    }
                }
            }
            engineBinaryWriter.Dispose();
            fileStream.Dispose();
            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("okeydialogs"), num), Color.Yellow, true, true);

            //creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("创建一键生成成功，共{0}个方块", (object)num), Color.Yellow, true, true);
        }

        public static void GenerationData(CreatorAPI creatorAPI, string path, Point3 position)
        {
            ChunkData chunkData = new ChunkData(creatorAPI);
            creatorAPI.revokeData = new ChunkData(creatorAPI);
            _ = creatorAPI.componentMiner.ComponentPlayer;
            Stream stream = new FileStream(path, FileMode.Open).CreateStream();
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream);
            int num1 = 0;
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            int num4 = engineBinaryReader.ReadInt32();
            int num5 = engineBinaryReader.ReadInt32();
            int num6 = engineBinaryReader.ReadInt32();
            int num7 = engineBinaryReader.ReadInt32();
            for (int index1 = num2; index1 <= num5; ++index1)
            {
                for (int index2 = num3; index2 <= num6; ++index2)
                {
                    for (int index3 = num4; index3 <= num7; ++index3)
                    {
                        if (!creatorAPI.launch)
                        {
                            return;
                        }

                        int num8 = engineBinaryReader.ReadInt32();
                        if (creatorAPI.AirIdentify || Terrain.ExtractContents(num8) != 0)
                        {
                            creatorAPI.CreateBlock(position.X + index1, position.Y + index2, position.Z + index3, num8, chunkData);
                            ++num1;
                        }
                    }
                }
            }
            engineBinaryReader.Dispose();
            stream.Dispose();
            chunkData.Render();
            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("okeydialogs2"), num1), Color.Yellow, true, true);
            // creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共{0}个方块", (object)num1), Color.Yellow, true, true);
        }

        public static void ExportOnekeyoMod2(string path, string exportFile)
        {
            if (path.IsFileInUse())
            {
                throw new FileLoadException("capadialogex");
            }
            // throw new FileLoadException("文件被占用");
            Stream stream1 = new FileStream(exportFile, FileMode.Create);
            Stream stream2 = File.Open(path, FileMode.Open);
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream2);
            string str = "";
            int num1 = engineBinaryReader.ReadInt32();
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            int num4 = engineBinaryReader.ReadInt32();
            int num5 = engineBinaryReader.ReadInt32();
            int num6 = engineBinaryReader.ReadInt32();
            string s = str + string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}", num1, num2, num3, num4, num5, num6);
            for (int index1 = num1; index1 <= num4; ++index1)
            {
                for (int index2 = num2; index2 <= num5; ++index2)
                {
                    for (int index3 = num3; index3 <= num6; ++index3)
                    {
                        s += string.Format("\n{0}", engineBinaryReader.ReadInt32());
                    }
                }
            }
            stream1.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
            engineBinaryReader.Dispose();
            stream2.Dispose();
            stream1.Dispose();
        }

        public static void ImportOnekeyoMod2(string path, string importFile)
        {
            _ = new List<string>();
            FileStream fileStream1 = File.OpenRead(importFile);
            StreamReader streamReader = new StreamReader(fileStream1);
            string end = streamReader.ReadToEnd();
            streamReader.Dispose();
            fileStream1.Dispose();
            FileStream fileStream2 = new FileStream(path, FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream2, true);
            string str = end;
            char[] separator = new char[1] { '\n' };
            foreach (string s in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                engineBinaryWriter.Write(int.Parse(s));
            }

            engineBinaryWriter.Dispose();
            fileStream2.Dispose();
        }

        public static void ExportOnekeyoMod(string path, string exportFile)
        {
            if (path.IsFileInUse())
            {
                throw new FileLoadException("capadialogex");
            }
            //throw new FileLoadException("文件被占用");
            Stream stream1 = new FileStream(exportFile, FileMode.Create);
            Stream stream2 = File.Open(path, FileMode.Open);
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream2);
            string s = "";
            int num1 = engineBinaryReader.ReadInt32();
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            int num4 = engineBinaryReader.ReadInt32();
            int num5 = engineBinaryReader.ReadInt32();
            int num6 = engineBinaryReader.ReadInt32();
            for (int index1 = num1; index1 <= num4; ++index1)
            {
                for (int index2 = num2; index2 <= num5; ++index2)
                {
                    for (int index3 = num3; index3 <= num6; ++index3)
                    {
                        int num7 = engineBinaryReader.ReadInt32();
                        if (Terrain.ExtractContents(num7) != 0)
                        {
                            if (index1 == num4 && index2 == num5 && index3 == num6)
                            {
                                s += string.Format("{0},{1},{2},{3}\n", index1, index2, index3, num7);
                            }
                            else
                            {
                                s += string.Format("{0},{1},{2},{3}\n", index1, index2, index3, num7);
                            }
                        }
                    }
                }
            }
            stream1.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
            engineBinaryReader.Dispose();
            stream2.Dispose();
            stream1.Dispose();
        }
    }
}
