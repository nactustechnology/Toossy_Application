using System.Collections.Generic;
using ToossyApp.Renderers;
using Xamarin.Forms;
using System;
using ToossyApp.ViewModels;
using ToossyApp.Resources;

namespace ToossyApp.Views
{
    public class FilterPage : ContentPage
    {
        FilterViewModel _vm
        {
            get { return BindingContext as FilterViewModel; }
        }

        public FilterPage()
        {
            try
            {
                Title = AppResources.Filter_PageTitle;

                //Form fields
                //_____________________SET THEME DROPDOWN LIST__________________________________
                //******************************************************************************
                var themeHeader = new TextCell { Text = AppResources.Filter_themeHeader };
                themeHeader.SetBinding(TextCell.DetailProperty, "DetailTheme", BindingMode.TwoWay);

                themeHeader.TextColor = Color.Firebrick;

                SelectMultipleBasePage themePicker = new SelectMultipleBasePage() { Title = AppResources.Filter_themePicker };

                themePicker.Appearing += (sender, e) => {
                    if (themePicker.WrappedItems.Count == 0)
                    {
                        themePicker.initialization(_vm.ListeTheme);
                        themePicker.SetSelection(_vm.FiltreTheme);
                    }
                };

                themeHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(themePicker, true);
                };

                themePicker.Disappearing += (sender, e) => {
                    var updateResult = updateMultiSelectPicker(sender, e);
                    _vm.FiltreTheme = updateResult[0];
                    _vm.DetailTheme = String.Join(", ", updateResult[1]);
                };


                //_____________________SET LANGUE DROPDOWN LIST__________________________________
                //******************************************************************************

                var langueHeader = new TextCell { Text = AppResources.Filter_langueHeader };
                langueHeader.SetBinding(TextCell.DetailProperty, "DetailLangue", BindingMode.TwoWay);

                langueHeader.TextColor = Color.Firebrick;

                SelectMultipleBasePage languePicker = new SelectMultipleBasePage() { Title = AppResources.Filter_languePicker };

                languePicker.Appearing += (sender, e) => {
                    if (languePicker.WrappedItems.Count == 0)
                    {
                        languePicker.initialization(_vm.ListeLangue);
                        languePicker.SetSelection(_vm.FiltreLangue);
                    }
                };

                langueHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(languePicker, true);
                };

                languePicker.Disappearing += (sender, e) => {
                    var updateResult = updateMultiSelectPicker(sender, e);
                    _vm.FiltreLangue = updateResult[0];
                    _vm.DetailLangue = String.Join(", ", updateResult[1]);
                };

                //_____________________SET BALLADE CIRCUIT PARCOURS DROPDOWN LIST__________________________________
                //*************************************************************************************************

                var typeParcoursHeader = new TextCell { Text = AppResources.Filter_typeParcoursHeader };
                typeParcoursHeader.SetBinding(TextCell.DetailProperty, "DetailTypeParcours", BindingMode.TwoWay);

                typeParcoursHeader.TextColor = Color.Firebrick;

                SelectMultipleBasePage typeParcoursPicker = new SelectMultipleBasePage() { Title = AppResources.Filter_typeParcoursPicker };

                typeParcoursPicker.Appearing += (sender, e) => {
                    if (typeParcoursPicker.WrappedItems.Count == 0)
                    {
                        typeParcoursPicker.initialization(_vm.ListeTypeParcours);
                        typeParcoursPicker.SetSelection(_vm.FiltreTypeParcours);
                    }
                };

                typeParcoursHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(typeParcoursPicker, true);
                };

                typeParcoursPicker.Disappearing += (sender, e) => {
                    var updateResult = updateMultiSelectPicker(sender, e);
                    _vm.FiltreTypeParcours = updateResult[0];
                    _vm.DetailTypeParcours = String.Join(", ", updateResult[1]);
                };

                //_____________________SET DISTANCE DROPDOWN LIST_______________________________
                //******************************************************************************

                var distanceHeader = new TextCell { Text = AppResources.Filter_distanceHeader };
                distanceHeader.SetBinding(TextCell.DetailProperty, "DetailDistance", BindingMode.TwoWay);

                distanceHeader.TextColor = Color.Firebrick;

                SelectSimpleBasePage distancePicker = new SelectSimpleBasePage() { Title = AppResources.Filter_distancePicker };

                distancePicker.Appearing += (sender, e) => {
                    if (distancePicker.WrappedItems.Count == 0)
                    {
                        distancePicker.initialization(_vm.ListeDistance);
                        distancePicker.SetSelection(_vm.FiltreDistance[0]);
                    }
                };

                distanceHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(distancePicker, true);
                };

                distancePicker.Disappearing += (sender, e) => {
                    var updateResult = updateSimpleSelectPicker(sender, e);
                    _vm.FiltreDistance[0] = updateResult[0];
                    _vm.DetailDistance = updateResult[1];
                };

                //_____________________SET DUREE DROPDOWN LIST__________________________________
                //******************************************************************************

                var dureeHeader = new TextCell { Text = AppResources.Filter_dureeHeader };
                dureeHeader.SetBinding(TextCell.DetailProperty, "DetailDuree", BindingMode.TwoWay);

                dureeHeader.TextColor = Color.Firebrick;

                SelectSimpleBasePage dureePicker = new SelectSimpleBasePage() { Title = AppResources.Filter_dureePicker };

                dureePicker.Appearing += (sender, e) => {
                    if (dureePicker.WrappedItems.Count == 0)
                    {
                        dureePicker.initialization(_vm.ListeDuree);
                        dureePicker.SetSelection(_vm.FiltreDuree[0]);
                    }
                };

                dureeHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(dureePicker, true);
                };

                dureePicker.Disappearing += (sender, e) => {
                    var updateResult = updateSimpleSelectPicker(sender, e);
                    _vm.FiltreDuree[0] = updateResult[0];
                    _vm.DetailDuree = updateResult[1];
                };

                //_____________________SET NOTE DROPDOWN LIST___________________________________
                //******************************************************************************
                var noteHeader = new TextCell { Text = AppResources.Filtre_noteHeader };
                noteHeader.SetBinding(TextCell.DetailProperty, "DetailNote", BindingMode.TwoWay);

                noteHeader.TextColor = Color.Firebrick;

                SelectSimpleBasePage notePicker = new SelectSimpleBasePage() { Title = AppResources.Filtre_notePicker };

                notePicker.Appearing += (sender, e) => {
                    if (notePicker.WrappedItems.Count == 0)
                    {
                        notePicker.initialization(_vm.ListeNote);
                        notePicker.SetSelection(_vm.FiltreNote[0]);
                    }
                };

                noteHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(notePicker, true);
                };

                notePicker.Disappearing += (sender, e) => {
                    var updateResult = updateSimpleSelectPicker(sender, e);
                    _vm.FiltreNote[0] = updateResult[0];
                    _vm.DetailNote = updateResult[1];
                };

                //_____________________SET GRATUIT TARIF DROPDOWN LIST__________________________
                //******************************************************************************
                var gratuitTarifHeader = new TextCell { Text = AppResources.Filtre_gratuitTarifHeader };
                gratuitTarifHeader.SetBinding(TextCell.DetailProperty, "DetailGratuitTarif", BindingMode.TwoWay);

                gratuitTarifHeader.TextColor = Color.Firebrick;

                SelectSimpleBasePage gratuitTarifPicker = new SelectSimpleBasePage() { Title = AppResources.Filtre_gratuitTarifPicker };

                gratuitTarifPicker.Appearing += (sender, e) => {
                    if (gratuitTarifPicker.WrappedItems.Count == 0)
                    {
                        gratuitTarifPicker.initialization(_vm.ListeGratuitTarif);
                        gratuitTarifPicker.SetSelection(_vm.FiltreGratuitTarif[0]);
                    }
                };

                gratuitTarifHeader.Tapped += async (sender, e) => {
                    await Navigation.PushAsync(gratuitTarifPicker, true);
                };

                gratuitTarifPicker.Disappearing += (sender, e) => {
                    var updateResult = updateSimpleSelectPicker(sender, e);
                    _vm.FiltreGratuitTarif[0] = updateResult[0];
                    _vm.DetailGratuitTarif = updateResult[1];
                };

                //_____________________Set TableView____________________________________________
                //******************************************************************************
                var filterForm = new TableView
                {
                    Intent = TableIntent.Settings,
                    Root = new TableRoot {
                        new TableSection{
                            themeHeader,
                            typeParcoursHeader,
                            distanceHeader,
                            dureeHeader,
                            noteHeader,
                            //gratuitTarifHeader,
                            langueHeader
                        }
                    }
                };

                Content = filterForm;
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _vm.saveFilter();
        }

        protected string[][] updateMultiSelectPicker(object sender, EventArgs e)
        {
            SelectMultipleBasePage picker = (SelectMultipleBasePage)sender;

            if (picker.GetSelection().Count == 0)
            {
                picker.SelectAll();
            }

            List<string> FiltreList = new List<string>();
            List<string> DetailList = new List<string>();

            var answers = picker.GetSelection();
            foreach (var a in answers)
            {
                FiltreList.Add(a.Clef);
                DetailList.Add(a.Name);
            }

            string[] updatedFiltre = FiltreList.ToArray();
            string[] updatedDetail = DetailList.ToArray();

            string[][] updateResult = { updatedFiltre, updatedDetail };

            return updateResult;
        }

        protected string[] updateSimpleSelectPicker(object sender, EventArgs e)
        {
            SelectSimpleBasePage picker = (SelectSimpleBasePage)sender;

            List<string> FiltreList = new List<string>();
            List<string> DetailList = new List<string>();

            var answers = picker.GetSelection();
            foreach (var a in answers)
            {
                FiltreList.Add(a.Clef);
                DetailList.Add(a.Name);
            }

            string updatedFiltre = FiltreList[0];
            string updatedDetail = DetailList[0];

            string[] updateResult = { updatedFiltre, updatedDetail };

            return updateResult;
        }
    }
}