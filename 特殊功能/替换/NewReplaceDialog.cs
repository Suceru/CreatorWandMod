// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.ReplaceDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*方块替换*/
/*namespace CreatorModAPI-=  public class ReplaceDialog : Dialog*/
using Engine;
using Game;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class NewReplaceDialog : Dialog
    {
        private readonly ButtonWidget cancelButton;
        private readonly ButtonWidget replaceButton;
        private readonly TextBoxWidget Blockid;
        private readonly TextBoxWidget Blockid2;
        private readonly ComponentPlayer player;
        private readonly CreatorAPI creatorAPI;
        //  private readonly SubsystemTerrain subsystemTerrain = GameManager.Project.FindSubsystem<SubsystemTerrain>();

        public NewReplaceDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/Replace");

            LoadChildren(this, new XElement(node));
            Children.Find<LabelWidget>("Replace1").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Replace", "Replace1");
            Children.Find<LabelWidget>("Index1:").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Replace", "Index1:");
            Children.Find<LabelWidget>("Index2:").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Replace", "Index2:");
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            cancelButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Replace", "Cancel");
            replaceButton = Children.Find<ButtonWidget>("Replace");
            replaceButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Replace", "Replace");
            Blockid = Children.Find<TextBoxWidget>("BlockID");
            Blockid2 = Children.Find<TextBoxWidget>("BlockID2");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (replaceButton.IsClicked)
            {
                if (Blockid2.Text == "")
                {
                    Blockid2.Text = "0";
                }

                Regex regex1 = new Regex("^[0-9]*$");
                Match match3 = regex1.Match(Blockid.Text);
                Match match4 = regex1.Match(Blockid2.Text);
                if (match3.Success && match4.Success)
                {


                    Task.Run(() =>
                   {
                       int num = NewReplace.Replace(int.Parse(Blockid.Text), int.Parse(Blockid2.Text), creatorAPI.Position[0], creatorAPI.Position[1]);
                       player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("rpldialogrs"), num), Color.LightYellow, true, true);
                       // this.player.ComponentGui.DisplaySmallMessage(string.Format("操作成功，共替换{0}个方块", (object)num), Color.LightYellow, true, true);
                   });
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("rpldialogrf"), Color.Red, true, true);
                }
                //this.player.ComponentGui.DisplaySmallMessage("操作失败", Color.Red, true, true);
                DialogsManager.HideDialog(this);
            }
        }
    }
}
