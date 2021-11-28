using Game;
using GameEntitySystem;
using TemplatesDatabase;

namespace CreatorModAPI
{
    public class CreatorModAPIEditPaletteDialog : EditPaletteDialog
    {
        private readonly ButtonWidget okButton;

        public CreatorModAPIEditPaletteDialog(WorldPalette palette)
            : base(palette)
        {
            okButton = Children.Find<ButtonWidget>("EditPaletteDialog.OK");
        }

        public override void Update()
        {
            base.Update();
            if (okButton.IsClicked)
            {
                ((Subsystem)GameManager.Project.FindSubsystem<SubsystemPalette>()).Load((ValuesDictionary)null);
            }
        }
    }
}

/*using Game;

namespace CreatorModAPI
{
    public class CreatorModAPIEditPaletteDialog : EditPaletteDialog
    {
        private readonly ButtonWidget okButton;

        public CreatorModAPIEditPaletteDialog(WorldPalette palette)
          : base(palette)
          => okButton = Children.Find<ButtonWidget>("EditPaletteDialog.OK");

        public override void Update()
        {
            base.Update();
            if (!okButton.IsClicked)
            {
                return;
            }

            GameManager.Project.FindSubsystem<SubsystemPalette>().Load(null);
        }
    }
}
*/