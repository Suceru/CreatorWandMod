using Engine;
using Game;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class CopyPasteDialog : Dialog
    {
        private class DerivedDialog : Dialog
        {
            private readonly ButtonWidget OK;

            private readonly ButtonWidget cancelButton;

            private readonly Game.TextBoxWidget TextBox;

            private readonly ComponentPlayer player;

            private readonly Dialog dialog;

            private readonly ListPanelWidget listView;

            public DerivedDialog(ComponentPlayer player, Dialog dialog, ListPanelWidget listView)
            {
                this.player = player;
                this.dialog = dialog;
                this.listView = listView;
                XElement node = ContentManager.Get<XElement>("Dialog/Manager3", (string)null);
                LoadChildren(this, node);
                ((FontTextWidget)Children.Find<LabelWidget>("Name")).Text=(CreatorMain.Display_Key_Dialog("moddialogname"));
                cancelButton = Children.Find<ButtonWidget>("Cancel");
                cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "Cancel");
                OK = Children.Find<ButtonWidget>("OK");
                OK.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "OK");
                TextBox = Children.Find<Game.TextBoxWidget>("BlockID");
                TextBox.Text = "";
                Children.Find<BlockIconWidget>("Block").IsVisible = false;
                Children.Find<BevelledButtonWidget>("SelectBlock").IsVisible = false;
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

                string export_CopyFile_Directory = CreatorMain.Export_CopyFile_Directory;
                string text = (TextBox.Text.Length > 0) ? TextBox.Text : (DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now);
                if (!CreatorMain.CopyFile.IsFileInUse())
                {
                    try
                    {
                        FileStream fileStream = new FileStream(CreatorMain.CopyFile, FileMode.Open);
                        if (!Directory.Exists(export_CopyFile_Directory))
                        {
                            Directory.CreateDirectory(export_CopyFile_Directory);
                        }

                        FileStream fileStream2 = new FileStream(export_CopyFile_Directory + "/" + text + ".w", FileMode.OpenOrCreate);
                        fileStream.CopyTo(fileStream2);
                        fileStream2.Dispose();
                        fileStream.Dispose();
                        switch (CreatorAPI.Language)
                        {
                            case Language.zh_CN:
                                player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + export_CopyFile_Directory + "/" + text + ".w", Color.LightYellow, blinking: true, playNotificationSound: false);
                                break;
                            case Language.en_US:
                                player.ComponentGui.DisplaySmallMessage("Export successful! File location: \n" + export_CopyFile_Directory + "/" + text + ".w", Color.LightYellow, blinking: true, playNotificationSound: false);
                                break;
                            default:
                                player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + export_CopyFile_Directory + "/" + text + ".w", Color.LightYellow, blinking: true, playNotificationSound: false);
                                break;
                        }

                        DialogsManager.HideDialog(this);
                        listView.ClearItems();
                        if (!Directory.Exists(CreatorMain.Export_CopyFile_Directory))
                        {
                            Directory.CreateDirectory(CreatorMain.Export_CopyFile_Directory);
                        }

                        string[] files = Directory.GetFiles(CreatorMain.Export_CopyFile_Directory);
                        foreach (string path in files)
                        {
                            if (Path.GetExtension(path) == ".w")
                            {
                                listView.AddItem(Path.GetFileName(path));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        switch (CreatorAPI.Language)
                        {
                            case Language.zh_CN:
                                player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + export_CopyFile_Directory, Color.Red, blinking: true, playNotificationSound: false);
                                break;
                            case Language.en_US:
                                player.ComponentGui.DisplaySmallMessage("A very serious error has occurred, \n error message: " + ex.Message + "\n" + export_CopyFile_Directory, Color.Red, blinking: true, playNotificationSound: false);
                                break;
                            default:
                                player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + export_CopyFile_Directory, Color.Red, blinking: true, playNotificationSound: false);
                                break;
                        }

                        DialogsManager.HideDialog(this);
                        DialogsManager.HideDialog(dialog);
                    }
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }
        }

        private readonly ButtonWidget cancelButton;

        private readonly ButtonWidget PasteButton;

        private readonly ButtonWidget CopyButton;

        private readonly ButtonWidget DerivedButton;

        private readonly ButtonWidget ImportButton;

        private readonly ButtonWidget MirrorButton;

        private readonly ComponentPlayer player;

        private readonly ListPanelWidget ListView;

        private readonly CreatorAPI creatorAPI;

        private readonly ButtonWidget DeleteButton;

        public CopyPasteDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/CopyandPaste", (string)null);
            LoadChildren(this, node);
            ((FontTextWidget)Children.Find<LabelWidget>("Copy and Paste")).Text=(CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Copy and Paste"));
            CopyButton = Children.Find<ButtonWidget>("Copy");
            CopyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Copy");
            DerivedButton = Children.Find<ButtonWidget>("Export");
            DerivedButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Export");
            ImportButton = Children.Find<ButtonWidget>("Import");
            ImportButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Import");
            DeleteButton = Children.Find<ButtonWidget>("Delete");
            DeleteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Delete");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Cancel");
            PasteButton = Children.Find<ButtonWidget>("Paste");
            PasteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Paste");
            MirrorButton = Children.Find<ButtonWidget>("Mirror");
            MirrorButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Mirror");
            MirrorBlockBehavior.IsMirror = false;
            ListView = Children.Find<ListPanelWidget>("ListView");
            UpList();
            LoadProperties(this, node);
        }

        private void UpList()
        {
            ListView.ClearItems();
            if (!Directory.Exists(CreatorMain.Export_CopyFile_Directory))
            {
                Directory.CreateDirectory(CreatorMain.Export_CopyFile_Directory);
            }

            string[] files = Directory.GetFiles(CreatorMain.Export_CopyFile_Directory);
            foreach (string path in files)
            {
                if (Path.GetExtension(path) == ".w")
                {
                    ListView.AddItem(Path.GetFileName(path));
                }
            }
        }

        public override void Update()
        {
            MirrorButton.Color = (MirrorBlockBehavior.IsMirror ? Color.Yellow : Color.Gray);
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (MirrorButton.IsClicked)
            {
                MirrorBlockBehavior.ChangeIsMirror();
            }

            if (PasteButton.IsClicked)
            {
                if (!CreatorMain.CopyFile.IsFileInUse())
                {
                    Task.Run(delegate
                    {
                        try
                        {
                            CopyAndPaste.PasetData(creatorAPI, CreatorMain.CopyFile, creatorAPI.Position[0], creatorAPI.Position[1]);
                        }
                        catch (Exception ex2)
                        {
                            DialogsManager.ShowDialog(player.GuiWidget, new MessageDialog("Exception", ex2.Message, "OK", null, null));
                        }
                    });
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }

            if (ImportButton.IsClicked)
            {
                if (!Directory.Exists(CreatorMain.CacheDirectory))
                {
                    Directory.CreateDirectory(CreatorMain.CacheDirectory);
                }

                string copyFile = CreatorMain.CopyFile;
                string text = CreatorMain.Export_CopyFile_Directory + "/" + (string)ListView.SelectedItem;
                if (!text.IsFileInUse() && (!File.Exists(copyFile) || !copyFile.IsFileInUse()))
                {
                    FileStream fileStream = new FileStream(text, FileMode.Open);
                    FileStream fileStream2 = new FileStream(copyFile, FileMode.Create);
                    fileStream.CopyTo(fileStream2);
                    fileStream2.Dispose();
                    fileStream.Dispose();
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Imported successfully!", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                    }
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }

            if (DeleteButton.IsClicked)
            {
                if ((CreatorMain.Export_CopyFile_Directory + "/" + (string)ListView.SelectedItem).Delete())
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Deleted successfully!", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, blinking: true, playNotificationSound: false);
                            break;
                    }

                    UpList();
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, blinking: true, playNotificationSound: false);
                            break;
                    }
                }
            }

            DerivedButton.IsEnabled = File.Exists(CreatorMain.CopyFile);
            if (DerivedButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new DerivedDialog(player, this, ListView));
            }

            if (CopyButton.IsClicked)
            {
                Task.Run(delegate
                {
                    try
                    {
                        if (!Directory.Exists(CreatorMain.CacheDirectory))
                        {
                            Directory.CreateDirectory(CreatorMain.CacheDirectory);
                        }

                        CopyAndPaste.CreateCopy(creatorAPI, CreatorMain.CacheDirectory, "CacheFile.cd", creatorAPI.Position[0], creatorAPI.Position[1]);
                    }
                    catch (Exception ex)
                    {
                        DialogsManager.ShowDialog(player.GuiWidget, new MessageDialog("Exception", ex.Message, "OK", null, null));
                    }
                });
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
        }
    }
}

/*复制粘贴*/
/*namespace CreatorModAPI-=  public class CopyPasteDialog : Dialog*//*
using Engine;
using Game;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class CopyPasteDialog : Dialog
    {
        private readonly ButtonWidget cancelButton;
        private readonly ButtonWidget PasteButton;
        private readonly ButtonWidget CopyButton;
        private readonly ButtonWidget DerivedButton;
        private readonly ButtonWidget ImportButton;
        private readonly ButtonWidget MirrorButton;
        private readonly ComponentPlayer player;
        private readonly ListPanelWidget ListView;
        private readonly CreatorAPI creatorAPI;
        private readonly ButtonWidget DeleteButton;
        //    private bool IsMirror=false;

        // public bool GetMirror() { return this.IsMirror; }

        public CopyPasteDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/CopyandPaste");

            LoadChildren(this, node);
            Children.Find<LabelWidget>("Copy and Paste").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Copy and Paste");
            CopyButton = Children.Find<ButtonWidget>("Copy");
            CopyButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Copy");
            DerivedButton = Children.Find<ButtonWidget>("Export");
            DerivedButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Export");
            ImportButton = Children.Find<ButtonWidget>("Import");
            ImportButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Import");
            DeleteButton = Children.Find<ButtonWidget>("Delete");
            DeleteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Delete");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Cancel");
            PasteButton = Children.Find<ButtonWidget>("Paste");
            PasteButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Paste");
            MirrorButton = Children.Find<ButtonWidget>("Mirror");
            MirrorButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "CopyandPaste", "Mirror");
            MirrorBlockBehavior.IsMirror = false;
            ListView = Children.Find<ListPanelWidget>(nameof(ListView));
            UpList();
            LoadProperties(this, node);
        }

        private void UpList()
        {
            ListView.ClearItems();
            //如果有文件导出，跳过，没有就创建指定目录
            if (!Directory.Exists(CreatorMain.Export_CopyFile_Directory))
            {
                Directory.CreateDirectory(CreatorMain.Export_CopyFile_Directory);
            }
            //每次从这个目录里面读出文件名字符串
            foreach (string file in Directory.GetFiles(CreatorMain.Export_CopyFile_Directory))
            {
                //如果拓展名为.W，将项目名加入展示栏
                if (Path.GetExtension(file) == ".w")
                {
                    ListView.AddItem(Path.GetFileName(file));
                }
            }
        }

        public override void Update()
        {

            MirrorButton.Color = MirrorBlockBehavior.IsMirror ? Color.Yellow : Color.Gray;
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (MirrorButton.IsClicked)
            {
                MirrorBlockBehavior.ChangeIsMirror();

            }
            //粘贴
            if (PasteButton.IsClicked)
            {
                //如果.cd的缓存文件没有使用，异步执行，粘贴。从.cd里面读出
                if (!CreatorMain.CopyFile.IsFileInUse())
                {
                    Task.Run(() =>
                   {
                       try
                       {
                           CopyAndPaste.PasetData(creatorAPI, CreatorMain.CopyFile, creatorAPI.Position[0], creatorAPI.Position[1]);
                       }
                       catch (Exception ex)
                       {
                           DialogsManager.ShowDialog(player.GuiWidget, new MessageDialog("Exception", ex.Message, "OK", null, null));
                           //  this.player.ComponentGui.DisplaySmallMessage(ex.Message, Color.Red, true, false);
                       }
                   });
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }
            //导入
            if (ImportButton.IsClicked)
            {
                //如果Cache目录不存在就创建一个Cache目录
                if (!Directory.Exists(CreatorMain.CacheDirectory))
                {
                    Directory.CreateDirectory(CreatorMain.CacheDirectory);
                }
                //将.cd文件的路径给cF
                string copyFile = CreatorMain.CopyFile;
                //导出目录加上选择的项目名作为str
                string str = CreatorMain.Export_CopyFile_Directory + "/" + (string)ListView.SelectedItem;
                //如果文件没有使用，cF文件不存在并上文件没有使用
                if (!str.IsFileInUse() && (!File.Exists(copyFile) || !copyFile.IsFileInUse()))
                {
                    //将外面的文件str，使用文件打开给流1，将cF文件创建给流2
                    FileStream fileStream1 = new FileStream(str, FileMode.Open);
                    FileStream fileStream2 = new FileStream(copyFile, FileMode.Create);
                    //将外面的文件复制给cF
                    fileStream1.CopyTo(fileStream2);
                    //销毁两个文件
                    fileStream2.Dispose();
                    fileStream1.Dispose();

                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Imported successfully!", Color.LightYellow, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("导入成功！", Color.LightYellow, true, false);
                            break;
                    }


                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }
            if (DeleteButton.IsClicked)
            {
                if ((CreatorMain.Export_CopyFile_Directory + "/" + (string)ListView.SelectedItem).Delete())
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Deleted successfully!", Color.LightYellow, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("删除成功！", Color.LightYellow, true, false);
                            break;
                    }

                    //删除后刷新列表
                    UpList();
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                    }
                }

            }
            //导出按钮能够使用的条件是存在cF文件
            DerivedButton.IsEnabled = File.Exists(CreatorMain.CopyFile);
            //导出按钮点击后，打开导出界面，在代码的下方
            if (DerivedButton.IsClicked)
            {
                DialogsManager.ShowDialog(player.ViewWidget.GameWidget, new CopyPasteDialog.DerivedDialog(player, this, ListView));
            }
            //如果复制按钮被点击
            if (CopyButton.IsClicked)
            {
                //异步执行
                Task.Run(() =>
               {
                   try
                   {
                       //如果Cache目录不存在创建Cache目录
                       if (!Directory.Exists(CreatorMain.CacheDirectory))
                       {
                           Directory.CreateDirectory(CreatorMain.CacheDirectory);
                       }
                       //调用创建复制，传入.cd。
                       CopyAndPaste.CreateCopy(creatorAPI, CreatorMain.CacheDirectory, "CacheFile.cd", creatorAPI.Position[0], creatorAPI.Position[1]);
                   }
                   catch (Exception ex)
                   {
                       DialogsManager.ShowDialog(player.GuiWidget, new MessageDialog("Exception", ex.Message, "OK", null, null));
                       //    this.player.ComponentGui.DisplaySmallMessage(ex.Message, Color.Red, true, false);
                   }
               });
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


            //  DialogsManager.HideDialog((Dialog)this);
        }

        //导出界面
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
                Children.Find<BevelledButtonWidget>("SelectBlock").IsVisible = false;
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

                string copyFileDirectory = CreatorMain.Export_CopyFile_Directory;
                string str = TextBox.Text.Length > 0 ? TextBox.Text : DateTime.Now.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString();*//*DateTime.Now.ToLongTimeString().ToString()*//*
                if (!CreatorMain.CopyFile.IsFileInUse())
                {
                    try
                    {
                        FileStream fileStream1 = new FileStream(CreatorMain.CopyFile, FileMode.Open);
                        if (!Directory.Exists(copyFileDirectory))
                        {
                            Directory.CreateDirectory(copyFileDirectory);
                        }

                        FileStream fileStream2 = new FileStream(copyFileDirectory + "/" + str + ".w", FileMode.OpenOrCreate);
                        fileStream1.CopyTo(fileStream2);
                        fileStream2.Dispose();
                        fileStream1.Dispose();
                        switch (CreatorAPI.Language)
                        {
                            case Language.zh_CN:
                                player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + copyFileDirectory + "/" + str + ".w", Color.LightYellow, true, false);
                                break;
                            case Language.en_US:
                                player.ComponentGui.DisplaySmallMessage("Export successful! File location: \n" + copyFileDirectory + "/" + str + ".w", Color.LightYellow, true, false);
                                break;
                            default:
                                player.ComponentGui.DisplaySmallMessage("导出成功！文件所在位置：\n" + copyFileDirectory + "/" + str + ".w", Color.LightYellow, true, false);
                                break;
                        }

                        DialogsManager.HideDialog(this);
                        listView.ClearItems();
                        if (!Directory.Exists(CreatorMain.Export_CopyFile_Directory))
                        {
                            Directory.CreateDirectory(CreatorMain.Export_CopyFile_Directory);
                        }

                        foreach (string file in Directory.GetFiles(CreatorMain.Export_CopyFile_Directory))
                        {
                            if (Path.GetExtension(file) == ".w")
                            {
                                listView.AddItem(Path.GetFileName(file));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        switch (CreatorAPI.Language)
                        {
                            case Language.zh_CN:
                                player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + copyFileDirectory, Color.Red, true, false);
                                break;
                            case Language.en_US:
                                player.ComponentGui.DisplaySmallMessage("A very serious error has occurred, \n error message: " + ex.Message + "\n" + copyFileDirectory, Color.Red, true, false);
                                break;
                            default:
                                player.ComponentGui.DisplaySmallMessage("发生了一个很严重的错误，\n 错误提示 :" + ex.Message + "\n" + copyFileDirectory, Color.Red, true, false);
                                break;
                        }

                        DialogsManager.HideDialog(this);
                        DialogsManager.HideDialog(dialog);
                    }
                }
                else
                {
                    switch (CreatorAPI.Language)
                    {
                        case Language.zh_CN:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                        case Language.en_US:
                            player.ComponentGui.DisplaySmallMessage("Operation failed!", Color.Red, true, false);
                            break;
                        default:
                            player.ComponentGui.DisplaySmallMessage("操作失败！", Color.Red, true, false);
                            break;
                    }
                }

                DialogsManager.HideDialog(this);
            }
        }
    }
}
*/