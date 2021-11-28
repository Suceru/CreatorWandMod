using Engine;
using Game;

namespace CreatorModAPI
{
    public class NewReplace
    {
        public static int Replace(int Src_Param, int Des_Param, Point3 Start, Point3 End)
        {
            int num = 0;
            CreatorMain.Math.StarttoEnd(ref Start, ref End);
            bool flag = false;
            bool flag2 = false;
            if (Src_Param >= 1024 || Src_Param < 0)
            {
                flag = true;
            }

            if (Des_Param >= 1024 || Src_Param < 0)
            {
                flag2 = true;
            }

            for (int i = Start.Y; i <= End.Y; i++)
            {
                for (int j = Start.Z; j <= End.Z; j++)
                {
                    for (int k = Start.X; k <= End.X; k++)
                    {
                        if (!flag)
                        {
                            if (Src_Param == GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellContentsFast(k, i, j))
                            {
                                if (flag2)
                                {
                                    GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(k, i, j, Terrain.MakeBlockValue(Terrain.ExtractContents(Des_Param), Terrain.ExtractLight(Des_Param), Terrain.ExtractData(Des_Param)));
                                }
                                else
                                {
                                    GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(k, i, j, Terrain.MakeBlockValue(Des_Param, GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellLight(k, i, j), 0));
                                }

                                num++;
                            }
                        }
                        else if (Terrain.ExtractContents(Src_Param) == GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellContentsFast(k, i, j) && Terrain.ExtractData(Src_Param) == Terrain.ExtractData(GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValue(k, i, j)))
                        {
                            if (flag2)
                            {
                                GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(k, i, j, Terrain.MakeBlockValue(Terrain.ExtractContents(Des_Param), Terrain.ExtractLight(Des_Param), Terrain.ExtractData(Des_Param)));
                            }
                            else
                            {
                                GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(k, i, j, Terrain.MakeBlockValue(Des_Param, GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellLight(k, i, j), 0));
                            }

                            num++;
                        }
                    }
                }
            }

            return num;
        }
    }
}

/*using Engine;
using Game;

namespace CreatorModAPI
{
    public class NewReplace
    {
        public static int Replace(*//*CreatorAPI creatorAPI,*//*int Src_Param, int Des_Param, Point3 Start, Point3 End)
        {
            int Return_num = 0;
            CreatorMain.Math.StarttoEnd(ref Start, ref End);
            bool SrcIsValue = false, DesIsValue = false;
            if (Src_Param >= 1024 || Src_Param < 0)
            {
                SrcIsValue = true;
            }

            if (Des_Param >= 1024 || Src_Param < 0)
            {
                DesIsValue = true;
            }

            for (int Y = Start.Y; Y <= End.Y; Y++)
            {
                for (int Z = Start.Z; Z <= End.Z; Z++)
                {
                    for (int X = Start.X; X <= End.X; X++)
                    {
                        switch (SrcIsValue)
                        {
                            case false:
                                if (Src_Param == GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellContentsFast(X, Y, Z))
                                {
                                    if (DesIsValue)
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(X, Y, Z, Terrain.MakeBlockValue(Terrain.ExtractContents(Des_Param), Terrain.ExtractLight(Des_Param), Terrain.ExtractData(Des_Param)));
                                    }
                                    else
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(X, Y, Z, Terrain.MakeBlockValue(Des_Param, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellLight(X, Y, Z), 0));
                                    }
                                    Return_num++;
                                }
                                break;
                            case true:
                                if ((Terrain.ExtractContents(Src_Param) == GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellContentsFast(X, Y, Z)) && (Terrain.ExtractData(Src_Param) == Terrain.ExtractData(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValue(X, Y, Z))))
                                {
                                    if (DesIsValue)
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(X, Y, Z, Terrain.MakeBlockValue(Terrain.ExtractContents(Des_Param), Terrain.ExtractLight(Des_Param), Terrain.ExtractData(Des_Param)));
                                    }
                                    else
                                    {
                                        GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(X, Y, Z, Terrain.MakeBlockValue(Des_Param, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellLight(X, Y, Z), 0));
                                    }
                                    Return_num++;
                                }
                                break;
                        }

                    }
                }
            }
            return Return_num;
        }
    }
}
*/