using Engine;
using Game;

namespace CreatorModAPI
{
    public class NewReplace
    {
        public static int Replace(/*CreatorAPI creatorAPI,*/int Src_Param, int Des_Param, Point3 Start, Point3 End)
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
