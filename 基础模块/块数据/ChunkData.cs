using Engine;
using Game;
using System;
using System.Collections.Generic;

namespace CreatorModAPI
{
    public class ChunkData
    {
        public class Chunk
        {
            public int chunkX;

            public int chunkY;

            public int[] Cells;

            public Chunk(int chunkX, int chunkY)
            {
                this.chunkX = chunkX;
                this.chunkY = chunkY;
            }
        }

        public List<Chunk> chunksData = new List<Chunk>();

        private readonly SubsystemTerrain subsystemTerrain;

        private readonly CreatorAPI creatorAPI;

        public ChunkData(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            subsystemTerrain = creatorAPI.componentMiner.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
        }

        public Chunk CreateChunk(int x, int z, bool unLimited = false)
        {
            int chunkX = x >> 4;
            int num = z >> 4;
            TerrainChunk terrainChunk = subsystemTerrain.Terrain.GetChunkAtCoords(chunkX, num);
            if (terrainChunk == null)
            {
                if (!unLimited)
                {
                    return null;
                }

                terrainChunk = subsystemTerrain.Terrain.AllocateChunk(chunkX, num);
                while (terrainChunk.ThreadState < TerrainChunkState.Valid)
                {
                    subsystemTerrain.TerrainUpdater.UpdateChunkSingleStep(terrainChunk, 15);
                }
            }

            Chunk chunk = new Chunk(chunkX, num)
            {
                Cells = new int[terrainChunk.Cells.Length]
            };
            for (int i = 0; i < terrainChunk.Cells.Length; i++)
            {
                chunk.Cells[i] = terrainChunk.Cells[i];
            }

            chunksData.Add(chunk);
            return chunk;
        }

        public int GetCellValueFast(int x, int y, int z)
        {
            int num = x >> 4;
            int num2 = z >> 4;
            Chunk chunk = null;
            foreach (Chunk chunksDatum in chunksData)
            {
                if (chunksDatum.chunkX == num && chunksDatum.chunkY == num2)
                {
                    chunk = chunksDatum;
                    break;
                }
            }

            if (chunk == null)
            {
                return 0;
            }

            return chunk.Cells[y + (x & 0xF) * 256 + (z & 0xF) * 256 * 16];
        }

        public Chunk GetChunk(int x, int z)
        {
            int num = x >> 4;
            int num2 = z >> 4;
            foreach (Chunk chunksDatum in chunksData)
            {
                if (chunksDatum.chunkX == num && chunksDatum.chunkY == num2)
                {
                    return chunksDatum;
                }
            }

            return null;
        }

        public virtual void SetBlock(int x, int y, int z, int value)
        {
            try
            {
                int num;
                Chunk chunk;
                if (y >= 0 && y < 256)
                {
                    num = y + (x & 0xF) * 256 + (z & 0xF) * 256 * 16;
                    chunk = GetChunk(x, z);
                    if (chunk != null)
                    {
                        goto IL_004e;
                    }

                    chunk = CreateChunk(x, z, creatorAPI.UnLimitedOfCreate);
                    if (chunk != null)
                    {
                        goto IL_004e;
                    }
                }

                goto end_IL_0000;
IL_004e:
                if (num < chunk.Cells.Length)
                {
                    chunk.Cells[num] = value;
                }

end_IL_0000:;
            }
            catch (Exception ex)
            {
                Log.Error(CreatorMain.Display_Key_Dialog("chunkDataErr") + ex.Message);
            }
        }

        public virtual void SetBlock(Point3 point3, int value)
        {
            SetBlock(point3.X, point3.Y, point3.Z, value);
        }

        public virtual void Render()
        {
            foreach (Chunk chunksDatum in chunksData)
            {
                TerrainChunk terrainChunk = subsystemTerrain.Terrain.GetChunkAtCoords(chunksDatum.chunkX, chunksDatum.chunkY);
                if (terrainChunk == null)
                {
                    if (!creatorAPI.UnLimitedOfCreate)
                    {
                        continue;
                    }

                    terrainChunk = subsystemTerrain.Terrain.AllocateChunk(chunksDatum.chunkX, chunksDatum.chunkY);
                }

                for (int i = 0; i < terrainChunk.Cells.Length; i++)
                {
                    terrainChunk.Cells[i] = chunksDatum.Cells[i];
                }

                terrainChunk.ModificationCounter++;
                if (creatorAPI.UnLimitedOfCreate)
                {
                    terrainChunk.State = TerrainChunkState.Valid;
                }

                if (terrainChunk.State > TerrainChunkState.InvalidLight)
                {
                    terrainChunk.State = TerrainChunkState.InvalidLight;
                }

                terrainChunk.WasDowngraded = true;
            }

            if (this != creatorAPI.revokeData)
            {
                chunksData.Clear();
            }
        }

        public void Clear()
        {
            chunksData.Clear();
        }
    }
}

/*using Engine;
using Game;
using System;
using System.Collections.Generic;

namespace CreatorModAPI
{
    public class ChunkData
    {
        public List<ChunkData.Chunk> chunksData = new List<ChunkData.Chunk>();
        private readonly SubsystemTerrain subsystemTerrain;
        private readonly CreatorAPI creatorAPI;

        public ChunkData(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            subsystemTerrain = creatorAPI.componentMiner.Project.FindSubsystem<SubsystemTerrain>(true);
        }

        public ChunkData.Chunk CreateChunk(int x, int z, bool unLimited = false)
        {
            int chunkX = x >> 4;
            int num = z >> 4;
            TerrainChunk chunk1 = subsystemTerrain.Terrain.GetChunkAtCoords(chunkX, num);
            if (chunk1 == null)
            {
                if (!unLimited)
                {
                    return null;
                }

                chunk1 = subsystemTerrain.Terrain.AllocateChunk(chunkX, num);
                while (chunk1.ThreadState < TerrainChunkState.Valid)
                {
                    subsystemTerrain.TerrainUpdater.UpdateChunkSingleStep(chunk1, 15);
                }
            }
            ChunkData.Chunk chunk2 = new ChunkData.Chunk(chunkX, num)
            {
                Cells = new int[chunk1.Cells.Length]
            };
            for (int index = 0; index < chunk1.Cells.Length; ++index)
            {
                chunk2.Cells[index] = chunk1.Cells[index];
            }

            chunksData.Add(chunk2);
            return chunk2;
        }

        public int GetCellValueFast(int x, int y, int z)
        {
            int num1 = x >> 4;
            int num2 = z >> 4;
            ChunkData.Chunk chunk1 = null;
            foreach (ChunkData.Chunk chunk2 in chunksData)
            {
                if (chunk2.chunkX == num1 && chunk2.chunkY == num2)
                {
                    chunk1 = chunk2;
                    break;
                }
            }
            return chunk1 != null ? chunk1.Cells[y + (x & 15) * 256 + (z & 15) * 256 * 16] : 0;
        }

        public ChunkData.Chunk GetChunk(int x, int z)
        {
            int num1 = x >> 4;
            int num2 = z >> 4;
            foreach (ChunkData.Chunk chunk in chunksData)
            {
                if (chunk.chunkX == num1 && chunk.chunkY == num2)
                {
                    return chunk;
                }
            }
            return null;
        }

        public virtual void SetBlock(int x, int y, int z, int value)
        {
            try
            {
                if (y < 0 || y >= 256)
                {
                    return;
                }

                int index = y + (x & 15) * 256 + (z & 15) * 256 * 16;
                ChunkData.Chunk chunk = GetChunk(x, z);
                if (chunk == null)
                {
                    chunk = CreateChunk(x, z, creatorAPI.UnLimitedOfCreate);
                    if (chunk == null)
                    {
                        return;
                    }
                }
                if (index >= chunk.Cells.Length)
                {
                    return;
                }

                chunk.Cells[index] = value;
            }
            catch (Exception ex)
            {

                Log.Error(CreatorMain.Display_Key_Dialog("chunkDataErr") + ex.Message);
                *//*   switch (CreatorAPI.Language)
                   {
                       case Language.zh_CN:
                           Log.Error("缓存生成发生错误:" + ex.Message);
                           break;
                       case Language.en_US:
                           Log.Error("An error has occurred in the cache generation function:" + ex.Message);
                           break;
                       default:
                           Log.Error("缓存生成发生错误:" + ex.Message);
                           break;
                   }*//*

            }
        }

        public virtual void SetBlock(Point3 point3, int value) => SetBlock(point3.X, point3.Y, point3.Z, value);

        public virtual void Render()
        {
            foreach (ChunkData.Chunk chunk in chunksData)
            {
                TerrainChunk terrainChunk = subsystemTerrain.Terrain.GetChunkAtCoords(chunk.chunkX, chunk.chunkY);
                if (terrainChunk == null)
                {
                    if (creatorAPI.UnLimitedOfCreate)
                    {
                        terrainChunk = subsystemTerrain.Terrain.AllocateChunk(chunk.chunkX, chunk.chunkY);
                    }
                    else
                    {
                        continue;
                    }
                }
                for (int index = 0; index < terrainChunk.Cells.Length; ++index)
                {
                    terrainChunk.Cells[index] = chunk.Cells[index];
                }

                ++terrainChunk.ModificationCounter;
                if (creatorAPI.UnLimitedOfCreate)
                {
                    terrainChunk.State = TerrainChunkState.Valid;
                }

                if (terrainChunk.State > TerrainChunkState.InvalidLight)
                {
                    terrainChunk.State = TerrainChunkState.InvalidLight;
                }

                terrainChunk.WasDowngraded = true;
            }
            if (this == creatorAPI.revokeData)
            {
                return;
            }

            chunksData.Clear();
        }

        public void Clear() => chunksData.Clear();

        public class Chunk
        {
            public int chunkX;
            public int chunkY;
            public int[] Cells;

            public Chunk(int chunkX, int chunkY)
            {
                this.chunkX = chunkX;
                this.chunkY = chunkY;
            }
        }
    }
}
*/