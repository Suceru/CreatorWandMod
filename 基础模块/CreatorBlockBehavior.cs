/*方块选择，专业版*/
/*namespace CreatorModAPI-=  public class CreatorBlockBehavior : SubsystemBlockBehavior, IDrawable*/
using Engine;
using Engine.Graphics;
using Game;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CreatorModAPI
{
    public class CreatorBlockBehavior : SubsystemBlockBehavior, IDrawable
    {
        public Dictionary<ComponentPlayer, CreatorAPI> dictionaryPlayers = new Dictionary<ComponentPlayer, CreatorAPI>();
        private readonly List<ComponentPlayer> listPlayer = new List<ComponentPlayer>();

        public override int[] HandledBlocks => new int[1] { 195 };
        public int[] DrawOrders => new int[1] { 800 };

        public override bool OnUse(Ray3 ray, ComponentMiner componentMiner)
        {
            TerrainRaycastResult? nullable = componentMiner.Raycast<TerrainRaycastResult>(ray, RaycastMode.Digging);
            if (!nullable.HasValue)
            {
                return false;
            }

            ComponentPlayer componentPlayer = componentMiner.ComponentPlayer;
            if (!dictionaryPlayers.TryGetValue(componentPlayer, out CreatorAPI creatorApi))
            {
                listPlayer.Add(componentPlayer);
                creatorApi = new CreatorAPI(componentMiner);
                dictionaryPlayers.Add(componentPlayer, creatorApi);
            }
            creatorApi.OnUse(nullable.Value);
            return true;
        }

        //创世神界面
        public override bool OnEditInventoryItem(IInventory inventory, int slotIndex, ComponentPlayer Player)
        {
            if (Player.ComponentGui.ModalPanelWidget is CreatorWidget)
            {
                Player.ComponentGui.ModalPanelWidget = null;
                DialogsManager.HideAllDialogs();
            }
            else
            {
                if (!dictionaryPlayers.TryGetValue(Player, out CreatorAPI creatorAPI))
                {
                    listPlayer.Add(Player);
                    creatorAPI = new CreatorAPI(Player.ComponentMiner);
                    dictionaryPlayers.Add(Player, creatorAPI);

                }


                CreatorAPI.IsAddedToProject = true;

                if (CreatorMain.canUse)
                {
                    Player.ComponentGui.ModalPanelWidget = new CreatorWidget(creatorAPI);
                    /*if (CreatorAPI.Language != Language.ot_OT)
                    {
                        
                    }*/
                }
                else
                {
                    DialogsManager.ShowDialog(Player.ViewWidget.GameWidget, new CreatorBlockBehavior.PasswordDialog(Player));
                }
            }
            return true;
        }

        public override bool OnHitAsProjectile(CellFace? cellFace, ComponentBody componentBody, WorldItem worldItem)
        {
            ((worldItem is Projectile projectile ? projectile.Owner : null) as ComponentPlayer).ComponentBody.Position = worldItem.Position;
            return true;
        }

        public void Draw(Camera camera, int drawOrder)
        {
            foreach (ComponentPlayer key in listPlayer)
            {
                if (key.ComponentHealth.Health <= 0.0)
                {
                    listPlayer.Remove(key);
                    break;
                }

                _ = Vector3.TransformNormal(0.03f * Vector3.Normalize(Vector3.Cross(camera.ViewDirection, camera.ViewUp)), camera.ViewMatrix);
                _ = Vector3.TransformNormal(-0.03f * Vector3.UnitY, camera.ViewMatrix);
                // BitmapFont font = ContentManager.Get<BitmapFont>("Fonts/Pericles");/*"Fonts/Pericles32"*/
                if (dictionaryPlayers.TryGetValue(key, out CreatorAPI creatorApi))
                {
                    if (creatorApi.Position[0].Y != -1 && creatorApi.Position[1].Y != -1)
                    {
                        Point3 Start = creatorApi.Position[0];
                        Point3 End = creatorApi.Position[1];
                        CreatorMain.Math.StarttoEnd(ref Start, ref End);
                        //框选坐标 BoundingBox boundingBox = new BoundingBox(new Vector3((float)End.X, (float)End.Y, (float)End.Z), new Vector3((float)(Start.X + 1), (float)(Start.Y + 1), (float)(Start.Z + 1)));
                        BoundingBox boundingBox = new BoundingBox(new Vector3(Start.X, Start.Y, Start.Z), new Vector3((float)End.X + 1, (float)End.Y + 1, (float)End.Z + 1));
                        creatorApi.primitivesRenderer3D = new PrimitivesRenderer3D();
                        creatorApi.primitivesRenderer3D.FlatBatch(-1, DepthStencilState.None).QueueBoundingBox(boundingBox, Color.Blue);
                        /* creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("1", new Vector3(creatorApi.Position[0]) + new Vector3(0.0f, 0.5f, 0.0f), right, down, Color.Blue);
                         creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("2", new Vector3(creatorApi.Position[1]) + new Vector3(0.0f, 0.5f, 0.0f), right, down, Color.Blue);
                         creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("x", new Vector3((float) (Start.X + 1), (float) (End.Y + 1), (float) (End.Z + 1)), right, down, Color.Blue);
                         creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("z", new Vector3((float) End.X, (float) (End.Y + 1), (float) (Start.Z + 2)), right, down, Color.Blue);
                        */
                        creatorApi.primitivesRenderer3D.Flush(camera.ViewProjectionMatrix);
                    }
                    if (creatorApi.Position[2].Y != -1 && creatorApi.Position[3].Y != -1)
                    {
                        Point3 Start = creatorApi.Position[2];
                        Point3 End = creatorApi.Position[3];
                        CreatorMain.Math.StarttoEnd(ref Start, ref End);
                        BoundingBox boundingBox = new BoundingBox(new Vector3(Start.X, Start.Y, Start.Z), new Vector3((float)End.X + 1, (float)End.Y + 1, (float)End.Z + 1));
                        creatorApi.primitivesRenderer3D = new PrimitivesRenderer3D();
                        creatorApi.primitivesRenderer3D.FlatBatch(-1, DepthStencilState.None).QueueBoundingBox(boundingBox, Color.Red);
                        /*  creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("3", new Vector3(creatorApi.Position[2]) + new Vector3(0.0f, 0.5f, 0.0f), right, down, Color.Red);
                          creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("4", new Vector3(creatorApi.Position[3]) + new Vector3(0.0f, 0.5f, 0.0f), right, down, Color.Red);
                          creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("x", new Vector3((float) (Start.X + 1), (float) (End.Y + 1), (float) (End.Z + 1)), right, down, Color.Red);
                          creatorApi.primitivesRenderer3D.FontBatch(font, -1, DepthStencilState.None).QueueText("z", new Vector3((float) End.X, (float) (End.Y + 1), (float) (Start.Z + 2)), right, down, Color.Red);
                         */
                        creatorApi.primitivesRenderer3D.Flush(camera.ViewProjectionMatrix);
                    }
                }
            }
        }

        private class PasswordDialog : Dialog
        {
            private readonly ButtonWidget OK;
            private readonly ButtonWidget cancelButton;
            private readonly TextBoxWidget TextBox;
            private readonly ComponentPlayer player;

            public PasswordDialog(ComponentPlayer player)
            {
                this.player = player;

                XElement node = ContentManager.Get<XElement>("Dialog/Manager3");

                LoadChildren(this, node);
                Children.Find<LabelWidget>("Name").Text = "请输入密匙";
                cancelButton = Children.Find<ButtonWidget>("Cancel");
                OK = Children.Find<ButtonWidget>("OK");
                TextBox = Children.Find<TextBoxWidget>("BlockID");
                TextBox.Title = "请输入密匙";
                TextBox.Text = "";
                Children.Find<BlockIconWidget>("Block").IsVisible = false;
                Children.Find<ButtonWidget>("SelectBlock").IsVisible = false;
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

                if (TextBox.Text == CreatorMain.password)
                {
                    CreatorMain.canUse = true;
                    player.ComponentGui.DisplaySmallMessage("创世神" + CreatorMain.version + "功能开启", Color.Yellow, true, false);
                }
                else
                {
                    player.ComponentGui.DisplaySmallMessage("创世神" + CreatorMain.version + "功能开启失败", Color.Red, true, false);
                }

                DialogsManager.HideDialog(this);
            }
        }
    }
}
