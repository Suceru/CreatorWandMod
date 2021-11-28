// Decompiled with JetBrains decompiler
// Type: CreatorModAPI.PrismColumnDialog
// Assembly: CreatorMod_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7D80CF5-3F89-46A6-B943-D040364C2CEC
// Assembly location: D:\Users\12464\Desktop\sc2\css\CreatorMod_Android.dll

/*棱柱生成*/
/*namespace CreatorModAPI-=  public class PrismColumnDialog : PillarsDialog*/
using Engine;
using Game;
using System.Threading.Tasks;

namespace CreatorModAPI
{
    public class PrismColumnDialog : PillarsDialog
    {
        public PrismColumnDialog(CreatorAPI creatorAPI)
          : base(creatorAPI)
        {

            Children.Find<LabelWidget>("Name").Text = CreatorMain.Display_Key_Dialog("pcddialog1");
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
                   foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.PrismColumn(creatorAPI.Position[0], (int)Radius.Value, (int)Height.Value, createType, typeBool))
                   {
                       creatorAPI.CreateBlock(point3, id, chunkData);
                       ++num;
                       if (!creatorAPI.launch)
                       {
                           return;
                       }
                   }
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
               foreach (Point3 point3 in creatorAPI.creatorGenerationAlgorithm.PrismColumn(creatorAPI.Position[0], (int)Radius.Value, (int)Height.Value, createType, typeBool, true))
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
    }
}
