using Engine;
using Engine.Graphics;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class CreatorAPI
    {
        public enum NumberPoint
        {
            One,
            Two,
            Three,
            Four
        }

        public enum OnekeyType
        {
            Tree,
            Build
        }

        public static Language Language;

        public static bool IsAddedToProject;

        public static IEnumerable<XElement> CreatorDisplayDataDialog;

        public static IEnumerable<XElement> CreatorDisplayDataUI;

        public bool oldMainWidget;

        public bool AirIdentify;

        public bool ClearBlock;

        public bool UnLimitedOfCreate;

        public bool RevokeSwitch = true;

        public ChunkData revokeData;

        public NumberPoint amountPoint = NumberPoint.Two;

        public NumberPoint numberPoint;

        public CreateBlockType CreateBlockType = CreateBlockType.Fast;

        public bool oneKeyGeneration;

        public OnekeyType onekeyType = OnekeyType.Build;

        public bool launch = true;

        public bool pasteRotate;

        public bool pasteLimit;

        public ComponentMiner componentMiner;

        public PrimitivesRenderer3D primitivesRenderer3D;

        public CreatorGenerationAlgorithm creatorGenerationAlgorithm;

        public List<Point3> Position
        {
            get;
            set;
        }

        public CreatorAPI(ComponentMiner componentMiner)
        {
            try
            {
                string a = ModsManager.Configs["Language"];
                if (!(a == "zh-CN"))
                {
                    if (a == "en-US")
                    {
                        Language = Language.en_US;
                    }
                    else
                    {
                        Language = Language.zh_CN;
                    }
                }
                else
                {
                    Language = Language.zh_CN;
                }

                XElement xElement = ContentManager.Get<XElement>("CreatorDisplay", (string)null);
                CreatorDisplayDataDialog = from xe in xElement.Element("CreatorDisplayDialog").Elements("CreatorDisplayData")
                                           where xe.Attribute("Language").Value == Language.ToString()
                                           select xe;
                CreatorDisplayDataUI = xElement.Element("CreatorDisplayUI").Elements();
                ContentManager.Dispose("CreatorDisplay");
            }
            catch (Exception ex)
            {
                Log.Write(LogType.Warning, "Failed to read language, need to restart\n" + ex.Message);
                Language = Language.en_US;
            }

            creatorGenerationAlgorithm = new CreatorGenerationAlgorithm();
            this.componentMiner = componentMiner;
            Position = new List<Point3>(4)
            {
                new Point3(0, -1, 0),
                new Point3(0, -1, 0),
                new Point3(0, -1, 0),
                new Point3(0, -1, 0)
            };
        }

        public void OnUse(TerrainRaycastResult terrainRaycastResult)
        {
            Point3 point = terrainRaycastResult.CellFace.Point;
            ComponentPlayer componentPlayer = componentMiner.ComponentPlayer;
            if (!OnTouch.Touch(this, point))
            {
                return;
            }

            int cellValue = GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValue(point.X, point.Y, point.Z);
            int num = Terrain.ExtractLight(cellValue);
            int num2 = Terrain.ExtractData(cellValue);
            int num3 = Terrain.ExtractContents(cellValue);
            if (BlocksManager.Blocks[num3] == null)
            {
                return;
            }

            if (numberPoint == NumberPoint.One)
            {
                Position[0] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint1"), point.X, point.Y, point.Z, num3, cellValue, num, num2, SetFaceAndRotate.GetFace(cellValue), SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(point.X, point.Z)), Color.White, blinking: true, playNotificationSound: false);
                }
                catch (Exception message)
                {
                    Log.Warning(message);
                }

                if (amountPoint == numberPoint)
                {
                    return;
                }

                numberPoint = NumberPoint.Two;
            }
            else if (numberPoint == NumberPoint.Two)
            {
                Position[1] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint2"), point.X, point.Y, point.Z, num3, cellValue, num, num2, SetFaceAndRotate.GetFace(cellValue), SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(point.X, point.Z)), Color.White, blinking: true, playNotificationSound: false);
                }
                catch (Exception message2)
                {
                    Log.Warning(message2);
                }

                SetFaceAndRotate.SetFace(cellValue, SetFaceAndRotate.GetFace(cellValue));
                if (amountPoint == numberPoint)
                {
                    numberPoint = NumberPoint.One;
                }
                else
                {
                    numberPoint = NumberPoint.Three;
                }
            }
            else if (numberPoint == NumberPoint.Three)
            {
                Position[2] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint3"), point.X, point.Y, point.Z, num3, cellValue, num, num2, SetFaceAndRotate.GetFace(cellValue), SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(point.X, point.Z)), Color.White, blinking: true, playNotificationSound: false);
                }
                catch (Exception message3)
                {
                    Log.Warning(message3);
                }

                if (amountPoint == numberPoint)
                {
                    numberPoint = NumberPoint.One;
                }
                else
                {
                    numberPoint = NumberPoint.Four;
                }
            }
            else
            {
                if (numberPoint != NumberPoint.Four)
                {
                    return;
                }

                Position[3] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint4"), point.X, point.Y, point.Z, num3, cellValue, num, num2, SetFaceAndRotate.GetFace(cellValue), SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(point.X, point.Z)), Color.White, blinking: true, playNotificationSound: false);
                }
                catch (Exception message4)
                {
                    Log.Warning(message4);
                }

                numberPoint = NumberPoint.One;
            }

            CreatorMain.Position = Position;
        }

        public void CreateBlock(int x, int y, int z, int value, ChunkData chunkData = null)
        {
            if (RevokeSwitch && revokeData != null && revokeData.GetChunk(x, y) == null)
            {
                revokeData.CreateChunk(x, y, unLimited: true);
            }

            switch (CreateBlockType)
            {
                case CreateBlockType.Normal:
                    GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(x, y, z, value);
                    break;
                case CreateBlockType.Fast:
                    SetBlock(x, y, z, value);
                    break;
                case CreateBlockType.Catch:
                    chunkData.SetBlock(x, y, z, value);
                    break;
            }
        }

        public void CreateBlock(Point3 point3, int value, ChunkData chunkData = null)
        {
            if (RevokeSwitch && revokeData != null && revokeData.GetChunk(point3.X, point3.Z) == null)
            {
                revokeData.CreateChunk(point3.X, point3.Z, unLimited: true);
            }

            switch (CreateBlockType)
            {
                case CreateBlockType.Normal:
                    GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(point3.X, point3.Y, point3.Z, value);
                    break;
                case CreateBlockType.Fast:
                    SetBlock(point3.X, point3.Y, point3.Z, value);
                    break;
                case CreateBlockType.Catch:
                    chunkData.SetBlock(point3, value);
                    break;
            }
        }

        public void SetBlock(int x, int y, int z, int value)
        {
            try
            {
                SubsystemTerrain subsystemTerrain = componentMiner.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
                if (!subsystemTerrain.Terrain.IsCellValid(x, y, z))
                {
                    return;
                }

                TerrainChunk terrainChunk = subsystemTerrain.Terrain.GetChunkAtCell(x, z);
                if (terrainChunk != null)
                {
                    goto IL_0071;
                }

                if (!UnLimitedOfCreate)
                {
                    return;
                }

                terrainChunk = subsystemTerrain.Terrain.AllocateChunk(x >> 4, z >> 4);
                while (terrainChunk.ThreadState < TerrainChunkState.Valid)
                {
                    subsystemTerrain.TerrainUpdater.UpdateChunkSingleStep(terrainChunk, 15);
                }

                goto IL_0071;
            IL_0071:
                terrainChunk.Cells[y + (x & 0xF) * 256 + (z & 0xF) * 256 * 16] = value;
                terrainChunk.ModificationCounter++;
                if (UnLimitedOfCreate)
                {
                    terrainChunk.State = TerrainChunkState.Valid;
                }

                if (terrainChunk.State > TerrainChunkState.InvalidLight)
                {
                    terrainChunk.State = TerrainChunkState.InvalidLight;
                }

                terrainChunk.WasDowngraded = true;
            }
            catch (Exception ex)
            {
                Log.Error(CreatorMain.Display_Key_Dialog("creatorAPIsetblock") + ex.Message);
            }
        }
    }
}

/*方块选择内容提示*/
/*namespace CreatorModAPI-=  public class CreatorAPI*//*
using Engine;
using Engine.Graphics;
using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public enum Language
    {
        zh_CN,
        en_US,
        ja_JP
    }
    public class CreatorAPI
    {
        public static Language Language;
        public static bool IsAddedToProject = false;
        // public static XElement CreatorDisplayData;
        public static IEnumerable<XElement> CreatorDisplayDataDialog;
        public static IEnumerable<XElement> CreatorDisplayDataUI;
        public bool oldMainWidget;
        public bool AirIdentify;
        public bool ClearBlock;
        public bool UnLimitedOfCreate;
        public bool RevokeSwitch = true;
        public ChunkData revokeData;
        public CreatorAPI.NumberPoint amountPoint = CreatorAPI.NumberPoint.Two;
        public CreatorAPI.NumberPoint numberPoint;
        public CreateBlockType CreateBlockType = CreateBlockType.Fast;
        public bool oneKeyGeneration;
        public CreatorAPI.OnekeyType onekeyType = CreatorAPI.OnekeyType.Build;
        public bool launch = true;
        public bool pasteRotate;
        public bool pasteLimit;
        public ComponentMiner componentMiner;
        public PrimitivesRenderer3D primitivesRenderer3D;
        public CreatorGenerationAlgorithm creatorGenerationAlgorithm;


        public List<Point3> Position { get; set; }



        public CreatorAPI(ComponentMiner componentMiner)
        {
            try
            {

                *//* Function abandonment
                 * 1.4
                 * is pass
                 * FileInfo fi = new FileInfo(CreatorMain.SCLanguage_Directory);
                switch (fi.Exists)
                {
                    default:
                        if (!fi.Directory.Exists)
                        {
                            fi.Directory.Create();

                        }
                        using (Stream stream_xml1 = new FileStream(CreatorMain.SCLanguage_Directory, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            XElement src = new XElement("CreatorAPI", new XElement("Language"));
                            src.Element("Language").SetAttributeValue("Language", Language.zh_CN.ToString());
                            src.Save(stream_xml1);
                        }
                        break;
                }
                 *Function abandonment
                 *1.33
                 *don't know it
                  using (Stream stream_xml = new FileStream(CreatorMain.Language_Directory, FileMode.Open, FileAccess.Read))
                {
                    XElement src = XElement.Load(stream_xml);
                    switch (int.Parse(src.Element("Set").Attribute("Value").Value))
                    {
                        case 0:
                            CreatorAPI.Language = Language.zh_CN;
                            break;
                        case 1:
                            CreatorAPI.Language = Language.en_US;
                            break;
                        case 2:
                            CreatorAPI.Language = Language.ot_OT;
                            break;
                        default:
                            CreatorAPI.Language = Language.ot_OT;
                            break;
                    };
                }
                 *//*
                //pass
                switch (ModsManager.APIVersion)
                {
                    case "1.40":
                        switch (ModsManager.Configs["Language"])
                        {
                            case "zh-CN":
                                CreatorAPI.Language = Language.zh_CN;
                                break;
                            case "en-US":
                                CreatorAPI.Language = Language.en_US;
                                break;
                            default:
                                CreatorAPI.Language = Language.zh_CN;
                                break;
                        };
                        break;
                    default:
                        CreatorAPI.Language = Language.en_US;
                }
                *//*Log.Information("beging+++++++++++++++++++++++++++++++++++++");
                try
                {

                    foreach (var item in ContentManager.List())
                    {
                        Log.Information(item.Filename);
                    }
                    Log.Information("end++++++++++++++++");

                }
                catch (Exception e)
                {

                    Log.Error(e);
                }*//*



                XElement element = ContentManager.Get<XElement>("CreatorDisplay");
                CreatorAPI.CreatorDisplayDataDialog = element.Element("CreatorDisplayDialog").Elements("CreatorDisplayData").Where(xe => xe.Attribute("Language").Value == CreatorAPI.Language.ToString());
                CreatorAPI.CreatorDisplayDataUI = element.Element("CreatorDisplayUI").Elements();
                ContentManager.Dispose("CreatorDisplay");

                *//* List<Tuple<string, Action>> tupleList = new List<Tuple<string, Action>>();
                 for (int i = (Enum.GetNames(typeof(Language)).GetLength(0)) - 1; i >= 0; i--)
                 {
                     int a = i;
                     *//*
                     if ((Language)System.Enum.ToObject(typeof(Language), i) == Language.ot_OT)
                     {
                         continue;
                     }*//*
                     //   DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", ((Language)System.Enum.ToObject(typeof(Language), i)).ToString(), "OK", null, null));
                     try
                     {
                         tupleList.Add(new Tuple<string, Action>(Language.GetName(typeof(Language), i).ToString(), () =>
                     {
                         CreatorAPI.Language = (Language)System.Enum.ToObject(typeof(Language), a);

                         using (Stream stream_xml1 = new FileStream(CreatorMain.SCLanguage_Directory, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                         {
                             XElement src = new XElement("CreatorAPI", new XElement("Language"));
                             src.Element("Language").SetAttributeValue("Language", Language.GetName(typeof(Language), a).ToString());
                             src.Save(stream_xml1);
                             //  stream_xml1.Flush();
                             //    DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", CreatorMain.SCLanguage_Directory + "\n" + src.ToString(), "OK", null, null));

                         }

                         //   DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", Language.ToString(), "OK", null, null));
                     }));
                     }
                     catch (Exception ex)
                     {
                         DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", "ERR:" + ex, "OK", null, null));

                     }
                 }*//*
                // DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new ListSelectionDialog("Choose a Language", (IEnumerable)tupleList, 60f, (Func<object, string>)(t => ((Tuple<string, Action>)t).Item1), (Action<object>)(t => ((Tuple<string, Action>)t).Item2())));

                *//*if (CreatorAPI.Language == Language.ot_OT)
                {
                    FileInfo fi = new FileInfo(CreatorMain.SCLanguage_Directory);
                    if (!fi.Directory.Exists)
                    {
                        fi.Directory.Create();
                    }

                    switch (File.Exists(CreatorMain.SCLanguage_Directory))
                    {
                        case true:
                            // DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("True", (Language)Enum.Parse(typeof(Language), src.Element("Language").Attribute("Language").Value), "OK", null, null));
                            using (Stream stream_xml2 = new FileStream(CreatorMain.SCLanguage_Directory, FileMode.Open, FileAccess.Read))
                            {
                                XElement src = XElement.Load(stream_xml2);
                                string cx = src.Element("Language").Attribute("Language").Value;
                                CreatorAPI.Language = CreatorMain.Language_EnumFormString(cx);
                                *//* var Enum_a = Enum.GetValues(typeof(Language));
                                 foreach (var Enum_b in Enum_a) {
                                     if (Language.GetName(typeof(Language), Enum_b) == cx) {
                                         CreatorAPI.Language = (Language)System.Enum.ToObject(typeof(Language), Enum_b);
                                         break;
                                     }
                                 }*//*
                                //    DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("True",Language.ToString(), "OK", null, null));
                                //  Enum.
                                // CreatorAPI.Language = (Language)Enum.Parse(typeof(Language), (src.Element("Language").Attribute("Language").Value).ToString());

                            }

                            break;
                        case false:
                            // DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("False", CreatorMain.SCLanguage_Directory, "OK", null, null));

                            DialogsManager.HideAllDialogs();
                            // CreatorAPI.IsAddedToProject = false;
                            DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new ListSelectionDialog("Choose a Language", tupleList, 60f, t => ((Tuple<string, Action>)t).Item1, t => ((Tuple<string, Action>)t).Item2()));

                            break;
                    }

                }*//*
                //CreatorAPI.Language = Language.zh_CN;
                *//*XElement element = ContentManager.Get<XElement>("CreatorDisplay");
                CreatorAPI.CreatorDisplayDataDialog = element.Element("CreatorDisplayDialog").Elements("CreatorDisplayData").Where(xe => xe.Attribute("Language").Value == CreatorAPI.Language.ToString());
                CreatorAPI.CreatorDisplayDataUI = element.Element("CreatorDisplayUI").Elements();
                ContentManager.Dispose("CreatorDisplay");*//*

                //读取语言信息进行语言筛选，仅保留当前语言

            }
            catch (Exception ex)
            {
                Log.Write(LogType.Warning, "Failed to read language, need to restart\n" + ex.Message);
                CreatorAPI.Language = Language.en_US;
            }

            creatorGenerationAlgorithm = new CreatorGenerationAlgorithm();
            this.componentMiner = componentMiner;

            Position = new List<Point3>(4)
      {
        new Point3(0, -1, 0),
        new Point3(0, -1, 0),
        new Point3(0, -1, 0),
        new Point3(0, -1, 0)
      };




        }

        public void OnUse(TerrainRaycastResult terrainRaycastResult)
        {
            Point3 point = terrainRaycastResult.CellFace.Point;
            ComponentPlayer componentPlayer = componentMiner.ComponentPlayer;
            if (!OnTouch.Touch(this, point))
            {
                return;
            }

            int cellValue = GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValue(point.X, point.Y, point.Z);
            int light = Terrain.ExtractLight(cellValue);
            int data = Terrain.ExtractData(cellValue);
            int contents = Terrain.ExtractContents(cellValue);
            if (BlocksManager.Blocks[contents] == null)
            {
                return;
            }
            *//*
                        if (CreatorAPI.Language == Language.ot_OT)
                        {
                            return;
                        }*//*

            if (numberPoint == CreatorAPI.NumberPoint.One)
            {
                Position[0] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint1"), point.X, point.Y, point.Z, contents, cellValue, light, data, CreatorModAPI.SetFaceAndRotate.GetFace(cellValue), CreatorModAPI.SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(point.X, point.Z)), Color.White, true, false);
                }
                catch (Exception e)
                {
                    Engine.Log.Warning(e);
                }
                //SetFaceAndRotate.SetFace(cellValue, SetFaceAndRotate.GetFace(cellValue));
                *//*Vector3 vectorqian = CellFace.FaceToVector3(SetFaceAndRotate.GetFace(cellValue));
                int qian = SetFaceAndRotate.GetFace(cellValue);
                Vector3 vectorxp90 = Vector3.TransformNormal(vectorqian,Matrix.CreateRotationX(90));
                int xp90 = CellFace.Vector3ToFace(vectorxp90);
                Vector3 vectoryp90 = Vector3.TransformNormal(vectorqian, Matrix.CreateRotationY(90));
                int yp90 = CellFace.Vector3ToFace(vectoryp90);
                Vector3 vectorzp90 = Vector3.TransformNormal(vectorqian, Matrix.CreateRotationZ(90));
                int zp90 = CellFace.Vector3ToFace(vectorzp90);
                componentPlayer.ComponentGui.DisplaySmallMessage(
                    "ProRotate:"+qian+" LastRotate"+ SetFaceAndRotate.GetFaceVector(cellValue, SetFaceAndRotate.GetFaceFunction((BlockFace)SetFaceAndRotate.GetFace(cellValue), (BlockFace)((SetFaceAndRotate.GetFace(cellValue)+1)%6)))+" Ider:"+ ((SetFaceAndRotate.GetFace(cellValue) + 1) % 6) + "\n"+
                    "X:" + xp90 + "\n" +
                    "Y:" + yp90 + "\n" +
                    "Z:" + zp90 + "\n" +"180:"+ Vector3.TransformNormal(vectorqian, SetFaceAndRotate.GetFaceFunction((BlockFace)SetFaceAndRotate.GetFace(cellValue), (BlockFace)((SetFaceAndRotate.GetFace(cellValue) + 1) % 6)))
                    , Color.Black, false, false);*//*
                // Vector3 view = SetFaceAndRotate.Vector3Normalize(Matrix.CreateFromQuaternion(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward);
                // int numxx1= SetFaceAndRotate.SetFace(cellValue,SetFaceAndRotate.GetFaceVector(cellValue, SetFaceAndRotate.GetFaceFunction((BlockFace)SetFaceAndRotate.GetFace(cellValue), (BlockFace)((SetFaceAndRotate.GetFace(cellValue) + 1) % 6))));

                //   GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.SetCellValueFast(point.X, point.Y, point.Z, numxx1);

                // componentPlayer.ComponentGui.DisplaySmallMessage("PlayerLook at"+CellFace.Vector3ToFace(view)+"NextFace at"+ SetFaceAndRotate.GetFaceVector(cellValue, SetFaceAndRotate.GetFaceFunction((BlockFace)CellFace.Vector3ToFace(view), (BlockFace)((CellFace.Vector3ToFace(view) + 1) % 6))) + "\n"+"TrueFace at"+ SetFaceAndRotate.Point3ToFace(CellFace.FaceToPoint3(SetFaceAndRotate.GetFace(cellValue))), Color.White,false,false);

                if (amountPoint == numberPoint)
                {
                    return;
                }

                numberPoint = CreatorAPI.NumberPoint.Two;
            }
            else if (numberPoint == CreatorAPI.NumberPoint.Two)
            {

                Position[1] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint2"), point.X, point.Y, point.Z, contents, cellValue, light, data, CreatorModAPI.SetFaceAndRotate.GetFace(cellValue), CreatorModAPI.SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(point.X, point.Z)), Color.White, true, false);
                }
                catch (Exception e)
                {
                    Engine.Log.Warning(e);
                } //
                SetFaceAndRotate.SetFace(cellValue, SetFaceAndRotate.GetFace(cellValue));
                if (amountPoint == numberPoint)
                {
                    numberPoint = CreatorAPI.NumberPoint.One;
                }
                else
                {
                    numberPoint = CreatorAPI.NumberPoint.Three;
                }
            }
            else if (numberPoint == CreatorAPI.NumberPoint.Three)
            {
                Position[2] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint3"), point.X, point.Y, point.Z, contents, cellValue, light, data, CreatorModAPI.SetFaceAndRotate.GetFace(cellValue), CreatorModAPI.SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(point.X, point.Z)), Color.White, true, false);
                }
                catch (Exception e)
                {
                    Engine.Log.Warning(e);
                }

                if (amountPoint == numberPoint)
                {
                    numberPoint = CreatorAPI.NumberPoint.One;
                }
                else
                {
                    numberPoint = CreatorAPI.NumberPoint.Four;
                }
            }
            else
            {
                if (numberPoint != CreatorAPI.NumberPoint.Four)
                {
                    return;
                }

                Position[3] = point;
                try
                {
                    componentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorAPIsetpoint4"), point.X, point.Y, point.Z, contents, cellValue, light, data, CreatorModAPI.SetFaceAndRotate.GetFace(cellValue), CreatorModAPI.SetFaceAndRotate.GetRotate(cellValue), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(point.X, point.Z), GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(point.X, point.Z)), Color.White, true, false);
                }
                catch (Exception e)
                {
                    Engine.Log.Warning(e);
                }
                numberPoint = CreatorAPI.NumberPoint.One;
            }
            CreatorMain.Position = Position;
        }

        public void CreateBlock(int x, int y, int z, int value, ChunkData chunkData = null)
        {
            if (RevokeSwitch && revokeData != null && revokeData.GetChunk(x, y) == null)
            {
                revokeData.CreateChunk(x, y, true);
            }

            switch (CreateBlockType)
            {
                case CreateBlockType.Normal:
                    GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(x, y, z, value);
                    break;
                case CreateBlockType.Fast:
                    SetBlock(x, y, z, value);
                    break;
                case CreateBlockType.Catch:
                    chunkData.SetBlock(x, y, z, value);
                    break;
            }
        }

        public void CreateBlock(Point3 point3, int value, ChunkData chunkData = null)
        {
            if (RevokeSwitch && revokeData != null && revokeData.GetChunk(point3.X, point3.Z) == null)
            {
                revokeData.CreateChunk(point3.X, point3.Z, true);
            }

            switch (CreateBlockType)
            {
                case CreateBlockType.Normal:
                    GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(point3.X, point3.Y, point3.Z, value);
                    break;
                case CreateBlockType.Fast:
                    SetBlock(point3.X, point3.Y, point3.Z, value);
                    break;
                case CreateBlockType.Catch:
                    chunkData.SetBlock(point3, value);
                    break;
            }
        }

        public void SetBlock(int x, int y, int z, int value)
        {
            try
            {
                SubsystemTerrain subsystem = componentMiner.Project.FindSubsystem<SubsystemTerrain>(true);
                if (!subsystem.Terrain.IsCellValid(x, y, z))
                {
                    return;
                }

                TerrainChunk chunk = subsystem.Terrain.GetChunkAtCell(x, z);
                if (chunk == null)
                {
                    if (!UnLimitedOfCreate)
                    {
                        return;
                    }

                    chunk = subsystem.Terrain.AllocateChunk(x >> 4, z >> 4);
                    while (chunk.ThreadState < TerrainChunkState.Valid)
                    {
                        subsystem.TerrainUpdater.UpdateChunkSingleStep(chunk, 15);
                    }
                }
                chunk.Cells[y + (x & 15) * 256 + (z & 15) * 256 * 16] = value;
                ++chunk.ModificationCounter;
                if (UnLimitedOfCreate)
                {
                    chunk.State = TerrainChunkState.Valid;
                }

                if (chunk.State > TerrainChunkState.InvalidLight)
                {
                    chunk.State = TerrainChunkState.InvalidLight;
                }

                chunk.WasDowngraded = true;
            }
            catch (Exception ex)
            {
                Log.Error(CreatorMain.Display_Key_Dialog("creatorAPIsetblock") + ex.Message);


            }
        }

        public enum NumberPoint
        {
            One,
            Two,
            Three,
            Four,
        }

        public enum OnekeyType
        {
            Tree,
            Build,
        }
    }
}
*/