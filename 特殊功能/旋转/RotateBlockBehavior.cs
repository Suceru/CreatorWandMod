using Engine;
using Game;

namespace CreatorModAPI
{
    public enum BlockFace
    {
        ZPositive,
        XPositive,
        ZNegative,
        XNegative,
        YPositive,
        YNegative
    }
    public enum BlockRotate
    {
        Up,
        Right,
        Down,
        Left
    }
    public enum ViewVector
    {
        ZPositive = 0,
        XPositive = 1,
        ZNegative = 2,
        XNegative = 3,
        YPZPositive = 10,
        YPXPositive = 11,
        YPZNegative = 12,
        YPXNegative = 13,
        YNZPositive = 20,
        YNXPositive = 21,
        YNZNegative = 22,
        YNXNegative = 23,
    }
    public static class SetFaceAndRotate
    {
        //guiyihuavector3
        public static Vector3 Vector3Normalize(Vector3 vector3)
        {
            switch (MathUtils.Max(MathUtils.Abs(vector3.X), MathUtils.Abs(vector3.Y), MathUtils.Abs(vector3.Z)) == MathUtils.Abs(vector3.X))
            {
                case true: return Vector3.Normalize(Vector3.Round(new Vector3(vector3.X, 0, 0)));
                default:
                    switch (MathUtils.Max(MathUtils.Abs(vector3.X), MathUtils.Abs(vector3.Y), MathUtils.Abs(vector3.Z)) == MathUtils.Abs(vector3.Y))
                    {
                        case true: return Vector3.Normalize(Vector3.Round(new Vector3(0, vector3.Y, 0)));
                        default: return Vector3.Normalize(Vector3.Round(new Vector3(0, 0, vector3.Z)));
                    }
            }
        }
        public static Matrix GetFaceFunction(Vector3 SrcFace, Vector3 DecFace, bool Mirror)
        {
            _ = Mirror;
            switch ((BlockFace)CellFace.Vector3ToFace(SrcFace))
            {
                case BlockFace.ZPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationZ(0);
                        case BlockFace.XPositive: return Matrix.CreateRotationY(MathUtils.PI / 2);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(MathUtils.PI);
                        case BlockFace.XNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(90);
                    }
                case BlockFace.XPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationX(0);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.YPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                    }
                case BlockFace.ZNegative:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.XPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationZ(0);
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                    }
                case BlockFace.XNegative:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationX(0);
                        case BlockFace.YPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                    }
                case BlockFace.YPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationY(0);
                        default: return Matrix.CreateRotationX((MathUtils.PI));
                    }
                default:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI));
                        default: return Matrix.CreateRotationY(0);
                    }
            }
        }
        public static Matrix GetRotateFunction(Vector3 SrcFace, Vector3 DecFace, Vector3 Cellface)
        {
            switch ((BlockFace)CellFace.Vector3ToFace(SrcFace))
            {
                case BlockFace.ZPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive:
                            switch ((BlockFace)CellFace.Vector3ToFace(Cellface))
                            {
                                case BlockFace.ZPositive://000
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XPositive: //001
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.ZNegative: //002
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XNegative: //003
                                    return Matrix.CreateRotationY(0);

                                case BlockFace.YPositive: //004
                                    return Matrix.CreateRotationY(0);
                                default://005
                                    return Matrix.CreateRotationY(0);
                            }
                        case BlockFace.XPositive:
                            switch ((BlockFace)CellFace.Vector3ToFace(Cellface))
                            {
                                case BlockFace.ZPositive://010
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XPositive: //011
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.ZNegative: //012
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XNegative: //013
                                    return Matrix.CreateRotationY(0);

                                case BlockFace.YPositive: //014
                                    return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                                default://015
                                    return Matrix.CreateRotationY((MathUtils.PI / 2));
                            }
                        case BlockFace.ZNegative:
                            switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                            {
                                case BlockFace.ZPositive://000
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XPositive: //001
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.ZNegative: //002
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XNegative: //003
                                    return Matrix.CreateRotationY(0);

                                case BlockFace.YPositive: //004
                                    return Matrix.CreateRotationY(0);
                                default://005
                                    return Matrix.CreateRotationY(0);
                            }
                        case BlockFace.XNegative:
                            switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                            {
                                case BlockFace.ZPositive://000
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XPositive: //001
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.ZNegative: //002
                                    return Matrix.CreateRotationY(0);
                                case BlockFace.XNegative: //003
                                    return Matrix.CreateRotationY(0);

                                case BlockFace.YPositive: //004
                                    return Matrix.CreateRotationY(0);
                                default://005
                                    return Matrix.CreateRotationY(0);
                            }
                        case BlockFace.YPositive:
                            switch ((BlockFace)CellFace.Vector3ToFace(DecFace, 3))
                            {
                                case BlockFace.ZPositive:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                case BlockFace.XPositive:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                case BlockFace.ZNegative:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                default:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                            }
                        default:
                            switch ((BlockFace)CellFace.Vector3ToFace(DecFace, 3))
                            {
                                case BlockFace.ZPositive:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                case BlockFace.XPositive:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                case BlockFace.ZNegative:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                                default:
                                    switch ((BlockFace)CellFace.OppositeFace(CellFace.Vector3ToFace(Cellface)))
                                    {
                                        case BlockFace.ZPositive://000
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XPositive: //001
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.ZNegative: //002
                                            return Matrix.CreateRotationY(0);
                                        case BlockFace.XNegative: //003
                                            return Matrix.CreateRotationY(0);

                                        case BlockFace.YPositive: //004
                                            return Matrix.CreateRotationY(0);
                                        default://005
                                            return Matrix.CreateRotationY(0);
                                    }
                            }
                    }
                case BlockFace.XPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationX(0);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.YPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                    }
                case BlockFace.ZNegative:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.XPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationZ(0);
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                    }
                case BlockFace.XNegative:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationX(0);
                        case BlockFace.YPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                    }
                case BlockFace.YPositive:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationY(0);
                        default: return Matrix.CreateRotationX((MathUtils.PI));
                    }
                default:
                    switch ((BlockFace)CellFace.Vector3ToFace(DecFace))
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI));
                        default: return Matrix.CreateRotationY(0);
                    }
            }
        }
        //huoqubiaomianbianhuangongneng
        public static Matrix GetFaceFunction(BlockFace SrcFace, BlockFace DecFace)
        {
            switch (SrcFace)
            {
                case BlockFace.ZPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationZ(0);
                        case BlockFace.XPositive: return Matrix.CreateRotationY(MathUtils.PI / 2);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(MathUtils.PI);
                        case BlockFace.XNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(90);
                    }
                case BlockFace.XPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationX(0);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.YPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                    }
                case BlockFace.ZNegative:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.XPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationZ(0);
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                    }
                case BlockFace.XNegative:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationX(0);
                        case BlockFace.YPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                    }
                case BlockFace.YPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationY(0);
                        default: return Matrix.CreateRotationX((MathUtils.PI));
                    }
                default:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI));
                        default: return Matrix.CreateRotationY(0);
                    }
            }
        }
        public static Matrix GetRotateFunction(BlockFace SrcFace, BlockFace DecFace)
        {
            switch (SrcFace)
            {
                case BlockFace.ZPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY(0);
                        case BlockFace.XPositive: return Matrix.CreateRotationY(MathUtils.PI / 2);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(MathUtils.PI);
                        case BlockFace.XNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.YPositive:

                            return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationY(0);
                    }
                case BlockFace.XPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationX(0);
                        case BlockFace.ZNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.YPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                    }
                case BlockFace.ZNegative:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.XPositive: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationZ(0);
                        case BlockFace.XNegative: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        default: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                    }
                case BlockFace.XNegative:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationY((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationY((MathUtils.PI));
                        case BlockFace.ZNegative: return Matrix.CreateRotationY(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationX(0);
                        case BlockFace.YPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        default: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                    }
                case BlockFace.YPositive:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationY(0);
                        default: return Matrix.CreateRotationX((MathUtils.PI));
                    }
                default:
                    switch (DecFace)
                    {
                        case BlockFace.ZPositive: return Matrix.CreateRotationX(-(MathUtils.PI / 2));
                        case BlockFace.XPositive: return Matrix.CreateRotationZ((MathUtils.PI / 2));
                        case BlockFace.ZNegative: return Matrix.CreateRotationX((MathUtils.PI / 2));
                        case BlockFace.XNegative: return Matrix.CreateRotationZ(-(MathUtils.PI / 2));
                        case BlockFace.YPositive: return Matrix.CreateRotationX((MathUtils.PI));
                        default: return Matrix.CreateRotationY(0);
                    }
            }
        }
        public static int GetFaceVector(int cellValue, Matrix Translate)
        {

            return CellFace.Vector3ToFace(Vector3Normalize(Vector3.TransformNormal(CellFace.FaceToVector3(SetFaceAndRotate.GetFace(cellValue)), Translate)));
            /* switch (BlocksManager.Blocks[Terrain.ExtractContents(cellValue)])
             {
                 case BottomSuckerBlock _:
                 case SwitchBlock _:
                 case PressurePlateBlock _:
                 case SpikedPlankBlock _:
                     return SpikedPlankBlock.GetMountingFace(Terrain.ExtractData(cellValue));

                 case MotionDetectorBlock _:
                 case ButtonBlock _:
                 case LightbulbBlock _:
                 case FourLedBlock _:
                 case LedBlock _:
                 case MulticoloredLedBlock _:
                 case OneLedBlock _:
                 case PhotodiodeBlock _:
                 case DetonatorBlock _:
                 case TorchBlock _:
                 case DispenserBlock _:
                 case PostedSignBlock _:
                     return (Terrain.ExtractData(cellValue) & 7);
                 case FenceBlock _:
                     return Terrain.ExtractData(cellValue) & 15;
                 case SevenSegmentDisplayBlock _:
                 case TargetBlock _:
                 case LadderBlock _:
                 case FenceGateBlock _:
                 case FurnitureBlock _:
                 case JackOLanternBlock _:
                 case IvyBlock _:
                 case DoorBlock _:
                 case TrapdoorBlock _:
                 case ThermometerBlock _:
                 case HygrometerBlock _:
                 case WireThroughBlock _:
                 case WoodBlock _:
                 case StairsBlock _:
                 case FurnaceBlock _:
                 case ChestBlock _:
                 case AttachedSignBlock _:
                     return Terrain.ExtractData(cellValue) & 3;
                 case PistonBlock _:
                     return (Terrain.ExtractData(cellValue) >> 3) & 7;
                 case WireBlock _:
                     return Terrain.ExtractData(cellValue) & 0x3F;
                 case GravestoneBlock _:
                     return (Terrain.ExtractData(cellValue) & 8) >> 3;
                 case MagnetBlock _:
                     return Terrain.ExtractData(cellValue) & 1;
                 case RotateableMountedElectricElementBlock _:
                     return (Terrain.ExtractData(cellValue) >> 2) & 7;
             }
             return cellValue;*/
        }
        public static int GetRotateVector(int cellValue, Matrix Translate)
        {
            return CellFace.Vector3ToFace(Vector3Normalize(Vector3.TransformNormal(CellFace.FaceToVector3(SetFaceAndRotate.GetFace(cellValue)), Translate)));
        }
        //在一个int中查找指定int的位置
        public static int Int_Indexof_Int(int Value, int Count)
        {
            if (Count > 9)
            {
                return -1;
            }

            int Face_return_1 = 0, Face_return_2 = -1;
            for (; Value != 0; Value /= 10, ++Face_return_1)
            {
                if (Value % 10 == Count)
                {
                    Face_return_2 = Face_return_1;
                }
            }
            return Face_return_2 == -1 ? -1 : Face_return_1 - Face_return_2 - 1;

        }
        //传入类型，转换前，转换后，返回int
        public static int GetFaceVector(int cellValue, int Src_Face_2, int Des_Face_2, int Src_Param)
        {
            //  int Src_Face_Wire=-1,Des_Face_Wire=-1;
            //将输入的方位转换为数组的索引
            // 
            int blocktype = -1;
            switch (Src_Face_2 / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Src_Face_2 = 0 + (Src_Face_2 % 10);
                    break;
                case 4:
                    Src_Face_2 = 10 + (Src_Face_2 % 10);
                    break;
                case 5:
                    Src_Face_2 = 20 + (Src_Face_2 % 10);
                    break;
            }
            switch (Des_Face_2 / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Des_Face_2 = 0 + (Des_Face_2 % 10);
                    break;
                case 4:
                    Des_Face_2 = 10 + (Des_Face_2 % 10);
                    break;
                case 5:
                    Des_Face_2 = 20 + (Des_Face_2 % 10);
                    break;
            }

            int Face_Type = 0;
            //定义一个旋转体，用于做置换,定义一个面，用于交换平视俯视仰视时的输出朝向,6无效，7为空白位
            int[,] BlockFace = new int[12, 4] {
                //3mian
                {6012, 6102, 6012, 6102} ,
                {6210, 6201, 6210, 6201} ,
                {6210, 6201, 6210, 6201},
                //4mian
                {6012377, 6123077, 6230177, 6301277} ,
                {6717320, 6727031, 6737102, 6707213} ,
                {6717302, 6727013, 6737120, 6707231},
                //5mian
                {6012347, 6123047, 6230147, 6301247} ,
                {6417320, 6427031, 6437102, 6407213} ,
                {6714302, 6724013, 6734120, 6704231},
                //6面
                {6012345, 6123045, 6230145, 6301245} ,
                {6415320, 6425031, 6435102, 6405213} ,
                {6514302, 6524013, 6534120, 6504231}
            };
            switch (Terrain.ExtractContents(cellValue))
            {
                //全部逻辑门等元件
                case 140:
                case 181:
                case 183:
                case 137:
                case 143:
                case 156:
                case 134:
                case 135:
                case 145:
                case 224:
                case 146:
                case 184:
                case 186:
                case 188:
                case 187:
                case 157:
                case 180:
                    //逻辑门元件完
                    Face_Type = 9;
                    break;
                //各种开关
                case 141:
                case 142:
                case 144:
                    //各种开关完
                    Face_Type = 9;
                    break;
                //感应开关,led,雷管，点led
                case 179:
                case 139:
                case 182:
                case 152:
                case 254:
                case 253:
                case 151:
                case 147:
                    //感应开关完
                    Face_Type = 9;
                    break;
                //16led，靶子,栏杆门
                case 185:
                case 199:
                case 59:
                case 166:
                case 227:
                case 132:
                case 197:
                case 56:
                case 84:
                case 120:
                case 121:
                    //16led完
                    Face_Type = 3;
                    break;
                //火把
                case 31:
                    if (Src_Param == 4)
                    {
                        return 4;
                    }

                    Face_Type = 3;
                    break;
                //各种活塞
                case 237:
                    //各种活塞完
                    Face_Type = 9;
                    break;
                //各种穿线块
                case 153:
                case 154:
                case 223:
                case 155:
                case 243:

                    //各种穿线块完
                    Face_Type = 0;
                    break;
                //各种树块
                case 9:
                case 10:
                case 11:
                case 255:
                    switch (Src_Param)
                    {
                        case 0:
                            Src_Param = 2;
                            break;
                        case 1:
                            Src_Param = 0;
                            break;
                        case 2:
                            Src_Param = 1;
                            break;
                    }
                    //各种树块完
                    blocktype = 9;
                    Face_Type = 0;
                    break;
                //地刺
                case 86:
                    //地刺完
                    Face_Type = 9;
                    break;
                //导线
                case 133:
                    //导线完
                    Face_Type = 9;
                    int a = 7 * 1000000 + ((Src_Param & 32) >> 5) * 600000 + ((Src_Param & 16) >> 4) * 50000 + ((Src_Param & 8) >> 3) * 4000 + ((Src_Param & 4) >> 2) * 300 + ((Src_Param & 2) >> 1) * 20 + ((Src_Param & 1) >> 0) * 1;
                    int a_return = 0;
                    // return a;
                    for (int i = 0; i < 6; i++, a /= 10)
                    {
                        if (i + 1 == a % 10)
                        {
                            int p = 0;
                            switch (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Src_Face_2 / 10, Src_Face_2 % 10], i))
                            {
                                case 1:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100000) % 10;
                                    break;
                                case 2:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10000) % 10;
                                    break;
                                case 3:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1000) % 10;
                                    break;
                                case 4:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100) % 10;
                                    break;
                                case 5:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10) % 10;
                                    break;
                                case 6:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1) % 10;
                                    break;

                            }
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                int Mi_return = p;
                                switch (Des_Face_2 % 10)
                                {
                                    case 0:
                                    case 2:
                                        if (Mi_return == 1)
                                        {
                                            Mi_return = 3;
                                        }
                                        else if (Mi_return == 3)
                                        {
                                            Mi_return = 1;
                                        }

                                        break;
                                    case 1:
                                    case 3:
                                        if (Mi_return == 0)
                                        {
                                            Mi_return = 2;
                                        }
                                        else if (Mi_return == 2)
                                        {
                                            Mi_return = 0;
                                        }

                                        break;
                                }
                                p = Mi_return;
                            }
                            switch (p)
                            {
                                case 0:
                                    a_return |= 1;
                                    break;
                                case 1:
                                    a_return |= 2;
                                    break;
                                case 2:
                                    a_return |= 4;
                                    break;
                                case 3:
                                    a_return |= 8;
                                    break;
                                case 4:
                                    a_return |= 16;
                                    break;
                                case 5:
                                    a_return |= 32;
                                    break;
                            }
                        }
                    }
                    //  return 9*1000+a;
                    return a_return;

                //围墙
                case 94:
                case 193:
                case 164:
                case 202:
                case 163:
                    //围墙完
                    Face_Type = 3;
                    int Fence = 7 * 10000 + ((Src_Param & 8) >> 3) * 4000 + ((Src_Param & 4) >> 2) * 300 + ((Src_Param & 2) >> 1) * 20 + ((Src_Param & 1) >> 0) * 1;
                    int Fence_return = 0;
                    // return a;
                    for (int i = 0; i < 4; i++, Fence /= 10)
                    {
                        if (i + 1 == Fence % 10)
                        {
                            int P_1 = 0;
                            switch (i)
                            {
                                case 0: P_1 = 1; break;
                                case 1: P_1 = 3; break;
                                case 2: P_1 = 0; break;
                                case 3: P_1 = 2; break;
                            }

                            int p = 0;
                            switch (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Src_Face_2 / 10, Src_Face_2 % 10], P_1))
                            {
                                case 1:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100000) % 10;
                                    break;
                                case 2:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10000) % 10;
                                    break;
                                case 3:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1000) % 10;
                                    break;
                                case 4:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100) % 10;
                                    break;
                                case 5:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10) % 10;
                                    break;
                                case 6:
                                    p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1) % 10;
                                    break;

                            }
                            if (MirrorBlockBehavior.IsMirror)
                            {
                                int Mi_return = p;
                                switch (Des_Face_2 % 10)
                                {
                                    case 0:
                                    case 2:
                                        if (Mi_return == 1)
                                        {
                                            Mi_return = 3;
                                        }
                                        else if (Mi_return == 3)
                                        {
                                            Mi_return = 1;
                                        }

                                        break;
                                    case 1:
                                    case 3:
                                        if (Mi_return == 0)
                                        {
                                            Mi_return = 2;
                                        }
                                        else if (Mi_return == 2)
                                        {
                                            Mi_return = 0;
                                        }

                                        break;
                                }
                                p = Mi_return;
                            }
                            switch (p)
                            {
                                case 0:
                                    Fence_return |= 4;
                                    break;
                                case 1:
                                    Fence_return |= 1;
                                    break;
                                case 2:
                                    Fence_return |= 8;
                                    break;
                                case 3:
                                    Fence_return |= 2;
                                    break;
                            }
                        }
                    }
                    //  return 9*1000+a;
                    return Fence_return;


                //楼梯
                case 217:
                case 48:
                case 49:
                case 50:
                case 76:
                case 51:
                case 69:
                case 96:
                    blocktype = 48;
                    //楼梯完
                    Face_Type = 3;
                    break;
            }
            // CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(6012345, 3)
            if (Face_Type == 0)
            {
                int p = -1;
                switch (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Src_Face_2 / 10, Src_Face_2 % 10], Src_Param))
                {
                    case 1:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100) % 10;
                        break;
                    case 2:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10) % 10;
                        break;
                    case 3:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1) % 10;
                        break;
                }
                if (blocktype == 9)
                {
                    switch (p)
                    {
                        case 0:
                            p = 1;
                            break;
                        case 1:
                            p = 2;
                            break;
                        case 2:
                            p = 0;
                            break;
                    }
                }

                return p;
            }
            else
            {
                int p = -1;
                switch (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Src_Face_2 / 10, Src_Face_2 % 10], Src_Param))
                {
                    case 1:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100000) % 10;
                        break;
                    case 2:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10000) % 10;
                        break;
                    case 3:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1000) % 10;
                        break;
                    case 4:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 100) % 10;
                        break;
                    case 5:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 10) % 10;
                        break;
                    case 6:
                        p = (BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10] / 1) % 10;
                        break;

                }
                if ((blocktype == 48) && (((Terrain.ExtractData(cellValue) & 16) >> 4 == 1) || ((Terrain.ExtractData(cellValue) & 8) >> 3 == 1)))
                {
                    int Mi_return = p;
                    if (((Src_Face_2 / 10 == 1) && (Des_Face_2 / 10 == 2)) || ((Src_Face_2 / 10 == 2) && (Des_Face_2 / 10 == 1)))
                    {
                        switch (Des_Face_2 % 10)
                        {
                            case 0:
                            case 2:
                                if (Mi_return == 3)
                                {
                                    Mi_return = 0;
                                }
                                else if (Mi_return == 2)
                                {
                                    Mi_return = 3;
                                }
                                else if (Mi_return == 1)
                                {
                                    Mi_return = 2;
                                }
                                else if (Mi_return == 0)
                                {
                                    Mi_return = 1;
                                }

                                break;
                            case 1:
                            case 3:
                                if (Mi_return == 3)
                                {
                                    Mi_return = 0;
                                }
                                else if (Mi_return == 0)
                                {
                                    Mi_return = 1;
                                }
                                else if (Mi_return == 1)
                                {
                                    Mi_return = 2;
                                }
                                else if (Mi_return == 2)
                                {
                                    Mi_return = 3;
                                }

                                break;
                        }
                    }
                    p = Mi_return;
                }
                if (MirrorBlockBehavior.IsMirror)
                {
                    int Mi_return = p;
                    if ((blocktype == 48) && (((Terrain.ExtractData(cellValue) & 16) >> 4 == 1) || ((Terrain.ExtractData(cellValue) & 8) >> 3 == 1)))
                    {

                        if (((Src_Face_2 / 10 == 1) && (Des_Face_2 / 10 == 2)) || ((Src_Face_2 / 10 == 2) && (Des_Face_2 / 10 == 1)))
                        {
                            switch (Des_Face_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 2)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 1)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 1;
                                    }

                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 3)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 2)
                                    {
                                        Mi_return = 1;
                                    }
                                    else if (Mi_return == 1)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 3;
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            switch (Des_Face_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 3)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 2)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 1)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 1;
                                    }

                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 3)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 1)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 2)
                                    {
                                        Mi_return = 1;
                                    }

                                    break;
                            }
                            Log.Warning("2");
                        }
                        p = Mi_return;
                    }
                    else
                    {
                        switch (Des_Face_2 % 10)
                        {
                            case 0:
                            case 2:
                                if (Mi_return == 1)
                                {
                                    Mi_return = 3;
                                }
                                else if (Mi_return == 3)
                                {
                                    Mi_return = 1;
                                }

                                break;
                            case 1:
                            case 3:
                                if (Mi_return == 0)
                                {
                                    Mi_return = 2;
                                }
                                else if (Mi_return == 2)
                                {
                                    Mi_return = 0;
                                }

                                break;
                        }
                        p = Mi_return;
                    }
                }
                return p;
            }

            // return (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Src_Face_2 / 10, Src_Face_2 % 10], Src_Param))*1000+ (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockFace[Face_Type + Des_Face_2 / 10, Des_Face_2 % 10], Src_Param)) * 100 + (Face_Type + Des_Face_2 / 10)*10 + (Des_Face_2 % 10);
            //return 0;
        }
        //传入类型，转换前，转换后，返回int
        public static int GetRotateVector(int cellValue, int Src_Rotate_2, int Des_Rotate_2, int Src_Param)
        {
            //将输入的方位转换为数组的索引
            switch (Src_Rotate_2 / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Src_Rotate_2 = 0 + (Src_Rotate_2 / 10);
                    break;
                case 4:
                    Src_Rotate_2 = 10 + (Src_Rotate_2 % 10);
                    break;
                case 5:
                    Src_Rotate_2 = 20 + (Src_Rotate_2 % 10);
                    break;
            }
            switch (Des_Rotate_2 / 10)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Des_Rotate_2 = 0 + (Des_Rotate_2 / 10);
                    break;
                case 4:
                    Des_Rotate_2 = 10 + (Des_Rotate_2 % 10);
                    break;
                case 5:
                    Des_Rotate_2 = 20 + (Des_Rotate_2 % 10);
                    break;
            }
            int[,] BlockRotate = new int[3, 4] {
                {60123, 60123,60123,60123} ,
                {62301, 63012, 60123, 61230},
                {62301, 61230, 60123, 63012}

            };
            /*int[,] BlockRotate = new int[3, 4] {
                            {60123, 60123,60123,60123} ,
                            {62301, 63012, 60123, 61230},
                            {62301, 61230, 60123, 63012}

                        };*/
            //检查是否是对应的方块
            int Face_Type = -1;
            switch (Terrain.ExtractContents(cellValue))
            {
                //全部逻辑门等元件
                case 140:
                case 181:
                case 183:
                case 137:
                case 143:
                case 156:
                case 134:
                case 135:
                case 145:
                case 224:
                case 146:
                case 184:
                case 186:
                case 188:
                case 187:
                case 157:
                case 180:
                    //逻辑门元件完
                    Face_Type = 0; break;
                //台阶
                case 217:
                case 48:
                case 49:
                case 50:
                case 76:
                case 51:
                case 69:
                case 96:
                    //台阶完
                    if (((Src_Rotate_2 / 10 == 1) && (Des_Rotate_2 / 10 == 2)) || ((Src_Rotate_2 / 10 == 2) && (Des_Rotate_2 / 10 == 1)))
                    {
                        return Src_Param == 1 ? 0 : 1;
                    }
                    else
                    {
                        return Src_Param;
                    }
            }
            bool FaceVectorOnW = false;
            switch (CreatorModAPI.SetFaceAndRotate.GetFace(cellValue))
            {
                case 0: case 1: case 2: case 3: FaceVectorOnW = true; break;
                case 4: case 5: FaceVectorOnW = false; break;
            }
            if (Face_Type == 0 & ((Src_Rotate_2 / 10 == 0 & Des_Rotate_2 / 10 == 0 & FaceVectorOnW == true) | (((Src_Rotate_2 / 10 == 1 & Des_Rotate_2 / 10 == 1) | (Src_Rotate_2 / 10 == 2 & Des_Rotate_2 / 10 == 2)) & FaceVectorOnW == true)))
            {
                int p = Src_Param;
                if (MirrorBlockBehavior.IsMirror)
                {
                    int Mi_return = p;
                    switch (Des_Rotate_2 / 10)
                    {
                        case 0:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                        case 1:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 2)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 2;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                        case 2:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 0)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 2)
                                    {
                                        Mi_return = 0;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                    }

                }
                return p;
            }
            else if (Face_Type == 0 & FaceVectorOnW == false)
            {
                /*int[,] BlockRotate = new int[3, 4] {
                                {60123, 60123,60123,60123} ,
                                {62301, 63012, 60123, 61230},
                                {62301, 61230, 60123, 63012}

                            };*/
                int p = -1;
                switch (CreatorModAPI.SetFaceAndRotate.Int_Indexof_Int(BlockRotate[Face_Type + Src_Rotate_2 / 10, Src_Rotate_2 % 10], Src_Param))
                {
                    case 1:
                        p = (BlockRotate[Face_Type + Des_Rotate_2 / 10, Des_Rotate_2 % 10] / 1000) % 10; break;

                    case 2:
                        p = (BlockRotate[Face_Type + Des_Rotate_2 / 10, Des_Rotate_2 % 10] / 100) % 10; break;
                    case 3:
                        p = (BlockRotate[Face_Type + Des_Rotate_2 / 10, Des_Rotate_2 % 10] / 10) % 10; break;
                    case 4:
                        p = (BlockRotate[Face_Type + Des_Rotate_2 / 10, Des_Rotate_2 % 10] / 1) % 10; break;
                }
                if (MirrorBlockBehavior.IsMirror)
                {
                    int Mi_return = p;
                    switch (Des_Rotate_2 / 10)
                    {
                        case 0:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                        case 1:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 2)
                                    {
                                        Mi_return = 0;
                                    }
                                    else if (Mi_return == 0)
                                    {
                                        Mi_return = 2;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                        case 2:
                            switch (Des_Rotate_2 % 10)
                            {
                                case 0:
                                case 2:
                                    if (Mi_return == 1)
                                    {
                                        Mi_return = 3;
                                    }
                                    else if (Mi_return == 3)
                                    {
                                        Mi_return = 1;
                                    }
                                    /*    if (Mi_return == 0) Mi_return = 2;
   else if (Mi_return == 1) Mi_return = 3;
   else if (Mi_return == 2) Mi_return = 0;
   else if (Mi_return == 3) Mi_return = 1;*/
                                    break;
                                case 1:
                                case 3:
                                    if (Mi_return == 0)
                                    {
                                        Mi_return = 2;
                                    }
                                    else if (Mi_return == 2)
                                    {
                                        Mi_return = 0;
                                    }

                                    break;
                            }
                            p = Mi_return;
                            break;
                    }

                }
                return p;
            }
            return 0;
        }
        public static int GetFace(int cellValue)
        {
            switch (BlocksManager.Blocks[Terrain.ExtractContents(cellValue)])
            {
                case BottomSuckerBlock _:
                    return BottomSuckerBlock.GetFace(Terrain.ExtractData(cellValue));
                case SwitchBlock _:
                case PressurePlateBlock _:
                    return (Terrain.ExtractData(cellValue) >> 1) & 7;
                case SpikedPlankBlock _:
                    return SpikedPlankBlock.GetMountingFace(Terrain.ExtractData(cellValue));

                case MotionDetectorBlock _:
                case ButtonBlock _:
                case LightbulbBlock _:
                case FourLedBlock _:
                case LedBlock _:
                case MulticoloredLedBlock _:
                case OneLedBlock _:
                case PhotodiodeBlock _:
                case DetonatorBlock _:
                case TorchBlock _:
                case DispenserBlock _:
                case PostedSignBlock _:
                    return (Terrain.ExtractData(cellValue) & 7);
                case FenceBlock _:
                    return Terrain.ExtractData(cellValue) & 15;
                case SevenSegmentDisplayBlock _:
                case TargetBlock _:
                case LadderBlock _:
                case FenceGateBlock _:
                case FurnitureBlock _:
                case JackOLanternBlock _:
                case IvyBlock _:
                case DoorBlock _:
                case TrapdoorBlock _:
                case ThermometerBlock _:
                case HygrometerBlock _:
                case WireThroughBlock _:
                case WoodBlock _:
                case StairsBlock _:
                case FurnaceBlock _:
                case ChestBlock _:
                case AttachedSignBlock _:
                    return Terrain.ExtractData(cellValue) & 3;
                case PistonBlock _:
                    return (Terrain.ExtractData(cellValue) >> 3) & 7;
                case WireBlock _:
                    return Terrain.ExtractData(cellValue) & 0x3F;
                case GravestoneBlock _:
                    return (Terrain.ExtractData(cellValue) & 8) >> 3;
                case MagnetBlock _:
                    return Terrain.ExtractData(cellValue) & 1;
                case RotateableMountedElectricElementBlock _:
                    return (Terrain.ExtractData(cellValue) >> 2) & 7;
                default:
                    return -1;
            }

        }
        public static int SetFace(int cellValue, int face)
        {
            switch (BlocksManager.Blocks[Terrain.ExtractContents(cellValue)])
            {
                case BottomSuckerBlock _:
                    return Terrain.ReplaceData(cellValue, BottomSuckerBlock.SetFace(Terrain.ExtractData(cellValue), face));
                case SwitchBlock _:
                case PressurePlateBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -15) | ((face & 7) << 1));
                case SpikedPlankBlock _:
                    return Terrain.ReplaceData(cellValue, SpikedPlankBlock.SetMountingFace(Terrain.ExtractData(cellValue), face));
                case MotionDetectorBlock _:
                case ButtonBlock _:
                case LightbulbBlock _:
                case FourLedBlock _:
                case LedBlock _:
                case MulticoloredLedBlock _:
                case OneLedBlock _:
                case PhotodiodeBlock _:
                case DetonatorBlock _:
                case TorchBlock _:
                case DispenserBlock _:
                case PostedSignBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -8) | (face & 7));
                case FenceBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -16) | (face & 15));
                case SevenSegmentDisplayBlock _:
                case TargetBlock _:
                case LadderBlock _:
                case FenceGateBlock _:
                case FurnitureBlock _:
                case JackOLanternBlock _:
                case IvyBlock _:
                case DoorBlock _:
                case TrapdoorBlock _:
                case ThermometerBlock _:
                case HygrometerBlock _:
                case WireThroughBlock _:
                case WoodBlock _:
                case StairsBlock _:
                case FurnaceBlock _:
                case ChestBlock _:
                case AttachedSignBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -4) | (face & 3));
                case PistonBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -57) | ((face & 7) << 3));
                case WireBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -0x40) | (face & 0x3F));
                case GravestoneBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -65) | ((face & 8) << 3));
                case MagnetBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -2) | (face & 1));
                case RotateableMountedElectricElementBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -29) | ((face & 7) << 2));
                default:
                    return cellValue;
            }

        }
        public static int GetRotate(int cellValue)
        {
            switch (BlocksManager.Blocks[Terrain.ExtractContents(cellValue)])
            {
                case RotateableMountedElectricElementBlock _:
                    return Terrain.ExtractData(cellValue) & 3;
                case StairsBlock _:
                    return (Terrain.ExtractData(cellValue) & 4) >> 2;
                case TrapdoorBlock _:
                    return (Terrain.ExtractData(cellValue) & 8) >> 3;
                case SlabBlock _:
                    return Terrain.ExtractData(cellValue) & 1;
                default: return -1;
            }
            /*
                        switch (Terrain.ExtractContents(cellValue))
                        {
                            //全部逻辑门等元件
                            case 140:
                            case 181:
                            case 183:
                            case 137:
                            case 143:
                            case 156:
                            case 134:
                            case 135:
                            case 145:
                            case 224:
                            case 146:
                            case 184:
                            case 186:
                            case 188:
                            case 187:
                            case 157:
                            case 180:
                                //逻辑门元件完
                                return Terrain.ExtractData(cellValue) & 3;
                            //台阶
                            case 217:
                            case 49:
                            case 48:
                            case 50:
                            case 76:
                            case 51:
                            case 69:
                            case 96:
                                //台阶完
                                return (Terrain.ExtractData(cellValue) & 4) >> 2;



                        }
                        return -1;*/

        }
        public static int SetRotate(int cellValue, int Rotate)
        {
            switch (BlocksManager.Blocks[Terrain.ExtractContents(cellValue)])
            {
                case RotateableMountedElectricElementBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -4) | (Rotate & 3));
                case StairsBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -5) | ((Rotate & 1) << 1));
                case TrapdoorBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -9) | ((Rotate & 8) << 3));
                case SlabBlock _:
                    return Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -2) | (Rotate & 1));
                default: return cellValue;
            }
            /*

                        int num_return = cellValue;
                        switch (Terrain.ExtractContents(cellValue))
                        {
                            //全部逻辑门等元件
                            case 140:
                            case 181:
                            case 183:
                            case 137:
                            case 143:
                            case 156:
                            case 134:
                            case 135:
                            case 145:
                            case 224:
                            case 146:
                            case 184:
                            case 186:
                            case 188:
                            case 187:
                            case 157:
                            case 180:
                                //逻辑门元件完
                                num_return = Terrain.ReplaceData(cellValue, RotateableMountedElectricElementBlock.SetRotation(Terrain.ExtractData(cellValue), Rotate % 4));
                                break;
                            //台阶
                            case 217:
                            case 48:
                            case 49:
                            case 50:
                            case 76:
                            case 51:
                            case 69:
                            case 96:
                                //台阶完
                                num_return = Terrain.ReplaceData(cellValue, (Terrain.ExtractData(cellValue) & -5) | Rotate << 2);
                                break;
                        }
                        return num_return;*/
        }

    }
}
