/*编辑区域*/
/*namespace CreatorModAPI-=  public class EditRegionDialog : Dialog*/
using Engine;
using Game;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class EditRegionDialog : Dialog
    {
        public SliderWidget TemperatureSlider;
        public SliderWidget HumiditySlider;
        public SliderWidget TopHeightSlider;
        public LabelWidget SliderData;
        public ButtonWidget OKButton;
        public ButtonWidget cancelButton;
        // private readonly SubsystemTerrain subsystemTerrain;
        private readonly CreatorAPI creatorAPI;
        private readonly ComponentPlayer player;

        public EditRegionDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            //   subsystemTerrain = creatorAPI.componentMiner.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/编辑区域");

            LoadChildren(this, node);
            SliderData = Children.Find<LabelWidget>("滑条数据");
            TemperatureSlider = Children.Find<SliderWidget>("滑条1");
            TemperatureSlider.Value = GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetSeasonalTemperature(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z);
            HumiditySlider = Children.Find<SliderWidget>("滑条2");
            HumiditySlider.Value = GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetSeasonalHumidity(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z);
            TopHeightSlider = Children.Find<SliderWidget>("滑条3");
            OKButton = Children.Find<ButtonWidget>("确定");
            cancelButton = Children.Find<ButtonWidget>("取消");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (OKButton.IsClicked)
            {
                Point3 Start = creatorAPI.Position[0];
                Point3 End = creatorAPI.Position[1];
                // !string.IsNullOrEmpty(Start.ToString()) && !string.IsNullOrEmpty(End.ToString())
                if ((Start.Y != -1) && (End.Y != -1))
                {
                    CreatorMain.Math.StartEnd(ref Start, ref End);

                    for (int x = End.X; x <= Start.X; ++x)
                    {
                        for (int z = End.Z; z <= Start.Z; ++z)
                        {
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetChunkAtCell(x, z).SetTemperatureFast(x, z, (int)TemperatureSlider.Value);
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetChunkAtCell(x, z).SetHumidityFast(x, z, (int)HumiditySlider.Value);

                        }
                    }
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog1"), Color.LightYellow, true, true);

                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog2") + string.Format("\nStart:{0} End:{1}", Start.Y, End.Y), Color.LightYellow, true, true);



                }
                DialogsManager.HideDialog(this);
            }
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            SliderData.Text = string.Format(CreatorMain.Display_Key_Dialog("editchunkdialog3"), (int)TemperatureSlider.Value, (int)HumiditySlider.Value);

        }
    }
}
