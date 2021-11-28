// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.OnekeyGenerationDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*一键界面*/
/*namespace CreatorModAPI-=  public class OnekeyGenerationDialog : Dialog*/
using Engine;
using Game;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class OnekeyGenerationDialog : Dialog
    {
        public static readonly string SdcardOnekey = CreatorMain.Sdcard;
        private readonly ButtonWidget OKButton;
        private readonly ButtonWidget TypeButton;
        private readonly ButtonWidget CreateButton;
        private readonly ButtonWidget DerivedButton;
        private readonly ButtonWidget ImportButton;
        private readonly ButtonWidget OnAndOffButton;
        // private readonly SubsystemTerrain subsystemTerrain;
        private readonly ComponentPlayer player;
        private readonly ListPanelWidget ListView;
        private readonly CreatorAPI creatorAPI;
        private readonly ButtonWidget DeleteButton;

        public OnekeyGenerationDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            //    subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
            XElement node = ContentManager.Get<XElement>("Dialog/OneClickGeneration");

            LoadChildren(this, node);
            CreateButton = Children.Find<ButtonWidget>("Create");
            CreateButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "Create");
            OnAndOffButton = Children.Find<ButtonWidget>("On");
            OnAndOffButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "On");
            DerivedButton = Children.Find<ButtonWidget>("Export");
            DerivedButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "Export");
            ImportButton = Children.Find<ButtonWidget>("Import");
            ImportButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "Import");
            DeleteButton = Children.Find<ButtonWidget>("Delete");
            DeleteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "Delete");
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "OK");
            ListView = Children.Find<ListPanelWidget>(nameof(ListView));
            TypeButton = Children.Find<ButtonWidget>("Type");
            TypeButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "OneClickGeneration", "Type");
            UpList();
            LoadProperties(this, node);
        }

        private void UpList()
        {
            ListView.ClearItems();
            if (!Directory.Exists(CreatorMain.Export_OnekeyFile_Directory))
            {
                Directory.CreateDirectory(CreatorMain.Export_OnekeyFile_Directory);
            }

            foreach (string file in Directory.GetFiles(CreatorMain.Export_OnekeyFile_Directory))
            {
                if (Path.GetExtension(file) == ".o")
                {
                    ListView.AddItem(Path.GetFileName(file));
                }
            }
        }

        public override void Update()
        {
            TypeButton.IsEnabled = false;
            if (OKButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (ImportButton.IsClicked)
            {
                if (!Directory.Exists(CreatorMain.CacheDirectory))
                {
                    Directory.CreateDirectory(CreatorMain.CacheDirectory);
                }

                string oneKeyFile = CreatorMain.OneKeyFile;
                string str = CreatorMain.Export_OnekeyFile_Directory + "/" + (string)ListView.SelectedItem;
                if (!str.IsFileInUse() && (!File.Exists(oneKeyFile) || !oneKeyFile.IsFileInUse()))
                {
                    FileStream fileStream1 = new FileStream(str, FileMode.Open);
                    FileStream fileStream2 = new FileStream(oneKeyFile, FileMode.Create);
                    fileStream1.CopyTo(fileStream2);
                    fileStream2.Dispose();
                    fileStream1.Dispose();
                    player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, true, false);
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                }

                DialogsManager.HideDialog(this);
            }
            if (DeleteButton.IsClicked)
            {
                if ((CreatorMain.Export_OnekeyFile_Directory + "/" + (string)ListView.SelectedItem).Delete())
                {
                    player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, true, false);
                    UpList();
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                }
            }
            DerivedButton.IsEnabled = File.Exists(CreatorMain.OneKeyFile);
            if (DerivedButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new OnekeyGenerationDialog.DerivedDialog(player, this, ListView));
            }

            if (CreateButton.IsClicked)
            {
                if (creatorAPI.Position[2] == new Point3(0, -1, 0))
                {
                    player.ComponentGui.DisplaySmallMessage("请设置点3！", Color.LightRed, true, false);
                }
                else
                {
                    Task.Run(() =>
                   {
                       try
                       {
                           if (!Directory.Exists(CreatorMain.CacheDirectory))
                           {
                               Directory.CreateDirectory(CreatorMain.CacheDirectory);
                           }

                           OnekeyGeneration.CreateOnekey(creatorAPI, CreatorMain.CacheDirectory + "/", "CacheFile.od", creatorAPI.Position[0], creatorAPI.Position[1], creatorAPI.Position[2]);
                       }
                       catch (Exception ex)
                       {
                           player.ComponentGui.DisplaySmallMessage(ex.Message, Color.Red, true, false);
                       }
                   });
                }

                DialogsManager.HideDialog(this);
            }
            if (!ListView.SelectedIndex.HasValue)
            {
                ImportButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else
            {
                ImportButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            OnAndOffButton.Color = !creatorAPI.oneKeyGeneration ? Color.Red : Color.Yellow;
            if (!OnAndOffButton.IsClicked)
            {
                return;
            }

            if (creatorAPI.oneKeyGeneration)
            {
                creatorAPI.oneKeyGeneration = false;
            }
            else
            {
                creatorAPI.oneKeyGeneration = true;
            }
        }

        private class DerivedDialog : Dialog
        {
            private readonly ButtonWidget OK;
            private readonly ButtonWidget cancelButton;
            private readonly TextBoxWidget TextBox;
            private readonly ComponentPlayer player;
            private readonly Dialog dialog;
            private readonly ListPanelWidget listView;

            public DerivedDialog(ComponentPlayer player, Dialog dialog, ListPanelWidget listView)
            {
                this.player = player;
                this.dialog = dialog;
                this.listView = listView;
                XElement node = ContentManager.Get<XElement>("Dialog/Manager3");

                LoadChildren(this, node);
                Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("moddialogname");
                cancelButton = Children.Find<ButtonWidget>("Cancel");
                cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "Cancel");
                OK = Children.Find<ButtonWidget>("OK");
                OK.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "OK");
                TextBox = Children.Find<TextBoxWidget>("BlockID");
                TextBox.Text = "";
                Children.Find<BlockIconWidget>("Block").IsVisible = false;
                LoadProperties(this, node);
            }

            public override void Update()
            {
                if (cancelButton.IsClicked)
                {
                    DialogsManager.HideDialog(this);
                }

                if (!OK.IsClicked)
                {
                    return;
                }

                string onekeyFileDirectory = CreatorMain.Export_OnekeyFile_Directory;
                string str = TextBox.Text.Length > 0 ? TextBox.Text : DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString();/*DateTime.Now.ToLongTimeString().ToString()*/
                if (!CreatorMain.OneKeyFile.IsFileInUse())
                {
                    try
                    {
                        FileStream fileStream1 = new FileStream(CreatorMain.OneKeyFile, FileMode.Open);
                        if (!Directory.Exists(onekeyFileDirectory))
                        {
                            Directory.CreateDirectory(onekeyFileDirectory);
                        }

                        FileStream fileStream2 = new FileStream(onekeyFileDirectory + "/" + str + ".o", FileMode.OpenOrCreate);
                        fileStream1.CopyTo(fileStream2);
                        fileStream2.Dispose();
                        fileStream1.Dispose();
                        player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + onekeyFileDirectory + "/" + str + ".o", Color.LightGreen, true, false);
                        DialogsManager.HideDialog(this);
                        listView.ClearItems();
                        if (!Directory.Exists(CreatorMain.Export_OnekeyFile_Directory))
                        {
                            Directory.CreateDirectory(CreatorMain.Export_OnekeyFile_Directory);
                        }

                        foreach (string file in Directory.GetFiles(CreatorMain.Export_OnekeyFile_Directory))
                        {
                            if (Path.GetExtension(file) == ".o")
                            {
                                listView.AddItem(Path.GetFileName(file));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + onekeyFileDirectory, Color.Red, true, false);
                        DialogsManager.HideDialog(this);
                        DialogsManager.HideDialog(dialog);
                    }
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                }

                DialogsManager.HideDialog(this);
            }
        }
    }
}
