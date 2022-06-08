using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;

using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class RatingPage : ContentPage
    {
        RatingViewModel _vm
        {
            get { return BindingContext as RatingViewModel; }
        }

        Label ratingLabel;

        public RatingPage()
        {
            Title = AppResources.RatingPage_PageTitle;

            /*var commentLabel = new WebView();

            var textWebView = "Vous venez de terminer le parcours. Laissez un commentaire pour dire ce qui vous a plu ou suggérer des améliorations à l'auteur(e), merci.";
            commentLabel.Source = "<html>" +
                                    "<body  style=\"text-align: justify; font-size: 14; \">" +
                                    String.Format("<p>{0}</p>", textWebView) +
                                    "</body>" +
                                "</html>";*/


            var commentLabel = new Label();

            commentLabel.Text = AppResources.RatingPage_mainLabelText;
            commentLabel.HorizontalTextAlignment = TextAlignment.Center;

            var comment = new Editor
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = 5
            };
            comment.SetBinding(Editor.TextProperty, "Comment", BindingMode.OneWayToSource);

            ratingLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            var rating = new Stepper
            {
                Value = 2.5,
                Minimum = 0,
                Maximum = 5,
                Increment = 0.5,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            rating.SetBinding(Stepper.ValueProperty, "Rating", BindingMode.OneWayToSource);
            rating.ValueChanged += (sender, e) => {
                OnStepperValueChanged(e.NewValue);
            };

            OnStepperValueChanged(rating.Value);

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
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(commentLabel, 0, 0);
            grid.Children.Add(comment, 0, 1);
            grid.Children.Add(ratingLabel, 0, 2);
            grid.Children.Add(rating, 0, 3);
            grid.Children.Add(submitButton, 0, 4);

            this.Content = grid;

            /*this.Content = new StackLayout
            {
                Children =
                {
                    commentLabel,
                    comment,
                    ratingLabel,
                    rating,
                    submitButton
                }
            };*/
        }

        void OnStepperValueChanged(double value)
        {
            ratingLabel.Text = String.Format(AppResources.RatingPage_noteLabelText + " : {0:F1} / 5", value);
        }

    }
}
