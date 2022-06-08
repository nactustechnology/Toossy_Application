using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;

using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class ReportPage : ContentPage
    {
        RatingViewModel _vm
        {
            get { return BindingContext as RatingViewModel; }
        }

        Label ratingLabel;

        public ReportPage()
        {
            Title = AppResources.ReportPage_PageTitle;

            var commentLabel = new Label();

            commentLabel.Text = AppResources.ReportPage_mainLabelText;
            commentLabel.HorizontalTextAlignment = TextAlignment.Center;

            var comment = new Editor
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 5
            };
            comment.SetBinding(Editor.TextProperty, "Comment", BindingMode.OneWayToSource);

            var submitButton = new Button
            {
                Text = AppResources.RatingPage_submitButton,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsEnabled = false
            };
            submitButton.SetBinding(Button.CommandProperty, "SaveCommand");


           var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(commentLabel, 0, 0);
            grid.Children.Add(comment, 0, 1);
            grid.Children.Add(submitButton, 0, 2);

            this.Content = grid;

        }
    }
}
