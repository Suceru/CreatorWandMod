// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.MountainDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*山脉生成*/
/*namespace CreatorModAPI-=  public class MountainDialog : Dialog*/
using Engine;
using Game;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class MountainDialog : Dialog
    {
        private readonly SliderWidget num1;
        private readonly SliderWidget num2;
        private readonly SliderWidget num3;
        private readonly LabelWidget num_1;
        private readonly LabelWidget num_2;
        private readonly LabelWidget num_3;
        private readonly ButtonWidget cancelButton;
        private readonly ButtonWidget OK;
        private readonly ButtonWidget restting;
        private readonly TextBoxWidget TextBox;
        private readonly ComponentPlayer player;
        private readonly CreatorAPI creatorAPI;

        public MountainDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/Mountains");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("Mountains").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Mountains", "Mountains");
            OK = Children.Find<ButtonWidget>("OK");
            OK.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Mountains", "OK");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Mountains", "Cancel");
            num_1 = Children.Find<LabelWidget>("SliderData1");
            num_2 = Children.Find<LabelWidget>("SliderData2");
            num_3 = Children.Find<LabelWidget>("SliderData3");
            TextBox = Children.Find<TextBoxWidget>("BlockID");
            num1 = Children.Find<SliderWidget>("Slider1");
            num2 = Children.Find<SliderWidget>("Slider2");
            num3 = Children.Find<SliderWidget>("Slider3");
            num3.Value = 100f;
            restting = Children.Find<ButtonWidget>("Reset");
            restting.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Mountains", "Reset");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            num_1.Text = string.Format(CreatorMain.Display_Key_Dialog("moundialog1"), (int)num1.Value);
            num_2.Text = string.Format(CreatorMain.Display_Key_Dialog("moundialog2"), (int)num2.Value);
            num_3.Text = string.Format(CreatorMain.Display_Key_Dialog("moundialog3"), (int)num3.Value);
            /*  switch (CreatorAPI.Language)
              {
                  case Language.zh_CN:
                      this.num_1.Text = string.Format("参数1 :{0}", (object)(int)this.num1.Value);
                      this.num_2.Text = string.Format("参数2 :{0}", (object)(int)this.num2.Value);
                      this.num_3.Text = string.Format("参数3 :{0}", (object)(int)this.num3.Value);
                      break;
                  case Language.en_US:
                      this.num_1.Text = string.Format("X-length:{0}", (object)(int)this.num1.Value);
                      this.num_2.Text = string.Format("Y-length:{0}", (object)(int)this.num2.Value);
                      this.num_3.Text = string.Format("Z-length:{0}", (object)(int)this.num3.Value);
                      break;
                  default:
                      this.num_1.Text = string.Format("参数1 :{0}", (object)(int)this.num1.Value);
                      this.num_2.Text = string.Format("参数2 :{0}", (object)(int)this.num2.Value);
                      this.num_3.Text = string.Format("参数3 :{0}", (object)(int)this.num3.Value);
                      break;
              }*/
            if (restting.IsClicked)
            {
                num1.Value = 0.0f;
                num2.Value = 0.0f;
                num3.Value = 100f;
                TextBox.Text = "3:2:8";
            }
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (!OK.IsClicked)
            {
                return;
            }

            int? BlockID_2 = new int?();
            int? BlockID_3 = new int?();
            string[] strArray = TextBox.Text.Split(new char[1]
            {
        ':'
            }, StringSplitOptions.RemoveEmptyEntries);
            int BlockID_1;
            if (strArray.Length == 2)
            {
                if (!int.TryParse(strArray[0], out BlockID_1))
                {
                    BlockID_1 = 0;
                }

                if (int.TryParse(strArray[1], out int result))
                {
                    BlockID_2 = new int?(result);
                }
            }
            else if (strArray.Length >= 3)
            {
                if (!int.TryParse(strArray[0], out BlockID_1))
                {
                    BlockID_1 = 0;
                }

                if (int.TryParse(strArray[1], out int result))
                {
                    BlockID_2 = new int?(result);
                }

                if (int.TryParse(strArray[2], out result))
                {
                    BlockID_3 = new int?(result);
                }
            }
            else if (!int.TryParse(TextBox.Text, out BlockID_1))
            {
                BlockID_1 = 0;
            }

            Vector3 vector = new Vector3()
            {
                X = num1.Value,
                Y = num2.Value,
                Z = num3.Value
            };
            Point3 Start = creatorAPI.Position[0];
            Point3 End = creatorAPI.Position[1];
            CreatorMain.Math.StartEnd(ref Start, ref End);
            float X_Radius = (Start.X - End.X) / 2f;
            float Z_Radius = (Start.Z - End.Z) / 2f;
            float Radius = X_Radius > (double)Z_Radius ? X_Radius : Z_Radius;
            float radius = X_Radius > (double)Z_Radius ? Z_Radius : X_Radius;
            Radius = System.Math.Abs(System.Math.Abs(Radius) - 2f + vector.X);
            if (!BlockID_2.HasValue)
            {
                BlockID_2 = new int?(BlockID_1);
            }

            if (!BlockID_3.HasValue)
            {
                BlockID_3 = BlockID_2;
            }

            Task.Run(() =>
           {
               ChunkData chunkData = new ChunkData(creatorAPI);
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               Game.Random random = new Game.Random();
               double num1 = System.Math.PI / 2.0;
               int num2 = 0;
               float num3 = (float)(1.25 + vector.Y / 99.0);
               double num4 = 25.0 + vector.Z / 10.0;
               float num5 = random.Float(18f, (float)num4);
               for (int index1 = (int)-X_Radius; index1 <= (int)X_Radius; ++index1)
               {
                   for (int index2 = (int)-Z_Radius; index2 <= (double)Z_Radius; ++index2)
                   {
                       double num6 = System.Math.Cos(num1 * index1 / Radius) * System.Math.Cos(num1 * index2 / radius) * (Start.Y - End.Y);
                       double num7 = System.Math.Sin(num1 * index1 * num3 / radius + 2.0) * System.Math.Cos(num1 * index2 * num3 / Radius + 7.0) * (Start.Y - End.Y) * 0.349999994039536;
                       double num8 = System.Math.Sin(num1 * index1 * num3 * 2.0 / Radius + 2.0 * num5) * System.Math.Sin(num1 * index2 * num3 * 2.0 / radius + 8.0 * num5) * (Start.Y - End.Y) * 0.200000002980232;
                       double num9 = System.Math.Sin(num1 * index1 * num3 * 3.5 / radius + 2.0 * num5 * 1.5) * System.Math.Sin(num1 * index2 * num3 * 3.5 / Radius + 12.0 * num5 * 1.5) * (Start.Y - End.Y) * 0.150000005960464;
                       double num10 = num7;
                       double num11 = num6 - num10 + num8 - num9;
                       if (num11 > 0.0)
                       {
                           for (int index3 = 0; index3 <= num11; ++index3)
                           {
                               Point3 point3 = new Point3((Start.X + End.X) / 2 + index1, End.Y + (int)num11 - index3, (Start.Z + End.Z) / 2 + index2);
                               if (index3 > 5)
                               {
                                   creatorAPI.CreateBlock(point3, BlockID_1, chunkData);
                               }
                               else if (index3 > 0)
                               {
                                   creatorAPI.CreateBlock(point3, BlockID_2.Value, chunkData);
                               }
                               else if (index3 == 0)
                               {
                                   creatorAPI.CreateBlock(point3, BlockID_3.Value, chunkData);
                               }

                               ++num2;
                               if (!creatorAPI.launch)
                               {
                                   return;
                               }
                           }
                       }
                   }
               }
               chunkData.Render();

               player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num2), Color.LightYellow, true, true);

               /* switch (CreatorAPI.Language)
                {
                    case Language.zh_CN:
                        this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num2), Color.LightYellow, true, true);

                        break;
                    case Language.en_US:
                        this.player.ComponentGui.DisplaySmallMessage(string.Format("The operation was successful, generating a total of {0}blocks", (object)num2), Color.LightYellow, true, true);
                        break;
                    default:
                        this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共生成{0}个方块", (object)num2), Color.LightYellow, true, true);
                        break;
                }*/
           });
            DialogsManager.HideDialog(this);
        }
    }
}
