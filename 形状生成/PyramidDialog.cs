// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.PyramidDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*棱锥生成*/
/*namespace CreatorModAPI-=  public class PyramidDialog : InterfaceDialog*/
using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class PyramidDialog : InterfaceDialog
    {
        private readonly SliderWidget Radius;
        private readonly LabelWidget delayLabel;
        private readonly ButtonWidget SoildButton;
        private readonly ButtonWidget HollowButton;

        public PyramidDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/Octahedron");

            LoadChildren(this, node);
            GeneralSet();
            Children.Find<StackPanelWidget>("XYZ").IsVisible = false;
            Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("pyddialog1");

            Radius = Children.Find<SliderWidget>("Slider");
            delayLabel = Children.Find<LabelWidget>("Slider data");
            SoildButton = Children.Find<ButtonWidget>("Solid");
            SoildButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Octahedron", "Solid");
            HollowButton = Children.Find<ButtonWidget>("Hollow");
            HollowButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Octahedron", "Hollow");

            Children.Find<ButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Octahedron", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            delayLabel.Text = string.Format(CreatorMain.Display_Key_Dialog("pyddialog2"), (int)Radius.Value);
            /*switch (CreatorAPI.Language)
            {
                case Language.zh_CN:
                    this.delayLabel.Text = string.Format("大小{0}块", (object)(int)this.Radius.Value);
                    break;
                case Language.en_US:
                    this.delayLabel.Text = string.Format("C-vertex D:{0}", (object)(int)this.Radius.Value);
                    break;
                default:
                    this.delayLabel.Text = string.Format("大小{0}块", (object)(int)this.Radius.Value);
                    break;
            }*/
            if (SoildButton.IsClicked)
            {
                Task.Run(() =>
               {
                   ChunkData chunkData = new ChunkData(creatorAPI);
                   creatorAPI.revokeData = new ChunkData(creatorAPI);
                   int num = 0;
                   foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Pyramid(creatorAPI.Position[0], (int)Radius.Value))
                   {
                       if (!creatorAPI.launch)
                       {
                           return;
                       }

                       creatorAPI.CreateBlock(point3, blockIconWidget.Value, chunkData);
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
                DialogsManager.HideDialog(this);
            }
            if (!HollowButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
           {
               ChunkData chunkData = new ChunkData(creatorAPI);
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               int num = 0;
               foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Pyramid(creatorAPI.Position[0], (int)Radius.Value, true))
               {
                   if (!creatorAPI.launch)
                   {
                       return;
                   }

                   creatorAPI.CreateBlock(point3, blockIconWidget.Value, chunkData);
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
            DialogsManager.HideDialog(this);
        }
    }
}
