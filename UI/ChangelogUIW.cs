using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace ItemModifier.UI
{
    public class ChangelogUIW : UIWindow
    {
        private string Website { get; set; }

        internal UIText ChangelogVersion;

        internal UIImageButton PreviousButton;

        internal UIImageButton NextButton;

        internal UIImageButton ChangelogWebsite;

        internal UIElement TextContainer;

        internal UIImage UpArrowScroll;

        internal UIImage DownArrowScroll;

        internal UIText[] ChangelogText = new UIText[16];

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
                ChangelogVersion.Left = new StyleDimension(-ChangelogVersion.Width.Pixels * 0.5f, 0.5f);
                Website = currentChangelog.Website;
                MaxLineIndex = currentChangelog.Raw.Count - ChangelogText.Length;
                LineIndex = 0;
            }
        }

        private int lineIndex;

        public int LineIndex
        {
            get
            {
                return lineIndex;
            }

            set
            {
                ItemModifier.Changelog currentChangelog = ItemModifier.Changelogs[changelogIndex];
                lineIndex = value < 0 ? 0 : value > MaxLineIndex ? MaxLineIndex : value;
                for (int i = 0; i < ChangelogText.Length; i++)
                {
                    ChangelogText[i].Text = "";
                }

                for (int i = 0; i < Math.Min(ChangelogText.Length, currentChangelog.Raw.Count); i++)
                {
                    ChangelogText[i].Text = currentChangelog.Raw[i + LineIndex];
                }

                UpArrowScroll.Visible = LineIndex > 0;
                DownArrowScroll.Visible = LineIndex < MaxLineIndex;
            }
        }

        private int maxLineIndex;

        public int MaxLineIndex
        {
            get
            {
                return maxLineIndex;
            }

            private set
            {
                maxLineIndex = value < 0 ? 0 : value;
            }
        }

        public ChangelogUIW() : base("Changelog")
        {
            InheritVisibility = false;
            Visible = false;
            Width = new StyleDimension(500f);
            Height = new StyleDimension(485f);
        }

        public override void OnInitialize()
        {
            base.OnInitialize();
            ChangelogVersion = new UIText("There's a problem")
            {
                SkipDescenderCheck = true
            };
            ChangelogVersion.Left = new StyleDimension(-ChangelogVersion.Width.Pixels * 0.5f, 0.5f);
            ChangelogVersion.Parent = this;

            PreviousButton = new UIImageButton(ItemModifier.Textures.LeftArrow, new Color(0, 100, 255));
            PreviousButton.OnLeftClick += (source, e) => ChangelogIndex -= 1;
            PreviousButton.OnRightClick += (source, e) => ChangelogIndex += 1;
            PreviousButton.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Previous Changelog";
            PreviousButton.Parent = this;

            ChangelogWebsite = new UIImageButton(ItemModifier.Textures.UpArrow, Color.Blue)
            {
                Left = new StyleDimension(PreviousButton.Left.Pixels + PreviousButton.Width.Pixels + 6f),
                Parent = this
            };
            ChangelogWebsite.OnLeftClick += (source, e) => Process.Start(Website);
            ChangelogWebsite.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Open On Wiki";

            NextButton = new UIImageButton(ItemModifier.Textures.RightArrow, new Color(255, 100, 0));
            NextButton.Left = new StyleDimension(Width.Pixels - NextButton.Width.Pixels);
            NextButton.Parent = this;
            NextButton.OnLeftClick += (source, e) => ChangelogIndex += 1;
            NextButton.OnRightClick += (source, e) => ChangelogIndex -= 1;
            NextButton.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Next Changelog";

            int uptimer = 0;
            int updelay = 20;
            UpArrowScroll = new UIImage(ItemModifier.Textures.UpArrowShort)
            {
                InheritVisibility = false,
                ColorTint = new Color(50, 50, 200),
                Top = new StyleDimension(ChangelogVersion.Height.Pixels)
            };
            UpArrowScroll.Left = new StyleDimension(-UpArrowScroll.Width.Pixels * 0.5f, 0.5f);
            UpArrowScroll.Parent = this;
            UpArrowScroll.OnLeftClick += (source, e) =>
            {
                updelay += 5;
                if (updelay > 20) updelay = 5;
            };
            UpArrowScroll.OnRightClick += (source, e) =>
            {
                updelay -= 5;
                if (updelay < 0) updelay = 20;
            };
            UpArrowScroll.WhileMouseHover += (source, e) =>
            {
                uptimer += 1;
                if (uptimer >= updelay)
                {
                    LineIndex -= 1;
                    uptimer = 0;
                }
            };

            TextContainer = new UIContainer(new Vector2(Width.Pixels, Height.Pixels - ChangelogVersion.Height.Pixels - UpArrowScroll.Height.Pixels))
            {
                Top = new StyleDimension(ChangelogVersion.Height.Pixels + UpArrowScroll.Height.Pixels),
                Parent = this
            };
            TextContainer.OnScrollWheel += (source, e) => LineIndex += e.ScrollWheelValue / -120;

            for (int i = 0; i < ChangelogText.Length; i++)
            {
                ChangelogText[i] = new UIText("")
                {
                    SkipDescenderCheck = true,
                    Top = new StyleDimension(28 * i),
                    Parent = TextContainer
                };
            }

            int downtimer = 0;
            int downdelay = 20;
            DownArrowScroll = new UIImage(ItemModifier.Textures.DownArrowShort)
            {
                InheritVisibility = false,
                ColorTint = new Color(50, 50, 200)
            };
            DownArrowScroll.Top = new StyleDimension(-DownArrowScroll.Height.Pixels, 1f);
            DownArrowScroll.Left = new StyleDimension(-DownArrowScroll.Width.Pixels * 0.5f, 0.5f);
            DownArrowScroll.Parent = this;
            DownArrowScroll.OnLeftClick += (source, e) =>
            {
                downdelay += 5;
                if (downdelay > 20) downdelay = 5;
            };
            DownArrowScroll.OnRightClick += (source, e) =>
            {
                downdelay -= 5;
                if (downdelay < 0) downdelay = 20;
            };
            DownArrowScroll.WhileMouseHover += (source, e) =>
            {
                downtimer += 1;
                if (downtimer >= downdelay)
                {
                    LineIndex += 1;
                    downtimer = 0;
                }
            };

            ChangelogIndex = 0;
        }
    }
}
