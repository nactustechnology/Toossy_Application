using System;
using Ninject.Modules;
using ToossyApp.ViewModels;
using ToossyApp.Services;
using ToossyApp.Resources;

namespace ToossyApp.Modules
{
    public class ItineraryCoreModule : NinjectModule
    {
        public override void Load()
        {
            try
            {
                // ViewModels
                Bind<MainViewModel>().ToSelf();
                Bind<ItineraryDetailViewModel>().ToSelf();
                Bind<ItineraryPreviewViewModel>().ToSelf();
                Bind<MessageMapViewModel>().ToSelf();
                Bind<MessageDetailViewModel>().ToSelf();
                Bind<FilterViewModel>().ToSelf();
                Bind<RatingViewModel>().ToSelf();
                Bind<ReportViewModel>().ToSelf();
                Bind<TutorialViewModel>().ToSelf();
                Bind<MentionsLegalesViewModel>().ToSelf();
                Bind<FavoriteListViewModel>().ToSelf();
                Bind<FavoriteDetailViewModel>().ToSelf();
                Bind<ItineraryPayViewModel>().ToSelf();

                

                string ApiEndPoint = AppResources.ApiEndPoint_RELEASE;

                #if DEBUG
                    ApiEndPoint = AppResources.ApiEndPoint_DEBUG;
                #endif

                var itineraryDataService = new ItineraryApiDataService(new Uri(ApiEndPoint));


                Bind<IItineraryDataService>().ToMethod(x => itineraryDataService).InSingletonScope();

                Bind<Akavache.IBlobCache>().ToConstant(Akavache.BlobCache.LocalMachine);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
