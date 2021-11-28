// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.SphereDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*球体生成*/
/*namespace CreatorModAPI-=  public class SphereDialog : InterfaceDialog*/
using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class SphereDialog : InterfaceDialog
    {
        private readonly SliderWidget XRadius;
        private readonly SliderWidget YRadius;
        private readonly SliderWidget ZRadius;
        private readonly LabelWidget XdelayLabel;
        private readonly LabelWidget YdelayLabel;
        private bool advanced;
        private readonly LabelWidget ZdelayLabel;
        private readonly ButtonWidget SoildButton;
        private readonly ButtonWidget HollowButton;
        private readonly ButtonWidget AdvancedButton;
        private readonly Widget AdvancedGenerate;
        private readonly ButtonWidget DoublePositionButton;
        private bool DoublePosition;

        public SphereDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            XElement node = ContentManager.Get<XElement>("Dialog/Sphere");
            LoadChildren(this, node);
            GeneralSet();
            Children.Find<LabelWidget>("Sphere").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "Sphere");
            XRadius = Children.Find<SliderWidget>("XSlider");
            YRadius = Children.Find<SliderWidget>("YSlider");
            ZRadius = Children.Find<SliderWidget>("ZSlider");
            XdelayLabel = Children.Find<LabelWidget>("XR");
            YdelayLabel = Children.Find<LabelWidget>("YR");
            ZdelayLabel = Children.Find<LabelWidget>("ZR");
            SoildButton = Children.Find<ButtonWidget>("Solid");
            SoildButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "Solid");
            HollowButton = Children.Find<ButtonWidget>("Hollow");
            HollowButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "Hollow");
            AdvancedButton = Children.Find<ButtonWidget>("ADV");
            AdvancedButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "ADV");
            AdvancedGenerate = Children.Find<Widget>("ADVG");
            DoublePositionButton = Children.Find<ButtonWidget>("Limit");
            DoublePositionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "Limit");
            Children.Find<BevelledButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Sphere", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            AdvancedButton.Color = advanced ? Color.Yellow : Color.White;
            DoublePositionButton.Color = DoublePosition ? Color.Yellow : Color.White;
            AdvancedGenerate.IsVisible = advanced;
            DoublePositionButton.IsVisible = advanced;
            XdelayLabel.Text = advanced ? string.Format(CreatorMain.Display_Key_Dialog("spdialogx"), (int)XRadius.Value) : string.Format(CreatorMain.Display_Key_Dialog("spdialogr"), (int)XRadius.Value);
            YdelayLabel.Text = string.Format(CreatorMain.Display_Key_Dialog("spdialogy"), (int)YRadius.Value);
            ZdelayLabel.Text = string.Format(CreatorMain.Display_Key_Dialog("spdialogz"), (int)ZRadius.Value);


            int id = blockIconWidget.Value;
            if (DoublePositionButton.IsClicked)
            {
                DoublePosition = !DoublePosition;
            }

            if (AdvancedButton.IsClicked)
            {
                advanced = !advanced;
            }

            if (SoildButton.IsClicked)
            {
                if (advanced)
                {
                    if (!DoublePosition)
                    {
                        Task.Run(() =>
                       {
                           ChunkData chunkData = new ChunkData(creatorAPI);
                           creatorAPI.revokeData = new ChunkData(creatorAPI);
                           int num = 0;
                           foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(creatorAPI.Position[0]), (int)XRadius.Value, (int)YRadius.Value, (int)ZRadius.Value))
                           {
                               if (!creatorAPI.launch)
                               {
                                   return;
                               }

                               creatorAPI.CreateBlock(point3, id, chunkData);
                               ++num;
                           }
                           chunkData.Render();
                           player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

                           /*switch (CreatorAPI.Language)
                           {
                               case Language.zh_CN:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                                   break;
                               case Language.en_US:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                                   break;
                               default:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                                   break;
                           }*/

                       });
                    }
                    else if (creatorAPI.Position[1].Y != -1)
                    {
                        Point3 Start = creatorAPI.Position[0];
                        Point3 End = creatorAPI.Position[1];
                        CreatorMain.Math.StartEnd(ref Start, ref End);
                        float x = System.Math.Abs(Start.X - (float)End.X) / 2f;
                        float y = System.Math.Abs(Start.Y - (float)End.Y) / 2f;
                        float z = System.Math.Abs(Start.Z - (float)End.Z) / 2f;
                        Task.Run(() =>
                       {
                           ChunkData chunkData = new ChunkData(creatorAPI);
                           creatorAPI.revokeData = new ChunkData(creatorAPI);
                           int num = 0;
                           foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(End.X + x, End.Y + y, End.Z + z), (int)x, (int)y, (int)z))
                           {
                               if (!creatorAPI.launch)
                               {
                                   return;
                               }

                               creatorAPI.CreateBlock(point3, id, chunkData);
                               ++num;
                           }
                           chunkData.Render();

                           /*switch (CreatorAPI.Language)
                           {
                               case Language.zh_CN:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                                   break;
                               case Language.en_US:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                                   break;
                               default:
                                   this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                                   break;
                           }*/

                       });
                    }
                }
                else
                {
                    Task.Run(() =>
                   {
                       ChunkData chunkData = new ChunkData(creatorAPI);
                       creatorAPI.revokeData = new ChunkData(creatorAPI);
                       int num = 0;
                       foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(creatorAPI.Position[0]), (int)XRadius.Value, (int)XRadius.Value, (int)XRadius.Value))
                       {
                           if (!creatorAPI.launch)
                           {
                               return;
                           }

                           creatorAPI.CreateBlock(point3, id, chunkData);
                           ++num;
                       }
                       chunkData.Render();
                       player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

                       /*switch (CreatorAPI.Language)
                       {
                           case Language.zh_CN:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                           case Language.en_US:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                               break;
                           default:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                       }*/
                   });
                }

                DialogsManager.HideDialog(this);
            }
            if (!HollowButton.IsClicked)
            {
                return;
            }

            if (advanced)
            {
                if (!DoublePosition)
                {
                    Task.Run(() =>
                   {
                       int num = 0;
                       ChunkData chunkData = new ChunkData(creatorAPI);
                       creatorAPI.revokeData = new ChunkData(creatorAPI);
                       foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(creatorAPI.Position[0]), (int)XRadius.Value, (int)YRadius.Value, (int)ZRadius.Value, true))
                       {
                           if (!creatorAPI.launch)
                           {
                               return;
                           }

                           creatorAPI.CreateBlock(point3, id, chunkData);
                           ++num;
                       }
                       chunkData.Render();
                       player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

                       /*switch (CreatorAPI.Language)
                       {
                           case Language.zh_CN:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                           case Language.en_US:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                               break;
                           default:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                       }*/
                   });
                }
                else if (creatorAPI.Position[1].Y != -1)
                {
                    Point3 Start = creatorAPI.Position[0];
                    Point3 End = creatorAPI.Position[1];
                    CreatorMain.Math.StartEnd(ref Start, ref End);
                    float x = System.Math.Abs(Start.X - (float)End.X) / 2f;
                    float y = System.Math.Abs(Start.Y - (float)End.Y) / 2f;
                    float z = System.Math.Abs(Start.Z - (float)End.Z) / 2f;
                    Task.Run(() =>
                   {
                       ChunkData chunkData = new ChunkData(creatorAPI);
                       creatorAPI.revokeData = new ChunkData(creatorAPI);
                       int num = 0;
                       foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(End.X + x, End.Y + y, End.Z + z), (int)x, (int)y, (int)z, true))
                       {
                           if (!creatorAPI.launch)
                           {
                               return;
                           }

                           creatorAPI.CreateBlock(point3, id, chunkData);
                           ++num;
                       }
                       chunkData.Render();
                       player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

                       /* switch (CreatorAPI.Language)
                       {
                           case Language.zh_CN:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                           case Language.en_US:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                               break;
                           default:
                               this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                               break;
                       }*/
                   });
                }
            }
            else
            {
                Task.Run(() =>
               {
                   ChunkData chunkData = new ChunkData(creatorAPI);
                   creatorAPI.revokeData = new ChunkData(creatorAPI);
                   int num = 0;
                   foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Sphere(new Vector3(creatorAPI.Position[0]), (int)XRadius.Value, (int)XRadius.Value, (int)XRadius.Value, true))
                   {
                       if (!creatorAPI.launch)
                       {
                           return;
                       }

                       creatorAPI.CreateBlock(point3, id, chunkData);
                       ++num;
                   }
                   chunkData.Render();
                   player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

                   /*switch (CreatorAPI.Language)
                   {
                       case Language.zh_CN:
                           this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                           break;
                       case Language.en_US:
                           this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true);
                           break;
                       default:
                           this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true);
                           break;
                   }*/
               });
            }

            DialogsManager.HideDialog(this);
        }
    }
}
