using Engine;
using Game;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class TransferDialog : Dialog
    {
        private readonly ButtonWidget cancelButton;

        private readonly ButtonWidget Transfer;

        private readonly Game.TextBoxWidget X;

        private readonly Game.TextBoxWidget Y;

        private readonly Game.TextBoxWidget Z;

        private BevelledButtonWidget P1;

        private BevelledButtonWidget P2;

        private BevelledButtonWidget P3;

        private BevelledButtonWidget P4;

        private BevelledButtonWidget Spawn;

        private readonly SubsystemTerrain subsystemTerrain;

        private readonly ComponentPlayer player;

        private Regex Regex;

        public TransferDialog(CreatorAPI creatorAPI)
        {
            P1 = new BevelledButtonWidget
            {
                Text = "Point1",
                Size = new Vector2(120f, 60f),
                Margin = new Vector2(120f, 60f)
            };
            P2 = new BevelledButtonWidget
            {
                Text = "Point2",
                Size = new Vector2(120f, 60f),
                Margin = new Vector2(240f, 60f)
            };
            P3 = new BevelledButtonWidget
            {
                Text = "Point3",
                Size = new Vector2(120f, 60f),
                Margin = new Vector2(360f, 60f)
            };
            P4 = new BevelledButtonWidget
            {
                Text = "Point4",
                Size = new Vector2(120f, 60f),
                Margin = new Vector2(480f, 60f)
            };
            Spawn = new BevelledButtonWidget
            {
                Text = "Spawn",
                Size = new Vector2(120f, 60f),
                Margin = new Vector2(120f, 120f)
            };
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
            XElement node = ContentManager.Get<XElement>("Dialog/Teleport", (string)null);
            LoadChildren(this, node);
            ((FontTextWidget)Children.Find<LabelWidget>("Teleport")).Text=(CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Teleport"));
            ((FontTextWidget)Children.Find<LabelWidget>("X:")).Text=(CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "X:"));
            ((FontTextWidget)Children.Find<LabelWidget>("Y:")).Text=(CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Y:"));
            ((FontTextWidget)Children.Find<LabelWidget>("Z:")).Text=(CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Z:"));
            Children.Add(P1, P2, P3, P4, Spawn);
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Cancel");
            Transfer = Children.Find<ButtonWidget>("OK");
            Transfer.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "OK");
            X = Children.Find<Game.TextBoxWidget>("X");
            Y = Children.Find<Game.TextBoxWidget>("Y");
            Z = Children.Find<Game.TextBoxWidget>("Z");
            Regex = new Regex("[^0-9]+");
            X.Text = ((int)player.ComponentBody.Position.X).ToString();
            Y.Text = ((int)player.ComponentBody.Position.Y).ToString();
            Z.Text = ((int)player.ComponentBody.Position.Z).ToString();
            P1.IsEnabled = false;
            P2.IsEnabled = false;
            P3.IsEnabled = false;
            P4.IsEnabled = false;
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (X.IsHitTestVisible || Y.IsHitTestVisible || Z.IsHitTestVisible)
            {
                X.Text = Regex.Replace(X.Text, "");
                Y.Text = Regex.Replace(Y.Text, "");
                Z.Text = Regex.Replace(Z.Text, "");
            }

            if (Spawn.IsClicked)
            {
                player.ComponentBody.Position = player.PlayerData.SpawnPosition;
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)player.PlayerData.SpawnPosition.X, (int)player.PlayerData.SpawnPosition.Y, (int)player.PlayerData.SpawnPosition.Z), Color.LightYellow, blinking: true, playNotificationSound: true);
                DialogsManager.HideDialog(this);
            }

            if (CreatorMain.Position != null)
            {
                Vector3 position = default(Vector3);
                bool flag = false;
                if (CreatorMain.Position[0].Y > 0)
                {
                    P1.IsEnabled = true;
                }

                if (CreatorMain.Position[1].Y > 0)
                {
                    P2.IsEnabled = true;
                }

                if (CreatorMain.Position[2].Y > 0)
                {
                    P3.IsEnabled = true;
                }

                if (CreatorMain.Position[3].Y > 0)
                {
                    P4.IsEnabled = true;
                }

                if (P1.IsClicked)
                {
                    position = new Vector3(CreatorMain.Position[0]);
                    flag = true;
                }

                if (P2.IsClicked)
                {
                    position = new Vector3(CreatorMain.Position[1]);
                    flag = true;
                }

                if (P3.IsClicked)
                {
                    position = new Vector3(CreatorMain.Position[2]);
                    flag = true;
                }

                if (P4.IsClicked)
                {
                    position = new Vector3(CreatorMain.Position[3]);
                    flag = true;
                }

                if (flag)
                {
                    player.ComponentBody.Position = position;
                    player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)position.X, (int)position.Y, (int)position.Z), Color.LightYellow, blinking: true, playNotificationSound: true);
                    DialogsManager.HideDialog(this);
                }
            }

            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (!Transfer.IsClicked)
            {
                return;
            }

            Point3 point = default(Point3);
            if (!int.TryParse(X.Text, out point.X) || !int.TryParse(Y.Text, out point.Y) || !int.TryParse(Z.Text, out point.Z) || (float)int.Parse(Y.Text) < 0f || (float)int.Parse(Y.Text) > 296f)
            {
                if ((float)int.Parse(Y.Text) < 0f || (float)int.Parse(Y.Text) > 296f)
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("tradialog1") + (((float)int.Parse(Y.Text) < 0f || (float)int.Parse(Y.Text) > 296f) ? ": 0<Y<297" : ""), Color.Red, blinking: true, playNotificationSound: true);
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("tradialog1"), Color.Red, blinking: true, playNotificationSound: true);
                }
            }
            else
            {
                Vector3 position2 = new Vector3(point.X, point.Y, point.Z);
                player.ComponentBody.Position = position2;
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)position2.X, (int)position2.Y, (int)position2.Z), Color.LightYellow, blinking: true, playNotificationSound: true);
            }

            DialogsManager.HideDialog(this);
        }
    }
}
/*传送*/
/*namespace CreatorModAPI-=  public class TransferDialog : Dialog*//*
using Engine;
using Game;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class TransferDialog : Dialog
    {
        private readonly ButtonWidget cancelButton;
        private readonly ButtonWidget Transfer;
        private readonly TextBoxWidget X;
        private readonly TextBoxWidget Y;
        private readonly TextBoxWidget Z;
        private BevelledButtonWidget P1;
        private BevelledButtonWidget P2;
        private BevelledButtonWidget P3;
        private BevelledButtonWidget P4;
        private BevelledButtonWidget Spawn;
        private readonly SubsystemTerrain subsystemTerrain;
        private readonly ComponentPlayer player;
        private System.Text.RegularExpressions.Regex Regex;

        public TransferDialog(CreatorAPI creatorAPI)
        {
            P1 = new BevelledButtonWidget { Text = "Point1", Size = new Vector2(120, 60), Margin = new Vector2(120f * 1, 60) };
            P2 = new BevelledButtonWidget { Text = "Point2", Size = new Vector2(120, 60), Margin = new Vector2(120f * 2, 60) };
            P3 = new BevelledButtonWidget { Text = "Point3", Size = new Vector2(120, 60), Margin = new Vector2(120f * 3, 60) };
            P4 = new BevelledButtonWidget { Text = "Point4", Size = new Vector2(120, 60), Margin = new Vector2(120f * 4, 60) };
            Spawn = new BevelledButtonWidget { Text = "Spawn", Size = new Vector2(120, 60), Margin = new Vector2(120f * 1, 60*2) };
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/Teleport");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("Teleport").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Teleport");
            Children.Find<LabelWidget>("X:").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "X:");
            Children.Find<LabelWidget>("Y:").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Y:");
            Children.Find<LabelWidget>("Z:").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Z:");
            this.Children.Add(P1, P2, P3, P4, Spawn);
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "Cancel");
            Transfer = Children.Find<ButtonWidget>("OK");
            Transfer.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Teleport", "OK");
            X = Children.Find<TextBoxWidget>(nameof(X));
            Y = Children.Find<TextBoxWidget>(nameof(Y));
            Z = Children.Find<TextBoxWidget>(nameof(Z));
            Regex = new System.Text.RegularExpressions.Regex(@"[^0-9]+");
            X.Text = ((int)player.ComponentBody.Position.X).ToString();
            Y.Text = ((int)player.ComponentBody.Position.Y).ToString();
            Z.Text = ((int)player.ComponentBody.Position.Z).ToString();
            P1.IsEnabled = false;
            P2.IsEnabled = false;
            P3.IsEnabled = false;
            P4.IsEnabled = false;
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (X.IsHitTestVisible || Y.IsHitTestVisible || Z.IsHitTestVisible)
            {
                X.Text = Regex.Replace(X.Text, "");
                Y.Text = Regex.Replace(Y.Text, "");
                Z.Text = Regex.Replace(Z.Text, "");
            }
            if (Spawn.IsClicked)
            {
                player.ComponentBody.Position = player.PlayerData.SpawnPosition;
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)player.PlayerData.SpawnPosition.X, (int)player.PlayerData.SpawnPosition.Y, (int)player.PlayerData.SpawnPosition.Z), Color.LightYellow, true, true);
                DialogsManager.HideDialog(this);
            }
            if (CreatorMain.Position != null)
            {
                Vector3 p = new Vector3();
                bool t = false;
                if (CreatorMain.Position[0].Y > 0) P1.IsEnabled = true;
                if (CreatorMain.Position[1].Y > 0) P2.IsEnabled = true;
                if (CreatorMain.Position[2].Y > 0) P3.IsEnabled = true;
                if (CreatorMain.Position[3].Y > 0) P4.IsEnabled = true;

                if (P1.IsClicked) { p = new Vector3(CreatorMain.Position[0]); t = true; }
                if (P2.IsClicked) { p = new Vector3(CreatorMain.Position[1]); t = true; }
                if (P3.IsClicked) { p = new Vector3(CreatorMain.Position[2]); t = true; }
                if (P4.IsClicked) { p = new Vector3(CreatorMain.Position[3]); t = true; }
                if (t)
                {
                    player.ComponentBody.Position = p;
                    player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)p.X, (int)p.Y, (int)p.Z), Color.LightYellow, true, true);
                    DialogsManager.HideDialog(this);
                }
            }
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (Transfer.IsClicked)
            {

                Point3 point3;
                if (!int.TryParse(X.Text, out point3.X) || !int.TryParse(Y.Text, out point3.Y) || !int.TryParse(Z.Text, out point3.Z) || (int.Parse(Y.Text) < 0f || int.Parse(Y.Text) > 296f))
                {
                    switch ((int.Parse(Y.Text) < 0f || int.Parse(Y.Text) > 296f))
                    {
                        case true:
                            player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("tradialog1") + ((int.Parse(Y.Text) < 0f || int.Parse(Y.Text) > 296f) ? ": 0<Y<297" : ""), Color.Red, true, true);
                            break;

                        default:
                            player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("tradialog1"), Color.Red, true, true);
                            break;
                    }

                    *//*
                                    switch (CreatorAPI.Language)
                                    {
                                        case Language.zh_CN:
                                            this.player.ComponentGui.DisplaySmallMessage("请输入正当的坐标", Color.Red, true, true);
                                            break;
                                        case Language.en_US:
                                            this.player.ComponentGui.DisplaySmallMessage("Please enter the correct coordinates", Color.Red, true, true);
                                            break;
                                        default:
                                            this.player.ComponentGui.DisplaySmallMessage("请输入正当的坐标", Color.Red, true, true);
                                            break;
                                    }*//*

                }
                else
                {
                    Vector3 vector3 = new Vector3(point3.X, point3.Y, point3.Z);
                    player.ComponentBody.Position = vector3;
                    player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("tradialog2"), (int)vector3.X, (int)vector3.Y, (int)vector3.Z), Color.LightYellow, true, true);
                    *//*
                                    switch (CreatorAPI.Language)
                                    {
                                        case Language.zh_CN:
                                            this.player.ComponentGui.DisplaySmallMessage(string.Format("成功传送到：\nX:{0} , Y:{1} , Z:{2}", (object)(int)vector3.X, (object)(int)vector3.Y, (object)(int)vector3.Z), Color.LightYellow, true, true);
                                            break;
                                        case Language.en_US:
                                            this.player.ComponentGui.DisplaySmallMessage(string.Format("Successful transmission to：\nX:{0} , Y:{1} , Z:{2}", (object)(int)vector3.X, (object)(int)vector3.Y, (object)(int)vector3.Z), Color.LightYellow, true, true);
                                            break;
                                        default:
                                            this.player.ComponentGui.DisplaySmallMessage(string.Format("成功传送到：\nX:{0} , Y:{1} , Z:{2}", (object)(int)vector3.X, (object)(int)vector3.Y, (object)(int)vector3.Z), Color.LightYellow, true, true);
                                            break;
                                    }*//*
                }
                DialogsManager.HideDialog(this);

            }

        }
    }
}
*/