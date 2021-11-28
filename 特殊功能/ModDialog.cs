// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.ModDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*模组界面*/
/*namespace CreatorModAPI-=  public class ModDialog : Dialog*/
using Engine;
using Game;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class ModDialog : Dialog
    {
        private readonly ButtonWidget ImportButton;
        private readonly ButtonWidget ExportOnekeyButton;
        private readonly ButtonWidget ExportCopyButton;
        private readonly ButtonWidget DeleteButton;
        private readonly ButtonWidget OK;
        private readonly CreatorAPI creatorAPI;
        private readonly ComponentPlayer player;
        private readonly ListPanelWidget ListView;
        private readonly ButtonWidget ExportOldCopyButton;
        public ButtonWidget ExportOldOnekeyButton;
        private readonly ButtonWidget DerivedSpecialButton;

        public ModDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Widget/CustomModule");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("ClassMOD").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "ClassMOD");
            OK = Children.Find<ButtonWidget>("OK");
            OK.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "OK");
            ExportOnekeyButton = Children.Find<ButtonWidget>("Export Key");
            ExportOnekeyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "Export Key");
            ImportButton = Children.Find<ButtonWidget>("ImportConfig");
            ImportButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "ImportConfig");
            ExportCopyButton = Children.Find<ButtonWidget>("Export Copy");
            ExportCopyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "Export Copy");
            ExportOldCopyButton = Children.Find<ButtonWidget>("ExportOldCopy");
            ExportOldCopyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "ExportOldCopy");
            ExportOldOnekeyButton = Children.Find<ButtonWidget>("ExportOldKey");
            ExportOldOnekeyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "ExportOldKey");
            DerivedSpecialButton = Children.Find<ButtonWidget>("Export Special");
            DerivedSpecialButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "Export Special");
            DeleteButton = Children.Find<ButtonWidget>("Del");
            DeleteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CustomModule", "Del");
            ListView = Children.Find<ListPanelWidget>(nameof(ListView));
            UpList();
            LoadProperties(this, node);
        }

        private void UpList()
        {
            ListView.ClearItems();
            if (!Directory.Exists(CreatorMain.Export_ModFile_Directory))
            {
                Directory.CreateDirectory(CreatorMain.Export_ModFile_Directory);
            }

            foreach (string file in Directory.GetFiles(CreatorMain.Export_ModFile_Directory))
            {
                if (Path.GetExtension(file) == ".oMod" || Path.GetExtension(file) == ".wMod" || (Path.GetExtension(file) == ".oMod2" || Path.GetExtension(file) == ".wMod2") || Path.GetExtension(file) == ".sMod")
                {
                    ListView.AddItem(Path.GetFileName(file));
                }
            }
        }

        public override void Update()
        {
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
            if (OK.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (DeleteButton.IsClicked)
            {
                if ((CreatorMain.Export_ModFile_Directory + "/" + (string)ListView.SelectedItem).Delete())
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("moddialogdel1"), Color.LightYellow, true, false);

                    //  this.player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, true, false);
                    UpList();
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("moddialogdel2"), Color.Red, true, false);
                }
                //this.player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
            }
            ExportOnekeyButton.IsEnabled = File.Exists(CreatorMain.OneKeyFile);
            ExportCopyButton.IsEnabled = File.Exists(CreatorMain.CopyFile);
            ExportOldCopyButton.IsEnabled = File.Exists(CreatorMain.CopyFile);
            ExportOldOnekeyButton.IsEnabled = File.Exists(CreatorMain.OneKeyFile);
            DerivedSpecialButton.IsEnabled = File.Exists(CreatorMain.SpecialCopyFile);
            if (ExportOnekeyButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog.DerivedDialog(player, this, ListView, ModDialog.DataType.OneKey));
            }

            if (ExportCopyButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog.DerivedDialog(player, this, ListView));
            }

            if (ExportOldCopyButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog.DerivedDialog(player, this, ListView, ModDialog.DataType.OldCopy));
            }

            if (ExportOldOnekeyButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog.DerivedDialog(player, this, ListView, ModDialog.DataType.OldOneKey));
            }

            if (DerivedSpecialButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new ModDialog.DerivedDialog(player, this, ListView, ModDialog.DataType.SpecialCopy));
            }

            if (!ImportButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
           {
               if (!Directory.Exists(CreatorMain.CacheDirectory))
               {
                   Directory.CreateDirectory(CreatorMain.CacheDirectory);
               }

               string str = CreatorMain.Export_ModFile_Directory + "/" + (string)ListView.SelectedItem;
               if (str.IsFileInUse())
               {
                   player.ComponentGui.DisplaySmallMessage("操作失败...\n" + str, Color.Red, true, false);
                   DialogsManager.HideDialog(this);
               }
               else if (Path.GetExtension(str) == ".oMod2")
               {
                   OnekeyGeneration.ImportOnekeyoMod2(CreatorMain.OneKeyFile, str);
                   player.ComponentGui.DisplaySmallMessage("导入一键生成MOD配置文件成功！", Color.LightYellow, true, false);
               }
               else if (Path.GetExtension(str) == ".wMod2")
               {
                   CopyAndPaste.ImportCopywMod2(CreatorMain.CopyFile, str);
                   player.ComponentGui.DisplaySmallMessage("导入复制MOD配置文件成功！", Color.LightYellow, true, false);
               }
               else if (Path.GetExtension(str) == ".wMod")
               {
                   CopyAndPaste.ImportCopywMod(CreatorMain.CopyFile, str);
                   player.ComponentGui.DisplaySmallMessage("导入复制MOD配置文件成功！", Color.LightYellow, true, false);
               }
               else if (Path.GetExtension(str) == ".oMod")
               {
                   player.ComponentGui.DisplaySmallMessage("抱歉，一键生成的旧文件数据无法导入！", Color.Red, true, false);
               }
               else if (Path.GetExtension(str) == ".sMod")
               {
                   if (!Directory.Exists(CreatorMain.CacheDirectory))
                   {
                       Directory.CreateDirectory(CreatorMain.CacheDirectory);
                   }

                   string specialCopyFile = CreatorMain.SpecialCopyFile;
                   if (!str.IsFileInUse() && (!File.Exists(specialCopyFile) || !specialCopyFile.IsFileInUse()))
                   {
                       FileStream fileStream1 = new FileStream(str, FileMode.Open);
                       FileStream fileStream2 = new FileStream(specialCopyFile, FileMode.Create);
                       fileStream1.CopyTo(fileStream2);
                       fileStream2.Dispose();
                       fileStream1.Dispose();
                       player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, true, false);
                   }
                   else
                   {
                       player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                   }
               }
               else
               {
                   player.ComponentGui.DisplaySmallMessage("操作失败...\n" + str, Color.Red, true, false);
               }
           });
            DialogsManager.HideDialog(this);
        }

        private enum DataType
        {
            OneKey,
            Copy,
            OldOneKey,
            OldCopy,
            SpecialCopy,
        }

        private class DerivedDialog : Dialog
        {
            private readonly ButtonWidget OK;
            private readonly ButtonWidget cancelButton;
            private readonly TextBoxWidget TextBox;
            private readonly ComponentPlayer player;
            private readonly Dialog dialog;
            private readonly ListPanelWidget listView;
            private readonly ModDialog.DataType dataType;

            public DerivedDialog(
              ComponentPlayer player,
              Dialog dialog,
              ListPanelWidget listView,
              ModDialog.DataType dataType = ModDialog.DataType.Copy)
            {
                this.dataType = dataType;
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

                string modFileDirectory = CreatorMain.Export_ModFile_Directory;
                string str1 = TextBox.Text.Length > 0 ? TextBox.Text : DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString();/*DateTime.Now.ToLongTimeString().ToString()*/
                try
                {
                    if (!Directory.Exists(modFileDirectory))
                    {
                        Directory.CreateDirectory(modFileDirectory);
                    }

                    string str2 = modFileDirectory + "/" + str1 + ".wMod2";
                    if (dataType == ModDialog.DataType.Copy)
                    {
                        CopyAndPaste.ExportCopywMod2(CreatorMain.CopyFile, str2);
                    }
                    else if (dataType == ModDialog.DataType.OneKey)
                    {
                        str2 = modFileDirectory + "/" + str1 + ".oMod2";
                        OnekeyGeneration.ExportOnekeyoMod2(CreatorMain.OneKeyFile, str2);
                    }
                    else if (dataType == ModDialog.DataType.OldCopy)
                    {
                        str2 = modFileDirectory + "/" + str1 + ".wMod";
                        CopyAndPaste.ExportCopywMod(CreatorMain.CopyFile, str2);
                    }
                    else if (dataType == ModDialog.DataType.OldOneKey)
                    {
                        str2 = modFileDirectory + "/" + str1 + ".oMod";
                        OnekeyGeneration.ExportOnekeyoMod(CreatorMain.OneKeyFile, str2);
                    }
                    else if (dataType == ModDialog.DataType.SpecialCopy)
                    {
                        str2 = modFileDirectory + "/" + str1 + ".sMod";
                        FileStream fileStream1 = new FileStream(CreatorMain.SpecialCopyFile, FileMode.Open);
                        if (!Directory.Exists(modFileDirectory))
                        {
                            Directory.CreateDirectory(modFileDirectory);
                        }

                        FileStream fileStream2 = new FileStream(str2, FileMode.OpenOrCreate);
                        fileStream1.CopyTo(fileStream2);
                        fileStream2.Dispose();
                        fileStream1.Dispose();
                    }
                    player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + str2, Color.LightYellow, true, false);
                    DialogsManager.HideDialog(this);
                    listView.ClearItems();
                    if (!Directory.Exists(CreatorMain.Export_ModFile_Directory))
                    {
                        Directory.CreateDirectory(CreatorMain.Export_ModFile_Directory);
                    }

                    foreach (string file in Directory.GetFiles(CreatorMain.Export_ModFile_Directory))
                    {
                        if (Path.GetExtension(file) == ".oMod" || Path.GetExtension(file) == ".wMod" || (Path.GetExtension(file) == ".oMod2" || Path.GetExtension(file) == ".wMod2") || Path.GetExtension(file) == ".sMod")
                        {
                            listView.AddItem(Path.GetFileName(file));
                        }
                    }
                }
                catch (Exception ex)
                {
                    player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + modFileDirectory, Color.Red, true, false);
                    DialogsManager.HideDialog(this);
                    DialogsManager.HideDialog(dialog);
                }
                DialogsManager.HideDialog(this);
            }
        }
    }
}
