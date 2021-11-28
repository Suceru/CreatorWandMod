// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.PavageDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*平铺界面*/
/*namespace CreatorModAPI-=  public class PavageDialog : Dialog*/
using Engine;
using Game;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class PavageDialog : Dialog
    {
        private readonly CreatorAPI creatorAPI;
        private readonly ComponentPlayer player;
        private readonly ButtonWidget OKButton;
        private readonly TextBoxWidget TextBox;
        private readonly SliderWidget slider;
        private readonly ButtonWidget cancelButton;

        public PavageDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/Tile");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("Tile").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Tile", "Tile");
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Tile", "OK");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Tile", "Cancel");
            TextBox = Children.Find<TextBoxWidget>("BlockID");
            slider = Children.Find<SliderWidget>("Slider1");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (this.slider.IsSliding)
            {
                this.slider.Value = (int)this.slider.Value;
            }

            SliderWidget slider = this.slider;
            int index = (int)this.slider.Value;

            string str = CreatorMain.Display_Key_Dialog("pavdialog1") + index.ToString();

            slider.Text = str;
            if (OKButton.IsClicked)
            {
                Point3 Start = creatorAPI.Position[0];
                Point3 End = creatorAPI.Position[1];
                CreatorMain.Math.StartEnd(ref Start, ref End);
                try
                {
                    List<int> BlockIDs = new List<int>();
                    string[] strArray = TextBox.Text.Split(new char[1]
                    {
            ':'
                    }, StringSplitOptions.RemoveEmptyEntries);
                    for (index = 0; index < strArray.Length; ++index)
                    {
                        string s = strArray[index];
                        BlockIDs.Add(int.Parse(s));
                    }
                    Task.Run(() =>
                   {
                       ChunkData chunkData = new ChunkData(creatorAPI);
                       creatorAPI.revokeData = new ChunkData(creatorAPI);
                       int num1 = 0;
                       for (int x = End.X; x <= Start.X; ++x)
                       {
                           for (int y = End.Y; y <= Start.Y; ++y)
                           {
                               for (int z = End.Z; z <= Start.Z; ++z)
                               {
                                   if (!creatorAPI.launch)
                                   {
                                       return;
                                   }

                                   int num2 = BlockIDs[((x - End.X) / (int)this.slider.Value + (y - End.Y) / (int)this.slider.Value + (z - End.Z) / (int)this.slider.Value) % BlockIDs.Count];
                                   creatorAPI.CreateBlock(x, y, z, num2, chunkData);
                                   ++num1;
                               }
                           }
                       }
                       chunkData.Render();
                       player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog2"), num1), Color.LightYellow, true, true);

                   });
                }
                catch
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("pavdialog1"), Color.Red, true, true);

                }
                DialogsManager.HideDialog(this);
            }
            if (!cancelButton.IsClicked)
            {
                return;
            }

            DialogsManager.HideDialog(this);
        }
    }
}
