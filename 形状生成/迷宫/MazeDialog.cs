﻿using Engine;
using Game;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreatorWandModAPI
{
    public class MazeDialog : InterfaceDialog
    {
        private readonly ButtonWidget OKButton;

        public MazeDialog(CreatorAPI creatorAPI)
            : base(creatorAPI)
        {
            base.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            subsystemTerrain = player.Project.FindSubsystem<SubsystemTerrain>(throwOnError: true);
            XElement node = ContentManager.Get<XElement>("Dialog/Manager3");
            LoadChildren(this, node);
            GeneralSet();
            (Children.Find<LabelWidget>("Name")).Text = (CreatorMain.Display_Key_Dialog("mazedialog1"));
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "OK");
            Children.Find<BevelledButtonWidget>("Cancel").Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "Manager3", "Cancel");
            LoadProperties(this, node);
        }

        public override void Update()
        {
            base.Update();
            if (!OKButton.IsClicked)
            {
                return;
            }

            Task.Run(delegate
            {
                int num = 0;
                ChunkData chunkData = new ChunkData(creatorAPI);
                creatorAPI.revokeData = new ChunkData(creatorAPI);
                foreach (Point3 item in creatorAPI.creatorGenerationAlgorithm.Maze(creatorAPI.Position[0], creatorAPI.Position[1]))
                {
                    creatorAPI.CreateBlock(item, blockIconWidget.Value, chunkData);
                    num++;
                    if (!creatorAPI.launch)
                    {
                        return;
                    }
                }

                chunkData.Render();
                player.ComponentGui.DisplaySmallMessage(string.Format(CreatorMain.Display_Key_Dialog("filldialog1"), num), Color.LightYellow, blinking: true, playNotificationSound: true);
            });
            DialogsManager.HideDialog(this);
        }
    }
}
