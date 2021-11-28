// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.CreatorGenerationAlgorithm
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

using Engine;
using System;
using System.Collections.Generic;

//形状生成
namespace CreatorModAPI
{
    public class CreatorGenerationAlgorithm
    {
        public IEnumerable<Point3> TwoPointLineGeneration(
          Point3 startPoint,
          Point3 endPoint)
        {
            int lengin = System.Math.Max(MathUtils.Max(System.Math.Abs(startPoint.X - endPoint.X), System.Math.Abs(startPoint.Y - endPoint.Y)), System.Math.Abs(startPoint.Z - endPoint.Z));
            for (int num = 0; num <= lengin; ++num)
            {
                yield return new Point3(startPoint.X + (int)System.Math.Round(num / (double)lengin * (endPoint.X - startPoint.X)), startPoint.Y + (int)System.Math.Round(num / (double)lengin * (endPoint.Y - startPoint.Y)), startPoint.Z + (int)System.Math.Round(num / (double)lengin * (endPoint.Z - startPoint.Z)));
            }
        }

        public IEnumerable<Point3> TwoPointLineGeneration2(
          Point3 startPoint,
          Point3 endPoint)
        {
            int lengin = System.Math.Max(MathUtils.Max(System.Math.Abs(startPoint.X - endPoint.X), System.Math.Abs(startPoint.Y - endPoint.Y)), System.Math.Abs(startPoint.Z - endPoint.Z));
            float num = 0.0f;
            bool sc = false;
            for (; num <= (double)lengin; num += 0.5f)
            {
                double mx = System.Math.Round(num / (double)lengin * (endPoint.X - startPoint.X) + (endPoint.X - startPoint.X > 0 ? 0.490000009536743 : -0.490000009536743));
                if ((int)mx != (int)System.Math.Round(num / (double)lengin * (endPoint.X - startPoint.X)))
                {
                    sc = true;
                }

                double mz = System.Math.Round(num / (double)lengin * (endPoint.Z - startPoint.Z) + (sc ? (endPoint.Z - startPoint.Z < 0 ? 0.00999999977648258 : -0.00999999977648258) : (endPoint.Z - startPoint.Z > 0 ? 0.00999999977648258 : -0.00999999977648258)));
                if (!sc && (int)mz == (int)System.Math.Round(num / (double)lengin * (endPoint.X - startPoint.X)))
                {
                    mx += mx > 0.0 ? 1.0 : -1.0;
                }

                yield return new Point3(startPoint.X + (int)mx, startPoint.Y + (int)System.Math.Round(num / (double)lengin * (endPoint.Y - startPoint.Y) + (endPoint.Y - startPoint.Y > 0 ? 0.00999999977648258 : -0.00999999977648258)), startPoint.Z + (int)mz);
                Log.Error(string.Format("{0},{1},{2}", (float)(startPoint.X + num / (double)lengin * (endPoint.X - startPoint.X)), (float)(startPoint.Y + num / (double)lengin * (endPoint.Y - startPoint.Y)), (float)(startPoint.Z + num / (double)lengin * (endPoint.Z - startPoint.Z))));
                Log.Error(string.Format("Round : {0},{1},{2}", mx, System.Math.Round(num / (double)lengin * (endPoint.Y - startPoint.Y) + (endPoint.Y - startPoint.Y > 0 ? 0.00999999977648258 : -0.00999999977648258)), mz));
            }
        }

        public IEnumerable<Point3> Sphere(
          Vector3 Position,
          int XRadius,
          int YRadius,
          int ZRadius,
          bool Hollow = false,
          CreatorMain.CreateType? XYZType = null,
          bool typeBool = false)
        {
            int MaxRadius = System.Math.Max(System.Math.Max(XRadius, YRadius), ZRadius);
            for (int x = -XRadius; x <= XRadius; ++x)
            {
                for (int y = -YRadius; y <= YRadius; ++y)
                {
                    for (int z = -ZRadius; z <= ZRadius; ++z)
                    {
                        if ((int)System.Math.Sqrt(x * (double)MaxRadius / XRadius * x * MaxRadius / XRadius + y * (double)MaxRadius / YRadius * y * MaxRadius / YRadius + z * (double)MaxRadius / ZRadius * z * MaxRadius / ZRadius) <= MaxRadius)
                        {
                            if (XYZType.HasValue)
                            {
                                CreatorMain.CreateType? nullable = XYZType;
                                CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                                if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                                {
                                    if (typeBool)
                                    {
                                        if (Position.X + (double)x < Position.X)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (Position.X + (double)x > Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    nullable = XYZType;
                                    CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                                    if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                                    {
                                        if (typeBool)
                                        {
                                            if (Position.Y + (double)y < Position.Y)
                                            {
                                                continue;
                                            }
                                        }
                                        else if (Position.Y + (double)y > Position.Y)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        nullable = XYZType;
                                        CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                                        if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                                        {
                                            if (typeBool)
                                            {
                                                if (Position.Z + (double)z < Position.Z)
                                                {
                                                    continue;
                                                }
                                            }
                                            else if (Position.Z + (double)z > Position.Z)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            if (!Hollow || (int)System.Math.Sqrt((System.Math.Abs(x) + 1) * (double)MaxRadius / XRadius * (System.Math.Abs(x) + 1) * MaxRadius / XRadius + y * (double)MaxRadius / YRadius * y * MaxRadius / YRadius + z * (double)MaxRadius / ZRadius * z * MaxRadius / ZRadius) > MaxRadius || ((int)System.Math.Sqrt(x * (double)MaxRadius / XRadius * x * MaxRadius / XRadius + (System.Math.Abs(y) + 1) * (double)MaxRadius / YRadius * (System.Math.Abs(y) + 1) * MaxRadius / YRadius + z * (double)MaxRadius / ZRadius * z * MaxRadius / ZRadius) > MaxRadius || (int)System.Math.Sqrt(x * (double)MaxRadius / XRadius * x * MaxRadius / XRadius + y * (double)MaxRadius / YRadius * y * MaxRadius / YRadius + (System.Math.Abs(z) + 1) * (double)MaxRadius / ZRadius * (System.Math.Abs(z) + 1) * MaxRadius / ZRadius) > MaxRadius))
                            {
                                yield return new Point3((int)(Position.X + (double)x), (int)(Position.Y + (double)y), (int)(Position.Z + (double)z));
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Sphere(
          Point3 Position,
          int Radius,
          bool Hollow = false,
          CreatorMain.CreateType? XYZType = null,
          bool typeBool = false)
        {
            int MRadius = Radius * Radius;
            for (int x = -Radius; x <= Radius; ++x)
            {
                for (int y = -Radius; y <= Radius; ++y)
                {
                    for (int z = -Radius; z <= Radius; ++z)
                    {
                        if (x * x + y * y + z * z <= MRadius)
                        {
                            if (XYZType.HasValue)
                            {
                                CreatorMain.CreateType? nullable = XYZType;
                                CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                                if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                                {
                                    if (typeBool)
                                    {
                                        if (Position.X + x < Position.X)
                                        {
                                            continue;
                                        }
                                    }
                                    else if (Position.X + x > Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    nullable = XYZType;
                                    CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                                    if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                                    {
                                        if (typeBool)
                                        {
                                            if (Position.Y + y < Position.Y)
                                            {
                                                continue;
                                            }
                                        }
                                        else if (Position.Y + y > Position.Y)
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        nullable = XYZType;
                                        CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                                        if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                                        {
                                            if (typeBool)
                                            {
                                                if (Position.Z + z < Position.Z)
                                                {
                                                    continue;
                                                }
                                            }
                                            else if (Position.Z + z > Position.Z)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }
                            if (!Hollow || (System.Math.Abs(x) + 1) * (System.Math.Abs(x) + 1) + y * y + z * z > MRadius || (x * x + (System.Math.Abs(y) + 1) * (System.Math.Abs(y) + 1) + z * z > MRadius || x * x + y * y + (System.Math.Abs(z) + 1) * (System.Math.Abs(z) + 1) > MRadius))
                            {
                                yield return new Point3(Position.X + x, Position.Y + y, Position.Z + z);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Sphere(Point3 Start, Point3 End, bool Hollow = false) => throw new Exception("no way");

        public IEnumerable<Point3> Prism(
          Point3 Position,
          int Radius,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool Hollow = false,
          CreatorMain.CreateType? XYZtype = null,
          bool typeBool = false)
        {
            for (int x = -Radius; x <= Radius; ++x)
            {
                for (int y = -Radius; y <= Radius; ++y)
                {
                    for (int z = -Radius; z <= Radius; ++z)
                    {
                        if (XYZtype.HasValue)
                        {
                            CreatorMain.CreateType? nullable = XYZtype;
                            CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                            if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.X + x < Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.X + x > Position.X)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZtype;
                            CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                            if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Y + y < Position.Y)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Y + y > Position.Y)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZtype;
                            CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                            if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Z + z < Position.Z)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Z + z > Position.Z)
                                {
                                    continue;
                                }
                            }
                        }
                        if ((createType != CreatorMain.CreateType.Y || System.Math.Abs(x) + System.Math.Abs(y) <= Radius && System.Math.Abs(z) + System.Math.Abs(y) <= Radius && (!Hollow || System.Math.Abs(x) + System.Math.Abs(y) >= Radius || System.Math.Abs(z) + System.Math.Abs(y) >= Radius)) && (createType != CreatorMain.CreateType.X || System.Math.Abs(x) + System.Math.Abs(y) <= Radius && System.Math.Abs(z) + System.Math.Abs(x) <= Radius && (!Hollow || System.Math.Abs(x) + System.Math.Abs(y) >= Radius || System.Math.Abs(x) + System.Math.Abs(z) >= Radius)) && (createType != CreatorMain.CreateType.Z || System.Math.Abs(z) + System.Math.Abs(y) <= Radius && System.Math.Abs(z) + System.Math.Abs(x) <= Radius && (!Hollow || System.Math.Abs(z) + System.Math.Abs(y) >= Radius || System.Math.Abs(z) + System.Math.Abs(x) >= Radius)))
                        {
                            yield return new Point3(Position.X + x, Position.Y + y, Position.Z + z);
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Pyramid(
          Point3 Position,
          int Radius,
          bool Hollow = false,
          CreatorMain.CreateType? XYZType = null,
          bool typeBool = false)
        {
            for (int x = -Radius; x <= Radius; ++x)
            {
                for (int y = -Radius; y <= Radius; ++y)
                {
                    for (int z = -Radius; z <= Radius; ++z)
                    {
                        if (XYZType.HasValue)
                        {
                            CreatorMain.CreateType? nullable = XYZType;
                            CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                            if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.X + x < Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.X + x > Position.X)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                            if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Y + y < Position.Y)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Y + y > Position.Y)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                            if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Z + z < Position.Z)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Z + z > Position.Z)
                                {
                                    continue;
                                }
                            }
                        }
                        if (System.Math.Abs(x) + System.Math.Abs(y) + System.Math.Abs(z) <= Radius && (!Hollow || System.Math.Abs(x) + System.Math.Abs(y) + System.Math.Abs(z) >= Radius))
                        {
                            yield return new Point3(Position.X + x, Position.Y + y, Position.Z + z);
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Cylindrical(
          Vector3 Position,
          int XRadius,
          int Height,
          int ZRadius,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool YType = true,
          bool Hollow = false)
        {
            int MaxRadius = System.Math.Max(XRadius, ZRadius);
            for (int x = -XRadius; x <= XRadius; ++x)
            {
                for (int z = -ZRadius; z <= ZRadius; ++z)
                {
                    if ((int)System.Math.Sqrt(x * (double)MaxRadius / XRadius * x * MaxRadius / XRadius + z * (double)MaxRadius / ZRadius * z * MaxRadius / ZRadius) <= MaxRadius && (!Hollow || (int)System.Math.Sqrt((System.Math.Abs(x) + 1.0) * MaxRadius / XRadius * (System.Math.Abs(x) + 1) * MaxRadius / XRadius + z * (double)MaxRadius / ZRadius * z * MaxRadius / ZRadius) > MaxRadius || (int)System.Math.Sqrt(x * (double)x * MaxRadius / XRadius * MaxRadius / XRadius + (System.Math.Abs(z) + 1) * (double)MaxRadius / ZRadius * (System.Math.Abs(z) + 1) * MaxRadius / ZRadius) > MaxRadius))
                    {
                        for (int y = 0; y < Height; ++y)
                        {
                            int num = !YType ? -y : y;
                            switch (createType)
                            {
                                case CreatorMain.CreateType.X:
                                    yield return new Point3((int)Position.X + num, (int)Position.Y + x, (int)Position.Z + z);
                                    break;
                                case CreatorMain.CreateType.Y:
                                    yield return new Point3((int)Position.X + x, (int)Position.Y + num, (int)Position.Z + z);
                                    break;
                                default:
                                    yield return new Point3((int)Position.X + x, (int)Position.Y + z, (int)Position.Z + num);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Cylindrical(
          Point3 Position,
          int Radius,
          int Height,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool YType = true,
          bool Hollow = false)
        {
            for (int x = -Radius; x <= Radius; ++x)
            {
                for (int z = -Radius; z <= Radius; ++z)
                {
                    if (System.Math.Sqrt(x * x + z * z) <= Radius && (!Hollow || System.Math.Sqrt((System.Math.Abs(x) + 1) * (System.Math.Abs(x) + 1) + z * z) > Radius || System.Math.Sqrt(x * x + (System.Math.Abs(z) + 1) * (System.Math.Abs(z) + 1)) > Radius))
                    {
                        for (int y = 0; y < Height; ++y)
                        {
                            int num = !YType ? -y : y;
                            switch (createType)
                            {
                                case CreatorMain.CreateType.X:
                                    yield return new Point3(Position.X + num, Position.Y + x, Position.Z + z);
                                    break;
                                case CreatorMain.CreateType.Y:
                                    yield return new Point3(Position.X + x, Position.Y + num, Position.Z + z);
                                    break;
                                default:
                                    yield return new Point3(Position.X + x, Position.Y + z, Position.Z + num);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> PrismColumn(
          Point3 Position,
          int Radius,
          int Height,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool YType = true,
          bool Hollow = false,
          CreatorMain.CreateType? XYZType = null,
          bool typeBool = false)
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = -Radius; x <= Radius; ++x)
                {
                    for (int z = -Radius; z <= Radius; ++z)
                    {
                        if (XYZType.HasValue)
                        {
                            CreatorMain.CreateType? nullable = XYZType;
                            CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                            if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.X + x < Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.X + x > Position.X)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                            if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Y + y < Position.Y)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Y + y > Position.Y)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                            if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Z + z < Position.Z)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Z + z > Position.Z)
                                {
                                    continue;
                                }
                            }
                        }
                        if (System.Math.Abs(x) + System.Math.Abs(z) <= Radius && (!Hollow || System.Math.Abs(x) + System.Math.Abs(z) >= Radius))
                        {
                            int num = !YType ? -y : y;
                            switch (createType)
                            {
                                case CreatorMain.CreateType.X:
                                    yield return new Point3(Position.X + num, Position.Y + x, Position.Z + z);
                                    continue;
                                case CreatorMain.CreateType.Y:
                                    yield return new Point3(Position.X + x, Position.Y + num, Position.Z + z);
                                    continue;
                                default:
                                    yield return new Point3(Position.X + x, Position.Y + z, Position.Z + num);
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Pillars(
          Point3 Position,
          int Radius,
          int Height,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool YType = true,
          bool Hollow = false,
          CreatorMain.CreateType? XYZType = null,
          bool typeBool = false)
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = -Radius; x <= Radius; ++x)
                {
                    for (int z = -Radius; z <= Radius; ++z)
                    {
                        if (XYZType.HasValue)
                        {
                            CreatorMain.CreateType? nullable = XYZType;
                            CreatorMain.CreateType createType1 = CreatorMain.CreateType.X;
                            if (nullable.GetValueOrDefault() == createType1 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.X + x < Position.X)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.X + x > Position.X)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType2 = CreatorMain.CreateType.Y;
                            if (nullable.GetValueOrDefault() == createType2 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Y + y < Position.Y)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Y + y > Position.Y)
                                {
                                    continue;
                                }
                            }
                            nullable = XYZType;
                            CreatorMain.CreateType createType3 = CreatorMain.CreateType.Z;
                            if (nullable.GetValueOrDefault() == createType3 & nullable.HasValue)
                            {
                                if (typeBool)
                                {
                                    if (Position.Z + z < Position.Z)
                                    {
                                        continue;
                                    }
                                }
                                else if (Position.Z + z > Position.Z)
                                {
                                    continue;
                                }
                            }
                        }
                        if ((System.Math.Abs(x) <= Radius || System.Math.Abs(z) <= Radius) && (!Hollow || System.Math.Abs(x) >= Radius || System.Math.Abs(z) >= Radius))
                        {
                            int num = !YType ? -y : y;
                            switch (createType)
                            {
                                case CreatorMain.CreateType.X:
                                    yield return new Point3(Position.X + num, Position.Y + x, Position.Z + z);
                                    continue;
                                case CreatorMain.CreateType.Y:
                                    yield return new Point3(Position.X + x, Position.Y + num, Position.Z + z);
                                    continue;
                                default:
                                    yield return new Point3(Position.X + x, Position.Y + z, Position.Z + num);
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Rectangular(Point3 Start, Point3 End, bool? type = null)
        {
            CreatorMain.Math.StartEnd(ref Start, ref End);
            for (int x = 0; x <= Start.X - End.X; ++x)
            {
                for (int y = 0; y <= Start.Y - End.Y; ++y)
                {
                    for (int z = 0; z <= Start.Z - End.Z; ++z)
                    {
                        bool? nullable = type;
                        bool flag1 = true;
                        if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) || x <= 0 || (x >= Start.X - End.X || y <= 0) || (y >= Start.Y - End.Y || z <= 0 || z >= Start.Z - End.Z))
                        {
                            nullable = type;
                            bool flag2 = false;
                            if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || (x < 0 || x > Start.X - End.X || (y <= 0 || y >= Start.Y - End.Y) || (z <= 0 || z >= Start.Z - End.Z)) && (y < 0 || y > Start.Y - End.Y || (x <= 0 || x >= Start.X - End.X) || (z <= 0 || z >= Start.Z - End.Z)) && (z < 0 || z > Start.Z - End.Z || (y <= 0 || y >= Start.Y - End.Y) || (x <= 0 || x >= Start.X - End.X)))
                            {
                                yield return new Point3(End.X + x, End.Y + y, End.Z + z);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Circle(
          Point3 Position,
          int Height,
          int Radius,
          CreatorMain.CreateType type = CreatorMain.CreateType.Y,
          bool Hollow = false)
        {
            int _Radius = Height - Radius;
            for (int x = -Radius; x <= Radius; ++x)
            {
                int _radius = _Radius + Radius - x;
                for (int z = -Radius; z <= Radius; ++z)
                {
                    if ((int)System.Math.Sqrt(x * x + z * z) <= Radius && (!Hollow || (int)System.Math.Sqrt((System.Math.Abs(x) + 1) * (System.Math.Abs(x) + 1) + z * z) > Radius || (int)System.Math.Sqrt(x * x + (System.Math.Abs(z) + 1) * (System.Math.Abs(z) + 1)) > Radius))
                    {
                        for (int x_2 = -_radius; x_2 <= _radius; ++x_2)
                        {
                            for (int z_2 = -_radius; z_2 <= _radius; ++z_2)
                            {
                                if ((int)System.Math.Sqrt(x_2 * x_2 + z_2 * z_2) <= _radius && ((int)System.Math.Sqrt((System.Math.Abs(x_2) + 0.5f) * (System.Math.Abs(x_2) + 0.5) + (System.Math.Abs(z_2) + 1) * (System.Math.Abs(z_2) + 1)) > _radius || (int)System.Math.Sqrt((System.Math.Abs(x_2) + 1) * (System.Math.Abs(x_2) + 1) + (System.Math.Abs(z_2) + 0.5) * (System.Math.Abs(z_2) + 0.5)) > _radius))
                                {
                                    switch (type)
                                    {
                                        case CreatorMain.CreateType.X:
                                            yield return new Point3(Position.X + z, Position.Y + x_2, Position.Z + z_2);
                                            continue;
                                        case CreatorMain.CreateType.Y:
                                            yield return new Point3(Position.X + x_2, Position.Y + z, Position.Z + z_2);
                                            continue;
                                        default:
                                            yield return new Point3(Position.X + z_2, Position.Y + x_2, Position.Z + z);
                                            continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Maze(Point3 Start, Point3 End)
        {
            CreatorMain.Math.StartEnd(ref Start, ref End);
            int StartX = Start.X - End.X;
            int StartZ = Start.Z - End.Z;
            bool[,] mazeArray = new CreatorModAPI.Maze(StartX / 2, StartZ / 2).GetBoolArray();
            for (int x = 0; x <= (StartX % 2 != 0 ? StartX - 1 : StartX); ++x)
            {
                for (int z = 0; z <= (StartZ % 2 != 0 ? StartZ - 1 : StartZ); ++z)
                {
                    if ((x != 1 || z != 0) && (x != (StartX % 2 != 0 ? StartX - 1 : StartX) || z != (StartZ % 2 != 0 ? StartZ - 1 : StartZ) - 1) && mazeArray[x, z])
                    {
                        for (int y = 0; y <= Start.Y - End.Y; ++y)
                        {
                            yield return new Point3(End.X + x, End.Y + y, End.Z + z);
                        }
                    }
                }
            }
        }

        public IEnumerable<Point3> Spiral(
          Point3 Position,
          int Height,
          int Radius,
          int Number,
          CreatorMain.CreateType createType = CreatorMain.CreateType.Y,
          bool YType = true)
        {
            for (int angle = 0; angle <= 360 * Number; ++angle)
            {
                float x = (float)(Radius * (double)angle / 360.0) * MathUtils.Cos((float)(angle * 3.14159274101257 / 180.0));
                float z = (float)(Radius * (double)angle / 360.0) * MathUtils.Sin((float)(angle * 3.14159274101257 / 180.0));
                for (int y = 0; y <= Height - 1; ++y)
                {
                    int num = !YType ? -y : y;
                    switch (createType)
                    {
                        case CreatorMain.CreateType.X:
                            yield return new Point3(Position.X + num, Position.Y + (int)x, Position.Z + (int)z);
                            break;
                        case CreatorMain.CreateType.Y:
                            yield return new Point3(Position.X + (int)x, Position.Y + num, Position.Z + (int)z);
                            break;
                        default:
                            yield return new Point3(Position.X + (int)x, Position.Y + (int)z, Position.Z + num);
                            break;
                    }
                }
            }
        }

        public IEnumerable<Point3> ThreePointPlane(Point3 p1, Point3 p2, Point3 p3)
        {
            List<Point3> listPoint3 = new List<Point3>();
            foreach (Point3 point3 in TwoPointLineGeneration(p1, p2))
            {
                listPoint3.Add(point3);
            }

            foreach (Point3 startPoint in listPoint3)
            {
                foreach (Point3 point3 in TwoPointLineGeneration(startPoint, p3))
                {
                    yield return point3;
                }
            }
            listPoint3.Clear();
            foreach (Point3 point3 in TwoPointLineGeneration(p1, p3))
            {
                listPoint3.Add(point3);
            }

            foreach (Point3 startPoint in listPoint3)
            {
                foreach (Point3 point3 in TwoPointLineGeneration(startPoint, p2))
                {
                    yield return point3;
                }
            }
            listPoint3.Clear();
            foreach (Point3 point3 in TwoPointLineGeneration(p3, p2))
            {
                listPoint3.Add(point3);
            }

            foreach (Point3 startPoint in listPoint3)
            {
                foreach (Point3 point3 in TwoPointLineGeneration(startPoint, p1))
                {
                    yield return point3;
                }
            }
        }

        public IEnumerable<Point3> ThreePointPlane2(Point3 p1, Point3 p2, Point3 p3)
        {
            Point3 point3_1;
            if (p2.Y > p1.Y)
            {
                point3_1 = p1;
                p1 = p2;
                p2 = point3_1;
            }
            if (p3.Y > p1.Y)
            {
                point3_1 = p1;
                p1 = p3;
                p3 = point3_1;
            }
            if (p3.X > p2.X)
            {
                point3_1 = p2;
                p2 = p3;
                p3 = point3_1;
            }
            List<Point3> listPoint3 = new List<Point3>();
            foreach (Point3 point3_2 in TwoPointLineGeneration(p1, p2))
            {
                listPoint3.Add(point3_2);
            }

            int lengin = System.Math.Max(MathUtils.Max(System.Math.Abs(p3.X - p1.X), System.Math.Abs(p3.Y - p1.Y)), System.Math.Abs(p3.Z - p1.Z));
            foreach (Point3 point3_2 in TwoPointLineGeneration(p3, p2))
            {
                Point3 point3 = point3_2;
                for (int num = 0; num <= lengin; ++num)
                {
                    point3_1 = new Point3(point3.X + (int)System.Math.Round(num / (double)lengin * (p1.X - p3.X)), point3.Y + (int)System.Math.Round(num / (double)lengin * (p1.Y - p3.Y)), point3.Z + (int)System.Math.Round(num / (double)lengin * (p1.Z - p3.Z)));
                    if (!listPoint3.Contains(point3_1))
                    {
                        yield return point3_1;
                    }
                    else
                    {
                        break;
                    }
                }
                point3 = new Point3();
            }
        }

        public IEnumerable<Point3> FourPointSpace(
          Point3 p1,
          Point3 p2,
          Point3 p3,
          Point3 p4)
        {
            foreach (Point3 point3 in ThreePointPlane(p1, p2, p3))
            {
                yield return point3;
            }

            foreach (Point3 point3 in ThreePointPlane(p1, p2, p4))
            {
                yield return point3;
            }

            foreach (Point3 point3 in ThreePointPlane(p1, p4, p3))
            {
                yield return point3;
            }

            foreach (Point3 point3 in ThreePointPlane(p4, p2, p3))
            {
                yield return point3;
            }
        }
    }
}
