// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.ReplaceDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*方块替换*/
/*namespace CreatorModAPI-=  public class ReplaceDialog : Dialog*/
using Engine;
using Game;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class ReplaceDialog : Dialog
    {
        private readonly ButtonWidget cancelButton;
        private readonly ButtonWidget replaceButton;
        private readonly ButtonWidget retainReplaceButton;
        private readonly ButtonWidget roughReplaceButton;
        private readonly TextBoxWidget Blockid;
        private readonly TextBoxWidget Blockid2;
        private readonly ComponentPlayer player;
        private readonly CreatorAPI creatorAPI;
        private readonly SubsystemTerrain subsystemTerrain = GameManager.Project.FindSubsystem<SubsystemTerrain>();

        public ReplaceDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/替换界面");

            LoadChildren(this, node);
            cancelButton = Children.Find<ButtonWidget>("取消");
            replaceButton = Children.Find<ButtonWidget>("替换");
            retainReplaceButton = Children.Find<ButtonWidget>("保留替换");
            roughReplaceButton = Children.Find<ButtonWidget>("粗糙替换");
            Blockid = Children.Find<TextBoxWidget>("方块ID");
            Blockid2 = Children.Find<TextBoxWidget>("方块ID2");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (roughReplaceButton.IsClicked)
            {
                Regex regex = new Regex("^[0-9]+$");
                if (Blockid2.Text == "")
                {
                    Blockid2.Text = "0";
                }

                Match match1 = regex.Match(Blockid.Text);
                Match match2 = regex.Match(Blockid2.Text);
                if (match1.Success && match2.Success)
                {
                    Point3 Start = creatorAPI.Position[0];
                    Point3 End = creatorAPI.Position[1];
                    CreatorMain.Math.StartEnd(ref Start, ref End);
                    Task.Run(() =>
                   {
                       int num = 0;
                       ChunkData chunkData = new ChunkData(creatorAPI);
                       creatorAPI.revokeData = new ChunkData(creatorAPI);
                       for (int index1 = 0; index1 <= Start.X - End.X; ++index1)
                       {
                           for (int index2 = 0; index2 <= Start.Y - End.Y; ++index2)
                           {
                               for (int index3 = 0; index3 <= Start.Z - End.Z; ++index3)
                               {
                                   int cellValueFast = subsystemTerrain.Terrain.GetCellValueFast(End.X + index1, End.Y + index2, End.Z + index3);
                                   if ((creatorAPI.AirIdentify || Terrain.ExtractContents(cellValueFast) != 0) && (cellValueFast == int.Parse(Blockid.Text) || Terrain.ExtractContents(cellValueFast) == int.Parse(Blockid.Text)))
                                   {
                                       if (!creatorAPI.launch)
                                       {
                                           return;
                                       }

                                       creatorAPI.CreateBlock(End.X + index1, End.Y + index2, End.Z + index3, int.Parse(Blockid2.Text), chunkData);
                                       ++num;
                                   }
                               }
                           }
                       }
                       chunkData.Render();
                       player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共替换{0}个方块", num), Color.LightYellow, true, true);
                   });
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage("操作失败", Color.Red, true, true);
                }

                DialogsManager.HideDialog(this);
            }
            if (!replaceButton.IsClicked && !retainReplaceButton.IsClicked)
            {
                return;
            }

            bool type = replaceButton.IsClicked;
            Regex regex1 = new Regex("^[0-9]+$");
            if (Blockid2.Text == "")
            {
                Blockid2.Text = "0";
            }

            Match match3 = regex1.Match(Blockid.Text);
            Match match4 = regex1.Match(Blockid2.Text);
            if (match3.Success && match4.Success)
            {
                Point3 Start = creatorAPI.Position[0];
                Point3 End = creatorAPI.Position[1];
                CreatorMain.Math.StartEnd(ref Start, ref End);
                Task.Run(() =>
               {
                   ChunkData chunkData = new ChunkData(creatorAPI);
                   creatorAPI.revokeData = new ChunkData(creatorAPI);
                   int num = 0;
                   for (int index1 = 0; index1 <= Start.X - End.X; ++index1)
                   {
                       for (int index2 = 0; index2 <= Start.Y - End.Y; ++index2)
                       {
                           for (int index3 = 0; index3 <= Start.Z - End.Z; ++index3)
                           {
                               int cellValueFast = subsystemTerrain.Terrain.GetCellValueFast(End.X + index1, End.Y + index2, End.Z + index3);
                               if (creatorAPI.AirIdentify || Terrain.ExtractContents(cellValueFast) != 0)
                               {
                                   if (type)
                                   {
                                       if (cellValueFast == int.Parse(Blockid.Text))
                                       {
                                           if (!creatorAPI.launch)
                                           {
                                               return;
                                           }

                                           creatorAPI.CreateBlock(End.X + index1, End.Y + index2, End.Z + index3, int.Parse(Blockid2.Text), chunkData);
                                           ++num;
                                       }
                                   }
                                   else if (cellValueFast != int.Parse(Blockid.Text))
                                   {
                                       if (!creatorAPI.launch)
                                       {
                                           return;
                                       }

                                       creatorAPI.CreateBlock(End.X + index1, End.Y + index2, End.Z + index3, int.Parse(Blockid2.Text), chunkData);
                                       ++num;
                                   }
                               }
                           }
                       }
                   }
                   chunkData.Render();
                   player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共替换{0}个方块", num), Color.LightYellow, true, true);
               });
            }
            else
            {
                player.ComponentGui.DisplaySmallMessage("操作失败", Color.Red, true, true);
            }

            DialogsManager.HideDialog(this);
        }
    }
}
