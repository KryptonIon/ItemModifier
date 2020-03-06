using ItemModifier.UIKit;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ItemModifier.UI
{
    public class NewItemUIW : UIWindow
    {
        internal class ItemMatch : UIElement
        {
            public Item Item
            {
                get => ItemDisplay.DisplayItem;

                set
                {
                    ItemDisplay.DisplayItem = value;
                }
            }

            private UIItemDisplay ItemDisplay;

            public ItemMatch(int ItemID)
            {
                Width = new StyleDimension(20f);
                Height = new StyleDimension(20f);
                ItemDisplay = new UIItemDisplay(new Vector2(20f), ItemID)
                {
                    Center = true,
                    Parent = this
                };
                WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = Lang.GetItemName(Item.type).Value;
            }
        }

        internal UICheckbox UseModifiedProperties;
        internal UIText Label1; // Use Modified Properties
        internal UIText Label2; // No Matches
        internal UITextbox ItemNameTextbox;
        internal UIIntTextbox ItemIDTextbox;
        internal UIImageButton Generate;
        internal UIItemDisplay ItemDisplay;
        internal UIContainer Matches;
        internal ItemMatch[] ItemMatches = new ItemMatch[39];
        private int columnIndex;
        public int ColumnIndex
        {
            get => columnIndex;

            set
            {
                columnIndex = value < 0 ? 0 : value > maxColumnIndex ? maxColumnIndex : value;
                ClearMatches();
                int matchIDIndex = columnIndex * 3;
                for (int matchIndex = 0; matchIDIndex < matchIDs.Count && matchIndex < ItemMatches.Length; matchIndex++, matchIDIndex++)
                {
                    ItemMatches[matchIndex].Item.SetDefaults(matchIDs[matchIDIndex]);
                    ItemMatches[matchIndex].Visible = true;
                }
            }
        }
        private int maxColumnIndex;
        private List<int> matchIDs;

        public NewItemUIW() : base("New Item") => (InheritVisibility, Visible, Width, Height) = (false, false, new StyleDimension(425f), new StyleDimension(100f));

        public override void OnInitialize()
        {
            base.OnInitialize();
            ItemDisplay = new UIItemDisplay(new Vector2(20f)) { Center = true };
            ItemDisplay.Parent = this;

            ItemNameTextbox = new UITextbox()
            {
                Text = "Air",
                Box = false,
                Width = new StyleDimension(320f),
                Left = new StyleDimension(ItemDisplay.Width.Pixels + 4f),
                Parent = this
            };
            ItemNameTextbox.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Item Name";
            ItemNameTextbox.OnTextChangedByUser += (source, value) =>
            {
                if (string.IsNullOrEmpty(value))
                {
                    Matches.Visible = false;
                }
                else
                {
                    ClearMatches();
                    matchIDs = KRUtils.FindItemsByName(value);
                    if (matchIDs.Count == 0)
                    {
                        Label2.Visible = true;
                        matchIDs = null;
                    }
                    else
                    {
                        ColumnIndex = 0;
                        maxColumnIndex = matchIDs.Count / 3 - 13;
                        if (matchIDs.Count % 3 != 0) maxColumnIndex += 1;
                        if (maxColumnIndex < 0) maxColumnIndex = 0;
                        ClearMatches();
                        for (int matchIndex = 0; matchIndex < matchIDs.Count && matchIndex < 39; matchIndex++)
                        {
                            ItemMatches[matchIndex].Item.SetDefaults(matchIDs[matchIndex]);
                            ItemMatches[matchIndex].Visible = true;
                        }
                    }
                    Matches.Visible = true;
                }
            };

            ItemIDTextbox = new UIIntTextbox(0, ItemLoader.ItemCount - 1)
            {
                Sign = false,
                Box = false,
                LimitType = UITextbox.CharacterLimitType.DynamicLimit,
                Width = new StyleDimension(51f),
                Left = new StyleDimension(ItemNameTextbox.Left.Pixels + ItemNameTextbox.Width.Pixels + 4f),
                Parent = this
            };
            ItemIDTextbox.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Item ID";
            ItemIDTextbox.OnValueChanged += (source, value) =>
            {
                ItemNameTextbox.Text = value == 0 ? "Air" : Lang.GetItemName(value).Value;
                Matches.Visible = false;
                ItemDisplay.DisplayItem.SetDefaults(value);
            };

            Label1 = new UIText("Use Modified Properties:")
            {
                SkipDescenderCheck = true,
                Top = new StyleDimension(ItemNameTextbox.Height.Pixels),
                Parent = this
            };

            UseModifiedProperties = new UICheckbox
            {
                Top = new StyleDimension(Label1.Top.Pixels),
                Left = new StyleDimension(Label1.Width.Pixels + 4f),
                Parent = this
            };

            Matches = new UIContainer(KRUtils.UIBackgroundColor, new Vector2(ItemNameTextbox.Width.Pixels, InnerDimensions.Height - ItemNameTextbox.Height.Pixels - 8))
            {
                InheritVisibility = false,
                Visible = false,
                OverflowHidden = true,
                Left = new StyleDimension(ItemNameTextbox.Left.Pixels),
                Top = new StyleDimension(ItemNameTextbox.Height.Pixels),
                Parent = this
            };
            Matches.OnScrollWheel += (source, e) => ColumnIndex += e.ScrollWheelValue / -120;

            int column = 0;
            int row = 0;
            for (int matchIndex = 0; matchIndex < ItemMatches.Length; matchIndex++)
            {
                ItemMatches[matchIndex] = new ItemMatch(0)
                {
                    Top = new StyleDimension(row * 25),
                    Left = new StyleDimension(column * 25),
                    Parent = Matches
                };
                ItemMatches[matchIndex].OnLeftClick += MatchClick;
                row++;
                if (row > 2)
                {
                    row = 0;
                    column++;
                }
            }

            Label2 = new UIText("No matches found.", Color.Red)
            {
                InheritVisibility = false,
                Parent = Matches
            };

            Generate = new UIImageButton(ItemModifier.Textures.NewItem)
            {
                Width = new StyleDimension(22f),
                Height = new StyleDimension(22f),
                Left = new StyleDimension(ItemIDTextbox.Left.Pixels + ItemIDTextbox.Width.Pixels + 4f),
                Parent = this
            };
            Generate.WhileMouseHover += (source, e) => ItemModifier.Instance.Tooltip = "Generate Item";
            Generate.OnLeftClick += (source, e) =>
            {
                int itemIndex = Item.NewItem(Main.LocalPlayer.getRect(), ItemIDTextbox.Value, 1, true);
                if (UseModifiedProperties.Value) Main.item[itemIndex].CopyItemProperties(ItemModifier.Instance.MainUI.ModifyWindow.ModifiedItem);
            };
        }

        private void MatchClick(UIElement source, UIMouseEventArgs e)
        {
            Matches.Visible = false;
            ItemMatch match = source as ItemMatch;
            ItemIDTextbox.Value = match.Item.type;
            ItemDisplay.DisplayItem.SetDefaults(match.Item.type);
            ItemNameTextbox.Text = Lang.GetItemName(match.Item.type).Value;
            ColumnIndex = 0;
            maxColumnIndex = 0;
            matchIDs = null;
        }

        private void ClearMatches()
        {
            for (int i = 0; i < ItemMatches.Length; i++)
            {
                ItemMatches[i].Visible = false;
                ItemMatches[i].Item.SetDefaults(0);
            }
            Label2.Visible = false;
        }
    }
}
