/*编辑世界界面*/
/*namespace CreatorModAPI-=  public class EditWorldDialog : Dialog*/
using Engine;
using Engine.Graphics;
using Game;
using System.Collections.Generic;
using System.Xml.Linq;
using TemplatesDatabase;

namespace CreatorModAPI
{
    public class EditWorldDialog : Dialog
    {
        private readonly ComponentPlayer player;
        private readonly ButtonWidget paletteButton;
        private readonly RectangleWidget blocksTextureIcon;
        private readonly LabelWidget blocksTextureLabel;
        private readonly LabelWidget blocksTextureDetails;
        private readonly ButtonWidget blocksTextureButton;
        private readonly ButtonWidget supernaturalCreaturesButton;
        private readonly ButtonWidget environmentBehaviorButton;
        private readonly ButtonWidget timeOfDayButton;
        private readonly ButtonWidget weatherEffectsButton;
        private readonly ButtonWidget adventureRespawnButton;
        private readonly ButtonWidget adventureSurvivalMechanicsButton;
        private readonly LabelWidget terrainGenerationLabel;
        private readonly ButtonWidget terrainGenerationButton;
        private readonly SliderWidget seaLevelOffsetSlider;
        private readonly SliderWidget temperatureOffsetSlider;
        private readonly SliderWidget humidityOffsetSlider;
        private readonly SliderWidget biomeSizeSlider;
        private readonly Widget islandTerrainPanel;
        private readonly SliderWidget islandSizeNS;
        private readonly SliderWidget islandSizeEW;
        private readonly Widget flatTerrainPanel;
        private readonly Widget continentTerrainPanel;
        private readonly SliderWidget flatTerrainLevelSlider;
        private readonly BlockIconWidget flatTerrainBlock;
        // private readonly LabelWidget flatTerrainBlockLabel;
        private readonly ButtonWidget flatTerrainBlockButton;
        private readonly CheckboxWidget flatTerrainMagmaOceanCheckbox;
        private readonly ButtonWidget OKButton;
        private readonly ButtonWidget UpdataWorldButton;
        private readonly ButtonWidget UpdataButton;
        private readonly WorldSettings worldSettings;
        // private readonly CreatorAPI creatorAPI;
        private readonly float[] islandSizes = new float[20]
        {
      30f,
      40f,
      50f,
      60f,
      80f,
      100f,
      120f,
      150f,
      200f,
      250f,
      300f,
      400f,
      500f,
      600f,
      800f,
      1000f,
      1200f,
      1500f,
      2000f,
      2500f
        };
        private readonly float[] biomeSizes = new float[7]
        {
      0.33f,
      0.5f,
      0.75f,
      1f,
      1.5f,
      2f,
      3f
        };
        private readonly BlocksTexturesCache blockTexturesCache = new BlocksTexturesCache();

        public EditWorldDialog(CreatorAPI creatorAPI)
        {
            //   this.creatorAPI = creatorAPI;
            player = creatorAPI.componentMiner.ComponentPlayer;
            XElement node = ContentManager.Get<XElement>("Dialog/EditWorld");
            LoadChildren(this, node);
            worldSettings = player.Project.FindSubsystem<SubsystemGameInfo>(true).WorldSettings;
            OKButton = Children.Find<ButtonWidget>("OK");
            OKButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditWorld", "OK");
            UpdataWorldButton = Children.Find<ButtonWidget>("Reload");
            UpdataWorldButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditWorld", "Reload");
            UpdataButton = Children.Find<ButtonWidget>("Refresh");
            UpdataButton.Text = CreatorMain.Display_Key_UI(CreatorAPI.Language.ToString(), "EditWorld", "Refresh");
            paletteButton = Children.Find<ButtonWidget>("Palette");
            blocksTextureIcon = Children.Find<RectangleWidget>("BlocksTextureIcon");
            blocksTextureLabel = Children.Find<LabelWidget>("BlocksTextureLabel");
            blocksTextureDetails = Children.Find<LabelWidget>("BlocksTextureDetails");
            blocksTextureButton = Children.Find<ButtonWidget>("BlocksTextureButton");
            supernaturalCreaturesButton = Children.Find<ButtonWidget>("SupernaturalCreatures");
            environmentBehaviorButton = Children.Find<ButtonWidget>("EnvironmentBehavior");
            timeOfDayButton = Children.Find<ButtonWidget>("TimeOfDay");
            weatherEffectsButton = Children.Find<ButtonWidget>("WeatherEffects");
            adventureRespawnButton = Children.Find<ButtonWidget>("AdventureRespawn");
            adventureSurvivalMechanicsButton = Children.Find<ButtonWidget>("AdventureSurvivalMechanics");
            terrainGenerationLabel = Children.Find<LabelWidget>("TerrainGenerationLabel");
            terrainGenerationButton = Children.Find<ButtonWidget>("TerrainGeneration");
            seaLevelOffsetSlider = Children.Find<SliderWidget>("SeaLevelOffset");
            temperatureOffsetSlider = Children.Find<SliderWidget>("TemperatureOffset");
            humidityOffsetSlider = Children.Find<SliderWidget>("HumidityOffset");
            biomeSizeSlider = Children.Find<SliderWidget>("BiomeSize");
            islandTerrainPanel = Children.Find<Widget>("IslandTerrainPanel");
            islandSizeNS = Children.Find<SliderWidget>("IslandSizeNS");
            islandSizeEW = Children.Find<SliderWidget>("IslandSizeEW");
            flatTerrainPanel = Children.Find<Widget>("FlatTerrainPanel");
            continentTerrainPanel = Children.Find<Widget>("ContinentTerrainPanel");
            flatTerrainLevelSlider = Children.Find<SliderWidget>("FlatTerrainLevel");
            flatTerrainBlock = Children.Find<BlockIconWidget>("FlatTerrainBlock");
            //  flatTerrainBlockLabel = Children.Find<LabelWidget>("FlatTerrainBlockLabel");
            flatTerrainBlockButton = Children.Find<ButtonWidget>("FlatTerrainBlockButton");
            flatTerrainMagmaOceanCheckbox = Children.Find<CheckboxWidget>("MagmaOcean");
            islandSizeEW.MinValue = 0.0f;
            islandSizeEW.MaxValue = islandSizes.Length - 1;
            islandSizeEW.Granularity = 1f;
            islandSizeNS.MinValue = 0.0f;
            islandSizeNS.MaxValue = islandSizes.Length - 1;
            islandSizeNS.Granularity = 1f;
            biomeSizeSlider.MinValue = 0.0f;
            biomeSizeSlider.MaxValue = biomeSizes.Length - 1;
            biomeSizeSlider.Granularity = 1f;
            LoadProperties(this, node);
        }

        public override void Update()
        {
            if (UpdataButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
                SubsystemTerrain subsystem = GameManager.Project.FindSubsystem<SubsystemTerrain>();
                foreach (TerrainChunk allocatedChunk in subsystem.Terrain.AllocatedChunks)
                {
                    foreach (SubsystemBlockBehavior blockBehavior in GameManager.Project.FindSubsystem<SubsystemBlockBehaviors>().BlockBehaviors)
                    {
                        blockBehavior.OnChunkDiscarding(allocatedChunk);
                    }

                    Point2 coords1 = allocatedChunk.Coords;
                    Point2 coords2 = allocatedChunk.Coords;
                    subsystem.Dispose();
                    subsystem.Load(new ValuesDictionary());
                    player.ComponentGui.DisplaySmallMessage(CreatorMain.Display_Key_Dialog("editworlddialogs"), Color.LightYellow, true, false);
                }
            }
            if (OKButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
            }
            //点击重新进入地图
            if (UpdataWorldButton.IsClicked)
            {
                DialogsManager.HideDialog(this);
                WorldInfo worldInfo = GameManager.WorldInfo;
                GameManager.SaveProject(true, true);
                GameManager.DisposeProject();
                ScreensManager.SwitchScreen("GameLoading", worldInfo, null);
            }
            if (paletteButton.IsClicked)
            {
                DialogsManager.ShowDialog(null, new CreatorModAPIEditPaletteDialog(worldSettings.Palette));
            }

            Texture2D texture1 = blockTexturesCache.GetTexture(worldSettings.BlocksTextureName);
            blocksTextureIcon.Subtexture = new Subtexture(texture1, Vector2.Zero, Vector2.One);
            blocksTextureLabel.Text = BlocksTexturesManager.GetDisplayName(worldSettings.BlocksTextureName);
            blocksTextureDetails.Text = string.Format("{0}x{1}", texture1.Width, texture1.Height);
            if (blocksTextureButton.IsClicked)
            {
                BlocksTexturesManager.UpdateBlocksTexturesList();
                DialogsManager.ShowDialog(null, new ListSelectionDialog("Select Blocks Texture", BlocksTexturesManager.BlockTexturesNames, 64f, item =>
            {
                ContainerWidget containerWidget = (ContainerWidget)Widget.LoadWidget(this, ContentManager.Get<XElement>("Widgets/BlocksTextureItem"), null);
                Texture2D texture2 = blockTexturesCache.GetTexture((string)item);
                containerWidget.Children.Find<LabelWidget>("BlocksTextureItem.Text").Text = BlocksTexturesManager.GetDisplayName((string)item);
                containerWidget.Children.Find<LabelWidget>("BlocksTextureItem.Details").Text = string.Format("{0}x{1}", texture2.Width, texture2.Height);
                containerWidget.Children.Find<RectangleWidget>("BlocksTextureItem.Icon").Subtexture = new Subtexture(texture2, Vector2.Zero, Vector2.One);
                return containerWidget;
            }, item =>
       {
           worldSettings.BlocksTextureName = (string)item;
           SubsystemBlocksTexture subsystem = GameManager.Project.FindSubsystem<SubsystemBlocksTexture>();
           subsystem.Dispose();
           subsystem.Load(new ValuesDictionary());
       }));
            }
            if (supernaturalCreaturesButton.IsClicked)
            {
                worldSettings.AreSupernaturalCreaturesEnabled = !worldSettings.AreSupernaturalCreaturesEnabled;
            }

            if (environmentBehaviorButton.IsClicked)
            {
                IList<int> enumValues = EnumUtils.GetEnumValues(typeof(EnvironmentBehaviorMode));
                worldSettings.EnvironmentBehaviorMode = (EnvironmentBehaviorMode)((enumValues.IndexOf((int)worldSettings.EnvironmentBehaviorMode) + 1) % enumValues.Count);
            }
            if (timeOfDayButton.IsClicked)
            {
                IList<int> enumValues = EnumUtils.GetEnumValues(typeof(TimeOfDayMode));
                worldSettings.TimeOfDayMode = (TimeOfDayMode)((enumValues.IndexOf((int)worldSettings.TimeOfDayMode) + 1) % enumValues.Count);
            }
            if (weatherEffectsButton.IsClicked)
            {
                worldSettings.AreWeatherEffectsEnabled = !worldSettings.AreWeatherEffectsEnabled;
            }

            if (adventureRespawnButton.IsClicked)
            {
                worldSettings.IsAdventureRespawnAllowed = !worldSettings.IsAdventureRespawnAllowed;
            }

            if (adventureSurvivalMechanicsButton.IsClicked)
            {
                worldSettings.AreAdventureSurvivalMechanicsEnabled = !worldSettings.AreAdventureSurvivalMechanicsEnabled;
            }

            if (terrainGenerationButton.IsClicked)
            {
                DialogsManager.ShowDialog(null, new ListSelectionDialog("Select World Type", EnumUtils.GetEnumValues(typeof(TerrainGenerationMode)), 56f, e => ((TerrainGenerationMode)e).ToString(), e =>
         {
             if (worldSettings.GameMode != GameMode.Creative && ((TerrainGenerationMode)e == TerrainGenerationMode.FlatContinent || (TerrainGenerationMode)e == TerrainGenerationMode.FlatIsland))
             {
                 DialogsManager.ShowDialog(null, new MessageDialog("Unavailable", "Flat terrain is only available in Creative Mode", "OK", null, null));
             }
             else
             {
                 worldSettings.TerrainGenerationMode = (TerrainGenerationMode)e;
                 SubsystemTerrain subsystem = GameManager.Project.FindSubsystem<SubsystemTerrain>();
                 if ((TerrainGenerationMode)e == TerrainGenerationMode.FlatContinent || (TerrainGenerationMode)e == TerrainGenerationMode.FlatIsland)
                 {
                     subsystem.TerrainContentsGenerator = new TerrainContentsGeneratorFlat(subsystem);
                 }
                 else
                 {
                     subsystem.TerrainContentsGenerator = new TerrainContentsGenerator22(subsystem);
                 }
             }
         }));
            }

            if (seaLevelOffsetSlider.IsSliding)
            {
                worldSettings.SeaLevelOffset = (int)seaLevelOffsetSlider.Value;
            }

            if (temperatureOffsetSlider.IsSliding)
            {
                worldSettings.TemperatureOffset = temperatureOffsetSlider.Value;
            }

            if (humidityOffsetSlider.IsSliding)
            {
                worldSettings.HumidityOffset = humidityOffsetSlider.Value;
            }

            if (biomeSizeSlider.IsSliding)
            {
                worldSettings.BiomeSize = biomeSizes[MathUtils.Clamp((int)biomeSizeSlider.Value, 0, biomeSizes.Length - 1)];
            }

            if (islandSizeEW.IsSliding)
            {
                worldSettings.IslandSize.X = islandSizes[MathUtils.Clamp((int)islandSizeEW.Value, 0, islandSizes.Length - 1)];
            }

            if (islandSizeNS.IsSliding)
            {
                worldSettings.IslandSize.Y = islandSizes[MathUtils.Clamp((int)islandSizeNS.Value, 0, islandSizes.Length - 1)];
            }

            if (flatTerrainLevelSlider.IsSliding)
            {
                worldSettings.TerrainLevel = (int)flatTerrainLevelSlider.Value;
            }

            if (flatTerrainBlockButton.IsClicked)
            {
                DialogsManager.ShowDialog(null, new ListSelectionDialog("Select Block", new int[20]
                {
          8,
          2,
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
          0
                }, 72f, index =>
       {
           ContainerWidget containerWidget = (ContainerWidget)Widget.LoadWidget(null, ContentManager.Get<XElement>("Widgets/SelectBlockItem"), null);
           containerWidget.Children.Find<BlockIconWidget>("SelectBlockItem.Block").Contents = (int)index;
           containerWidget.Children.Find<LabelWidget>("SelectBlockItem.Text").Text = BlocksManager.Blocks[(int)index].GetDisplayName(null, Terrain.MakeBlockValue((int)index));
           return containerWidget;
       }, index => worldSettings.TerrainBlockIndex = (int)index));
            }

            if (flatTerrainMagmaOceanCheckbox.IsClicked)
            {
                worldSettings.TerrainOceanBlockIndex = worldSettings.TerrainOceanBlockIndex == 18 ? 92 : 18;
            }

            islandTerrainPanel.IsVisible = worldSettings.TerrainGenerationMode == TerrainGenerationMode.Island;
            flatTerrainPanel.IsVisible = worldSettings.TerrainGenerationMode == TerrainGenerationMode.FlatContinent;
            continentTerrainPanel.IsVisible = worldSettings.TerrainGenerationMode == TerrainGenerationMode.Continent;
            flatTerrainLevelSlider.Value = worldSettings.TerrainLevel;
            flatTerrainLevelSlider.Text = worldSettings.TerrainLevel.ToString();
            flatTerrainBlock.Contents = worldSettings.TerrainBlockIndex;
            flatTerrainMagmaOceanCheckbox.IsChecked = worldSettings.TerrainOceanBlockIndex == 92;
            seaLevelOffsetSlider.Value = worldSettings.SeaLevelOffset;
            seaLevelOffsetSlider.Text = WorldOptionsScreen.FormatOffset(worldSettings.SeaLevelOffset);
            temperatureOffsetSlider.Value = worldSettings.TemperatureOffset;
            temperatureOffsetSlider.Text = WorldOptionsScreen.FormatOffset(worldSettings.TemperatureOffset);
            humidityOffsetSlider.Value = worldSettings.HumidityOffset;
            humidityOffsetSlider.Text = WorldOptionsScreen.FormatOffset(worldSettings.HumidityOffset);
            biomeSizeSlider.Value = FindNearestIndex(biomeSizes, worldSettings.BiomeSize);
            biomeSizeSlider.Text = worldSettings.BiomeSize.ToString() + "x";
            islandSizeEW.Value = FindNearestIndex(islandSizes, worldSettings.IslandSize.X);
            islandSizeEW.Text = worldSettings.IslandSize.X.ToString();
            islandSizeNS.Value = FindNearestIndex(islandSizes, worldSettings.IslandSize.Y);
            islandSizeNS.Text = worldSettings.IslandSize.Y.ToString();
            supernaturalCreaturesButton.Text = worldSettings.AreSupernaturalCreaturesEnabled ? "Enabled" : "Disabled";
            environmentBehaviorButton.Text = worldSettings.EnvironmentBehaviorMode.ToString();
            timeOfDayButton.Text = worldSettings.TimeOfDayMode.ToString();
            weatherEffectsButton.Text = worldSettings.AreWeatherEffectsEnabled ? "Enabled" : "Disabled";
            adventureRespawnButton.Text = worldSettings.IsAdventureRespawnAllowed ? "Allowed" : "Not Allowed";
            adventureSurvivalMechanicsButton.Text = worldSettings.AreAdventureSurvivalMechanicsEnabled ? "Enabled" : "Disabled";
            terrainGenerationLabel.Text = worldSettings.TerrainGenerationMode.ToString();
        }

        private int FindNearestIndex(IList<float> list, float v)
        {
            int index1 = 0;
            for (int index2 = 0; index2 < list.Count; ++index2)
            {
                if (MathUtils.Abs(list[index2] - v) < (double)MathUtils.Abs(list[index1] - v))
                {
                    index1 = index2;
                }
            }
            return index1;
        }
    }
}
