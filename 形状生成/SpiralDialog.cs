using Engine;
using Game;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public class SpiralDialog : PillarsDialog
    {
        private readonly SliderWidget Number;

        private readonly LabelWidget numberLabelWidget;

        public SpiralDialog(CreatorAPI creatorAPI)
            : base(creatorAPI)
        {
            (Children.Find<LabelWidget>("Name")).Text = (CreatorMain.Display_Key_Dialog("spidialog1"));
            Children.Find<StackPanelWidget>("Data3").IsVisible = true;
            Number = Children.Find<SliderWidget>("Slider3");
            numberLabelWidget = Children.Find<LabelWidget>("Slider data3");
            SoildButton.Text = CreatorMain.Display_Key_Dialog("spidialog2");
            HollowButton.IsVisible = false;
        }

        public override void Update()
        {
            base.Update();
            (radiusDelayLabel).Text = (string.Format(CreatorMain.Display_Key_Dialog("spidialogr"), (int)Radius.Value));
            (numberLabelWidget).Text = (string.Format(CreatorMain.Display_Key_Dialog("spidialogn"), (int)Number.Value));
        }

        public override void upClickButton(int id)
        {
            if (!SoildButton.IsClicked)
            {
                return;
            }

            Task.Run(delegate
            {
                ChunkData chunkData = new ChunkData(creatorAPI);
                creatorAPI.revokeData = new ChunkData(creatorAPI);
                int num = 0;
                foreach (Point3 item in creatorAPI.creatorGenerationAlgorithm.Spiral(creatorAPI.Position[0], (int)Height.Value, (int)Radius.Value, (int)Number.Value, createType, typeBool))
                {
                    if (!creatorAPI.launch)
                    {
                        return;
                    }

                    creatorAPI.CreateBlock(item, id, chunkData);
                    num++;
                }

                chunkData.Render();
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, blinking: true, playNotificationSound: true);
            });
            DialogsManager.HideDialog(this);
        }
    }
}

/*螺旋线生成*/
/*namespace CreatorModAPI-=  public class SpiralDialog : PillarsDialog*//*
using Engine;
using Game;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public class SpiralDialog : PillarsDialog
    {
        private readonly SliderWidget Number;
        private readonly LabelWidget numberLabelWidget;

        public SpiralDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("spidialog1");

            Children.Find<StackPanelWidget>("Data3").IsVisible = true;
            Number = Children.Find<SliderWidget>("Slider3");
            numberLabelWidget = Children.Find<LabelWidget>("Slider data3");
            SoildButton.Text = CreatorMain.Display_Key_Dialog("spidialog2");

            HollowButton.IsVisible = false;
        }

        public override void Update()
        {
            base.Update();
            radiusDelayLabel.Text = string.Format(CreatorMain.Display_Key_Dialog("spidialogr"), (int)Radius.Value);
            numberLabelWidget.Text = string.Format(CreatorMain.Display_Key_Dialog("spidialogn"), (int)Number.Value);


        }

        public override void upClickButton(int id)
        {
            if (!SoildButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
           {
               ChunkData chunkData = new ChunkData(creatorAPI);
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               int num = 0;
               foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Spiral(creatorAPI.Position[0], (int)Height.Value, (int)Radius.Value, (int)Number.Value, createType, typeBool))
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

               *//*switch (CreatorAPI.Language)
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
               }*//*
           });
            DialogsManager.HideDialog(this);
        }
    }
}
*/