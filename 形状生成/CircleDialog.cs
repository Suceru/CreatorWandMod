// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.CircleDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*圆环*/
/*namespace CreatorModAPI-=  public class CircleDialog : PillarsDialog*/
using Engine;
using Game;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public class CircleDialog : PillarsDialog
    {
        public CircleDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {
            Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("cirdialogn");
            Y_Shaft.Text = CreatorMain.Display_Key_Dialog("pilldialogx2");


        }

        public override void upClickButton(int id)
        {
            if (SoildButton.IsClicked)
            {
                Task.Run(() =>
               {
                   int num = 0;
                   ChunkData chunkData = new ChunkData(creatorAPI);
                   creatorAPI.revokeData = new ChunkData(creatorAPI);
                   foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Circle(creatorAPI.Position[0], (int)Height.Value, (int)Radius.Value, createType))
                   {
                       creatorAPI.CreateBlock(point3, id, chunkData);
                       ++num;
                       if (!creatorAPI.launch)
                       {
                           return;
                       }
                   }
                   chunkData.Render();
                   player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

               });
                DialogsManager.HideDialog(this);
            }
            if (!HollowButton.IsClicked)
            {
                return;
            }

            Task.Run(() =>
           {
               int num = 0;
               ChunkData chunkData = new ChunkData(creatorAPI);
               creatorAPI.revokeData = new ChunkData(creatorAPI);
               foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.Circle(creatorAPI.Position[0], (int)Height.Value, (int)Radius.Value, createType, true))
               {
                   creatorAPI.CreateBlock(point3, id, chunkData);
                   ++num;
                   if (!creatorAPI.launch)
                   {
                       return;
                   }
               }
               chunkData.Render();
               player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, true, true);

           });
            DialogsManager.HideDialog(this);
        }

        public override void upDataButton(CreatorMain.CreateType createType, ButtonWidget button)
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
            return;

        }
    }
}
