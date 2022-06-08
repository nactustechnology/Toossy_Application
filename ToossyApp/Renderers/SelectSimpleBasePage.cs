using System;

using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using ToossyApp.Controls;

namespace ToossyApp.Renderers
{
    class SelectSimpleBasePage : ContentPage
    {
        public class WrappedSelection : INotifyPropertyChanged
        {
            public CheckItem Item { get; set; }
            bool isSelected = false;
            public bool IsSelected
            {
                get
                {
                    return isSelected;
                }
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                        //PropertyChanged (this, new PropertyChangedEventArgs (nameof (IsSelected))); // C# 6
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged = delegate { };
        }

        public class WrappedItemSelectionTemplate : ViewCell
        {
            public WrappedItemSelectionTemplate() : base()
            {
                Label name = new Label();

                name.SetBinding(Label.TextProperty, new Binding("Item.Name"));

                Switch mainSwitch = new Switch();

                mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));

                mainSwitch.IsEnabled = false;
                mainSwitch.IsVisible = false;

                mainSwitch.Toggled += (sender, e) => {
                    mainSwitch.IsEnabled = !mainSwitch.IsEnabled;
                    mainSwitch.IsVisible = !mainSwitch.IsVisible;
                };

                RelativeLayout layout = new RelativeLayout();

                layout.Children.Add(name,
                    Constraint.Constant(5),
                    Constraint.Constant(5),
                    Constraint.RelativeToParent(p => p.Width - 60),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );

                layout.Children.Add(mainSwitch,
                    Constraint.RelativeToParent(p => p.Width - 55),
                    Constraint.Constant(5),
                    Constraint.Constant(50),
                    Constraint.RelativeToParent(p => p.Height - 10)
                );

                View = layout;
            }
        }

        public List<WrappedSelection> WrappedItems = new List<WrappedSelection>();

        public SelectSimpleBasePage()
        {
        }

        public void initialization(List<CheckItem> items)
        {
            WrappedItems = items.Select(item => new WrappedSelection() { Item = item, IsSelected = false }).ToList();

            ListView mainList = new ListView()
            {
                ItemsSource = WrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            /*mainList.ItemTapped += (sender, e) => {
                if (e.Item == null)
                    return;
                var o = (WrappedSelection)e.Item;
                SelectNone();
                o.IsSelected = true;
                ((ListView)sender).SelectedItem = null; //de-select
            };*/

            mainList.ItemSelected += (sender, e) => {
                if (e.SelectedItem == null)
                    return;
                var o = (WrappedSelection)e.SelectedItem;
                SelectNone();
                o.IsSelected = true;
                ((ListView)sender).SelectedItem = null; //de-select
            };

            Content = mainList;
        }

        void SelectNone()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = false;
            }
        }

        public List<CheckItem> GetSelection()
        {
            return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }

        public void SetSelection(string selection)
        {
            foreach (var wi in WrappedItems)
            {
                CheckItem selectedItem = wi.Item;
                string selectedItemClef = selectedItem.Clef;

                if (selectedItemClef != null)
                {
                    if (selection == selectedItemClef)
                    {
                        wi.IsSelected = true;
                    }
                }

            }
        }

    }
}
