using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using ToossyApp.Controls;
using ToossyApp.Resources;

namespace ToossyApp.Renderers
{
    public class SelectMultipleBasePage : ContentPage
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

        public SelectMultipleBasePage()
        {
            ToolbarItems.Add(new ToolbarItem(AppResources.Filter_All, null, SelectAll, ToolbarItemOrder.Primary));
            ToolbarItems.Add(new ToolbarItem(AppResources.Filter_None, null, SelectNone, ToolbarItemOrder.Primary));
        }

        public void initialization(List<CheckItem> items)
        {
            WrappedItems = items.Select(item => new WrappedSelection() { Item = item, IsSelected = false }).ToList();

            ListView mainList = new ListView()
            {
                ItemsSource = WrappedItems,
                ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
            };

            mainList.ItemSelected += (sender, e) => {
                if (e.SelectedItem == null) return;
                var o = (WrappedSelection)e.SelectedItem;
                o.IsSelected = !o.IsSelected;
                ((ListView)sender).SelectedItem = null; //de-select
            };

            Content = mainList;
        }

        public void SelectAll()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = true;
            }
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

        public void SetSelection(string[] selection)
        {
            foreach (var wi in WrappedItems)
            {
                CheckItem selectedItem = wi.Item as CheckItem;
                string selectedItemClef = selectedItem.Clef;

                if(selectedItemClef != null)
                {
                    if (Array.Exists(selection, itemClef => itemClef == selectedItemClef))
                    {
                        wi.IsSelected = true;
                    }
                }
                    
            }
        }
    }
}
