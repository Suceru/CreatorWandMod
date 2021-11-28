using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class FourPointSpaceDialog : InterfaceDialog
    {
        private readonly ButtonWidget OKButton;

        public FourPointSpaceDialog(CreatorAPI creatorAPI)
            : base(creatorAPI)
        {
            base.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
            XElement node = ContentManager.Get<XElement>("Dialog/Manager3", (string)null);
            LoadChildren(this, node);
            GeneralSet();
            ((FontTextWidget)Children.Find<LabelWidget>("Name")).Text=(CreatorMain.Display_Key_Dialog("fpointSdialog1"));
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "OK");
            Children.Find<BevelledButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            if (!OKButton.IsClicked)
            {
                return;
            }

            Task.Run(delegate
            {
                int num = 0;
                ChunkData chunkData = new ChunkData(creatorAPI);
                creatorAPI.revokeData = new ChunkData(creatorAPI);
                foreach (Point3 item in creatorAPI.creatorGenerationAlgorithm.FourPointSpace(creatorAPI.Position[0], creatorAPI.Position[1], creatorAPI.Position[2], creatorAPI.Position[3]))
                {
                    creatorAPI.CreateBlock(item, blockIconWidget.Value, chunkData);
                    num++;
                    if (!creatorAPI.launch)
                    {
                        return;
                    }
                }

                chunkData.Render();
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("fpointSdialog2"), num), Color.LightYellow, blinking: true, playNotificationSound: true);
            });
            DialogsManager.HideDialog(this);
        }
    }
}

/*四点空间*/
/*namespace CreatorModAPI-=  public class FourPointSpaceDialog : InterfaceDialog*//*
using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class FourPointSpaceDialog : InterfaceDialog
    {
        private readonly ButtonWidget OKButton;

        public FourPointSpaceDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/Manager3");

            LoadChildren(this, node);
            GeneralSet();

            Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("fpointSdialog1");

            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "OK");
            Children.Find<BevelledButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            if (!OKButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
           {
               int num = 0;
               ChunkData chunkData = new ChunkData(creatorAPI);
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.FourPointSpace(creatorAPI.Position[0], creatorAPI.Position[1], creatorAPI.Position[2], creatorAPI.Position[3]))
               {
                   creatorAPI.CreateBlock(point3, blockIconWidget.Value, chunkData);
                   ++num;
                   if (!creatorAPI.launch)
                   {
                       return;
                   }
               }
               chunkData.Render();

               player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("fpointSdialog2"), num), Color.LightYellow, true, true);
               *//*
                              switch (CreatorAPI.Language)
                              {
                                  case Language.zh_CN:
                                      this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true); break;
                                  case Language.en_US:
                                      this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0} blocks", (object)num), Color.LightYellow, true, true); break;
                                  default:
                                      this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num), Color.LightYellow, true, true); break;
                              }*//*
           });
            DialogsManager.HideDialog(this);
        }
    }
}
*/