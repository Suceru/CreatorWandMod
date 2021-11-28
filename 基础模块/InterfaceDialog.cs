// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.InterfaceDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*方块选单界面*/
/*namespace CreatorModAPI-=  public class InterfaceDialog : Dialog*/
using Engine;
using Game;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class InterfaceDialog : Dialog
    {
        public CreatorAPI creatorAPI;
        public SubsystemTerrain subsystemTerrain;
        public ComponentPlayer player;
        public ButtonWidget cancelButton;
        public TextBoxWidget blockID;
        public BlockIconWidget blockIconWidget;
        public ButtonWidget SelectBlockButton;
        public CreatorMain.CreateType createType = CreatorMain.CreateType.Y;
        public ButtonWidget X_Shaft;
        public ButtonWidget Y_Shaft;
        public ButtonWidget Z_Shaft;

        public InterfaceDialog(CreatorAPI creatorAPI)
        {
            this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(true);
        }

        public override void Update()
        {
            try
            {
                blockIconWidget.Value = int.Parse(blockID.Text);
            }
            catch
            {
                blockIconWidget.Value = 0;
            }
            if (cancelButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }

            if (!SelectBlockButton.IsClicked)
            {
                return;
            }

            DialogsManager.ShowDialog(null, new ListSelectionDialog(CreatorMain.Display_Key_Dialog("intdialogs"), new int[22]
            {
        0,
        2,
        8,
        7,
        3,
        67,
        66,
        4,
        5,
        26,
        73,
        21,
        46,
        47,
        15,
        62,
        68,
        126,
        71,
        1,
        92,
        18
            }, 72f, index =>
     {
         ContainerWidget containerWidget = (ContainerWidget)Widget.LoadWidget(null, ContentManager.Get<XElement>("Widgets/SelectBlockItem"), null);
         containerWidget.Children.Find<BlockIconWidget>("SelectBlockItem.Block").Contents = (int)index;
         containerWidget.Children.Find<LabelWidget>("SelectBlockItem.Text").Text = BlocksManager.Blocks[(int)index].GetDisplayName(null, Terrain.MakeBlockValue((int)index));
         return containerWidget;
     }, index => blockID.Text = ((int)index).ToString()));
        }

        public void GeneralSet()
        {
            cancelButton = Children.Find<ButtonWidget>("Cancel");
            blockID = Children.Find<TextBoxWidget>("BlockID");
            blockIconWidget = Children.Find<BlockIconWidget>("Block");
            SelectBlockButton = Children.Find<ButtonWidget>("SelectBlock");
        }

        public void setShaftXYZ()
        {
            X_Shaft = Children.Find<ButtonWidget>("X-axis");
            Y_Shaft = Children.Find<ButtonWidget>("Y-axis");
            Z_Shaft = Children.Find<ButtonWidget>("Z-axis");
            Y_Shaft.Color = Color.Green;
        }
        //更新形状生成轴方向
        public virtual void upDataButton()
        {
            if (X_Shaft.IsClicked)
            {
                upDataButton(CreatorMain.CreateType.X, X_Shaft);
            }

            if (Y_Shaft.IsClicked)
            {
                upDataButton(CreatorMain.CreateType.Y, Y_Shaft);
            }

            if (Z_Shaft.IsClicked)
            {
                upDataButton(CreatorMain.CreateType.Z, Z_Shaft);
            }

            return;

        }
        //更新形状生成轴方向
        public virtual void upDataButton(CreatorMain.CreateType createType, ButtonWidget button)
        {
            if (this.createType != createType)
            {
                this.createType = createType;
                button.Color = Color.Green;
                if (X_Shaft != button)
                {
                    X_Shaft.Color = Color.White;
                }

                if (Y_Shaft != button)
                {
                    Y_Shaft.Color = Color.White;
                }

                if (Z_Shaft != button)
                {
                    Z_Shaft.Color = Color.White;
                }
            }

        }
        //获取形状轴类型名
        public string getTypeName(CreatorMain.CreateType typeName)
        {
            switch (typeName)
            {
                case CreatorMain.CreateType.X: return "X";
                case CreatorMain.CreateType.Y: return "Y";
                case CreatorMain.CreateType.Z: return "Z";
                default: return "X";
            }

        }
    }
}
