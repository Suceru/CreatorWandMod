using CreatorWand2;
using Engine;
using Game;
using GameEntitySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TemplatesDatabase;

namespace CreatorModAPI
{
    public class CreatorWidget : CanvasWidget
    {
        /*public virtual decimal CalculatePay()
        {
            return _basepay;
        }*/
        private readonly ComponentPlayer player;

        private readonly CreatorAPI creatorAPI;

        private readonly WorldSettings worldSettings;

        private readonly SubsystemTerrain subsystemTerrain;

        private readonly ButtonWidget SphereButton;

        private readonly ButtonWidget PrismButton;

        private readonly ButtonWidget PyramidButton;

        private readonly ButtonWidget CylindricalButton;

        private readonly ButtonWidget PillarsButton;

        private readonly ButtonWidget PrismColumnButton;

        private readonly ButtonWidget RectangularButton;

        private readonly ButtonWidget CircleButton;

        private readonly ButtonWidget MazeButton;

        private readonly ButtonWidget SpiralButton;

        private readonly ButtonWidget LevelSetButton;

        private readonly ButtonWidget TransferButton;

        private readonly ButtonWidget SetPositionButton;

        private readonly ButtonWidget RevokeButton;

        private readonly ButtonWidget SetSpawn;

        private readonly ButtonWidget SetLinkButton;

        private readonly ButtonWidget RemoveItemButton;

        private readonly ButtonWidget RemoveAnimalButton;

        private readonly ButtonWidget MountainButton;

        private readonly ButtonWidget SetModeButton;

        private readonly ButtonWidget SetButton;

        private readonly ButtonWidget ClearCacheButton;

        private readonly ButtonWidget CopyPasteButton;

        private readonly ButtonWidget OnekeyButton;

        private readonly ButtonWidget ReplaceButton;

        private readonly ButtonWidget ModButton;

        private readonly ButtonWidget EditRegionButton;

        private readonly ButtonWidget EditWorldButton;

        private readonly ButtonWidget PenetrateButton;

        private readonly ButtonWidget TerrainTestButton;

        private readonly ButtonWidget FillingButton;

        private readonly ButtonWidget PavageButton;

        private readonly ButtonWidget ClearBlockButton;

        private readonly ButtonWidget SetPositionCarefulButton;

        private readonly ButtonWidget AdjustPositionButton;

        private readonly ButtonWidget SetDifficultyButton;

        private readonly ButtonWidget ThreePointPlaneButton;

        private readonly ButtonWidget FourPointSpaceButton;

        private readonly ButtonWidget LightWorldButton;

        private readonly ButtonWidget WeatherButton;

        private readonly ButtonWidget LangugeButton;

        private int ViewButton;

        private bool IsRain;

        public CreatorWidget(CreatorAPI creatorAPI)
        {
            player = creatorAPI.componentMiner.ComponentPlayer;
            this.creatorAPI = creatorAPI;
            XElement node = (!creatorAPI.oldMainWidget) ? ContentManager.Get<XElement>("NewCreatorAPIWidget") : ContentManager.Get<XElement>("CreatorAPIWidget");
            LoadChildren(this, node);
            worldSettings = player.Project.FindSubsystem<SubsystemGameInfo>(throwOnError: true).WorldSettings;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
            SphereButton = Children.Find<ButtonWidget>("Sphere");
            SphereButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Sphere");
            PrismButton = Children.Find<ButtonWidget>("FCC Octahedron");
            PrismButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "FCC Octahedron");
            PyramidButton = Children.Find<ButtonWidget>("BCC Octahedron");
            PyramidButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "BCC Octahedron");
            CylindricalButton = Children.Find<ButtonWidget>("Cylinder");
            CylindricalButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Cylinder");
            PrismColumnButton = Children.Find<ButtonWidget>("FCC Square Column");
            PrismColumnButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "FCC Square Column");
            PillarsButton = Children.Find<ButtonWidget>("BCC Square Column");
            PillarsButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "BCC Square Column");
            RectangularButton = Children.Find<ButtonWidget>("Cuboid");
            RectangularButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Cuboid");
            CircleButton = Children.Find<ButtonWidget>("Torus");
            CircleButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Torus");
            MazeButton = Children.Find<ButtonWidget>("Maze");
            MazeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Maze");
            FillingButton = Children.Find<ButtonWidget>("Fill");
            FillingButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Fill");
            PavageButton = Children.Find<ButtonWidget>("Tile");
            PavageButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Tile");
            MountainButton = Children.Find<ButtonWidget>("Mountains");
            MountainButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Mountains");
            TransferButton = Children.Find<ButtonWidget>("Teleport");
            TransferButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Teleport");
            LevelSetButton = Children.Find<ButtonWidget>("Level");
            LevelSetButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Level");
            SetButton = Children.Find<ButtonWidget>("Mod Settings");
            SetButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Mod Settings");
            SpiralButton = Children.Find<ButtonWidget>("Spiral");
            SpiralButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Spiral");
            PenetrateButton = Children.Find<ButtonWidget>("Disable Collision");
            PenetrateButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Disable Collision");
            SetLinkButton = Children.Find<ButtonWidget>("Connect 2 Points");
            SetLinkButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 2 Points");
            SetPositionButton = Children.Find<ButtonWidget>("Set the Point");
            SetPositionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Set the Point");
            RevokeButton = Children.Find<ButtonWidget>("Undo");
            RevokeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Undo");
            SetSpawn = Children.Find<ButtonWidget>("Set the Spawn Point");
            SetSpawn.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Set the Spawn Point");
            EditWorldButton = Children.Find<ButtonWidget>("World Settings");
            EditWorldButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "World Settings");
            EditRegionButton = Children.Find<ButtonWidget>("Edit Chunk");
            EditRegionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Edit Chunk");
            ModButton = Children.Find<ButtonWidget>("Custom Module");
            ModButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Custom Module");
            ReplaceButton = Children.Find<ButtonWidget>("Replace");
            ReplaceButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Replace");
            OnekeyButton = Children.Find<ButtonWidget>("One Click Generation");
            OnekeyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "One Click Generation");
            CopyPasteButton = Children.Find<ButtonWidget>("Copy and Paste");
            CopyPasteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Copy and Paste");
            ClearCacheButton = Children.Find<ButtonWidget>("Clear Cache");
            ClearCacheButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Clear Cache");
            SetModeButton = Children.Find<ButtonWidget>("Game Mode");
            SetModeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Game Mode");
            RemoveItemButton = Children.Find<ButtonWidget>("Remove Drops");
            RemoveItemButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Remove Drops");
            RemoveAnimalButton = Children.Find<ButtonWidget>("Despawn Entities");
            RemoveAnimalButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Despawn Entities");
            ClearBlockButton = Children.Find<ButtonWidget>("Clear Blocks");
            ClearBlockButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Clear Blocks");
            TerrainTestButton = Children.Find<ButtonWidget>("Terrain");
            TerrainTestButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Terrain");
            SetPositionCarefulButton = Children.Find<ButtonWidget>("Custom Point");
            SetPositionCarefulButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Custom Point");
            SetPositionCarefulButton.IsEnabled = false;
            AdjustPositionButton = Children.Find<ButtonWidget>("Point adjustment");
            AdjustPositionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Point adjustment");
            AdjustPositionButton.IsEnabled = false;
            SetDifficultyButton = Children.Find<ButtonWidget>("Init Difficulty");
            SetDifficultyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Init Difficulty");
            ThreePointPlaneButton = Children.Find<ButtonWidget>("Connect 3 Points");
            ThreePointPlaneButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 3 Points");
            FourPointSpaceButton = Children.Find<ButtonWidget>("Connect 4 Points");
            FourPointSpaceButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 4 Points");
            LightWorldButton = Children.Find<ButtonWidget>("Light Up Blocks");
            LightWorldButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Light Up Blocks");
            Children.Find<BevelledButtonWidget>("Coming Soon").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Coming Soon");
            Children.Find<BevelledButtonWidget>("Coming Soon").Color = Color.White;
            LangugeButton = Children.Find<ButtonWidget>("Language");
            LangugeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Language");
            WeatherButton = Children.Find<ButtonWidget>("Weather");
            WeatherButton.Text = "Weather";
            LangugeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Language");
            TerrainTestButton.IsEnabled = false;
            IsRain = (SubsystemWeather1.subsystem.m_precipitationStartTime < GameManager.Project.FindSubsystem<SubsystemGameInfo>(throwOnError: true).TotalElapsedGameTime);
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (Children.Find<BevelledButtonWidget>("Coming Soon").IsClicked)
            {
                ViewButton++;
                /*void p(Terrain terrain, Point3 point3)
                {
                    Log.Information("C(" + point3 + ")");
                }
                Cw2CopyAndPaste.CopyBlock += (Action<Terrain, Point3>)p;
                Cw2CopyAndPaste.LoopCopy(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation, CreatorMain.Position[0], CreatorMain.Position[1]);
                Cw2CopyAndPaste.CopyBlock -= (Action<Terrain, Point3>)p;

                void q(SubsystemTerrain subsystemTerrain, Point3 point3, Matrix matrix) {
                    Log.Information("P(" + point3 + ")");
                }
                Cw2CopyAndPaste.PasteBlock += q;
                Cw2CopyAndPaste.LoopPaste(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation, CreatorMain.Position[0], Cw2CopyAndPaste.GetPoint3Num(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation, CreatorMain.Position[0], CreatorMain.Position[1]), new Matrix());
                Cw2CopyAndPaste.PasteBlock -= q;*/
                /*var a = CreatorMain.Position[0];
                CW2EntityManager.RemoveEntity(a);*/


            }

            try
            {
                if (WeatherButton.IsClicked)
                {
                    IsRain = !IsRain;
                    SubsystemWeather1.SetPrecipitationTime(IsRain);
                    /*Log.Information(CellFace.FaceToVector3(1));
                    Log.Information(CellFace.FaceToVector3(3));
                    Log.Information(CW2Matrix.CW2FromTwoVectors(CellFace.FaceToVector3(1), CellFace.FaceToVector3(3)));
                    Log.Information(Vector3.TransformNormal(CellFace.FaceToVector3(1), CW2Matrix.CW2FromTwoVectors(CellFace.FaceToVector3(1), CellFace.FaceToVector3(3))));
                    Log.Information(SetFaceAndRotate.Vector3Normalize(Vector3.TransformNormal(CellFace.FaceToVector3(1), CW2Matrix.CW2FromTwoVectors(CellFace.FaceToVector3(1), CellFace.FaceToVector3(3)))));
                    Log.Information(CellFace.Vector3ToFace(SetFaceAndRotate.Vector3Normalize(Vector3.TransformNormal(CellFace.FaceToVector3(1), CW2Matrix.CW2FromTwoVectors(CellFace.FaceToVector3(1), CellFace.FaceToVector3(3))))));*/
                    /* int ps=0, pe=4;
                     Log.Information(CellFace.Vector3ToFace(Vector3.TransformNormal(CellFace.FaceToVector3(ps), CW2Matrix.CW2FromTwoVectors(CellFace.FaceToVector3(ps), CellFace.FaceToVector3(pe)))));
                     Point2 point1 = new Point2(4,1);
                     Point2 point2 = new Point2(5, 0);
                     Point2 point3 = new Point2(5, 1);
                     CW2Matrix.CW2FromTwoVectors(CW2Matrix.FaceToVector3(point1), CW2Matrix.FaceToVector3(point2));
                     Log.Information(point1+"->"+point2);
                     Log.Information(point1 + "->" + point3);
                     Log.Information("[][]");
                     for (int i = 0; i < 23; i++)
                     {
                         Log.Information("[][]"+i); 
                         Log.Information(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4)) + "->" + CW2Matrix.FaceToVector3(CW2Matrix.Vector3ToFace(Vector3.TransformNormal(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4)), CW2Matrix.GetFaceFunction(CW2Matrix.FaceToVector3(point1), CW2Matrix.FaceToVector3(point2))))));
                         Log.Information(CW2Matrix.Vector3ToFace(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4))) + "->" + CW2Matrix.Vector3ToFace(Vector3.TransformNormal(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4)), CW2Matrix.GetFaceFunction(CW2Matrix.FaceToVector3(point1), CW2Matrix.FaceToVector3(point2)))));
                         Log.Information(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4))+"->"+ CW2Matrix.FaceToVector3(CW2Matrix.Vector3ToFace(Vector3.TransformNormal(CW2Matrix.FaceToVector3(new Point2(i/4, i % 4)), CW2Matrix.GetFaceFunction(CW2Matrix.FaceToVector3(point1), CW2Matrix.FaceToVector3(point3))))));
                         Log.Information(CW2Matrix.Vector3ToFace(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4))) + "->" + CW2Matrix.Vector3ToFace(Vector3.TransformNormal(CW2Matrix.FaceToVector3(new Point2(i / 4, i % 4)), CW2Matrix.GetFaceFunction(CW2Matrix.FaceToVector3(point1), CW2Matrix.FaceToVector3(point3)))));
                     }*/


                }
                else
                {
                    bool flag = SubsystemWeather1.subsystem.m_precipitationStartTime < GameManager.Project.FindSubsystem<SubsystemGameInfo>(throwOnError: true).TotalElapsedGameTime;
                    Vector3 position = player.ComponentBody.Position;
                    string str = (SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain) ? CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Rain:") : CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Snow:");
                    string str2 = (SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain) ? CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "No Rain:") : CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "No Snow:");
                    WeatherButton.Text = (flag ? (str + (SubsystemWeather1.subsystem.m_precipitationEndTime - GameManager.Project.FindSubsystem<SubsystemGameInfo>(throwOnError: true).TotalElapsedGameTime).ToString("0.0")) : (str2 + (SubsystemWeather1.subsystem.m_precipitationStartTime - GameManager.Project.FindSubsystem<SubsystemGameInfo>(throwOnError: true).TotalElapsedGameTime).ToString("0.0")));
                    WeatherButton.Color = ((!flag) ? Color.White : ((SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain) ? Color.Blue : Color.White));
                }
            }
            catch (Exception ex)
            {
                Log.Warning("Err:" + ex);
            }

            switch (ViewButton)
            {
                case 0:
                    string vec;
                    switch (CW2Matrix.Vector3ToFace(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation.GetForwardVector()).X)
                    {
                        case 0:
                            vec = "North";
                            break;
                        case 1:
                            vec = "West";
                            break;
                        case 2:
                            vec = "South";
                            break;
                        case 3:
                            vec = "East";
                            break;
                        case 4:
                            vec = "Up";
                            break;
                        default:
                            vec = "Down";
                            break;
                    }
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "View: " + vec;// +"->" + CW2Matrix.Vector3ToFace(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation.GetForwardVector());
                    break;
                case 1:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = CreatorMain.version;
                    break;
                case 2:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "Coming Soon";
                    break;
                default:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "Coming Soon";
                    ViewButton = 0;
                    break;
            }

            if (SphereButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SphereDialog(creatorAPI));
            }

            if (PrismButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PrismDialog(creatorAPI));
            }

            if (PyramidButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PyramidDialog(creatorAPI));
            }

            if (CylindricalButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CylindricalDialog(creatorAPI));
            }

            if (PrismColumnButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PrismColumnDialog(creatorAPI));
            }

            if (PillarsButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PillarsDialog(creatorAPI));
            }

            if (RectangularButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new RectangularDialog(creatorAPI));
            }

            if (CircleButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CircleDialog(creatorAPI));
            }

            if (MazeButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MazeDialog(creatorAPI));
            }

            if (PavageButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PavageDialog(creatorAPI));
            }

            if (FillingButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewFillingDialog(creatorAPI));
            }

            if (MountainButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MountainDialog(creatorAPI));
            }

            if (TransferButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new TransferDialog(creatorAPI));
            }

            if (LevelSetButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new LevelSetDialog(creatorAPI));
            }

            if (SetButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SetDialog(creatorAPI));
            }

            if (SpiralButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SpiralDialog(creatorAPI));
            }

            if (PenetrateButton.IsClicked)
            {
                if (CreatorMain.Penetrate)
                {
                    foreach (int item in CreatorMain.PenetrateBlocksID)
                    {
                        BlocksManager.Blocks[item].IsCollidable = true;
                    }

                    CreatorMain.PenetrateBlocksID.Clear();
                }
                else
                {
                    worldSettings.EnvironmentBehaviorMode = EnvironmentBehaviorMode.Static;
                    Block[] blocks = BlocksManager.Blocks;
                    foreach (Block block in blocks)
                    {
                        if (block.IsCollidable)
                        {
                            block.IsCollidable = false;
                            CreatorMain.PenetrateBlocksID.Add(block.BlockIndex);
                        }
                    }
                }

                CreatorMain.Penetrate = !CreatorMain.Penetrate;
            }

            if (LightWorldButton.IsClicked)
            {
                if (CreatorMain.LightWorld)
                {
                    foreach (int item2 in CreatorMain.LightWorldBlockID)
                    {
                        BlocksManager.Blocks[item2].DefaultEmittedLightAmount = 0;
                    }

                    CreatorMain.LightWorldBlockID.Clear();
                }
                else
                {
                    Block[] blocks = BlocksManager.Blocks;
                    foreach (Block block2 in blocks)
                    {
                        if (block2.DefaultEmittedLightAmount == 0)
                        {
                            block2.DefaultEmittedLightAmount = 15;
                            CreatorMain.LightWorldBlockID.Add(block2.BlockIndex);
                        }
                    }
                }

                CreatorMain.LightWorld = !CreatorMain.LightWorld;
            }

            LightWorldButton.Color = ((!CreatorMain.LightWorld) ? Color.White : Color.Yellow);
            PenetrateButton.Color = ((!CreatorMain.Penetrate) ? Color.White : Color.Yellow);
            if (CreatorMain.Position != null)
            {
                if (CreatorMain.Position[0].Y > 0 && CreatorMain.Position[1].Y > 0)
                {
                    EditRegionButton.IsEnabled = true;
                }
                else
                {
                    EditRegionButton.IsEnabled = false;
                }
            }
            else
            {
                EditRegionButton.IsEnabled = false;
            }
            if (EditRegionButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewEditChunkDialog(creatorAPI));
            }

            if (EditWorldButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new EditWorldDialog(creatorAPI));
            }

            if (ClearBlockButton.IsClicked)
            {
                player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetclear"), Color.LightRed, blinking: true, playNotificationSound: false);
                creatorAPI.ClearBlock = !creatorAPI.ClearBlock;
            }

            ClearBlockButton.Color = ((!creatorAPI.ClearBlock) ? Color.White : Color.Yellow);
            if (SetLinkButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new TwoPointLineDialog(creatorAPI));
            }

            if (SetPositionButton.IsClicked)
            {
                Vector3 position2 = player.ComponentBody.Position;
                Point3 point3 = new Point3((int)position2.X, (int)position2.Y, (int)position2.Z);
                DialogsManager.ShowDialog(null, new ListSelectionDialog(CreatorMain.Display_Key_Dialog("creatorWidgetsetpt"), new int[4]
                {
                    1,
                    2,
                    3,
                    4
                }, 56f, (object e) => string.Format("{0}{1}", CreatorMain.Display_Key_Dialog("creatorWidgetsetp1"), (int)e), delegate (object e)
                {
                    creatorAPI.Position[(int)e - 1] = point3;
                    player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetsetpd"), (int)e, point3.X, point3.Y, point3.Z), Color.Yellow, blinking: true, playNotificationSound: true);
                }));
            }

            if (creatorAPI.RevokeSwitch)
            {
                if (creatorAPI.revokeData == null)
                {
                    RevokeButton.IsEnabled = false;
                }
                else
                {
                    RevokeButton.IsEnabled = true;
                }
            }
            else
            {
                RevokeButton.IsEnabled = false;
            }

            if (RevokeButton.IsClicked)
            {
                RevokeButton.IsEnabled = false;
                creatorAPI.revokeData = null;
                subsystemTerrain.Dispose();
                ((Subsystem)subsystemTerrain).Load(new ValuesDictionary());
                player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetundo"), Color.Yellow, blinking: true, playNotificationSound: true);
            }

            if (SetSpawn.IsClicked)
            {
                Vector3 position3 = player.ComponentBody.Position;
                player.PlayerData.SpawnPosition = position3 + new Vector3(0f, 0.1f, 0f);
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetrp"), (int)position3.X, (int)position3.Y, (int)position3.Z), Color.Yellow, blinking: true, playNotificationSound: true);
            }

            if (ModButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog(creatorAPI));
            }

            if (ReplaceButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewReplaceDialog(creatorAPI));
            }

            if (SetModeButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SetModeDialog(creatorAPI));
            }

            if (OnekeyButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new OnekeyGenerationDialog(creatorAPI));
            }

            if (CopyPasteButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CopyPasteDialog(creatorAPI));
            }

            ClearCacheButton.IsEnabled = Directory.Exists(CreatorMain.CacheDirectory);
            if (ClearCacheButton.IsClicked)
            {
                if (CreatorMain.CacheDirectory.Delete())
                {
                    for (int j = 0; j < 4; j++)
                    {
                        creatorAPI.Position[j] = new Point3(0, -1, 0);
                    }

                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetcls"), Color.Yellow, blinking: true, playNotificationSound: true);
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetclf"), Color.Red, blinking: true, playNotificationSound: true);
                }
            }

            if (RemoveAnimalButton.IsClicked)
            {
                int num = 0;
                foreach (ComponentCreature creature in player.Project.FindSubsystem<SubsystemCreatureSpawn>(throwOnError: true).Creatures)
                {
                    if (!(creature is ComponentPlayer))
                    {
                        creature.ComponentSpawn.Despawn();
                        num++;
                    }
                }

                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetcla"), num), Color.Yellow, blinking: true, playNotificationSound: false);
            }

            if (RemoveItemButton.IsClicked)
            {
                int num2 = 0;
                foreach (Pickable pickable in GameManager.Project.FindSubsystem<SubsystemPickables>(throwOnError: true).Pickables)
                {
                    pickable.Count = 0;
                    pickable.ToRemove = true;
                    num2++;
                }

                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetcld"), num2), Color.Yellow, blinking: true, playNotificationSound: false);
            }

            _ = TerrainTestButton.IsClicked;
            if (SetDifficultyButton.IsClicked)
            {
                Vector3 position4 = player.ComponentBody.Position;
                new Point3((int)position4.X, (int)position4.Y, (int)position4.Z);
                int[] items = new int[3]
                {
                    0,
                    1,
                    2
                };
                string[] difference = new string[3]
                {
                    CreatorMain.Display_Key_Dialog("creatorWidgetde"),
                    CreatorMain.Display_Key_Dialog("creatorWidgetdm"),
                    CreatorMain.Display_Key_Dialog("creatorWidgetdh")
                };
                DialogsManager.ShowDialog(null, new ListSelectionDialog(CreatorMain.Display_Key_Dialog("creatorWidgetseld"), items, 56f, (object e) => difference[(int)e], delegate (object e)
                {
                    worldSettings.StartingPositionMode = (StartingPositionMode)e;
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetdd") + difference[(int)e], Color.Yellow, blinking: true, playNotificationSound: true);
                }));
            }

            _ = SetPositionCarefulButton.IsClicked;
            _ = AdjustPositionButton.IsClicked;
            if (ThreePointPlaneButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ThreePointToPlaneDialog(creatorAPI));
            }

            if (FourPointSpaceButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new FourPointSpaceDialog(creatorAPI));
            }

            if (LangugeButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MessageDialog("Choose a Language", "Please confirm if you want to change the language", "OK", "Cancel", delegate (MessageDialogButton result)
                {
                    if (result == MessageDialogButton.Button1)
                    {
                        List<Tuple<string, Action>> list = new List<Tuple<string, Action>>();
                        for (int num3 = Enum.GetNames(typeof(Language)).GetLength(0) - 1; num3 >= 0; num3--)
                        {
                            int a = num3;
                            try
                            {
                                list.Add(new Tuple<string, Action>(Enum.GetName(typeof(Language), num3).ToString(), delegate
                                {
                                    CreatorMain.Language = CreatorAPI.Language = (Language)Enum.ToObject(typeof(Language), a);
                                    CreatorAPI.IsAddedToProject = false;
                                    Log.Warning((Language)Enum.ToObject(typeof(Language), a) + "  " + a + " is " + CreatorAPI.Language.ToString());
                                    /*
                                    XElement xElement = ContentManager.Get<XElement>("CreatorDisplay");
                                    CreatorAPI.CreatorDisplayDataDialog = from xe in xElement.Element("CreatorDisplayDialog").Elements("CreatorDisplayData")  where xe.Attribute("Language").Value == CreatorAPI.Language.ToString() select xe;
                                    CreatorAPI.CreatorDisplayDataUI = xElement.Element("CreatorDisplayUI").Elements();
                                    ContentManager.Dispose("CreatorDisplay");*/
                                }));
                            }
                            catch (Exception ex2)
                            {
                                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MessageDialog("Exception", "ERR:" + ex2, "OK", null, null));
                            }
                        }

                        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ListSelectionDialog("Choose a Language", list, 60f, (object t) => ((Tuple<string, Action>)t).Item1, delegate (object t)
                        {
                            ((Tuple<string, Action>)t).Item2();
                        }));
                    }
                }));
            }

            if (!CreatorAPI.IsAddedToProject)
            {
                base.ParentWidget.Children.Clear();
                DialogsManager.HideAllDialogs();
            }
        }

        public static void Dismiss(bool result)
        {
            CreatorAPI.IsAddedToProject = false;
        }
    }
}

/*创世神界面*/
/*namespace CreatorModAPI-=  public class CreatorWidget : CanvasWidget*//*
using Engine;
using Game;
using System;
using System.Collections.Generic;
using System.IO;
//using System.IO;
using System.Xml.Linq;
using TemplatesDatabase;

namespace CreatorModAPI
{
    public class CreatorWidget : CanvasWidget
    {
        private readonly ComponentPlayer player;
        private readonly CreatorAPI creatorAPI;
        private readonly WorldSettings worldSettings;
        private readonly SubsystemTerrain subsystemTerrain;
        private readonly ButtonWidget SphereButton;
        private readonly ButtonWidget PrismButton;
        private readonly ButtonWidget PyramidButton;
        private readonly ButtonWidget CylindricalButton;
        private readonly ButtonWidget PillarsButton;
        private readonly ButtonWidget PrismColumnButton;
        private readonly ButtonWidget RectangularButton;
        private readonly ButtonWidget CircleButton;
        private readonly ButtonWidget MazeButton;
        private readonly ButtonWidget SpiralButton;
        private readonly ButtonWidget LevelSetButton;
        private readonly ButtonWidget TransferButton;
        private readonly ButtonWidget SetPositionButton;
        private readonly ButtonWidget RevokeButton;
        private readonly ButtonWidget SetSpawn;
        private readonly ButtonWidget SetLinkButton;
        private readonly ButtonWidget RemoveItemButton;
        private readonly ButtonWidget RemoveAnimalButton;
        private readonly ButtonWidget MountainButton;
        private readonly ButtonWidget SetModeButton;
        private readonly ButtonWidget SetButton;
        private readonly ButtonWidget ClearCacheButton;
        private readonly ButtonWidget CopyPasteButton;
        private readonly ButtonWidget OnekeyButton;
        private readonly ButtonWidget ReplaceButton;
        private readonly ButtonWidget ModButton;
        private readonly ButtonWidget EditRegionButton;
        private readonly ButtonWidget EditWorldButton;
        private readonly ButtonWidget PenetrateButton;
        private readonly ButtonWidget TerrainTestButton;
        private readonly ButtonWidget FillingButton;
        private readonly ButtonWidget PavageButton;
        private readonly ButtonWidget ClearBlockButton;
        private readonly ButtonWidget SetPositionCarefulButton;
        private readonly ButtonWidget AdjustPositionButton;
        private readonly ButtonWidget SetDifficultyButton;
        private readonly ButtonWidget ThreePointPlaneButton;
        private readonly ButtonWidget FourPointSpaceButton;
        private readonly ButtonWidget LightWorldButton;
        private readonly ButtonWidget WeatherButton;
        private readonly ButtonWidget LangugeButton;
        private int ViewButton = 0;
        private bool IsRain = false;
        //private SubsystemWeather1 subsystemWeather1= new SubsystemWeather1();

        public CreatorWidget(CreatorAPI creatorAPI)
        {
            player = creatorAPI.componentMiner.ComponentPlayer;
            this.creatorAPI = creatorAPI;
            //System.IO.Path.
            // XElement node = !creatorAPI.oldMainWidget ? ContentManager.Get<XElement>(@"zh_CN\NewCreatorAPIWidget") : ContentManager.Get<XElement>(@"zh_CN\CreatorAPIWidget");
            XElement node = !creatorAPI.oldMainWidget ? ContentManager.Get<XElement>("NewCreatorAPIWidget") : ContentManager.Get<XElement>("CreatorAPIWidget");

            LoadChildren(this, node);
            worldSettings = player.Project.FindSubsystem<SubsystemGameInfo>(true).WorldSettings;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            SphereButton = Children.Find<ButtonWidget>("Sphere");
            SphereButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Sphere");
            PrismButton = Children.Find<ButtonWidget>("FCC Octahedron");
            PrismButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "FCC Octahedron");
            PyramidButton = Children.Find<ButtonWidget>("BCC Octahedron");
            PyramidButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "BCC Octahedron");
            CylindricalButton = Children.Find<ButtonWidget>("Cylinder");
            CylindricalButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Cylinder");
            PrismColumnButton = Children.Find<ButtonWidget>("FCC Square Column");
            PrismColumnButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "FCC Square Column");
            PillarsButton = Children.Find<ButtonWidget>("BCC Square Column");
            PillarsButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "BCC Square Column");
            RectangularButton = Children.Find<ButtonWidget>("Cuboid");
            RectangularButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Cuboid");
            CircleButton = Children.Find<ButtonWidget>("Torus");
            CircleButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Torus");
            MazeButton = Children.Find<ButtonWidget>("Maze");
            MazeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Maze");
            FillingButton = Children.Find<ButtonWidget>("Fill");
            FillingButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Fill");
            //  this.FillingButton = this.Children.Find<ButtonWidget>("填充");
            PavageButton = Children.Find<ButtonWidget>("Tile");
            PavageButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Tile");
            MountainButton = Children.Find<ButtonWidget>("Mountains");
            MountainButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Mountains");
            TransferButton = Children.Find<ButtonWidget>("Teleport");
            TransferButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Teleport");
            LevelSetButton = Children.Find<ButtonWidget>("Level");
            LevelSetButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Level");
            SetButton = Children.Find<ButtonWidget>("Mod Settings");
            SetButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Mod Settings");
            SpiralButton = Children.Find<ButtonWidget>("Spiral");
            SpiralButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Spiral");
            PenetrateButton = Children.Find<ButtonWidget>("Disable Collision");
            PenetrateButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Disable Collision");
            SetLinkButton = Children.Find<ButtonWidget>("Connect 2 Points");
            SetLinkButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 2 Points");
            SetPositionButton = Children.Find<ButtonWidget>("Set the Point");
            SetPositionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Set the Point");
            RevokeButton = Children.Find<ButtonWidget>("Undo");
            RevokeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Undo");
            SetSpawn = Children.Find<ButtonWidget>("Set the Spawn Point");
            SetSpawn.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Set the Spawn Point");
            EditWorldButton = Children.Find<ButtonWidget>("World Settings");
            EditWorldButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "World Settings");
            EditRegionButton = Children.Find<ButtonWidget>("Edit Chunk");
            EditRegionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Edit Chunk");
            ModButton = Children.Find<ButtonWidget>("Custom Module");
            ModButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Custom Module");
            ReplaceButton = Children.Find<ButtonWidget>("Replace");
            ReplaceButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Replace");
            OnekeyButton = Children.Find<ButtonWidget>("One Click Generation");
            OnekeyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "One Click Generation");
            CopyPasteButton = Children.Find<ButtonWidget>("Copy and Paste");
            CopyPasteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Copy and Paste");
            ClearCacheButton = Children.Find<ButtonWidget>("Clear Cache");
            ClearCacheButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Clear Cache");
            SetModeButton = Children.Find<ButtonWidget>("Game Mode");
            SetModeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Game Mode");
            RemoveItemButton = Children.Find<ButtonWidget>("Remove Drops");
            RemoveItemButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Remove Drops");
            RemoveAnimalButton = Children.Find<ButtonWidget>("Despawn Entities");
            RemoveAnimalButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Despawn Entities");
            ClearBlockButton = Children.Find<ButtonWidget>("Clear Blocks");
            ClearBlockButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Clear Blocks");
            TerrainTestButton = Children.Find<ButtonWidget>("Terrain");
            TerrainTestButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Terrain");
            SetPositionCarefulButton = Children.Find<ButtonWidget>("Custom Point");
            SetPositionCarefulButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Custom Point");
            SetPositionCarefulButton.IsEnabled = false;
            AdjustPositionButton = Children.Find<ButtonWidget>("Point adjustment");
            AdjustPositionButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Point adjustment");
            AdjustPositionButton.IsEnabled = false;
            SetDifficultyButton = Children.Find<ButtonWidget>("Init Difficulty");
            SetDifficultyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Init Difficulty");
            ThreePointPlaneButton = Children.Find<ButtonWidget>("Connect 3 Points");
            ThreePointPlaneButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 3 Points");
            FourPointSpaceButton = Children.Find<ButtonWidget>("Connect 4 Points");
            FourPointSpaceButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Connect 4 Points");
            LightWorldButton = Children.Find<ButtonWidget>("Light Up Blocks");
            LightWorldButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Light Up Blocks");
            Children.Find<BevelledButtonWidget>("Coming Soon").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Coming Soon");
            LangugeButton = Children.Find<ButtonWidget>("Language");
            LangugeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Language");
            WeatherButton = Children.Find<ButtonWidget>("Weather");
            WeatherButton.Text = "Weather";
            LangugeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Language");
            TerrainTestButton.IsEnabled = false;
            IsRain = SubsystemWeather1.subsystem.m_precipitationStartTime < GameManager.Project.FindSubsystem<SubsystemGameInfo>(true).TotalElapsedGameTime;
            LoadProperties(this, node);
        }

        public override void Update()
        {

            switch (Children.Find<BevelledButtonWidget>("Coming Soon").IsClicked)
            {
                case true:
                    ViewButton++;
                    break;

            }
            try
            {
                switch (WeatherButton.IsClicked)
                {

                    case true:
                        IsRain = !IsRain;
                        SubsystemWeather1.SetPrecipitationTime(IsRain);
                        break;
                    default:
                        bool rain = SubsystemWeather1.subsystem.m_precipitationStartTime < GameManager.Project.FindSubsystem<SubsystemGameInfo>(true).TotalElapsedGameTime;
                        Vector3 position = player.ComponentBody.Position;
                        string precipitationType = SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain ? CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Rain:") : CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "Snow:");
                        string NoprecipitationType = SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain ? CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "No Rain:") : CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "NewCreatorAPIWidget", "No Snow:");
                        WeatherButton.Text = rain ? precipitationType + (SubsystemWeather1.subsystem.m_precipitationEndTime - GameManager.Project.FindSubsystem<SubsystemGameInfo>(true).TotalElapsedGameTime).ToString("0.0") : NoprecipitationType + (SubsystemWeather1.subsystem.m_precipitationStartTime - GameManager.Project.FindSubsystem<SubsystemGameInfo>(true).TotalElapsedGameTime).ToString("0.0");
                        WeatherButton.Color = rain ? SubsystemWeather1.subsystem.GetPrecipitationShaftInfo((int)position.X, (int)position.Z).Type == PrecipitationType.Rain ? Color.Blue : Color.White : Color.White;
                        break;

                }

            }
            catch (Exception e)
            {
                Log.Warning("Err:" + e);
            }

            switch (ViewButton)
            {
                case 0:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "View: " + CellFace.Vector3ToFace(Matrix.CreateFromQuaternion(player.ComponentMiner.ComponentCreature.ComponentCreatureModel.EyeRotation).Forward, 3).ToString();
                    break;
                case 1:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = CreatorMain.version;
                    break;
                case 2:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "Coming Soon";
                    break;
                default:
                    Children.Find<BevelledButtonWidget>("Coming Soon").Text = "Coming Soon";
                    ViewButton = 0;
                    break;
            }
            //形状生成界面按钮
            if (SphereButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SphereDialog(creatorAPI));
            }

            if (PrismButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PrismDialog(creatorAPI));
            }

            if (PyramidButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PyramidDialog(creatorAPI));
            }

            if (CylindricalButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CylindricalDialog(creatorAPI));
            }

            if (PrismColumnButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PrismColumnDialog(creatorAPI));
            }

            if (PillarsButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PillarsDialog(creatorAPI));
            }

            if (RectangularButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new RectangularDialog(creatorAPI));
            }

            if (CircleButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CircleDialog(creatorAPI));
            }

            if (MazeButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MazeDialog(creatorAPI));
            }

            if (PavageButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new PavageDialog(creatorAPI));
            }

            if (FillingButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewFillingDialog(creatorAPI));*//*FillingDialog*//*
            }

            if (MountainButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MountainDialog(creatorAPI));
            }

            if (TransferButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new TransferDialog(creatorAPI));
            }

            if (LevelSetButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new LevelSetDialog(creatorAPI));
            }

            if (SetButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SetDialog(creatorAPI));
            }

            if (SpiralButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SpiralDialog(creatorAPI));
            }

            //穿透
            if (PenetrateButton.IsClicked)
            {
                *//* GameWidget Camere = this.player.ViewWidget.GameWidget;
                 switch (CreatorMain.Penetrate) {
                     case true:
                         CreatorAPI.IsAddedToProject = false;
                         Camere.ActiveCamera = new FlyCamera(Camere);

                         break;
                     case false:
                         CreatorAPI.IsAddedToProject = false;
                         Camere.ActiveCamera = new FPPRCamera(Camere);
                         break;
                 }*//*
                if (CreatorMain.Penetrate)
                {
                    foreach (int index in CreatorMain.PenetrateBlocksID)
                    {
                        BlocksManager.Blocks[index].IsCollidable = true;
                    }

                    CreatorMain.PenetrateBlocksID.Clear();
                }
                else
                {
                    worldSettings.EnvironmentBehaviorMode = EnvironmentBehaviorMode.Static;
                    foreach (Block block in BlocksManager.Blocks)
                    {
                        if (block.IsCollidable)
                        {
                            block.IsCollidable = false;
                            CreatorMain.PenetrateBlocksID.Add(block.BlockIndex);
                        }
                    }
                }
                CreatorMain.Penetrate = !CreatorMain.Penetrate;
            }

            //发光世界
            if (LightWorldButton.IsClicked)
            {
                if (CreatorMain.LightWorld)
                {
                    foreach (int index in CreatorMain.LightWorldBlockID)
                    {
                        BlocksManager.Blocks[index].DefaultEmittedLightAmount = 0;
                    }

                    CreatorMain.LightWorldBlockID.Clear();
                }
                else
                {
                    foreach (Block block in BlocksManager.Blocks)
                    {
                        if (block.DefaultEmittedLightAmount == 0)
                        {
                            block.DefaultEmittedLightAmount = 15;
                            CreatorMain.LightWorldBlockID.Add(block.BlockIndex);
                        }
                    }
                }
                CreatorMain.LightWorld = !CreatorMain.LightWorld;
            }

            //发光按钮和穿透按钮的颜色
            LightWorldButton.Color = !CreatorMain.LightWorld ? Color.White : Color.Yellow;
            PenetrateButton.Color = !CreatorMain.Penetrate ? Color.White : Color.Yellow;

            //编辑区域和编辑世界
            if (EditRegionButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewEditChunkDialog(creatorAPI));
            }

            if (EditWorldButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new EditWorldDialog(creatorAPI));
            }

            //清理方块
            if (ClearBlockButton.IsClicked)
            {
                //CreatorMain.Display_Key_Dialog("creatorWidgetclear")
                player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetclear"), Color.LightRed, true, false);
                *//*
                                switch (CreatorAPI.Language)
                                {
                                    case Language.zh_CN:
                                        this.player.ComponentGui.DisplaySmallMessage("可在设置中关闭生成在来停止正在清理的进程，在超距模式下谨慎使用清理方块", Color.LightRed, true, false);
                                        break;
                                    case Language.en_US:
                                        this.player.ComponentGui.DisplaySmallMessage("The generation function can be switched off in the settings to stop the process being cleaned up and the clean-up block function can be used sparingly in overdistance mode", Color.LightRed, true, false);
                                        break;
                                    default:
                                        this.player.ComponentGui.DisplaySmallMessage("可在设置中关闭生成在来停止正在清理的进程，在超距模式下谨慎使用清理方块", Color.LightRed, true, false);
                                        break;
                                }
                *//*
                creatorAPI.ClearBlock = !creatorAPI.ClearBlock;
            }

            //修改清理方块的颜色
            ClearBlockButton.Color = !creatorAPI.ClearBlock ? Color.White : Color.Yellow;

            //两点连线按钮
            if (SetLinkButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new TwoPointLineDialog(creatorAPI));
            }

            //手动设置点按钮
            if (SetPositionButton.IsClicked)
            {
                Vector3 position = player.ComponentBody.Position;
                Point3 point3 = new Point3((int)position.X, (int)position.Y, (int)position.Z);
                *//*  string[] SelPoint = new string[3]
         {
            "选择设置的点",
            "Which point to set to player's coordinates?",
            "选择设置的点"
         };*/
/*  string[] SelPoint1 = new string[3]
{
"设置点",
"Set ",
"设置点"
};*//*

        DialogsManager.ShowDialog(null, new ListSelectionDialog(CreatorMain.Display_Key_Dialog("creatorWidgetsetpt"), new int[4]
{
  1,
  2,
  3,
  4
}, 56f, e => string.Format("{0}{1}", CreatorMain.Display_Key_Dialog("creatorWidgetsetp1"), (int)e), e =>
{
 creatorAPI.Position[(int)e - 1] = point3;
 player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetsetpd"), (int)e, point3.X, point3.Y, point3.Z), Color.Yellow, true, true);


}));
    }

    //撤回按钮设置初始状态
    if (creatorAPI.RevokeSwitch)
    {
        if (creatorAPI.revokeData == null)
        {
            RevokeButton.IsEnabled = false;
        }
        else
        {
            RevokeButton.IsEnabled = true;
        }
    }
    else
    {
        RevokeButton.IsEnabled = false;
    }

    //撤回按钮点击
    if (RevokeButton.IsClicked)
    {
        // this.creatorAPI.revokeData.Render();
        RevokeButton.IsEnabled = false;
        creatorAPI.revokeData = null;
        subsystemTerrain.Dispose();
        subsystemTerrain.Load(new ValuesDictionary());
        player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetundo"), Color.Yellow, true, true);
        *//* switch (CreatorAPI.Language)
         {
             case Language.zh_CN:
                 this.player.ComponentGui.DisplaySmallMessage("撤回成功", Color.Yellow, true, true); break;
             case Language.en_US:
                 this.player.ComponentGui.DisplaySmallMessage("Successful withdrawal", Color.Yellow, true, true); break;
             default:
                 this.player.ComponentGui.DisplaySmallMessage("撤回成功", Color.Yellow, true, true); break;
         }*//*

    }

    //重生点按钮点击
    if (SetSpawn.IsClicked)
    {
        Vector3 position = player.ComponentBody.Position;
        player.PlayerData.SpawnPosition = position + new Vector3(0.0f, 0.1f, 0.0f);
        player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetrp"), (int)position.X, (int)position.Y, (int)position.Z), Color.Yellow, true, true);
        *//*
                        switch (CreatorAPI.Language)
                        {
                            case Language.zh_CN:
                                this.player.ComponentGui.DisplaySmallMessage(string.Format("玩家重生点位置设置\n X: {0} Y : {1} Z : {2}", (object)(int)position.X, (object)(int)position.Y, (object)(int)position.Z), Color.Yellow, true, true);
                                break;
                            case Language.en_US:
                                this.player.ComponentGui.DisplaySmallMessage(string.Format("Set the spawn point\n X: {0} Y : {1} Z : {2}", (object)(int)position.X, (object)(int)position.Y, (object)(int)position.Z), Color.Yellow, true, true);
                                break;
                            default:
                                this.player.ComponentGui.DisplaySmallMessage(string.Format("玩家重生点位置设置\n X: {0} Y : {1} Z : {2}", (object)(int)position.X, (object)(int)position.Y, (object)(int)position.Z), Color.Yellow, true, true);
                                break;
                        }*//*
    }

    //多个界面的入口
    if (ModButton.IsClicked)
    {
        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog(creatorAPI));
    }

    if (ReplaceButton.IsClicked)
    {
        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new NewReplaceDialog(creatorAPI));
    }

    if (SetModeButton.IsClicked)
    {
        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new SetModeDialog(creatorAPI));
    }

    if (OnekeyButton.IsClicked)
    {
        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new OnekeyGenerationDialog(creatorAPI));
    }

    if (CopyPasteButton.IsClicked)
    {
        DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CopyPasteDialog(creatorAPI));
    }

    //清理缓存检查是否有缓存
    ClearCacheButton.IsEnabled = System.IO.Directory.Exists(CreatorMain.CacheDirectory);

    //清理缓存，清理掉落，清理动物
    if (ClearCacheButton.IsClicked)
    {
        if (CreatorMain.CacheDirectory.Delete())
        {
            for (int i = 0; i < 4; i++)
            {
                creatorAPI.Position[i] = new Point3(0, -1, 0);
            }

            player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetcls"), Color.Yellow, true, true);
        }
        *//*switch (CreatorAPI.Language)
{
case Language.zh_CN:
   this.player.ComponentGui.DisplaySmallMessage("清除成功", Color.Yellow, true, true);
   break;
case Language.en_US:
   this.player.ComponentGui.DisplaySmallMessage("Successful clearance", Color.Yellow, true, true);
   break;
default:
   this.player.ComponentGui.DisplaySmallMessage("清除成功", Color.Yellow, true, true);
   break;
}*//*
             else
             {
                 player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetclf"), Color.Red, true, true);
             }
             *//*switch (CreatorAPI.Language)
{
    case Language.zh_CN:
        this.player.ComponentGui.DisplaySmallMessage("清除失败", Color.Red, true, true); break;
    case Language.en_US:
        this.player.ComponentGui.DisplaySmallMessage("Clearance failure", Color.Red, true, true); break;
    default:
        this.player.ComponentGui.DisplaySmallMessage("清除失败", Color.Red, true, true); break;
}*//*

         }
         if (RemoveAnimalButton.IsClicked)
         {
             int num = 0;
             foreach (ComponentCreature creature in player.Project.FindSubsystem<SubsystemCreatureSpawn>(true).Creatures)
             {
                 if (!(creature is ComponentPlayer))
                 {
                     creature.ComponentSpawn.Despawn();
                     ++num;
                 }
             }
             player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetcla"), num), Color.Yellow, true, false);

             *//* switch (CreatorAPI.Language)
              {
                  case Language.zh_CN:
                      this.player.ComponentGui.DisplaySmallMessage(string.Format("清除成功，共清除{0}只动物", (object)num), Color.Yellow, true, false);
                      break;
                  case Language.en_US:
                      this.player.ComponentGui.DisplaySmallMessage(string.Format("Successful clearance, a total of {0} animals removed", (object)num), Color.Yellow, true, false);
                      break;
                  default:
                      this.player.ComponentGui.DisplaySmallMessage(string.Format("清除成功，共清除{0}只动物", (object)num), Color.Yellow, true, false);
                      break;
              }*//*
         }
         if (RemoveItemButton.IsClicked)
         {
             int num = 0;
             foreach (Pickable pickable in GameManager.Project.FindSubsystem<SubsystemPickables>(true).Pickables)
             {
                 pickable.Count = 0;
                 pickable.ToRemove = true;
                 ++num;
             }
             player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("creatorWidgetcld"), num), Color.Yellow, true, false);
             *//*
                             switch (CreatorAPI.Language)
                             {
                                 case Language.zh_CN:
                                     this.player.ComponentGui.DisplaySmallMessage(string.Format("清除成功，共清除{0}个掉落物", (object)num), Color.Yellow, true, false); break;
                                 case Language.en_US:
                                     this.player.ComponentGui.DisplaySmallMessage(string.Format("Successful clearance, total of {0} dropped objects removed", (object)num), Color.Yellow, true, false); break;
                                 default:
                                     this.player.ComponentGui.DisplaySmallMessage(string.Format("清除成功，共清除{0}个掉落物", (object)num), Color.Yellow, true, false); break;
                             }*//*

         }

         //方块测试按钮是否被点击
         int num1 = TerrainTestButton.IsClicked ? 1 : 0;

         //设置难度按钮点击
         if (SetDifficultyButton.IsClicked)
         {
             Vector3 position = player.ComponentBody.Position;
             Point3 point3 = new Point3((int)position.X, (int)position.Y, (int)position.Z);
             int[] numArray = new int[3] { 0, 1, 2 };
             string[] difference = new string[3]
             {
       CreatorMain.Display_Key_Dialog("creatorWidgetde"),
       CreatorMain.Display_Key_Dialog("creatorWidgetdm"),
       CreatorMain.Display_Key_Dialog("creatorWidgetdh")
             };

             DialogsManager.ShowDialog(null, new ListSelectionDialog(CreatorMain.Display_Key_Dialog("creatorWidgetseld"), numArray, 56f, e => difference[(int)e], e =>
{
   worldSettings.StartingPositionMode = (StartingPositionMode)e;
   player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("creatorWidgetdd") + difference[(int)e], Color.Yellow, true, true);

}));
         }

         //设置状态
         int num2 = SetPositionCarefulButton.IsClicked ? 1 : 0;
         int num3 = AdjustPositionButton.IsClicked ? 1 : 0;

         //三点模式和四点模式
         if (ThreePointPlaneButton.IsClicked)
         {
             DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ThreePointToPlaneDialog(creatorAPI));
         }

         if (FourPointSpaceButton.IsClicked)
         {
             DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new FourPointSpaceDialog(creatorAPI));
         }

         if (LangugeButton.IsClicked)
         {
             DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MessageDialog("Choose a Language", "Please confirm if you want to change the language", "OK", "Cancel", (result =>
             {
                 if (result != MessageDialogButton.Button1)
                 {
                     return;
                 }

                 List<Tuple<string, Action>> tupleList = new List<Tuple<string, Action>>();
                 for (int i = (Enum.GetNames(typeof(Language)).GetLength(0)) - 1; i >= 0; i--)
                 {

                     int a = i;
                     *//*if ((Language)System.Enum.ToObject(typeof(Language), i) == Language.ot_OT)
                     {
                         continue;
                     }*//*
                     //   DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", ((Language)System.Enum.ToObject(typeof(Language), i)).ToString(), "OK", null, null));
                     try
                     {
                         tupleList.Add(new Tuple<string, Action>(Language.GetName(typeof(Language), i).ToString(), () =>
                         {
                             CreatorAPI.Language = (Language)System.Enum.ToObject(typeof(Language), a);
                             *//*FileInfo fi = new FileInfo(CreatorMain.SCLanguage_Directory);
                             if (!fi.Directory.Exists)
                             {
                                 fi.Directory.Create();
                             }

                             using (Stream stream_xml1 = new FileStream(CreatorMain.SCLanguage_Directory, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                             {
                                 XElement src = new XElement("CreatorAPI", new XElement("Language"));
                                 src.Element("Language").SetAttributeValue("Language", Language.GetName(typeof(Language), a).ToString());
                                 src.Save(stream_xml1);
                                 //  stream_xml1.Flush();
                                 //    DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", CreatorMain.SCLanguage_Directory + "\n" + src.ToString(), "OK", null, null));

                             }*//*
                             CreatorAPI.IsAddedToProject = false;
                             //   DialogsManager.ShowDialog(componentMiner.ComponentPlayer.GuiWidget, new MessageDialog("Exception", Language.ToString(), "OK", null, null));
                         }));
                     }
                     catch (Exception ex)
                     {
                         DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new MessageDialog("Exception", "ERR:" + ex, "OK", null, null));

                     }
                 }
                 //  CreatorAPI.IsAddedToProject = false;

                 DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ListSelectionDialog("Choose a Language", tupleList, 60f, t => ((Tuple<string, Action>)t).Item1, t => ((Tuple<string, Action>)t).Item2()));
                 //DialogsManager.HideAllDialogs();

             })));

         }
         if (CreatorAPI.IsAddedToProject)
         {
             return;
         }
         else
         {
             ParentWidget.Children.Clear();//.Remove((Widget)this);
             DialogsManager.HideAllDialogs();
         }
     }


     *//*移除接口*//*
     public static void Dismiss(bool result)
     {
         _ = result;
         CreatorAPI.IsAddedToProject = false;
         //this.ParentWidget.Children.Remove((Widget)this);
         //DialogsManager.HideDialog((Dialog)this);
     }

 }
}
*/