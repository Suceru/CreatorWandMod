// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.CreatorNormalTerrain
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

using Engine;
using Game;
using System;

namespace CreatorModAPI
{
    public class CreatorNormalTerrain : ITerrainContentsGenerator
    {
        public int OceanLevel => throw new NotImplementedException();

        public float CalculateHeight(float x, float z) => throw new NotImplementedException();

        public int CalculateHumidity(float x, float z) => throw new NotImplementedException();

        public float CalculateMountainRangeFactor(float x, float z) => throw new NotImplementedException();

        public float CalculateOceanShoreDistance(float x, float z) => throw new NotImplementedException();

        public int CalculateTemperature(float x, float z) => throw new NotImplementedException();

        public Vector3 FindCoarseSpawnPosition() => throw new NotImplementedException();

        public void GenerateChunkContentsPass1(TerrainChunk chunk) => throw new NotImplementedException();

        public void GenerateChunkContentsPass2(TerrainChunk chunk) => throw new NotImplementedException();

        public void GenerateChunkContentsPass3(TerrainChunk chunk) => throw new NotImplementedException();

        public void GenerateChunkContentsPass4(TerrainChunk chunk) => throw new NotImplementedException();
    }
}
