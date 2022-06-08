using ToossyApp.Converters;
using ToossyApp.Resources;
using ToossyApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ToossyApp.Views
{
    public class TutorialPage : ContentPage
    {
        TutorialViewModel _vm
        {
            get { return BindingContext as TutorialViewModel; }
        }

        public TutorialPage()
        {
            try
            { 
                Title = AppResources.TutorialPage_PageTitle;

                var tutorial_text_1_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_2_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_3_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_4_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_5_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_6_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_7_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_8_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_9_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_10_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_11_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_12_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_13_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };
                var tutorial_text_14_Row = new RowDefinition { Height = new GridLength(10, GridUnitType.Absolute) };

                var tutorial_text_1_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_2_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_3_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_4_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_5_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_6_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_7_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_8_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_9_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_10_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_11_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_12_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_13_RowHeight = new RowDefinition { Height = GridLength.Auto };
                var tutorial_text_14_RowHeight = new RowDefinition { Height = GridLength.Auto };

                var grid = new Grid()
                {
                    RowDefinitions =
                        {
                            tutorial_text_1_Row,
                            tutorial_text_1_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_2_Row,
                            tutorial_text_2_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_3_Row,
                            tutorial_text_3_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_4_Row,
                            tutorial_text_4_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_5_Row,
                            tutorial_text_5_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_6_Row,
                            tutorial_text_6_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_7_Row,
                            tutorial_text_7_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_8_Row,
                            tutorial_text_8_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_9_Row,
                            tutorial_text_9_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_10_Row,
                            tutorial_text_10_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_11_Row,
                            tutorial_text_11_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_12_Row,
                            tutorial_text_12_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_13_Row,
                            tutorial_text_13_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                            tutorial_text_14_Row,
                            tutorial_text_14_RowHeight,
                            new RowDefinition { Height = GridLength.Auto },
                        },
                    ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                        }
                };


                //______________________Partie 1______________________________________________________
                //************************************************************************************


                string imageFolder = "";

                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        imageFolder = "";
                        break;
                    case Device.Android:
                        imageFolder = "Resources/drawable/";
                        break;
                }

                var text_1 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_1) +
                                "</body>" +
                                "</html>"
                    }
                };

                var image_1 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_1.png")
                };
                

                var textHeight_1 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_1
                };

                
                grid.Children.Add(text_1, 0, 0);
                grid.Children.Add(textHeight_1, 0, 1);
                grid.Children.Add(image_1, 0, 2);

                
                //______________________Partie 2______________________________________________________
                //************************************************************************************

                var text_2 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_2) +
                                "</body>" +
                                "</html>"
                    }
                };




                var image_2 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_2.png"),
                };

                var textHeight_2 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_2
                };

                grid.Children.Add(text_2, 0, 3);
                grid.Children.Add(textHeight_2, 0, 4);
                grid.Children.Add(image_2, 0, 5);

                //______________________Partie 3______________________________________________________
                //************************************************************************************

                var text_3 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource() {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_3) +
                                "</body>" +
                                "</html>" }
                };


                var image_3 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_3.png"),
                };

                var textHeight_3 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_3
                };

                grid.Children.Add(text_3, 0, 6);
                grid.Children.Add(textHeight_3, 0, 7);
                grid.Children.Add(image_3, 0, 8);

                //______________________Partie 4______________________________________________________
                //************************************************************************************

                var text_4 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_4) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_4 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_4.png"),
                };

                var textHeight_4 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_4
                };

                grid.Children.Add(text_4, 0, 9);
                grid.Children.Add(textHeight_4, 0, 10);
                grid.Children.Add(image_4, 0, 11);

                //______________________Partie 5______________________________________________________
                //************************************************************************************

                var text_5 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_5) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_5 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_5.png"),
                };

                var textHeight_5 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_5
                };

                grid.Children.Add(text_5, 0, 12);
                grid.Children.Add(textHeight_5, 0, 13);
                grid.Children.Add(image_5, 0, 14);

                //______________________Partie 6______________________________________________________
                //************************************************************************************

                var text_6 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_6) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_6 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_6.png"),
                };

                var textHeight_6 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_6
                };

                grid.Children.Add(text_6, 0, 15);
                grid.Children.Add(textHeight_6, 0, 16);
                grid.Children.Add(image_6, 0, 17);

                //______________________Partie 7______________________________________________________
                //************************************************************************************

                var text_7 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_7) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_7 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_7.png"),
                };

                var image_7_bis = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_7_bis.png"),
                };

                var textHeight_7 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_7
                };

                grid.Children.Add(text_7, 0, 18);
                grid.Children.Add(textHeight_7, 0, 19);
                grid.Children.Add(image_7, 0, 20);
                grid.Children.Add(image_7_bis, 0, 21);
                
                //______________________Partie 8______________________________________________________
                //************************************************************************************

                var text_8 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html =  "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_8) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_8 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_8.png"),
                };

                var textHeight_8 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_8
                };

                grid.Children.Add(text_8, 0, 22);
                grid.Children.Add(textHeight_8, 0, 23);
                grid.Children.Add(image_8, 0, 24);

                //______________________Partie 9______________________________________________________
                //************************************************************************************

                var text_9 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_9) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_9 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_9.png"),
                };

                var textHeight_9 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_9
                };

                grid.Children.Add(text_9, 0, 25);
                grid.Children.Add(textHeight_9, 0, 26);
                grid.Children.Add(image_9, 0, 27);

                //______________________Partie 10______________________________________________________
                //************************************************************************************

                var text_10 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_10) +
                                "</body>" +
                                "</html>"
                    }
                };


                var image_10 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_10.png"),
                };

                var textHeight_10 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_10
                };

                grid.Children.Add(text_10, 0, 28);
                grid.Children.Add(textHeight_10, 0, 29);
                grid.Children.Add(image_10, 0, 30);

                //______________________Partie 11______________________________________________________
                //************************************************************************************
                
                var text_11 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_11) +
                                "</body>" +
                                "</html>"
                    }
                };
                

                var image_11 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_11.png"),
                };

                var textHeight_11 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_11
                };
                
                grid.Children.Add(text_11, 0, 31);
                grid.Children.Add(textHeight_11, 0, 32);
                grid.Children.Add(image_11, 0, 33);
                
                //______________________Partie 12______________________________________________________
                //************************************************************************************
                
                var text_12 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_12) +
                                "</body>" +
                                "</html>"
                    }
                };

                var image_12 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_12.png"),
                };
                
                var textHeight_12 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_12
                };

                grid.Children.Add(text_12, 0, 34);
                grid.Children.Add(textHeight_12, 0, 35);
                grid.Children.Add(image_12, 0, 36);
                
                //______________________Partie 13______________________________________________________
                //************************************************************************************
                
                var text_13 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_13) +
                                "</body>" +
                                "</html>"
                    }
                };
                

                var image_13 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_13.png"),
                };

                
                var textHeight_13 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_13
                };

                grid.Children.Add(text_13, 0, 37);
                grid.Children.Add(textHeight_13, 0, 38);
                grid.Children.Add(image_13, 0, 39);
                
                //______________________Partie 14______________________________________________________
                //************************************************************************************
                
                var text_14 = new WebView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Source = new HtmlWebViewSource()
                    {
                        Html = "<html>" +
                                "<body  style=\"text-align: justify; font-size: 14; \">" +
                                String.Format("<p>{0}</p>", AppResources.Tutorial_text_14) +
                                "</body>" +
                                "</html>"
                    }
                };
                

                var image_14 = new Image
                {
                    //Aspect = Aspect.AspectFit,
                    Margin = 10,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = ImageSource.FromFile(imageFolder + "tutorial_image_14.png"),
                };
                
                var textHeight_14 = new Label
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Style = Device.Styles.BodyStyle,
                    Text = AppResources.Tutorial_text_14
                };

                grid.Children.Add(text_14, 0, 40);
                grid.Children.Add(textHeight_14, 0, 41);
                grid.Children.Add(image_14, 0, 42);
                

                grid.SetBinding(Grid.IsVisibleProperty, "IsLoading", converter: new ReverseBooleanConverter());

                //************************************************************************************
                //************************************************************************************
                //************************************************************************************


                var loading = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new ActivityIndicator { IsRunning = true },
                        new Label { Text = AppResources.MessageDetail_LoadingMsg, HorizontalTextAlignment = TextAlignment.Center }
                    }
                };

                loading.SetBinding(StackLayout.IsVisibleProperty, "IsLoading");

                double heightMax = 0;

                loading.LayoutChanged += (sender, e) =>
                {
                    if (grid.Children.Contains(textHeight_1) && textHeight_1.Height > heightMax)
                    {
                        var RowHeight_1 = textHeight_1.Height * 1.1;
                        var RowHeight_2 = textHeight_2.Height * 1.1;
                        var RowHeight_3 = textHeight_3.Height * 1.1;
                        var RowHeight_4 = textHeight_4.Height * 1.1;
                        var RowHeight_5 = textHeight_5.Height * 1.1;
                        var RowHeight_6 = textHeight_6.Height * 1.1;
                        var RowHeight_7 = textHeight_7.Height * 1.1;
                        var RowHeight_8 = textHeight_8.Height * 1.1;
                        var RowHeight_9 = textHeight_9.Height * 1.1;
                        var RowHeight_10 = textHeight_10.Height * 1.1;
                        var RowHeight_11 = textHeight_11.Height * 1.1;
                        var RowHeight_12 = textHeight_12.Height * 1.1;
                        var RowHeight_13 = textHeight_13.Height * 1.1;
                        var RowHeight_14 = textHeight_14.Height * 1.1;

                        tutorial_text_1_Row.Height = new GridLength(RowHeight_1, GridUnitType.Absolute);
                        tutorial_text_1_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);
                        
                        tutorial_text_2_Row.Height = new GridLength(RowHeight_2, GridUnitType.Absolute);
                        tutorial_text_2_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);
                        
                        tutorial_text_3_Row.Height = new GridLength(RowHeight_3, GridUnitType.Absolute);
                        tutorial_text_3_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_4_Row.Height = new GridLength(RowHeight_4, GridUnitType.Absolute);
                        tutorial_text_4_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_5_Row.Height = new GridLength(RowHeight_5, GridUnitType.Absolute);
                        tutorial_text_5_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_6_Row.Height = new GridLength(RowHeight_6, GridUnitType.Absolute);
                        tutorial_text_6_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_7_Row.Height = new GridLength(RowHeight_7, GridUnitType.Absolute);
                        tutorial_text_7_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_8_Row.Height = new GridLength(RowHeight_8, GridUnitType.Absolute);
                        tutorial_text_8_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_9_Row.Height = new GridLength(RowHeight_9, GridUnitType.Absolute);
                        tutorial_text_9_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_10_Row.Height = new GridLength(RowHeight_10, GridUnitType.Absolute);
                        tutorial_text_10_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_11_Row.Height = new GridLength(RowHeight_11, GridUnitType.Absolute);
                        tutorial_text_11_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_12_Row.Height = new GridLength(RowHeight_12, GridUnitType.Absolute);
                        tutorial_text_12_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_13_Row.Height = new GridLength(RowHeight_13, GridUnitType.Absolute);
                        tutorial_text_13_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        tutorial_text_14_Row.Height = new GridLength(RowHeight_14, GridUnitType.Absolute);
                        tutorial_text_14_RowHeight.Height = new GridLength(0, GridUnitType.Absolute);

                        _vm.IsLoading = false;
                    }
                };


                var mainLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = { loading, grid }
                };


                Content = new ScrollView()
                {
                    Content = mainLayout,
                    Orientation = ScrollOrientation.Vertical
                };
            }
            catch(Exception ex)
            {

            }
        }
    }
}
