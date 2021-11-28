using Engine;
using Engine.Serialization;
using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreatorModAPI
{
    public static class CopyAndPaste
    {
        public static long CreateCopy(CreatorAPI creatorAPI, string directory, string path, Point3 Start, Point3 End)
        {
            long result = 0L;
            int num = 0;
            CreatorMain.Math.StarttoEnd(ref Start, ref End);
            ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
            Vector3 forward = Matrix.CreateFromQuaternion(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward;
            float num2 = float.NegativeInfinity;
            int num3 = 0;
            for (int i = 0; i < 6; i++)
            {
                float num4 = Vector3.Dot(CellFace.FaceToVector3(i), forward);
                if ((double)num4 > (double)num2)
                {
                    num2 = num4;
                    num3 = i;
                }
            }

            float num5 = Vector3.Dot(forward, -Vector3.UnitZ);
            float num6 = Vector3.Dot(forward, -Vector3.UnitX);
            float num7 = Vector3.Dot(forward, Vector3.UnitZ);
            float num8 = Vector3.Dot(forward, Vector3.UnitX);
            int num9 = 0;
            if ((double)num5 == (double)MathUtils.Max(num5, num6, num7, num8))
            {
                num9 = 2;
            }
            else if ((double)num6 == (double)MathUtils.Max(num5, num6, num7, num8))
            {
                num9 = 3;
            }
            else if ((double)num7 == (double)MathUtils.Max(num5, num6, num7, num8))
            {
                num9 = 0;
            }
            else if ((double)num8 == (double)MathUtils.Max(num5, num6, num7, num8))
            {
                num9 = 1;
            }

            Point3 point = default(Point3);
            point.X = Start.Y;
            Point3 point2 = default(Point3);
            point2.X = End.Y;
            point.Y = Start.Z;
            point2.Y = End.Z;
            point.Z = Start.X;
            point2.Z = End.X;
            int num10;
            switch (num3)
            {
                case 0:
                    point.X = Start.Y;
                    point2.X = End.Y;
                    point.Y = Start.Z;
                    point2.Y = End.Z;
                    point.Z = Start.X;
                    point2.Z = End.X;
                    num10 = 2310;
                    break;
                case 1:
                    point.X = Start.Y;
                    point2.X = End.Y;
                    point.Y = Start.X;
                    point2.Y = End.X;
                    point.Z = End.Z;
                    point2.Z = Start.Z;
                    num10 = 2130;
                    break;
                case 2:
                    point.X = Start.Y;
                    point2.X = End.Y;
                    point.Y = End.Z;
                    point2.Y = Start.Z;
                    point.Z = End.X;
                    point2.Z = Start.X;
                    num10 = 2310;
                    break;
                case 3:
                    point.X = Start.Y;
                    point2.X = End.Y;
                    point.Y = End.X;
                    point2.Y = Start.X;
                    point.Z = Start.Z;
                    point2.Z = End.Z;
                    num10 = 2130;
                    break;
                case 4:
                    switch (num9)
                    {
                        case 0:
                            point.X = End.Z;
                            point2.X = Start.Z;
                            point.Y = Start.Y;
                            point2.Y = End.Y;
                            point.Z = Start.X;
                            point2.Z = End.X;
                            num10 = 3214;
                            break;
                        case 1:
                            point.X = End.X;
                            point2.X = Start.X;
                            point.Y = Start.Y;
                            point2.Y = End.Y;
                            point.Z = End.Z;
                            point2.Z = Start.Z;
                            num10 = 1234;
                            break;
                        case 2:
                            point.X = Start.Z;
                            point2.X = End.Z;
                            point.Y = Start.Y;
                            point2.Y = End.Y;
                            point.Z = End.X;
                            point2.Z = Start.X;
                            num10 = 3214;
                            break;
                        case 3:
                            point.X = Start.X;
                            point2.X = End.X;
                            point.Y = Start.Y;
                            point2.Y = End.Y;
                            point.Z = Start.Z;
                            point2.Z = End.Z;
                            num10 = 1234;
                            break;
                        default:
                            point.X = End.Z;
                            point2.X = Start.Z;
                            point.Y = Start.Y;
                            point2.Y = End.Y;
                            point.Z = Start.X;
                            point2.Z = End.X;
                            num10 = 3214;
                            break;
                    }

                    break;
                case 5:
                    switch (num9)
                    {
                        case 0:
                            point.X = Start.Z;
                            point2.X = End.Z;
                            point.Y = End.Y;
                            point2.Y = Start.Y;
                            point.Z = Start.X;
                            point2.Z = End.X;
                            num10 = 3215;
                            break;
                        case 1:
                            point.X = Start.X;
                            point2.X = End.X;
                            point.Y = End.Y;
                            point2.Y = Start.Y;
                            point.Z = End.Z;
                            point2.Z = Start.Z;
                            num10 = 1235;
                            break;
                        case 2:
                            point.X = End.Z;
                            point2.X = Start.Z;
                            point.Y = End.Y;
                            point2.Y = Start.Y;
                            point.Z = End.X;
                            point2.Z = Start.X;
                            num10 = 3215;
                            break;
                        case 3:
                            point.X = End.X;
                            point2.X = Start.X;
                            point.Y = End.Y;
                            point2.Y = Start.Y;
                            point.Z = Start.Z;
                            point2.Z = End.Z;
                            num10 = 1235;
                            break;
                        default:
                            point.X = Start.Z;
                            point2.X = End.Z;
                            point.Y = End.Y;
                            point2.Y = Start.Y;
                            point.Z = Start.X;
                            point2.Z = End.X;
                            num10 = 3215;
                            break;
                    }

                    break;
                default:
                    point.X = Start.Y;
                    point2.X = End.Y;
                    point.Y = Start.Z;
                    point2.Y = End.Z;
                    point.Z = Start.X;
                    point2.Z = End.X;
                    num10 = 2319;
                    break;
            }

            FileStream fileStream = new FileStream(Path.Combine(directory, path), FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream, leaveOpen: true);
            engineBinaryWriter.Write($"This is the v{CreatorMain.version} version of the creator API");
            engineBinaryWriter.Write(CreatorMain.Numversion);
            engineBinaryWriter.Write('P');
            engineBinaryWriter.Write(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation);
            engineBinaryWriter.Write(num3 * 10 + num9);
            engineBinaryWriter.Write(Math.Abs(point2.X - point.X));
            engineBinaryWriter.Write(Math.Abs(point2.Y - point.Y));
            engineBinaryWriter.Write(Math.Abs(point2.Z - point.Z));
            int num11 = point.X;
            while ((point.X <= point2.X && num11 <= point2.X) || (point.X > point2.X && num11 >= point2.X))
            {
                int num12 = point.Y;
                while ((point.Y <= point2.Y && num12 <= point2.Y) || (point.Y > point2.Y && num12 >= point2.Y))
                {
                    int num13 = point.Z;
                    while ((point.Z <= point2.Z && num13 <= point2.Z) || (point.Z > point2.Z && num13 >= point2.Z))
                    {
                        int x;
                        int y;
                        int z;
                        switch (num10 / 10)
                        {
                            case 123:
                                x = num11;
                                y = num12;
                                z = num13;
                                break;
                            case 132:
                                x = num11;
                                z = num12;
                                y = num13;
                                break;
                            case 213:
                                y = num11;
                                x = num12;
                                z = num13;
                                break;
                            case 231:
                                y = num11;
                                z = num12;
                                x = num13;
                                break;
                            case 312:
                                z = num11;
                                x = num12;
                                y = num13;
                                break;
                            case 321:
                                z = num11;
                                y = num12;
                                x = num13;
                                break;
                            default:
                                creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogc1"), num), Color.LightYellow, blinking: true, playNotificationSound: true);
                                return result;
                        }

                        engineBinaryWriter.Write(GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValueFast(x, y, z));
                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValueFast(x, y, z)))
                        {
                            case 186:
                                {
                                    engineBinaryWriter.Write('M');
                                    MemoryBankData memoryBankData = GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(throwOnError: true).GetBlockData(new Point3(x, y, z)) ?? new MemoryBankData();
                                    engineBinaryWriter.Write(memoryBankData.SaveString());
                                    break;
                                }
                            case 188:
                                {
                                    engineBinaryWriter.Write('T');
                                    TruthTableData truthTableData = GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(throwOnError: true).GetBlockData(new Point3(x, y, z)) ?? new TruthTableData();
                                    engineBinaryWriter.Write(truthTableData.SaveString());
                                    break;
                                }
                            case 97:
                            case 98:
                            case 210:
                            case 211:
                                {
                                    engineBinaryWriter.Write('S');
                                    SignData signData = GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(throwOnError: true).GetSignData(new Point3(x, y, z)) ?? new SignData();
                                    engineBinaryWriter.Write(signData.Lines[0] ?? "");
                                    engineBinaryWriter.Write(signData.Colors[0]);
                                    engineBinaryWriter.Write(signData.Lines[1] ?? "");
                                    engineBinaryWriter.Write(signData.Colors[1]);
                                    engineBinaryWriter.Write(signData.Lines[2] ?? "");
                                    engineBinaryWriter.Write(signData.Colors[2]);
                                    engineBinaryWriter.Write(signData.Lines[3] ?? "");
                                    engineBinaryWriter.Write(signData.Colors[3]);
                                    engineBinaryWriter.Write(signData.Url ?? "");
                                    break;
                                }
                            case 184:
                                {
                                    float? num14 = GameManager.Project.FindSubsystem<SubsystemElectricity>(throwOnError: true).ReadPersistentVoltage(new Point3(x, y, z));
                                    if (!num14.HasValue)
                                    {
                                        engineBinaryWriter.Write(value: false);
                                        break;
                                    }

                                    engineBinaryWriter.Write(value: true);
                                    engineBinaryWriter.Write((double)num14.Value);
                                    break;
                                }
                        }

                        num++;
                        if (point.Z <= point2.Z)
                        {
                            num13++;
                        }
                        else if (point.Z > point2.Z)
                        {
                            num13--;
                        }
                    }

                    if (point.Y <= point2.Y)
                    {
                        num12++;
                    }
                    else if (point.Y > point2.Y)
                    {
                        num12--;
                    }
                }

                if (point.X <= point2.X)
                {
                    num11++;
                }
                else if (point.X > point2.X)
                {
                    num11--;
                }
            }

            result = fileStream.Length;
            engineBinaryWriter.Dispose();
            fileStream.Dispose();
            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogc2"), num, result), Color.LightYellow, blinking: true, playNotificationSound: true);
            return result;
        }

        public static void PasetData(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
        {
            long num = 0L;
            ChunkData chunkData = new ChunkData(creatorAPI);
            creatorAPI.revokeData = new ChunkData(creatorAPI);
            ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
            Stream stream = new FileStream(path, FileMode.Open).CreateStream();
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream);
            new MemoryBankData();
            int[,] array = new int[3, 4]
            {
                {
                    6012345,
                    6123045,
                    6230145,
                    6301245
                },
                {
                    6415320,
                    6425031,
                    6435102,
                    6405213
                },
                {
                    6514302,
                    6524013,
                    6534120,
                    6504231
                }
            };
            int[,] array2 = new int[3, 4]
            {
                {
                    60123,
                    60123,
                    60123,
                    60123
                },
                {
                    62301,
                    63012,
                    60123,
                    61230
                },
                {
                    62301,
                    61230,
                    60123,
                    63012
                }
            };
            bool flag = false;
            int num2 = 0;
            string str = engineBinaryReader.ReadString();
            int num3 = engineBinaryReader.ReadInt32();
            int[] sumnumversion = CreatorMain.Sumnumversion;
            for (int i = 0; i < sumnumversion.Length; i++)
            {
                if (sumnumversion[i] == num3)
                {
                    flag = true;
                    break;
                }
            }

            bool flag2 = false;
            sumnumversion = CreatorMain.Donotnumversion;
            for (int i = 0; i < sumnumversion.Length; i++)
            {
                if (sumnumversion[i] == num3)
                {
                    flag2 = true;
                    break;
                }
            }

            if (!flag)
            {
                if (flag2)
                {
                    DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Version control", str + "\nNot compatible with current version!", "OK", null, null));
                }
                else
                {
                    DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Version control", str + "\nThis is a discarded version!", "OK", null, null));
                }

                return;
            }

            engineBinaryReader.ReadChar();
            engineBinaryReader.ReadStruct<Quaternion>();
            int num4 = engineBinaryReader.ReadInt32();
            int num5 = Math.Abs(engineBinaryReader.ReadInt32());
            int num6 = Math.Abs(engineBinaryReader.ReadInt32());
            int num7 = Math.Abs(engineBinaryReader.ReadInt32());
            Vector3 forward = Matrix.CreateFromQuaternion(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward;
            float num8 = float.NegativeInfinity;
            int num9 = 0;
            for (int j = 0; j < 6; j++)
            {
                float num10 = Vector3.Dot(CellFace.FaceToVector3(j), forward);
                if ((double)num10 > (double)num8)
                {
                    num8 = num10;
                    num9 = j;
                }
            }

            float num11 = Vector3.Dot(forward, -Vector3.UnitZ);
            float num12 = Vector3.Dot(forward, -Vector3.UnitX);
            float num13 = Vector3.Dot(forward, Vector3.UnitZ);
            float num14 = Vector3.Dot(forward, Vector3.UnitX);
            int num15 = 0;
            if ((double)num11 == (double)MathUtils.Max(num11, num12, num13, num14))
            {
                num15 = 2;
            }
            else if ((double)num12 == (double)MathUtils.Max(num11, num12, num13, num14))
            {
                num15 = 3;
            }
            else if ((double)num13 == (double)MathUtils.Max(num11, num12, num13, num14))
            {
                num15 = 0;
            }
            else if ((double)num14 == (double)MathUtils.Max(num11, num12, num13, num14))
            {
                num15 = 1;
            }

            Point3 point = default(Point3);
            point.X = Start.Y;
            point.Y = Start.Z;
            point.Z = Start.X;
            Point3 point2 = default(Point3);
            point2.X = num5;
            point2.Y = num6;
            point2.Z = num7;
            int num16;
            switch (num9)
            {
                case 0:
                    point.X = Start.Y;
                    point2.X = Start.Y + num5;
                    point.Y = Start.Z;
                    point2.Y = Start.Z + num6;
                    point.Z = Start.X;
                    point2.Z = Start.X + num7;
                    if (MirrorBlockBehavior.IsMirror)
                    {
                        point.Z = Start.X;
                        point2.Z = Start.X - num7;
                    }

                    num16 = 2310;
                    break;
                case 1:
                    point.X = Start.Y;
                    point2.X = Start.Y + num5;
                    point.Y = Start.X;
                    point2.Y = Start.X + num6;
                    point.Z = Start.Z;
                    point2.Z = Start.Z - num7;
                    if (MirrorBlockBehavior.IsMirror)
                    {
                        point.Z = Start.Z;
                        point2.Z = Start.Z + num7;
                    }

                    num16 = 2130;
                    break;
                case 2:
                    point.X = Start.Y;
                    point2.X = Start.Y + num5;
                    point.Y = Start.Z;
                    point2.Y = Start.Z - num6;
                    point.Z = Start.X;
                    point2.Z = Start.X - num7;
                    if (MirrorBlockBehavior.IsMirror)
                    {
                        point.Z = Start.X;
                        point2.Z = Start.X + num7;
                    }

                    num16 = 2310;
                    break;
                case 3:
                    point.X = Start.Y;
                    point2.X = Start.Y + num5;
                    point.Y = Start.X;
                    point2.Y = Start.X - num6;
                    point.Z = Start.Z;
                    point2.Z = Start.Z + num7;
                    if (MirrorBlockBehavior.IsMirror)
                    {
                        point.Z = Start.Z;
                        point2.Z = Start.Z - num7;
                    }

                    num16 = 2130;
                    break;
                case 4:
                    switch (num15)
                    {
                        case 0:
                            point.X = Start.Z;
                            point2.X = Start.Z - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.X;
                            point2.Z = Start.X + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X - num7;
                            }

                            num16 = 3214;
                            break;
                        case 1:
                            point.X = Start.X;
                            point2.X = Start.X - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.Z;
                            point2.Z = Start.Z - num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.Z;
                                point2.Z = Start.Z + num7;
                            }

                            num16 = 1234;
                            break;
                        case 2:
                            point.X = Start.Z;
                            point2.X = Start.Z + num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.X;
                            point2.Z = Start.X - num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X + num7;
                            }

                            num16 = 3214;
                            break;
                        case 3:
                            point.X = Start.X;
                            point2.X = Start.X + num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.Z;
                            point2.Z = Start.Z + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.Z;
                                point2.Z = Start.Z - num7;
                            }

                            num16 = 1234;
                            break;
                        default:
                            point.X = Start.Z;
                            point2.X = Start.Z - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.X;
                            point2.Z = Start.X + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X - num7;
                            }

                            num16 = 3214;
                            break;
                    }

                    break;
                case 5:
                    switch (num15)
                    {
                        case 0:
                            point.X = Start.Z;
                            point2.X = Start.Z + num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y - num6;
                            point.Z = Start.X;
                            point2.Z = Start.X + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X - num7;
                            }

                            num16 = 3214;
                            break;
                        case 1:
                            point.X = Start.X;
                            point2.X = Start.X + num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y - num6;
                            point.Z = Start.Z;
                            point2.Z = Start.Z - num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.Z;
                                point2.Z = Start.Z + num7;
                            }

                            num16 = 1234;
                            break;
                        case 2:
                            point.X = Start.Z;
                            point2.X = Start.Z - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y - num6;
                            point.Z = Start.X;
                            point2.Z = Start.X - num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X + num7;
                            }

                            num16 = 3214;
                            break;
                        case 3:
                            point.X = Start.X;
                            point2.X = Start.X - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y - num6;
                            point.Z = Start.Z;
                            point2.Z = Start.Z + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.Z;
                                point2.Z = Start.Z - num7;
                            }

                            num16 = 1234;
                            break;
                        default:
                            point.X = Start.Z;
                            point2.X = Start.Z - num5;
                            point.Y = Start.Y;
                            point2.Y = Start.Y + num6;
                            point.Z = Start.X;
                            point2.Z = Start.X + num7;
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                point.Z = Start.X;
                                point2.Z = Start.X - num7;
                            }

                            num16 = 3214;
                            break;
                    }

                    break;
                default:
                    point.X = Start.Y;
                    point2.X = Start.Y + num5;
                    point.Y = Start.Z;
                    point2.Y = Start.Z + num6;
                    point.Z = Start.X;
                    point2.Z = Start.X + num7;
                    if (MirrorBlockBehavior.IsMirror)
                    {
                        point.Z = Start.X;
                        point2.Z = Start.X - num7;
                    }

                    num16 = 2310;
                    break;
            }

            int num17;
            switch (num9)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    num17 = 0;
                    break;
                case 4:
                    num17 = 1;
                    break;
                case 5:
                    num17 = 2;
                    break;
                default:
                    num17 = 0;
                    break;
            }

            int num18;
            switch (num4 / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    num18 = 0;
                    break;
                case 4:
                    num18 = 1;
                    break;
                case 5:
                    num18 = 2;
                    break;
                default:
                    num18 = 0;
                    break;
            }

            _ = array[num18, num4 % 10];
            _ = array[num17, num15];
            _ = array2[num18, num4 % 10];
            _ = array2[num17, num15];
            int num19 = point.X;
            while ((point.X <= point2.X && num19 <= point2.X) || (point.X > point2.X && num19 >= point2.X))
            {
                int num20 = point.Y;
                while ((point.Y <= point2.Y && num20 <= point2.Y) || (point.Y > point2.Y && num20 >= point2.Y))
                {
                    int num21 = point.Z;
                    while ((point.Z <= point2.Z && num21 <= point2.Z) || (point.Z > point2.Z && num21 >= point2.Z))
                    {
                        int x;
                        int y;
                        int z;
                        switch (num16 / 10)
                        {
                            case 123:
                                x = num19;
                                y = num20;
                                z = num21;
                                break;
                            case 132:
                                x = num19;
                                z = num20;
                                y = num21;
                                break;
                            case 213:
                                y = num19;
                                x = num20;
                                z = num21;
                                break;
                            case 231:
                                y = num19;
                                z = num20;
                                x = num21;
                                break;
                            case 312:
                                z = num19;
                                x = num20;
                                y = num21;
                                break;
                            case 321:
                                z = num19;
                                y = num20;
                                x = num21;
                                break;
                            default:
                                creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogp1"), num2), Color.LightYellow, blinking: true, playNotificationSound: true);
                                return;
                        }

                        if (!creatorAPI.launch)
                        {
                            return;
                        }

                        int num22 = engineBinaryReader.ReadInt32();
                        if (creatorAPI.AirIdentify || Terrain.ExtractContents(num22) != 0)
                        {
                            SetFaceAndRotate.GetFace(num22);
                            SetFaceAndRotate.GetRotate(num22);
                            num22 = SetFaceAndRotate.SetFace(num22, SetFaceAndRotate.GetFaceVector(num22, num4, num9 * 10 + num15, SetFaceAndRotate.GetFace(num22)));
                            num22 = SetFaceAndRotate.SetRotate(num22, SetFaceAndRotate.GetRotateVector(num22, num4, num9 * 10 + num15, SetFaceAndRotate.GetRotate(num22)));
                            SetFaceAndRotate.GetRotate(num22);
                            creatorAPI.CreateBlock(x, y, z, num22, chunkData);
                            switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValueFast(x, y, z)))
                            {
                                case 186:
                                    {
                                        engineBinaryReader.ReadChar();
                                        MemoryBankData memoryBankData = new MemoryBankData();
                                        memoryBankData.LoadString(engineBinaryReader.ReadString());
                                        GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(throwOnError: true).SetBlockData(new Point3(x, y, z), memoryBankData);
                                        break;
                                    }
                                case 188:
                                    {
                                        engineBinaryReader.ReadChar();
                                        TruthTableData truthTableData = new TruthTableData();
                                        truthTableData.LoadString(engineBinaryReader.ReadString());
                                        GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(throwOnError: true).SetBlockData(new Point3(x, y, z), truthTableData);
                                        break;
                                    }
                                case 97:
                                case 98:
                                case 210:
                                case 211:
                                    {
                                        engineBinaryReader.ReadChar();
                                        SignData signData = new SignData();
                                        signData.Lines[0] = engineBinaryReader.ReadString();
                                        signData.Colors[0] = engineBinaryReader.ReadColor();
                                        signData.Lines[1] = engineBinaryReader.ReadString();
                                        signData.Colors[1] = engineBinaryReader.ReadColor();
                                        signData.Lines[2] = engineBinaryReader.ReadString();
                                        signData.Colors[2] = engineBinaryReader.ReadColor();
                                        signData.Lines[3] = engineBinaryReader.ReadString();
                                        signData.Colors[3] = engineBinaryReader.ReadColor();
                                        signData.Url = engineBinaryReader.ReadString();
                                        GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(throwOnError: true).SetSignData(new Point3(x, y, z), signData.Lines, signData.Colors, signData.Url);
                                        break;
                                    }
                                case 184:
                                    if (engineBinaryReader.ReadBoolean())
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemElectricity>(throwOnError: true).WritePersistentVoltage(new Point3(x, y, z), (float)engineBinaryReader.ReadDouble());
                                    }

                                    break;
                            }

                            num2++;
                        }

                        if (point.Z <= point2.Z)
                        {
                            num21++;
                        }
                        else if (point.Z > point2.Z)
                        {
                            num21--;
                        }
                    }

                    if (point.Y <= point2.Y)
                    {
                        num20++;
                    }
                    else if (point.Y > point2.Y)
                    {
                        num20--;
                    }
                }

                if (point.X <= point2.X)
                {
                    num19++;
                }
                else if (point.X > point2.X)
                {
                    num19--;
                }
            }

            num = stream.Length;
            engineBinaryReader.Dispose();
            stream.Dispose();
            chunkData.Render();
            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogp2"), num2, num), Color.LightYellow, blinking: true, playNotificationSound: true);
        }

        public static void ExportCopywMod2(string path, string exportFile)
        {
            if (path.IsFileInUse())
            {
                throw new FileLoadException("capadialogex");
            }

            Stream stream = new FileStream(exportFile, FileMode.Create);
            Stream stream2 = File.Open(path, FileMode.Open);
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream2);
            int num = engineBinaryReader.ReadInt32();
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            string text = "" + $"{num}\n{num2}\n{num3}";
            for (int i = 0; i <= num; i++)
            {
                for (int j = 0; j <= num2; j++)
                {
                    for (int k = 0; k <= num3; k++)
                    {
                        text += $"\n{engineBinaryReader.ReadInt32()}";
                    }
                }
            }

            stream.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetBytes(text).Length);
            engineBinaryReader.Dispose();
            stream2.Dispose();
            stream.Dispose();
        }

        public static void ImportCopywMod2(string path, string importFile)
        {
            new List<string>();
            FileStream fileStream = File.OpenRead(importFile);
            StreamReader streamReader = new StreamReader(fileStream);
            string text = streamReader.ReadToEnd();
            streamReader.Dispose();
            fileStream.Dispose();
            FileStream fileStream2 = new FileStream(path, FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream2, leaveOpen: true);
            string text2 = text;
            char[] separator = new char[1]
            {
                '\n'
            };
            string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in array)
            {
                engineBinaryWriter.Write(int.Parse(s));
            }

            engineBinaryWriter.Dispose();
            fileStream2.Dispose();
        }

        public static void ExportCopywMod(string path, string exportFile)
        {
            if (path.IsFileInUse())
            {
                throw new FileLoadException("capadialogex");
            }

            Stream stream = new FileStream(exportFile, FileMode.Create);
            Stream stream2 = File.Open(path, FileMode.Open);
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream2);
            string str = "";
            int num = engineBinaryReader.ReadInt32();
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            string text = str + $"{num},{num2},{num3}";
            for (int i = 0; i <= num; i++)
            {
                for (int j = 0; j <= num2; j++)
                {
                    for (int k = 0; k <= num3; k++)
                    {
                        int num4 = engineBinaryReader.ReadInt32();
                        if (Terrain.ExtractContents(num4) != 0)
                        {
                            text += $"\n{i},{j},{k},{num4}";
                        }
                    }
                }
            }

            stream.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetBytes(text).Length);
            engineBinaryReader.Dispose();
            stream2.Dispose();
            stream.Dispose();
        }

        public static void ImportCopywMod(string path, string importFile)
        {
            List<string> list = new List<string>();
            FileStream fileStream = File.OpenRead(importFile);
            StreamReader streamReader = new StreamReader(fileStream);
            string text = streamReader.ReadToEnd();
            streamReader.Dispose();
            fileStream.Dispose();
            string text2 = text;
            char[] separator = new char[1]
            {
                '\n'
            };
            string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in array)
            {
                list.Add(item);
            }

            FileStream fileStream2 = new FileStream(path, FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream2, leaveOpen: true);
            string[] array2 = list[0].Split(new char[1]
            {
                ','
            }, StringSplitOptions.RemoveEmptyEntries);
            int num = int.Parse(array2[0]);
            int num2 = int.Parse(array2[1]);
            int num3 = int.Parse(array2[2]);
            list.RemoveAt(0);
            engineBinaryWriter.Write(num);
            engineBinaryWriter.Write(num2);
            engineBinaryWriter.Write(num3);
            for (int j = 0; j <= num; j++)
            {
                for (int k = 0; k <= num2; k++)
                {
                    for (int l = 0; l <= num3; l++)
                    {
                        int value = 0;
                        for (int m = 0; m < list.Count; m++)
                        {
                            string[] array3 = list[m].Split(new char[1]
                            {
                                ','
                            }, StringSplitOptions.RemoveEmptyEntries);
                            if (int.Parse(array3[0]) == j && int.Parse(array3[1]) == k && int.Parse(array3[2]) == l)
                            {
                                value = int.Parse(array3[3]);
                                list.RemoveAt(m);
                            }
                        }

                        engineBinaryWriter.Write(value);
                    }
                }
            }

            engineBinaryWriter.Dispose();
            fileStream2.Dispose();
        }
    }
}

/*复制粘贴*/
/*namespace CreatorModAPI-=  public static class CopyAndPaste*//*
using Engine;
using Engine.Serialization;
using Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CreatorModAPI
{
    public static class CopyAndPaste
    {
        public static long CreateCopy(CreatorAPI creatorAPI, string directory, string path, Point3 Start, Point3 End)
        {
            long buffer = 0;
            int num = 0;
            CreatorMain.Math.StarttoEnd(ref Start, ref End);

            ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
            //获取当前朝向的面的值wiredFace
            Vector3 forward = Matrix.CreateFromQuaternion(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward;
            float num1 = float.NegativeInfinity;
            int wiredFace = 0;
            for (int face = 0; face < 6; ++face)
            {
                //将0-6转换成一个面的定义，然后和面向进行比较，最终返回面向的值
                float num2 = Vector3.Dot(CellFace.FaceToVector3(face), forward);
                if (num2 > (double)num1)
                {
                    num1 = num2;
                    wiredFace = face;
                }
            }
            //获取上望下望时的视线朝向rotation
            float x1 = Vector3.Dot(forward, -Vector3.UnitZ);
            float x2 = Vector3.Dot(forward, -Vector3.UnitX);
            float x3 = Vector3.Dot(forward, Vector3.UnitZ);
            float x4 = Vector3.Dot(forward, Vector3.UnitX);
            int rotation = 0;
            if (x1 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 2;
            }
            else if (x2 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 3;
            }
            else if (x3 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 0;
            }
            else if (x4 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 1;
            }

            Point3 numS, numE;
            numS.X = Start.Y; numE.X = End.Y;
            numS.Y = Start.Z; numE.Y = End.Z;
            numS.Z = Start.X; numE.Z = End.X;
            //测试方位,123为xyz,0为平视，45仰视俯视
            int T_VEC;
            switch (wiredFace)
            {
                case 0:
                    numS.X = Start.Y; numE.X = End.Y;
                    numS.Y = Start.Z; numE.Y = End.Z;
                    numS.Z = Start.X; numE.Z = End.X;
                    T_VEC = 2310;
                    break;
                case 1:
                    numS.X = Start.Y; numE.X = End.Y;
                    numS.Y = Start.X; numE.Y = End.X;
                    numS.Z = End.Z; numE.Z = Start.Z;
                    T_VEC = 2130;
                    break;
                case 2:
                    numS.X = Start.Y; numE.X = End.Y;
                    numS.Y = End.Z; numE.Y = Start.Z;
                    numS.Z = End.X; numE.Z = Start.X;
                    T_VEC = 2310;
                    break;
                case 3:
                    numS.X = Start.Y; numE.X = End.Y;
                    numS.Y = End.X; numE.Y = Start.X;
                    numS.Z = Start.Z; numE.Z = End.Z;
                    T_VEC = 2130;
                    break;
                case 4:
                    switch (rotation)
                    {
                        case 0:
                            numS.X = End.Z; numE.X = Start.Z;
                            numS.Y = Start.Y; numE.Y = End.Y;
                            numS.Z = Start.X; numE.Z = End.X;
                            T_VEC = 3214;
                            break;
                        case 1:
                            numS.X = End.X; numE.X = Start.X;
                            numS.Y = Start.Y; numE.Y = End.Y;
                            numS.Z = End.Z; numE.Z = Start.Z;
                            T_VEC = 1234;
                            break;
                        case 2:
                            numS.X = Start.Z; numE.X = End.Z;
                            numS.Y = Start.Y; numE.Y = End.Y;
                            numS.Z = End.X; numE.Z = Start.X;
                            T_VEC = 3214;
                            break;
                        case 3:
                            numS.X = Start.X; numE.X = End.X;
                            numS.Y = Start.Y; numE.Y = End.Y;
                            numS.Z = Start.Z; numE.Z = End.Z;
                            T_VEC = 1234;
                            break;
                        default:
                            numS.X = End.Z; numE.X = Start.Z;
                            numS.Y = Start.Y; numE.Y = End.Y;
                            numS.Z = Start.X; numE.Z = End.X;
                            T_VEC = 3214;
                            break;
                    }

                    break;
                case 5:
                    switch (rotation)
                    {
                        case 0:
                            numS.X = Start.Z; numE.X = End.Z;
                            numS.Y = End.Y; numE.Y = Start.Y;
                            numS.Z = Start.X; numE.Z = End.X;
                            T_VEC = 3215;
                            break;
                        case 1:
                            numS.X = Start.X; numE.X = End.X;
                            numS.Y = End.Y; numE.Y = Start.Y;
                            numS.Z = End.Z; numE.Z = Start.Z;
                            T_VEC = 1235;
                            break;
                        case 2:
                            numS.X = End.Z; numE.X = Start.Z;
                            numS.Y = End.Y; numE.Y = Start.Y;
                            numS.Z = End.X; numE.Z = Start.X;
                            T_VEC = 3215;
                            break;
                        case 3:
                            numS.X = End.X; numE.X = Start.X;
                            numS.Y = End.Y; numE.Y = Start.Y;
                            numS.Z = Start.Z; numE.Z = End.Z;
                            T_VEC = 1235;
                            break;
                        default:
                            numS.X = Start.Z; numE.X = End.Z;
                            numS.Y = End.Y; numE.Y = Start.Y;
                            numS.Z = Start.X; numE.Z = End.X;
                            T_VEC = 3215;
                            break;
                    }
                    break;
                default:
                    numS.X = Start.Y; numE.X = End.Y;
                    numS.Y = Start.Z; numE.Y = End.Z;
                    numS.Z = Start.X; numE.Z = End.X;
                    T_VEC = 2319;
                    break;

            }
            *//*  DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1", numS.ToString() + "\n" + numE.ToString() + "\n" + T_VEC.ToString() + "\n" + Start.ToString() + "\n" + End.ToString() + "\n" + numS.X.GetType().ToString(), "OK", null, null));
  *//*
            //   CreatorModAPI.CPPreview.Preview(creatorAPI, ref Start, ref End);
            //打开空cF文件，然后创建字节写入器
            FileStream fileStream = new FileStream(Path.Combine(directory, path), FileMode.Create);
            EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream, true);
            //开始写入
            engineBinaryWriter.Write(string.Format("This is the v{0} version of the creator API", CreatorMain.version));
            engineBinaryWriter.Write(CreatorMain.Numversion);
            engineBinaryWriter.Write('P');
            //写入外层，中层，内层
            engineBinaryWriter.Write(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation);
            engineBinaryWriter.Write(wiredFace * 10 + rotation);
            engineBinaryWriter.Write(Math.Abs(numE.X - numS.X));
            engineBinaryWriter.Write(Math.Abs(numE.Y - numS.Y));
            engineBinaryWriter.Write(Math.Abs(numE.Z - numS.Z));
            // engineBinaryWriter.
            //  DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1",wiredFace.ToString(), "OK", null, null));
            for (int numo = numS.X, X, Y, Z; (numS.X <= numE.X && numo <= numE.X) || (numS.X > numE.X && numo >= numE.X);)
            {
                // DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1", numo.ToString(), "OK", null, null));

                for (int numm = numS.Y; (numS.Y <= numE.Y && numm <= numE.Y) || (numS.Y > numE.Y && numm >= numE.Y);)
                {
                    //  DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1", numo.ToString(), "OK", null, null));
                    for (int numi = numS.Z; (numS.Z <= numE.Z && numi <= numE.Z) || (numS.Z > numE.Z && numi >= numE.Z);)
                    {
                        //数据回代
                        switch (T_VEC / 10)
                        {
                            case 123: X = numo; Y = numm; Z = numi; break;
                            case 132: X = numo; Z = numm; Y = numi; break;
                            case 213: Y = numo; X = numm; Z = numi; break;
                            case 231: Y = numo; Z = numm; X = numi; break;
                            case 312: Z = numo; X = numm; Y = numi; break;
                            case 321: Z = numo; Y = numm; X = numi; break;
                            default:

                                creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogc1"), num), Color.LightYellow, true, true);

                                *//* switch (CreatorAPI.Language)
                                 {
                                     case Language.zh_CN:
                                         creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("复制失败，共{0}个方块", (object)num), Color.LightYellow, true, true);
                                         break;
                                     case Language.en_US:
                                         creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Copied failed for a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                                         break;
                                     default:
                                         creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("复制失败，共{0}个方块", (object)num), Color.LightYellow, true, true);
                                         break;
                                 }*//*
                                return buffer;
                        }
                        //DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1", new Point3(X, Y,Z).ToString(), "OK", null, null));
                        engineBinaryWriter.Write(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(X, Y, Z));
                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(X, Y, Z)))
                        {
                            case 186:
                                engineBinaryWriter.Write('M');
                                //写入得到的M板数据
                                MemoryBankData memoryBankData = GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).GetBlockData(new Point3(X, Y, Z)) ?? new MemoryBankData();
                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                engineBinaryWriter.Write(memoryBankData.SaveString());
                                break;
                            case 188:
                                engineBinaryWriter.Write('T');
                                //写入得到的M板数据
                                TruthTableData TruthTableBankData = GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).GetBlockData(new Point3(X, Y, Z)) ?? new TruthTableData();
                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                engineBinaryWriter.Write(TruthTableBankData.SaveString());
                                break;
                            case 211:
                            case 210:
                            case 97:
                            case 98:
                                engineBinaryWriter.Write('S');
                                SignData SignBankData = GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).GetSignData(new Point3(X, Y, Z)) ?? new SignData();

                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                engineBinaryWriter.Write(SignBankData.Lines[0] ?? "");
                                engineBinaryWriter.Write(SignBankData.Colors[0]);
                                engineBinaryWriter.Write(SignBankData.Lines[1] ?? "");
                                engineBinaryWriter.Write(SignBankData.Colors[1]);
                                engineBinaryWriter.Write(SignBankData.Lines[2] ?? "");
                                engineBinaryWriter.Write(SignBankData.Colors[2]);
                                engineBinaryWriter.Write(SignBankData.Lines[3] ?? "");
                                engineBinaryWriter.Write(SignBankData.Colors[3]);
                                engineBinaryWriter.Write(SignBankData.Url ?? "");
                                //   engineBinaryWriter.WriteStruct<SignData>(SignBankData);
                                break;
                            case 227:
                                //     FurnitureDesign furnitureDesign = GameManager.Project.FindSubsystem<SubsystemFurnitureBlockBehavior>(true).GetDesign(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z));
                                //   engineBinaryWriter.WriteStruct<FurnitureDesign>(furnitureDesign);
                                break;
                            case 184:
                                //复制出来方块的内容值，
                                float? nullable = GameManager.Project.FindSubsystem<SubsystemElectricity>(true).ReadPersistentVoltage(new Point3(X, Y, Z));

                                if (!nullable.HasValue)
                                {
                                    engineBinaryWriter.Write(false);
                                }
                                else
                                {
                                    engineBinaryWriter.Write(true);
                                    engineBinaryWriter.Write((double)nullable);
                                    //    engineBinaryWriter.Write(nullable.Value < 0.0);
                                };
                                //     FurnitureDesign furnitureDesign = GameManager.Project.FindSubsystem<SubsystemFurnitureBlockBehavior>(true).GetDesign(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z));
                                //   engineBinaryWriter.WriteStruct<FurnitureDesign>(furnitureDesign);
                                break;
                            default: break;

                        }
                        ++num;
                        if (numS.Z <= numE.Z)
                        {
                            ++numi;
                        }
                        else if (numS.Z > numE.Z)
                        {
                            --numi;
                        }
                    }
                    if (numS.Y <= numE.Y)
                    {
                        ++numm;
                    }
                    else if (numS.Y > numE.Y)
                    {
                        --numm;
                    }
                }
                if (numS.X <= numE.X)
                {
                    ++numo;
                }
                else if (numS.X > numE.X)
                {
                    --numo;
                }
            }
            *//*  DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported2", (numE.X - numS.X).ToString() + "," + (numE.Y - numS.Y).ToString() + "," + (numE.Z - numS.Z).ToString(), "OK", null, null));
              DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported2", GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(Start.X, Start.Y, Start.Z).ToString(), "OK", null, null));
             *//*
            //关闭写入器，退出文件
            buffer = fileStream.Length;
            engineBinaryWriter.Dispose();
            fileStream.Dispose();

            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogc2"), num, buffer), Color.LightYellow, true, true);

            *//*switch (CreatorAPI.Language)
            {
                case Language.zh_CN:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("复制成功，共{0}个方块\n文件长度：{1}", (object)num, buffer), Color.LightYellow, true, true);
                    break;
                case Language.en_US:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Copied successfully for a total of {0} blocks\nFile size:{1}", (object)num, buffer), Color.LightYellow, true, true);
                    break;
                default:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("复制成功，共{0}个方块\n文件长度：{1}", (object)num, buffer), Color.LightYellow, true, true);
                    break;
            }*//*
            return buffer;
        }



        public static void PasetData(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
        {
            long buffer = 0;
            ChunkData chunkData = new ChunkData(creatorAPI);
            creatorAPI.revokeData = new ChunkData(creatorAPI);
            ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
            _ = End;
            //打开cF文件，然后创建比特读取器
            Stream stream = new FileStream(path, FileMode.Open).CreateStream();
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream);
            MemoryBankData memoryData = new MemoryBankData();
            //定义一个旋转体，用于做置换,定义一个面，用于交换平视俯视仰视时的输出朝向
            int[,] Rotouzi = new int[3, 4] {
                {6012345, 6123045, 6230145, 6301245} ,
                {6415320, 6425031, 6435102, 6405213} ,
                {6514302, 6524013, 6534120, 6504231}
            };
            int[,] Romian = new int[3, 4] {
                {60123, 60123,60123,60123} ,
                {62301, 63012, 60123, 61230} ,
                {62301, 61230, 60123, 63012}
            };
            // int strLene = 0;
            //读取得到的M板数据
            //允许复制粘贴
            bool allow_copy = false;
            int num1 = 0;
            // int llo = 0;
            string version_string = engineBinaryReader.ReadString();
            int version_num = engineBinaryReader.ReadInt32();
            foreach (int i in CreatorMain.Sumnumversion)
            {
                //     llo += i;
                if (i == version_num) { allow_copy = true; break; }
            }
            bool donot = false;
            foreach (int i in CreatorMain.Donotnumversion)
            {
                //     llo += i;
                if (i == version_num) { donot = true; break; }
            }
            if (allow_copy == false)
            {
                switch (donot)
                {
                    case true:
                        DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Version control", version_string + "\nNot compatible with current version!", "OK", null, null));
                        break;
                    default:
                        DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Version control", version_string + "\nThis is a discarded version!", "OK", null, null));
                        break;

                }

                return;
            }
            *//* switch (version_num)
{
case 902021003: break;
default:
DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Version control", version_string + "\nNot compatible with current version!", "OK", null, null));

return;
}*//*
            // engineBinaryWriter.Write("This is the v2.2.10.h.03 version of the creator API");
            //    engineBinaryWriter.Write(902021003);
            _ = engineBinaryReader.ReadChar();//读取P
                                              //huoquxuanxiang,(eye)
            _ = engineBinaryReader.ReadStruct<Quaternion>();
            int numF, numFl;//获取视角信息，wiredface转换成旋转体的列坐标,numFl是本地坐标,numrotouzi为远程临时数组值，numrotouzil为本地临时数组值,numromian为远程临时数组值，numromianl为本地临时数组值
            int numR = engineBinaryReader.ReadInt32();//读取旋转信息
            int numH = Math.Abs(engineBinaryReader.ReadInt32());//读取外层长,高
            int numW = Math.Abs(engineBinaryReader.ReadInt32());//读取中层长,宽
            int numL = Math.Abs(engineBinaryReader.ReadInt32());//读取内层长,长
            //获取当前朝向的面的值wiredFace
            Vector3 forward = Matrix.CreateFromQuaternion(componentPlayer.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward;
            float num11 = float.NegativeInfinity;
            int wiredFace = 0;
            for (int face = 0; face < 6; ++face)
            {
                //将0-6转换成一个面的定义，然后和面向进行比较，最终返回面向的值
                float num21 = Vector3.Dot(CellFace.FaceToVector3(face), forward);
                if (num21 > (double)num11)
                {
                    num11 = num21;
                    wiredFace = face;
                }
            }
            //获取上望下望时的视线朝向rotation
            float x1 = Vector3.Dot(forward, -Vector3.UnitZ);
            float x2 = Vector3.Dot(forward, -Vector3.UnitX);
            float x3 = Vector3.Dot(forward, Vector3.UnitZ);
            float x4 = Vector3.Dot(forward, Vector3.UnitX);
            int rotation = 0;
            if (x1 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 2;
            }
            else if (x2 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 3;
            }
            else if (x3 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 0;
            }
            else if (x4 == (double)MathUtils.Max(x1, x2, x3, x4))
            {
                rotation = 1;
            }

            Point3 numS, numE;
            numS.X = Start.Y;
            numS.Y = Start.Z;
            numS.Z = Start.X;
            numE.X = numH;
            numE.Y = numW;
            numE.Z = numL;
            //测试方位,123为xyz,0为平视，45仰视俯视，123先循环x，再y，再z
            int T_VEC;
            //numE.X为最外层
            switch (wiredFace)
            {
                case 0:
                    numS.X = Start.Y; numE.X = Start.Y + numH;
                    numS.Y = Start.Z; numE.Y = Start.Z + numW;

                    numS.Z = Start.X; numE.Z = Start.X + numL;
                    if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                    T_VEC = 2310;
                    break;
                case 1:
                    numS.X = Start.Y; numE.X = Start.Y + numH;
                    numS.Y = Start.X; numE.Y = Start.X + numW;
                    numS.Z = Start.Z; numE.Z = Start.Z - numL;
                    if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z + numL; }
                    T_VEC = 2130;
                    break;
                case 2:
                    numS.X = Start.Y; numE.X = Start.Y + numH;
                    numS.Y = Start.Z; numE.Y = Start.Z - numW;
                    numS.Z = Start.X; numE.Z = Start.X - numL;
                    if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X + numL; }
                    T_VEC = 2310;
                    break;
                case 3:
                    numS.X = Start.Y; numE.X = Start.Y + numH;
                    numS.Y = Start.X; numE.Y = Start.X - numW;
                    numS.Z = Start.Z; numE.Z = Start.Z + numL;
                    if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z - numL; }
                    T_VEC = 2130;
                    break;
                case 4:
                    switch (rotation)
                    {
                        case 0:
                            numS.X = Start.Z; numE.X = Start.Z - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.X; numE.Z = Start.X + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                            T_VEC = 3214;
                            break;
                        case 1:
                            numS.X = Start.X; numE.X = Start.X - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.Z; numE.Z = Start.Z - numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z + numL; }
                            T_VEC = 1234;
                            break;
                        case 2:
                            numS.X = Start.Z; numE.X = Start.Z + numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.X; numE.Z = Start.X - numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X + numL; }
                            T_VEC = 3214;
                            break;
                        case 3:
                            numS.X = Start.X; numE.X = Start.X + numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.Z; numE.Z = Start.Z + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z - numL; }
                            T_VEC = 1234;
                            break;
                        default:
                            numS.X = Start.Z; numE.X = Start.Z - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.X; numE.Z = Start.X + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                            T_VEC = 3214;
                            break;
                    }
                    break;
                case 5:
                    switch (rotation)
                    {
                        case 0:
                            numS.X = Start.Z; numE.X = Start.Z + numH;
                            numS.Y = Start.Y; numE.Y = Start.Y - numW;
                            numS.Z = Start.X; numE.Z = Start.X + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                            T_VEC = 3214;
                            break;
                        case 1:
                            numS.X = Start.X; numE.X = Start.X + numH;
                            numS.Y = Start.Y; numE.Y = Start.Y - numW;
                            numS.Z = Start.Z; numE.Z = Start.Z - numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z + numL; }
                            T_VEC = 1234;
                            break;
                        case 2:
                            numS.X = Start.Z; numE.X = Start.Z - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y - numW;
                            numS.Z = Start.X; numE.Z = Start.X - numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X + numL; }
                            T_VEC = 3214;
                            break;
                        case 3:
                            numS.X = Start.X; numE.X = Start.X - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y - numW;
                            numS.Z = Start.Z; numE.Z = Start.Z + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.Z; numE.Z = Start.Z - numL; }
                            T_VEC = 1234;
                            break;
                        default:
                            numS.X = Start.Z; numE.X = Start.Z - numH;
                            numS.Y = Start.Y; numE.Y = Start.Y + numW;
                            numS.Z = Start.X; numE.Z = Start.X + numL;
                            if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                            T_VEC = 3214;
                            break;
                    }
                    break;
                default:
                    numS.X = Start.Y; numE.X = Start.Y + numH;
                    numS.Y = Start.Z; numE.Y = Start.Z + numW;
                    numS.Z = Start.X; numE.Z = Start.X + numL;
                    if (MirrorBlockBehavior.IsMirror) { numS.Z = Start.X; numE.Z = Start.X - numL; }
                    T_VEC = 2310;
                    break;

            }
            switch (wiredFace)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    numFl = 0;
                    break;
                case 4:
                    numFl = 1;
                    break;
                case 5:
                    numFl = 2;
                    break;
                default:
                    numFl = 0;
                    break;
            }
            switch (numR / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    numF = 0;
                    break;
                case 4:
                    numF = 1;
                    break;
                case 5:
                    numF = 2;
                    break;
                default:
                    numF = 0;
                    break;
            }
            _ = Rotouzi[numF, numR % 10];
            _ = Rotouzi[numFl, rotation];
            _ = Romian[numF, numR % 10];
            _ = Romian[numFl, rotation];
            *//*   DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported1", numrotouzi.ToString() + "p" + numrotouzil.ToString(), "OK", null, null));
             *//*
            for (int numo = numS.X, X, Y, Z; (numS.X <= numE.X && numo <= numE.X) || (numS.X > numE.X && numo >= numE.X);)
            {
                for (int numm = numS.Y; (numS.Y <= numE.Y && numm <= numE.Y) || (numS.Y > numE.Y && numm >= numE.Y);)
                {
                    for (int numi = numS.Z; (numS.Z <= numE.Z && numi <= numE.Z) || (numS.Z > numE.Z && numi >= numE.Z);)
                    {
                        //数据回代
                        switch (T_VEC / 10)
                        {
                            case 123: X = numo; Y = numm; Z = numi; break;
                            case 132: X = numo; Z = numm; Y = numi; break;
                            case 213: Y = numo; X = numm; Z = numi; break;
                            case 231: Y = numo; Z = numm; X = numi; break;
                            case 312: Z = numo; X = numm; Y = numi; break;
                            case 321: Z = numo; Y = numm; X = numi; break;
                            default:

                                creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogp1"), num1), Color.LightYellow, true, true);

                                *//*switch (CreatorAPI.Language)
                                {
                                    case Language.zh_CN:
                                        creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴失败，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                                        break;
                                    case Language.en_US:
                                        creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Paste failed for a total of {0} blocks", (object)num1), Color.LightYellow, true, true);
                                        break;
                                    default:
                                        creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴失败，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                                        break;
                                }*//*
                                return;
                        }
                        //是否启动创世神
                        if (!creatorAPI.launch)
                        {
                            return;
                        }
                        //检查方块是否为空气
                        int num5 = engineBinaryReader.ReadInt32();
                        if (creatorAPI.AirIdentify || Terrain.ExtractContents(num5) != 0)
                        {
                            //准备完成
                            _ = CreatorModAPI.SetFaceAndRotate.GetFace(num5);
                            _ = CreatorModAPI.SetFaceAndRotate.GetRotate(num5);
                            //    DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Exported="+CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(6012345,0), "V1(Load)=" + numR.ToString()+"\n"+ "V2(local)=" + (wiredFace * 10 + rotation).ToString() + "\n" + "GetFace=" + CreatorModAPI.SetFaceAndRotate.GetFace(num5).ToString() + "\n" + CreatorModAPI.SetFaceAndRotate.GetFaceVector(num5, numR, wiredFace * 10 + rotation, CreatorModAPI.SetFaceAndRotate.GetFace(num5)).ToString(), "OK", null, null));
                            num5 = CreatorModAPI.SetFaceAndRotate.SetFace(num5, CreatorModAPI.SetFaceAndRotate.GetFaceVector(num5, numR, wiredFace * 10 + rotation, CreatorModAPI.SetFaceAndRotate.GetFace(num5)));
                            //   int tb = CreatorModAPI.SetFaceAndRotate.GetFace(num5);

                            num5 = CreatorModAPI.SetFaceAndRotate.SetRotate(num5, CreatorModAPI.SetFaceAndRotate.GetRotateVector(num5, numR, wiredFace * 10 + rotation, CreatorModAPI.SetFaceAndRotate.GetRotate(num5)));
                            _ = CreatorModAPI.SetFaceAndRotate.GetRotate(num5);
                            //DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Exported", "copy 前" + numR + "+" + rotation + "+" + Ro_a + "copy 后" + Ro_b, "OK", null, null));
                            //    DialogsManager.ShowDialog(componentPlayer.GuiWidget, new MessageDialog("Exported"+ CreatorModAPI.SetFaceAndRotate.GetFaceVector(num5, numR, wiredFace * 10 + rotation, CreatorModAPI.SetFaceAndRotate.GetFace(num5)), "V1(Load替换前)=" + numR.ToString() + "\n" + "V2(local替换后)=" + (wiredFace * 10 + rotation).ToString() + "\n" + "GetFace（替换前）="+ta + "\n" + "GetFace（替换后）=" + tb, "OK", null, null));



                            creatorAPI.CreateBlock(X, Y, Z, num5, chunkData);

                            switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(X, Y, Z)))
                            {
                                case 186:
                                    engineBinaryReader.ReadChar();
                                    //写入得到的M板数据
                                    MemoryBankData memoryBankData1 = new MemoryBankData();
                                    memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                    //写入得到的M板数据
                                    GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(X, Y, Z), memoryBankData1);
                                    break;
                                case 188:
                                    engineBinaryReader.ReadChar();
                                    //写入得到的M板数据
                                    TruthTableData TruthTableBankData1 = new TruthTableData();
                                    TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                    //写入得到的M板数据
                                    GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(X, Y, Z), TruthTableBankData1);
                                    break;
                                case 211:
                                case 210:
                                case 97:
                                case 98:
                                    engineBinaryReader.ReadChar();
                                    SignData SignBankData = new SignData();
                                    //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                    SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                    SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                    SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                    SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                    SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                    SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                    SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                    SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                    SignBankData.Url = engineBinaryReader.ReadString();
                                    GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(X, Y, Z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                    break;
                                case 184:
                                    //粘贴出来方块的内容值，


                                    if (engineBinaryReader.ReadBoolean())
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemElectricity>(true).WritePersistentVoltage(new Point3(X, Y, Z), (float)engineBinaryReader.ReadDouble());
                                        //   engineBinaryReader.ReadBoolean();
                                    }

                                    //     FurnitureDesign furnitureDesign = GameManager.Project.FindSubsystem<SubsystemFurnitureBlockBehavior>(true).GetDesign(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z));
                                    //   engineBinaryWriter.WriteStruct<FurnitureDesign>(furnitureDesign);
                                    break;
                                default: break;
                            }

                            ++num1;
                        }
                        if (numS.Z <= numE.Z)
                        {
                            ++numi;
                        }
                        else if (numS.Z > numE.Z)
                        {
                            --numi;
                        }
                    }
                    if (numS.Y <= numE.Y)
                    {
                        ++numm;
                    }
                    else if (numS.Y > numE.Y)
                    {
                        --numm;
                    }
                }
                if (numS.X <= numE.X)
                {
                    ++numo;
                }
                else if (numS.X > numE.X)
                {
                    --numo;
                }
            }
            //移除读取器，关闭流
            buffer = stream.Length;
            engineBinaryReader.Dispose();
            stream.Dispose();
            chunkData.Render();

            creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("capadialogp2"), num1, buffer), Color.LightYellow, true, true);

            *//*  switch (CreatorAPI.Language)
              {
                  case Language.zh_CN:
                      creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块\n文件长度：{1}", (object)num1, buffer), Color.LightYellow, true, true);
                      break;
                  case Language.en_US:
                      creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Paste successfully for a total of {0} blocks\nFile size:{1}", (object)num1, buffer), Color.LightYellow, true, true);
                      break;
                  default:
                      creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块\n文件长度：{1}", (object)num1, buffer), Color.LightYellow, true, true);
                      break;
              }*//*

        }


        *//* public static void PasetData(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
         {
             ChunkData chunkData = new ChunkData(creatorAPI);
             creatorAPI.revokeData = new ChunkData(creatorAPI);
             ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
             Stream stream = new FileStream(path, FileMode.Open).CreateStream();
             EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream);
             MemoryBankData memoryData = new MemoryBankData();
             // int strLene = 0;
             //读取得到的M板数据
             int num1 = 0;
             char xline = engineBinaryReader.ReadChar();//读取P
             int num2 = Math.Abs(engineBinaryReader.ReadInt32());//读取X长
             int num3 = Math.Abs(engineBinaryReader.ReadInt32());//读取Y长
             int num4 = Math.Abs(engineBinaryReader.ReadInt32());//读取Z长
             DialogsManager.ShowDialog(creatorAPI.componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exported2", num2.ToString() + "," + num3.ToString() + "," + num4.ToString(), "OK", null, null));
             for (int index1 = 0; index1 <= num2; ++index1)
             {
                 for (int index2 = 0; index2 <= num3; ++index2)
                 {
                     for (int index3 = 0; index3 <= num4; ++index3)
                     {
                         if (!creatorAPI.launch) return;
                         int num5 = engineBinaryReader.ReadInt32();
                         if (creatorAPI.AirIdentify || Terrain.ExtractContents(num5) != 0)
                         {
                             int y = Start.Y + index2;
                             if (Start.X <= End.X && Start.Z <= End.Z)
                             {
                                 int x = Start.X + index1;
                                 int z = Start.Z + index3;
                                 if (!creatorAPI.pasteLimit || End.X >= x && End.Z >= z)
                                 {
                                     creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                     switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                     {
                                         case 186:
                                             engineBinaryReader.ReadChar();
                                             //写入得到的M板数据
                                             MemoryBankData memoryBankData1 = new MemoryBankData();
                                             memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                             //写入得到的M板数据
                                             GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                             break;
                                         case 188:
                                             engineBinaryReader.ReadChar();
                                             //写入得到的M板数据
                                             TruthTableData TruthTableBankData1 = new TruthTableData();
                                             TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                             //写入得到的M板数据
                                             GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                             break;
                                         case 211:
                                         case 210:
                                         case 97:
                                         case 98:
                                             engineBinaryReader.ReadChar();
                                             SignData SignBankData = new SignData();
                                             //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                             SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                             SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                             SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                             SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                             SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                             SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                             SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                             SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                             SignBankData.Url = engineBinaryReader.ReadString();
                                             GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                             break;
                                         default: break;
                                     }
                                 }
                                 else
                                     continue;
                             }
                             else if (Start.X >= End.X && Start.Z <= End.Z)
                             {
                                 if (!creatorAPI.pasteRotate)
                                 {
                                     int x = Start.X - num2 + index1;
                                     int z = Start.Z + index3;
                                     if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && End.Z >= z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }
                                 else
                                 {
                                     int x = Start.X - index3;
                                     int z = Start.Z + index1;
                                     if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && End.Z >= z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }
                             }
                             else if (Start.X >= End.X && Start.Z >= End.Z)
                             {
                                 if (!creatorAPI.pasteRotate)
                                 {
                                     int x = Start.X - num2 + index1;
                                     int z = Start.Z - num4 + index3;
                                     if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && Start.Z <= z + Start.Z - End.Z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }
                                 else
                                 {
                                     int x = Start.X - index1;
                                     int z = Start.Z - index3;
                                     if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && Start.Z <= z + Start.Z - End.Z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }
                             }
                             else if (Start.X <= End.X && Start.Z >= End.Z)
                             {
                                 if (!creatorAPI.pasteRotate)
                                 {
                                     int x = Start.X + index1;
                                     int z = Start.Z - num4 + index3;
                                     if (!creatorAPI.pasteLimit || End.X >= x && Start.Z <= z + Start.Z - End.Z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }
                                 else
                                 {
                                     int x = Start.X + index3;
                                     int z = Start.Z - index1;
                                     if (!creatorAPI.pasteLimit || End.X >= x && Start.Z <= z + Start.Z - End.Z)
                                     {
                                         creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                         switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                         {
                                             case 186:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 MemoryBankData memoryBankData1 = new MemoryBankData();
                                                 memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                 break;
                                             case 188:
                                                 engineBinaryReader.ReadChar();
                                                 //写入得到的M板数据
                                                 TruthTableData TruthTableBankData1 = new TruthTableData();
                                                 TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                 //写入得到的M板数据
                                                 GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                 break;
                                             case 211:
                                             case 210:
                                             case 97:
                                             case 98:
                                                 engineBinaryReader.ReadChar();
                                                 SignData SignBankData = new SignData();
                                                 //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                 SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                 SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                 SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                 SignBankData.Url = engineBinaryReader.ReadString();
                                                 GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                 break;
                                             default: break;
                                         }
                                     }
                                     else
                                         continue;
                                 }

                             }

                             ++num1;
                         }

                     }
                 }
             }
             engineBinaryReader.Dispose();
             stream.Dispose();
             chunkData.Render();

             switch (CreatorAPI.Language)
             {
                 case Language.zh_CN:
                     creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                     break;
                 case Language.en_US:
                     creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Paste successfully for a total of {0} blocks", (object)num1), Color.LightYellow, true, true);
                     break;
                 default:
                     creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                     break;
             }

         }
 */

/*
        public static void MirrorData(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
        {
            ChunkData chunkData = new ChunkData(creatorAPI);
            creatorAPI.revokeData = new ChunkData(creatorAPI);
            ComponentPlayer componentPlayer = creatorAPI.componentMiner.ComponentPlayer;
            Stream stream = new FileStream(path, FileMode.Open).CreateStream();
            EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream);
            int num1 = 0;
            char xline = engineBinaryReader.ReadChar();//读取P
            int num2 = engineBinaryReader.ReadInt32();
            int num3 = engineBinaryReader.ReadInt32();
            int num4 = engineBinaryReader.ReadInt32();
            for (int index1 = num2; index1 >= 0; --index1)
            {
                for (int index2 = 0; index2 <= num3; ++index2)
                {
                    for (int index3 = 0; index3 <= num4; ++index3)
                    {
                        if (!creatorAPI.launch)
                            return;
                        int num5 = engineBinaryReader.ReadInt32();
                        if (creatorAPI.AirIdentify || Terrain.ExtractContents(num5) != 0)
                        {
                            int y = Start.Y + index2;
                            if (Start.X <= End.X && Start.Z <= End.Z)
                            {
                                int x = Start.X + index1;
                                int z = Start.Z + index3;
                                if (!creatorAPI.pasteLimit || End.X >= x && End.Z >= z)
                                {
                                    creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                    switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                    {
                                        case 186:
                                            engineBinaryReader.ReadChar();
                                            //写入得到的M板数据
                                            MemoryBankData memoryBankData1 = new MemoryBankData();
                                            memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                            //写入得到的M板数据
                                            GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                            break;
                                        case 188:
                                            engineBinaryReader.ReadChar();
                                            //写入得到的M板数据
                                            TruthTableData TruthTableBankData1 = new TruthTableData();
                                            TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                            //写入得到的M板数据
                                            GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                            break;
                                        case 211:
                                        case 210:
                                        case 97:
                                        case 98:
                                            engineBinaryReader.ReadChar();
                                            SignData SignBankData = new SignData();
                                            //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                            SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                            SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                            SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                            SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                            SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                            SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                            SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                            SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                            SignBankData.Url = engineBinaryReader.ReadString();
                                            GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                            break;
                                        default: break;
                                    }
                                }
                                else
                                    continue;
                            }
                            else if (Start.X >= End.X && Start.Z <= End.Z)
                            {
                                if (!creatorAPI.pasteRotate)
                                {
                                    int x = Start.X - num2 + index1;
                                    int z = Start.Z + index3;
                                    if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && End.Z >= z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                else
                                {
                                    int x = Start.X - index3;
                                    int z = Start.Z + index1;
                                    if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && End.Z >= z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                            }
                            else if (Start.X >= End.X && Start.Z >= End.Z)
                            {
                                if (!creatorAPI.pasteRotate)
                                {
                                    int x = Start.X - num2 + index1;
                                    int z = Start.Z - num4 + index3;
                                    if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && Start.Z <= z + Start.Z - End.Z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                else
                                {
                                    int x = Start.X - index1;
                                    int z = Start.Z - index3;
                                    if (!creatorAPI.pasteLimit || Start.X <= x + Start.X - End.X && Start.Z <= z + Start.Z - End.Z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                            }
                            else if (Start.X <= End.X && Start.Z >= End.Z)
                            {
                                if (!creatorAPI.pasteRotate)
                                {
                                    int x = Start.X + index1;
                                    int z = Start.Z - num4 + index3;
                                    if (!creatorAPI.pasteLimit || End.X >= x && Start.Z <= z + Start.Z - End.Z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                else
                                {
                                    int x = Start.X + index3;
                                    int z = Start.Z - index1;
                                    if (!creatorAPI.pasteLimit || End.X >= x && Start.Z <= z + Start.Z - End.Z)
                                    {
                                        creatorAPI.CreateBlock(x, y, z, num5, chunkData);
                                        switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z)))
                                        {
                                            case 186:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                MemoryBankData memoryBankData1 = new MemoryBankData();
                                                memoryBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<MemoryBankData>>(true).SetBlockData(new Point3(x, y, z), memoryBankData1);
                                                break;
                                            case 188:
                                                engineBinaryReader.ReadChar();
                                                //写入得到的M板数据
                                                TruthTableData TruthTableBankData1 = new TruthTableData();
                                                TruthTableBankData1.LoadString(engineBinaryReader.ReadString());
                                                //写入得到的M板数据
                                                GameManager.Project.FindSubsystem<SubsystemEditableItemBehavior<TruthTableData>>(true).SetBlockData(new Point3(x, y, z), TruthTableBankData1);
                                                break;
                                            case 211:
                                            case 210:
                                            case 97:
                                            case 98:
                                                engineBinaryReader.ReadChar();
                                                SignData SignBankData = new SignData();
                                                //engineBinaryWriter.Write(memoryBankData.SaveString().Length);
                                                SignBankData.Lines[0] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[0] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[1] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[1] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[2] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[2] = engineBinaryReader.ReadColor();
                                                SignBankData.Lines[3] = engineBinaryReader.ReadString();
                                                SignBankData.Colors[3] = engineBinaryReader.ReadColor();
                                                SignBankData.Url = engineBinaryReader.ReadString();
                                                GameManager.Project.FindSubsystem<SubsystemSignBlockBehavior>(true).SetSignData(new Point3(x, y, z), SignBankData.Lines, SignBankData.Colors, SignBankData.Url);
                                                break;
                                            default: break;
                                        }
                                    }
                                    else
                                        continue;
                                }
                            }
                            ++num1;
                        }
                    }
                }
            }
            engineBinaryReader.Dispose();
            stream.Dispose();
            chunkData.Render();
            switch (CreatorAPI.Language)
            {
                case Language.zh_CN:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                    break;
                case Language.en_US:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("Paste successfully for a total of {0} blocks", (object)num1), Color.LightYellow, true, true);
                    break;
                default:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num1), Color.LightYellow, true, true);
                    break;
            }


        }
*//*
public static void ExportCopywMod2(string path, string exportFile)
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
    string s = str + string.Format("{0}\n{1}\n{2}", num1, num2, num3);
    for (int index1 = 0; index1 <= num1; ++index1)
    {
        for (int index2 = 0; index2 <= num2; ++index2)
        {
            for (int index3 = 0; index3 <= num3; ++index3)
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

public static void ImportCopywMod2(string path, string importFile)
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

public static void ExportCopywMod(string path, string exportFile)
{
    if (path.IsFileInUse())
    {
        throw new FileLoadException("capadialogex");
    }
    //throw new FileLoadException("文件被占用");
    Stream stream1 = new FileStream(exportFile, FileMode.Create);
    Stream stream2 = File.Open(path, FileMode.Open);
    EngineBinaryReader engineBinaryReader = new EngineBinaryReader(stream2);
    string str = "";
    int num1 = engineBinaryReader.ReadInt32();
    int num2 = engineBinaryReader.ReadInt32();
    int num3 = engineBinaryReader.ReadInt32();
    string s = str + string.Format("{0},{1},{2}", num1, num2, num3);
    for (int index1 = 0; index1 <= num1; ++index1)
    {
        for (int index2 = 0; index2 <= num2; ++index2)
        {
            for (int index3 = 0; index3 <= num3; ++index3)
            {
                int num4 = engineBinaryReader.ReadInt32();
                if (Terrain.ExtractContents(num4) != 0)
                {
                    s += string.Format("\n{0},{1},{2},{3}", index1, index2, index3, num4);
                }
            }
        }
    }
    stream1.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
    engineBinaryReader.Dispose();
    stream2.Dispose();
    stream1.Dispose();
}

public static void ImportCopywMod(string path, string importFile)
{
    List<string> stringList = new List<string>();
    FileStream fileStream1 = File.OpenRead(importFile);
    StreamReader streamReader = new StreamReader(fileStream1);
    string end = streamReader.ReadToEnd();
    streamReader.Dispose();
    fileStream1.Dispose();
    string str1 = end;
    char[] separator = new char[1] { '\n' };
    foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
        stringList.Add(str2);
    }

    FileStream fileStream2 = new FileStream(path, FileMode.Create);
    EngineBinaryWriter engineBinaryWriter = new EngineBinaryWriter(fileStream2, true);
    string[] strArray1 = stringList[0].Split(new char[1]
    {
','
    }, StringSplitOptions.RemoveEmptyEntries);
    int num1 = int.Parse(strArray1[0]);
    int num2 = int.Parse(strArray1[1]);
    int num3 = int.Parse(strArray1[2]);
    stringList.RemoveAt(0);
    engineBinaryWriter.Write(num1);
    engineBinaryWriter.Write(num2);
    engineBinaryWriter.Write(num3);
    for (int index1 = 0; index1 <= num1; ++index1)
    {
        for (int index2 = 0; index2 <= num2; ++index2)
        {
            for (int index3 = 0; index3 <= num3; ++index3)
            {
                int num4 = 0;
                for (int index4 = 0; index4 < stringList.Count; ++index4)
                {
                    string[] strArray2 = stringList[index4].Split(new char[1]
                    {
        ','
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.Parse(strArray2[0]) == index1 && int.Parse(strArray2[1]) == index2 && int.Parse(strArray2[2]) == index3)
                    {
                        num4 = int.Parse(strArray2[3]);
                        stringList.RemoveAt(index4);
                    }
                }
                engineBinaryWriter.Write(num4);
            }
        }
    }
    engineBinaryWriter.Dispose();
    fileStream2.Dispose();
}
*//*
        public static void CreateSpecialCopy(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
        {
            CreatorMain.Math.StartEnd(ref Start, ref End);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            List<Entity> entityList1 = new List<Entity>();
            List<Entity> entityList2 = new List<Entity>();
            string str1 = "" + string.Format("{0},{1},{2}", (object)(Start.X - End.X), (object)(Start.Y - End.Y), (object)(Start.Z - End.Z));
            for (int x = End.X; x <= Start.X; ++x)
            {
                for (int y = End.Y; y <= Start.Y; ++y)
                {
                    for (int z = End.Z; z <= Start.Z; ++z)
                    {
                        if (GameManager.Project.FindSubsystem<SubsystemBlockEntities>().GetBlockEntity(x, y, z) != null)
                            entityList1.Add(GameManager.Project.FindSubsystem<SubsystemBlockEntities>().GetBlockEntity(x, y, z).Entity);
                        str1 = str1 + "|" + GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(x, y, z).ToString();
                    }
                }
            }
            foreach (ComponentCreature creature in GameManager.Project.FindSubsystem<SubsystemCreatureSpawn>(true).Creatures)
            {
                if (creature.DisplayName != "Male Player" && creature.DisplayName != "Female Player")
                {
                    Vector3 position = creature.ComponentBody.Position;
                    if ((double)position.X <= (double)Start.X && (double)position.X >= (double)End.X && ((double)position.Y <= (double)Start.Y && (double)position.Y >= (double)End.Y) && ((double)position.Z <= (double)Start.Z && (double)position.Z >= (double)End.Z))
                        entityList2.Add(creature.Entity);
                }
            }
            string str2 = str1 + "\nBlockEntity";
            foreach (Entity entity in entityList1)
            {
                ComponentBlockEntity component1 = entity.FindComponent<ComponentBlockEntity>();
                if (component1 != null)
                {
                    Point3 coordinates = component1.Coordinates;
                    string str3;
                    if (entity.FindComponent<ComponentDispenser>() != null)
                        str3 = "Dispenser";
                    else if (entity.FindComponent<ComponentFurnace>() != null)
                    {
                        str3 = "Furnace";
                    }
                    else
                    {
                        if (entity.FindComponent<ComponentCraftingTable>() == null)
                            throw new Exception("检测到一个无法识别的方块实体，现在电路元件暂时还不能识别");
                        str3 = "CraftingTable";
                    }
                    str2 += string.Format("|{0}\t{1},{2},{3}", (object)str3, (object)(coordinates.X - End.X), (object)(coordinates.Y - End.Y), (object)(coordinates.Z - End.Z));
                    ComponentInventoryBase component2 = entity.FindComponent<ComponentInventoryBase>();
                    if (component2 != null)
                    {
                        for (int slotIndex = 0; slotIndex < component2.SlotsCount; ++slotIndex)
                        {
                            int slotValue = component2.GetSlotValue(slotIndex);
                            int slotCount = component2.GetSlotCount(slotIndex);
                            if (slotValue != 0 && slotCount > 0)
                                str2 += string.Format("\t{0}:{1}", (object)slotValue, (object)slotCount);
                        }
                    }
                }
            }
            string s = str2 + "\nEntity";
            foreach (Entity entity in entityList2)
            {
                ComponentCreature component1 = entity.FindComponent<ComponentCreature>();
                Vector3 position = component1.ComponentBody.Position;
                s += string.Format("|{0}\t{1},{2},{3}", (object)component1.DisplayName, (object)(float)((double)position.X - (double)End.X), (object)(float)((double)position.Y - (double)End.Y), (object)(float)((double)position.Z - (double)End.Z));
                ComponentInventoryBase component2 = entity.FindComponent<ComponentInventoryBase>();
                if (component2 != null)
                {
                    for (int slotIndex = 0; slotIndex < component2.SlotsCount; ++slotIndex)
                    {
                        int slotValue = component2.GetSlotValue(slotIndex);
                        int slotCount = component2.GetSlotCount(slotIndex);
                        if (slotValue != 0 && slotCount > 0)
                            s += string.Format("\t{0}:{1}", (object)slotValue, (object)slotCount);
                    }
                }
            }
            fileStream.Write(Encoding.UTF8.GetBytes(s), 0, Encoding.UTF8.GetBytes(s).Length);
            fileStream.Dispose();
            switch (CreatorAPI.Language)
            {
                case Language.zh_CN:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage("复制成功", Color.LightYellow, true, true);
                    break;
                case Language.en_US:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage("复制成功", Color.LightYellow, true, true);
                    break;
                default:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage("复制成功", Color.LightYellow, true, true);
                    break;
            }


        }*/
/*
        public static void SpecialPasetData(CreatorAPI creatorAPI, string path, Point3 Start, Point3 End)
        {
            ChunkData chunkData = new ChunkData(creatorAPI);
            creatorAPI.revokeData = new ChunkData(creatorAPI);
            FileStream fileStream = File.OpenRead(path);
            CreatorMain.Math.StartEnd(ref Start, ref End);
            StreamReader streamReader = new StreamReader((Stream)fileStream);
            string end = streamReader.ReadToEnd();
            streamReader.Dispose();
            fileStream.Dispose();
            string[] strArray1 = end.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray2 = strArray1[0].Split(new char[1]
            {
        '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            string[] strArray3 = strArray2[0].Split(new char[1]
            {
        ','
            }, StringSplitOptions.RemoveEmptyEntries);
            int num1 = int.Parse(strArray3[0]);
            int num2 = int.Parse(strArray3[1]);
            int num3 = int.Parse(strArray3[2]);
            int num4 = 0;
            for (int index1 = 0; index1 <= num1; ++index1)
            {
                for (int index2 = 0; index2 <= num2; ++index2)
                {
                    for (int index3 = 0; index3 <= num3; ++index3)
                    {
                        creatorAPI.CreateBlock(End.X + index1, End.Y + index2, End.Z + index3, int.Parse(strArray2[num4 + 1]), chunkData);
                        ++num4;
                    }
                }
            }
            chunkData.Render();
            string[] strArray4 = strArray1[1].Split(new char[1]
            {
        '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index1 = 1; index1 < strArray4.Length; ++index1)
            {
                string[] strArray5 = strArray4[index1].Split(new char[1]
                {
          '\t'
                }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArray6 = strArray5[1].Split(new char[1]
                {
          ','
                }, StringSplitOptions.RemoveEmptyEntries);
                DatabaseObject databaseObject = GameManager.Project.GameDatabase.Database.FindDatabaseObject(strArray5[0], GameManager.Project.GameDatabase.EntityTemplateType, true);
                ValuesDictionary valuesDictionary = new ValuesDictionary();
                valuesDictionary.PopulateFromDatabaseObject(databaseObject);
                valuesDictionary.GetValue<ValuesDictionary>("BlockEntity").SetValue<Point3>("Coordinates", new Point3(int.Parse(strArray6[0]) + End.X, int.Parse(strArray6[1]) + End.Y, int.Parse(strArray6[2]) + End.Z));
                Entity entity = GameManager.Project.CreateEntity(valuesDictionary);
                ComponentInventoryBase component = entity.FindComponent<ComponentInventoryBase>();
                if (component != null)
                {
                    int index2 = 2;
                    int slotIndex = 0;
                    while (index2 < strArray5.Length)
                    {
                        string[] strArray7 = strArray5[index2].Split(new char[1]
                        {
              ':'
                        }, StringSplitOptions.RemoveEmptyEntries);
                        component.AddSlotItems(slotIndex, int.Parse(strArray7[0]), int.Parse(strArray7[1]));
                        ++index2;
                        ++slotIndex;
                    }
                }
                GameManager.Project.AddEntity(entity);
            }
            string[] strArray8 = strArray1[2].Split(new char[1]
            {
        '|'
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index1 = 1; index1 < strArray8.Length; ++index1)
            {
                string[] strArray5 = strArray8[index1].Split(new char[1]
                {
          '\t'
                }, StringSplitOptions.RemoveEmptyEntries);
                string[] strArray6 = strArray5[1].Split(new char[1]
                {
          ','
                }, StringSplitOptions.RemoveEmptyEntries);
                Entity entity = DatabaseManager.CreateEntity(GameManager.Project, strArray5[0], true);
                entity.FindComponent<ComponentBody>(true).Position = new Vector3(float.Parse(strArray6[0]) + (float)End.X, float.Parse(strArray6[1]) + (float)End.Y, float.Parse(strArray6[2]) + (float)End.Z);
                entity.FindComponent<ComponentBody>(true).Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, 1.2f);
                entity.FindComponent<ComponentSpawn>(true).SpawnDuration = 0.25f;
                ComponentInventoryBase component = entity.FindComponent<ComponentInventoryBase>();
                if (component != null)
                {
                    int index2 = 2;
                    int slotIndex = 0;
                    while (index2 < strArray5.Length)
                    {
                        string[] strArray7 = strArray5[index2].Split(new char[1]
                        {
              ':'
                        }, StringSplitOptions.RemoveEmptyEntries);
                        component.AddSlotItems(slotIndex, int.Parse(strArray7[0]), int.Parse(strArray7[1]));
                        ++index2;
                        ++slotIndex;
                    }
                }
                GameManager.Project.AddEntity(entity);
            }
            switch (CreatorAPI.Language)
            {
                case Language.zh_CN:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num4), Color.LightYellow, true, true);
                    break;
                case Language.en_US:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num4), Color.LightYellow, true, true);
                    break;
                default:
                    creatorAPI.componentMiner.ComponentPlayer.ComponentGui.DisplaySmallMessage(string.Format("粘贴成功，共{0}个方块", (object)num4), Color.LightYellow, true, true);
                    break;
            }

        }*//*
}
}
*/