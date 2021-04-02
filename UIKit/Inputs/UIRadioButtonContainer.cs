using System.Collections.Generic;

namespace ItemModifier.UIKit.Inputs
{
    public delegate void UIRadioButtonEventHandler(UIRadioButtonContainer sender, UIRadioButtonEventArgs e);

    public class UIRadioButtonContainer : UIElement
    {
        public event UIRadioButtonEventHandler OnSelected;

        public event UIRadioButtonEventHandler OnDeselected;

        public bool AlwaysSelected { get; set; }

        public bool Multiselect { get; set; }

        private List<UIRadioButton> selected = new List<UIRadioButton>();

        public IReadOnlyCollection<UIRadioButton> Selected
        {
            get
            {
                return selected.AsReadOnly();
            }
        }

        protected List<UIRadioButton> Choices { get; } = new List<UIRadioButton>();

        public int ChoicesCount
        {
            get
            {
                return Choices.Count;
            }
        }

        public UIRadioButtonContainer()
        {
            OnChildAdded += (source, e) =>
            {
                if (e.Target is UIRadioButton radio)
                {
                    Choices.Add(radio);
                    radio.OnValueChanged += SelectChange;
                }
            };
            OnChildRemoved += (source, e) =>
            {
                if (e.Target is UIRadioButton radio)
                {
                    Choices.Remove(radio);
                }
            };
        }

        private void SelectChange(UIElement source, EventArgs<bool> e)
        {
            if (e.Value)
            {
                UIRadioButton radio = (UIRadioButton)source;
                selected.Add(radio);
                OnSelected?.Invoke(this, new UIRadioButtonEventArgs(radio));
                if (!Multiselect)
                {
                    for (int i = 0; i < selected.Count; i++)
                    {
                        if (selected[i] != radio)
                        {
                            selected[i].Selected = false;
                        }
                    }
                }
            }
            else
            {
                UIRadioButton radio = (UIRadioButton)source;
                selected.Remove(radio);
                OnDeselected?.Invoke(this, new UIRadioButtonEventArgs(radio));
                if (AlwaysSelected && selected.Count < 1)
                {
                    radio.Selected = true;
                    e.Value = true;
                }
            }
        }

        public virtual void DeselectAllRadio()
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                Choices[i].Selected = false;
            }
        }

        public UIRadioButton GetChoice(int index)
        {
            return Choices[index];
        }
    }
}
