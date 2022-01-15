using Engine;
using Game;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class NewEditChunkDialog : Dialog
    {
        public SliderWidget TemperatureSlider;

        public SliderWidget HumiditySlider;

        public LabelWidget SliderData;

        public ButtonWidget OKButton;

        public ButtonWidget cancelButton;

        private readonly CreatorAPI creatorAPI;

        private readonly ComponentPlayer player;

        public NewEditChunkDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/EditChunk");
            LoadChildren(this, node);
            (Children.Find<LabelWidget>("EditingChunk")).Text = (CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "EditingChunk"));
            SliderData = Children.Find<LabelWidget>("Slider Data");
            TemperatureSlider = Children.Find<SliderWidget>("Slider 1");
            TemperatureSlider.Value = (GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(this.creatorAPI.Position[0].X, this.creatorAPI.Position[0].Z) + GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTemperature(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z)) / 2;
            HumiditySlider = Children.Find<SliderWidget>("Slider 2");
            HumiditySlider.Value = (GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(this.creatorAPI.Position[0].X, this.creatorAPI.Position[0].Z) + GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetHumidity(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z)) / 2;
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "OK");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (OKButton.IsClicked)
            {
                Point3 Start = creatorAPI.Position[0];
                Point3 End = creatorAPI.Position[1];
                if (Start.Y != -1 && End.Y != -1)
                {
                    CreatorMain.Math.StarttoEnd(ref Start, ref End);
                    Point2 p = new Point2(int.MinValue, int.MinValue);
                    for (int i = Start.X; i <= End.X; i++)
                    {
                        for (int j = Start.Z; j <= End.Z; j++)
                        {
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.SetTemperature(i, j, (int)TemperatureSlider.Value);
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.SetHumidity(i, j, (int)HumiditySlider.Value);
                            if (!(p == new Point2(i >> 4 << 4, j >> 4 << 4)))
                            {
                                p = new Point2(i >> 4 << 4, j >> 4 << 4);
                                int cellValue = GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetCellValue(p.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTopHeight(i, j), p.Y);
                                GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(p.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTopHeight(p.X, p.Y), p.Y, Terrain.MakeBlockValue(1));
                                GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).ChangeCell(p.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Terrain.GetTopHeight(p.X, p.Y), p.Y, cellValue);
                            }
                        }
                    }

                    GameManager.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true).Project.Save();
                    GameManager.Project.FindSubsystem<SubsystemUpdate>(throwOnError: true).Update();
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog1"), Color.LightYellow, blinking: true, playNotificationSound: true);
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog2") + $"\nStart:{Start.Y} End:{End.Y}", Color.LightYellow, blinking: true, playNotificationSound: true);
                }

                DialogsManager.HideDialog(this);
            }

            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            (SliderData).Text = (string.Format(CreatorMain.Display_Key_Dialog("editchunkdialog3"), (int)TemperatureSlider.Value, (int)HumiditySlider.Value));
        }
    }
}

/*编辑区域*/
/*namespace CreatorModAPI-=  public class EditRegionDialog : Dialog*//*
using Engine;
using Game;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class NewEditChunkDialog : Dialog
    {
        public SliderWidget TemperatureSlider;
        public SliderWidget HumiditySlider;
        // public SliderWidget TopHeightSlider;
        public LabelWidget SliderData;
        public ButtonWidget OKButton;
        public ButtonWidget cancelButton;
        //   private readonly SubsystemTerrain subsystemTerrain;
        private readonly CreatorAPI creatorAPI;
        private readonly ComponentPlayer player;

        public NewEditChunkDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            //  subsystemTerrain = creatorAPI.componentMiner.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/EditChunk");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("EditingChunk").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "EditingChunk");
            SliderData = Children.Find<LabelWidget>("Slider Data");
            TemperatureSlider = Children.Find<SliderWidget>("Slider 1");
            TemperatureSlider.Value = (GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(this.creatorAPI.Position[0].X, this.creatorAPI.Position[0].Z) + GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTemperature(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z)) / 2;
            HumiditySlider = Children.Find<SliderWidget>("Slider 2");
            HumiditySlider.Value = (GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(this.creatorAPI.Position[0].X, this.creatorAPI.Position[0].Z) + GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetHumidity(this.creatorAPI.Position[1].X, this.creatorAPI.Position[1].Z)) / 2;

            //  this.TopHeightSlider = this.Children.Find<SliderWidget>("滑条3");
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "OK");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditChunk", "Cancel");
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
                    CreatorMain.Math.StarttoEnd(ref Start, ref End);
                    Point2 point2 = new Point2(int.MinValue, int.MinValue);
                    //  TerrainChunk terrainChunk
                      *//* = GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetChunkAtCell(Start.X, Start.Z)*//*;
                    for (int x = Start.X; x <= End.X; ++x)
                    {
                        for (int z = Start.Z; z <= End.Z; ++z)
                        {
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.SetTemperature(x, z, (int)TemperatureSlider.Value);
                            GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.SetHumidity(x, z, (int)HumiditySlider.Value);
                            // GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(x, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(x, z), z, Terrain.MakeBlockValue(1));

                            switch (point2 == new Point2((x >> 4) << 4, (z >> 4) << 4))
                            {
                                case true:

                                    break;
                                default:
                                    point2 = new Point2((x >> 4) << 4, (z >> 4) << 4);
                                    int sblock = GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetCellValue(point2.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(x, z), point2.Y);
                                    GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(point2.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(point2.X, point2.Y), point2.Y, Terrain.MakeBlockValue(1));
                                    GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(point2.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(point2.X, point2.Y), point2.Y, sblock);
                                    //  GameManager.Project.FindSubsystem<SubsystemTerrain>(true).ChangeCell(point2.X, GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(x, z), point2.Y,sblock);

                                    break;
                            }
                        }
                    }
                    GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Project.Save();
                    GameManager.Project.FindSubsystem<SubsystemUpdate>(true).Update();
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog1"), Color.LightYellow, true, true);
                    // this.player.ComponentGui.DisplaySmallMessage(GameManager.Project.FindSubsystem<SubsystemTerrain>(true).Terrain.GetTopHeight(point2.X, point2.Y).ToString()+"X"+point2.X+"Z"+ point2.Y, Color.LightYellow, true, true);

                    *//* switch (CreatorAPI.Language)
                     {
                         case Language.zh_CN:
                             this.player.ComponentGui.DisplaySmallMessage("修改成功", Color.LightYellow, true, true); break;
                         case Language.en_US:
                             this.player.ComponentGui.DisplaySmallMessage("Modified successfully", Color.LightYellow, true, true); break;

                         default:
                             this.player.ComponentGui.DisplaySmallMessage("修改成功", Color.LightYellow, true, true); break;
                     }*//*
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editchunkdialog2") + string.Format("\nStart:{0} End:{1}", Start.Y, End.Y), Color.LightYellow, true, true);

                    *//*  switch (CreatorAPI.Language)
                      {
                          case Language.zh_CN:
                              this.player.ComponentGui.DisplaySmallMessage("修改失败，请检查是否选择了区域" + string.Format("\nStart:{0} End:{1}", Start.Y, End.Y), Color.LightYellow, true, true); break;
                          case Language.en_US:
                              this.player.ComponentGui.DisplaySmallMessage("Modification failed, please check if region is selected" + string.Format("\nStart:{0} End:{1}", Start.Y, End.Y), Color.LightYellow, true, true); break;
                          default:
                              this.player.ComponentGui.DisplaySmallMessage("修改失败，请检查是否选择了区域" + string.Format("\nStart:{0} End:{1}", Start.Y, End.Y), Color.LightYellow, true, true); break;
                      }*//*

                }
                DialogsManager.HideDialog(this);
            }
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            SliderData.Text = string.Format(CreatorMain.Display_Key_Dialog("editchunkdialog3"), (int)TemperatureSlider.Value, (int)HumiditySlider.Value);

            *//* switch (CreatorAPI.Language)
             {
                 case Language.zh_CN:
                     this.SliderData.Text = string.Format("温度 :{0} 湿度 :{1}", (object)(int)this.TemperatureSlider.Value, (object)(int)this.HumiditySlider.Value);
                     break;
                 case Language.en_US:
                     this.SliderData.Text = string.Format("Temperature :{0} Humidity :{1}", (object)(int)this.TemperatureSlider.Value, (object)(int)this.HumiditySlider.Value);
                     break;
                 default:
                     this.SliderData.Text = string.Format("温度 :{0} 湿度 :{1}", (object)(int)this.TemperatureSlider.Value, (object)(int)this.HumiditySlider.Value);
                     break;
             }*//*
        }
    }
}
*/