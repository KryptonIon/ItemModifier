using System.Collections.Generic;

namespace ItemModifier.UIKit.Inputs
{
    public class UIRadioButtonContainer : UIElement
    {
        public event UIEventHandler<EventArgs<UIRadioButton>> OnSelected;

        public event UIEventHandler<EventArgs<UIRadioButton>> OnDeselected;

        public bool AlwaysSelected { get; set; } = true;

        public bool Multiselect { get; set; }

        public List<UIRadioButton> Selected { get; } = new List<UIRadioButton>();

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
                Selected.Add(radio);
                OnSelected?.Invoke(this, new EventArgs<UIRadioButton>(radio));
                if (!Multiselect)
                {
                    for (int i = 0; i < Selected.Count; i++)
                    {
                        if (Selected[i] != radio)
                        {
                            Selected[i].Selected = false;
                        }
                    }
                }
            }
            else
            {
                UIRadioButton radio = (UIRadioButton)source;
                Selected.Remove(radio);
                OnDeselected?.Invoke(this, new EventArgs<UIRadioButton>(radio));
                if (AlwaysSelected && Selected.Count < 1)
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
