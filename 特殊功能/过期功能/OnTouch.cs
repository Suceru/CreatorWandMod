using Engine;
using Game;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public static class OnTouch
    {
        public static object SleepTime
        {
            get;
            private set;
        }

        public static bool Touch(CreatorAPI creatorAPI, Point3 position)
        {
            ComponentPlayer player = creatorAPI.componentMiner.ComponentPlayer;
            GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValue(position.X, position.Y, position.Z);
            if (creatorAPI.oneKeyGeneration)
            {
                if (creatorAPI.onekeyType == CreatorAPI.OnekeyType.Build)
                {
                    if (File.Exists(CreatorMain.OneKeyFile))
                    {
                        Task.Run(delegate
                        {
                            OnekeyGeneration.GenerationData(creatorAPI, CreatorMain.OneKeyFile, position);
                        });
                    }
                    else
                    {
                        player.ComponentGui.DisplaySmallMessage("未发现一键生成缓存文件，目录:" + CreatorMain.OneKeyFile + "\n请变更一键生成类型或关闭该功能", Color.Red, blinking: true, playNotificationSound: true);
                    }
                }

                return false;
            }

            if (!creatorAPI.ClearBlock)
            {
                return true;
            }

            Task.Run(delegate
            {
                creatorAPI.revokeData = new ChunkData(creatorAPI);
                int num = 0;
                List<Point3> list = new List<Point3>();
                List<Point3> list2 = new List<Point3>();
                list.Add(position);
                while (list.Count > 0)
                {
                    foreach (Point3 item2 in list)
                    {
                        if (!creatorAPI.launch)
                        {
                            return;
                        }

                        if (creatorAPI.revokeData != null && creatorAPI.revokeData.GetChunk(item2.X, item2.Z) == null)
                        {
                            creatorAPI.revokeData.CreateChunk(item2.X, item2.Z, unLimited: true);
                        }

                        creatorAPI.SetBlock(item2.X, item2.Y, item2.Z, 0);
                        num++;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                for (int k = -1; k <= 1; k++)
                                {
                                    if (item2.Y + j <= 255)
                                    {
                                        int cellContentsFast = GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellContentsFast(item2.X + i, item2.Y + j, item2.Z + k);
                                        if ((uint)cellContentsFast > 1u && MathUtils.Abs(i) + MathUtils.Abs(j) + MathUtils.Abs(k) <= 1)
                                        {
                                            Point3 item = new Point3(item2.X + i, item2.Y + j, item2.Z + k);
                                            if (!list.Contains(item) && !list2.Contains(item))
                                            {
                                                list2.Add(item);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    list = list2;
                    list2 = new List<Point3>();
                }

                player.ComponentGui.DisplaySmallMessage($"操作成功，共清除{num}个方块", Color.Yellow, blinking: true, playNotificationSound: true);
            });
            return false;
        }
    }
}

/*一键生成*/
/*namespace CreatorModAPI-=  public static class OnTouch*//*
using Engine;
using Game;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public static class OnTouch
    {
        public static object SleepTime { get; private set; }

        public static bool Touch(CreatorAPI creatorAPI, Point3 position)
        {
            ComponentPlayer player = creatorAPI.componentMiner.ComponentPlayer;
            GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValue(position.X, position.Y, position.Z);
            if (creatorAPI.oneKeyGeneration)
            {
                if (creatorAPI.onekeyType == CreatorAPI.OnekeyType.Build)
                {
                    if (File.Exists(CreatorMain.OneKeyFile))
                    {
                        Task.Run(() => OnekeyGeneration.GenerationData(creatorAPI, CreatorMain.OneKeyFile, position));
                    }
                    else
                    {
                        player.ComponentGui.DisplaySmallMessage("未发现一键生成缓存文件，目录:" + CreatorMain.OneKeyFile + "\n请变更一键生成类型或关闭该功能", Color.Red, true, true);
                    }
                }
                return false;
            }
            if (!creatorAPI.ClearBlock)
            {
                return true;
            }

            Task.Run(() =>
           {
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               int num = 0;
               List<Point3> point3List1 = new List<Point3>();
               List<Point3> point3List2 = new List<Point3>();
               point3List1.Add(position);
               while (point3List1.Count > 0)
               {
                   foreach (Point3 point3_1 in point3List1)
                   {
                       if (!creatorAPI.launch)
                       {
                           return;
                       }

                       if (creatorAPI.revokeData != null && creatorAPI.revokeData.GetChunk(point3_1.X, point3_1.Z) == null)
                       {
                           creatorAPI.revokeData.CreateChunk(point3_1.X, point3_1.Z, true);
                       }

                       creatorAPI.SetBlock(point3_1.X, point3_1.Y, point3_1.Z, 0);
                       ++num;
                       for (int x1 = -1; x1 <= 1; ++x1)
                       {
                           for (int x2 = -1; x2 <= 1; ++x2)
                           {
                               for (int x3 = -1; x3 <= 1; ++x3)
                               {
                                   if (point3_1.Y + x2 <= byte.MaxValue)
                                   {
                                       switch (GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellContentsFast(point3_1.X + x1, point3_1.Y + x2, point3_1.Z + x3))
                                       {
                                           case 0:
                                           case 1:
                                               continue;
                                           default:
                                               if (MathUtils.Abs(x1) + MathUtils.Abs(x2) + MathUtils.Abs(x3) <= 1)
                                               {
                                                   Point3 point3_2 = new Point3(point3_1.X + x1, point3_1.Y + x2, point3_1.Z + x3);
                                                   if (!point3List1.Contains(point3_2) && !point3List2.Contains(point3_2))
                                                   {
                                                       point3List2.Add(point3_2);
                                                       continue;
                                                   }
                                                   continue;
                                               }
                                               continue;
                                       }
                                   }
                               }
                           }
                       }
                   }
                   point3List1 = point3List2;
                   point3List2 = new List<Point3>();
               }
               player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共清除{0}个方块", num), Color.Yellow, true, true);
           });
            return false;
        }
    }
}
*/