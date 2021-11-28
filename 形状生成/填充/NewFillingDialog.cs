// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.RectangularDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*矩形生成*/
/*namespace CreatorModAPI-=  public class RectangularDialog : InterfaceDialog*/
using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class NewFillingDialog : InterfaceDialog
    {
        private readonly ButtonWidget SoildButton;

        public NewFillingDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/Fill");

            LoadChildren(this, node);
            GeneralSet();
            Children.Find<LabelWidget>("Fill").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Fill", "Fill");
            SoildButton = Children.Find<ButtonWidget>("Fill1");
            SoildButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Fill", "Fill1");
            Children.Find<BevelledButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Fill", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            if (!SoildButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
       {
           ChunkData chunkData = new ChunkData(creatorAPI);
           creatorAPI.revokeData = new ChunkData(creatorAPI);
           int num = 0;
           foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Rectangular(creatorAPI.Position[0], creatorAPI.Position[1]))
           {
               if (!creatorAPI.launch)
               {
                   return;
               }
               switch (Terrain.ExtractContents(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValueFast(point3.X, point3.Y, point3.Z)))
               {
                   case 0:
                       creatorAPI.CreateBlock(point3, blockIconWidget.Value, chunkData);
                       ++num;
                       break;
                   default:
                       break;
               }

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
       }
        );
            DialogsManager.HideDialog(this);
        }
    }
}
