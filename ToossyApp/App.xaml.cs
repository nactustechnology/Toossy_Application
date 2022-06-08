using Akavache;
using Ninject;
using Ninject.Modules;
using System;
using ToossyApp.Modules;
using ToossyApp.ViewModels;
using ToossyApp.Views;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace ToossyApp
{
	public partial class App : Application
	{
        public IKernel Kernel { get; set; }

        public App(params INinjectModule[] platformModules)
        {
            var mainPage = new NavigationPage(new MasterDetailPage() { Master= new MenuPage() { Title = "test" }, Detail= new ItineraryListPage() });

            // Register core services
            Kernel = new StandardKernel(new ItineraryCoreModule(), new ItineraryNavModule(mainPage.Navigation));

            // Register platform specific services
            Kernel.Load(platformModules);

            // Get the ItineraryListViewModel from the IoC
            mainPage.BindingContext = Kernel.Get<MainViewModel>();

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            BlobCache.ApplicationName = "Itinerary";
            BlobCache.EnsureInitialized();
        }

        protected async override void OnSleep()
        {
            try
            {
                await BlobCache.LocalMachine.Flush();
                await BlobCache.LocalMachine.InsertObject("ItineraryFilter", Current.Properties["ItineraryFilter"]);
                await BlobCache.LocalMachine.InsertObject("SelectedItineraries", Current.Properties["SelectedItineraries"]);
                await BlobCache.LocalMachine.InsertObject("ViewedMessages", Current.Properties["ViewedMessages"]);
                await BlobCache.LocalMachine.InsertObject("RatedItineraries", Current.Properties["RatedItineraries"]);
                await BlobCache.LocalMachine.InsertObject("VisitesPayees", Current.Properties["VisitesPayees"]);
                BlobCache.Shutdown().Wait();
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
