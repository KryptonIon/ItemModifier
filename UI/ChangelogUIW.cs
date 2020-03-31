using ItemModifier.UIKit;
using ItemModifier.UIKit.Inputs;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class ChangelogUIW : UIWindow
    {
        private string Website { get; set; }

        internal UIText ChangelogVersion;

        internal UIImageButton PreviousButton;

        internal UIImageButton NextButton;

        internal UIImageButton ChangelogWebsite;

        internal UIContainer TextContainer;

        internal UIText ChangelogText;

        private int changelogIndex;

        public int ChangelogIndex
        {
            get
            {
                return changelogIndex;
            }

            set
            {
                changelogIndex = value < 0 ? ItemModifier.Changelogs.Count - 1 : value >= ItemModifier.Changelogs.Count ? 0 : value;
                ItemModifier.Changelog currentChangelog = ItemModifier.Changelogs[changelogIndex];
                ChangelogVersion.Text = $"{currentChangelog.Version} {currentChangelog.Title}";
                ChangelogText.Text = currentChangelog.Raw;
                TextContainer.ScrollValue = 0;
                TextContainer.Recalculate();
                Website = currentChangelog.Website;
            }
        }

        public ChangelogUIW() : base("Changelog")
        {
            Visible = false;
            Width = new SizeDimension(500f);
            Height = new SizeDimension(485f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            ItemModifier instance = ModContent.GetInstance<ItemModifier>();
            ChangelogVersion = new UIText("There's a problem")
            {
                SkipDescenderCheck = true,
                HorizontalAlign = 0.5f
            };
            ChangelogVersion.Parent = this;

            PreviousButton = new UIImageButton(ItemModifier.Textures.LeftArrow, colorTint: new Color(0, 100, 255))
            {
                Parent = this
            };
            PreviousButton.OnLeftClick += (source, e) => ChangelogIndex -= 1;
            PreviousButton.OnRightClick += (source, e) => ChangelogIndex += 1;
            PreviousButton.WhileMouseHover += (source, e) => instance.Tooltip = "Previous Changelog";

            ChangelogWebsite = new UIImageButton(ItemModifier.Textures.UpArrow, colorTint: Color.Blue)
            {
                XOffset = new SizeDimension(PreviousButton.CalculatedXOffset + PreviousButton.InnerWidth + 6f),
                Parent = this
            };
            ChangelogWebsite.OnLeftClick += (source, e) => Process.Start(Website);
            ChangelogWebsite.WhileMouseHover += (source, e) => instance.Tooltip = "Open On Wiki";

            NextButton = new UIImageButton(ItemModifier.Textures.RightArrow, colorTint: new Color(255, 100, 0));
            NextButton.XOffset = new SizeDimension(InnerWidth - NextButton.OuterWidth);
            NextButton.Parent = this;
            NextButton.OnLeftClick += (source, e) => ChangelogIndex += 1;
            NextButton.OnRightClick += (source, e) => ChangelogIndex -= 1;
            NextButton.WhileMouseHover += (source, e) => instance.Tooltip = "Next Changelog";

            TextContainer = new UIContainer()
            {
                Width = new SizeDimension(InnerWidth),
                Height = new SizeDimension(InnerHeight - ChangelogVersion.InnerHeight),
                YOffset = new SizeDimension(ChangelogVersion.OuterHeight),
                OverflowHidden = true,
                Parent = this
            };

            ChangelogText = new UIText(string.Empty)
            {
                SkipDescenderCheck = true,
                Parent = TextContainer
            };

            ChangelogIndex = 0;
        }
    }
}
