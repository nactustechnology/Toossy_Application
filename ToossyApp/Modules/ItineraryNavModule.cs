using ToossyApp.Services;
using ToossyApp.ViewModels;
using ToossyApp.Views;
using Ninject.Modules;
using System;
using Xamarin.Forms;

namespace ToossyApp.Modules
{
    public class ItineraryNavModule : NinjectModule
    {
        readonly INavigation _xfNav;

        public ItineraryNavModule(INavigation xamarinFormsNavigation)
        {
            _xfNav = xamarinFormsNavigation;
        }

        public override void Load()
        {
            try
            {
                var navService = new XamarinFormsNavService();
                navService.XamarinFormsNav = _xfNav;

                //Register view mappings
                navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
                navService.RegisterViewMapping(typeof(ItineraryDetailViewModel), typeof(ItineraryDetailPage));
                navService.RegisterViewMapping(typeof(ItineraryPreviewViewModel), typeof(ItineraryPreviewPage));
                navService.RegisterViewMapping(typeof(MessageMapViewModel), typeof(MessageMapPage));
                navService.RegisterViewMapping(typeof(MessageDetailViewModel), typeof(MessageDetailPage));
                navService.RegisterViewMapping(typeof(FilterViewModel), typeof(FilterPage));
                navService.RegisterViewMapping(typeof(RatingViewModel), typeof(RatingPage));
                navService.RegisterViewMapping(typeof(ReportViewModel), typeof(ReportPage));
                navService.RegisterViewMapping(typeof(TutorialViewModel), typeof(TutorialPage));
                navService.RegisterViewMapping(typeof(MentionsLegalesViewModel), typeof(MentionsLegalesPage));
                navService.RegisterViewMapping(typeof(FavoriteListViewModel),typeof(FavoriteListPage));
                navService.RegisterViewMapping(typeof(FavoriteDetailViewModel), typeof(FavoriteDetailPage));
                navService.RegisterViewMapping(typeof(ItineraryPayViewModel), typeof(ItineraryPayPage));

                Bind<INavService>().ToMethod(x => navService).InSingletonScope();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
